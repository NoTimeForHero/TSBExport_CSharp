﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using TSBExport_CSharp.Grid;
using TSBExport_CSharp.Other;
using TSBExport_CSharp.Properties;
using Action = System.Action;
using Application = Microsoft.Office.Interop.Excel.Application;
using XlBorderWeight = Microsoft.Office.Interop.Excel.XlBorderWeight;
using XlHAlign = Microsoft.Office.Interop.Excel.XlHAlign;

namespace TSBExport_CSharp.Logic.Export
{
    class ToExcel
    {
        private Workbook book;
        private Application app;
        private Worksheet worksheet;

        private dynamic Cells => worksheet.Cells;
        private dynamic Columns => worksheet.Columns;
        private dynamic Rows => worksheet.Rows;

        private readonly GridDataTable data;
        private int headerLen;

        public CancellationToken cancellationToken;
        public event Action<string, int, int> OnStateChanged;

        public bool Visible
        {
            get => app.Visible;
            set => app.Visible = value;
        }

        public ToExcel(GridDataTable data)
        {
            this.data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public async Task AsyncCreate(int updateInterval=20)
        {
            await Task.Run(() =>
            {
                app = new Application();
                app.Visible = false;
                book = app.Workbooks.Add();
                worksheet = app.Worksheets.Add();

                int columns = data.Columns.Count;
                int rows = data.Rows.Count;

                OnStateChanged?.Invoke("Making headers", -1, -1);
                headerLen = makeHeader(1, columns);
                makeValues(headerLen + 1, rows, columns, updateInterval);
                makeFooter(headerLen + rows + 1, columns);

                InsertPicture(1, columns + 1, 2.5, 2.5);
                OnStateChanged?.Invoke("Worksheet finished!", 0, 0);
            }, cancellationToken);
        }

        public async Task AsyncColorize(GridCellsAppearance appearance, int updateInterval=10)
        {
            if (app == null) throw new NullReferenceException("Trying to colorize on NULL Excel.Application!");
            if (book == null) throw new NullReferenceException("Trying to colorize on NULL Excel.WorkBook!");
            if (worksheet == null) throw new NullReferenceException("Trying to colorize on NULL Excel.WorkSheet!");

            await Task.Run(() => _colorizeValues(appearance, 1, updateInterval), cancellationToken);
        }

        private void _colorizeValues(GridCellsAppearance appearance, int startY, int updateInterval)
        {
            if (appearance.colorize == null) return;
            if (data.Columns.Count < 1) return;
            int rows = data.Rows.Count;
            int columns = data.Columns.Count;
            Range range;

            // HEADER
            range = worksheet.Range[Cells[startY, 1], Cells[startY + headerLen, columns]];
            range.Borders.Color = appearance.gridColor;
            range.Font.Color = appearance.styleHeader.ForeColor;
            range.Interior.Color = appearance.styleHeader.BackColor;

            // FOOTER
            int footerY = startY + headerLen + rows;
            range = worksheet.Range[Cells[footerY, 1], Cells[footerY, columns]];
            range.Borders.Color = appearance.gridColor;
            range.Font.Color = appearance.styleFooter.ForeColor;
            range.Interior.Color = appearance.styleFooter.BackColor;

            // VALUES
            int valueY = startY + headerLen;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 1; x <= columns; x++)
                {
                    var style = appearance.colorize(x, y);
                    range = worksheet.Range[Cells[valueY + y, x], Cells[valueY + y, x]];
                    range.Interior.Color = style.BackColor;
                    range.Font.Color = style.ForeColor;
                }

                if (OnStateChanged != null && updateInterval > 0 && y % updateInterval == 0)
                    OnStateChanged.Invoke($"Current colorized: {y}/{rows}", y, rows);
                cancellationToken.ThrowIfCancellationRequested();
            }

            // GRID COLOR FOR VALUES
            range = worksheet.Range[Cells[valueY, 1], Cells[valueY + rows, columns]];
            range.Borders.Color = appearance.gridColor;

            OnStateChanged?.Invoke("Colorized finished!", 0, 0);
        }


        private void InsertPicture(int row, int col, double offsetY = 0, double offsetX = 0)
        {
            Bitmap image = Resources.ImgSystem;
            String fullPath = Decorator.GetTempFileNameWithExt("png");

            double X = col < 2 ? 0 : Enumerable.Range(1, col-1).Sum(x => (double)Columns[x].Width);
            double Y = row < 2 ? 0 : Enumerable.Range(1, row-1).Sum(y => (double)Rows[y].Height);

            image.Save(fullPath, ImageFormat.Png);
            worksheet.Shapes.AddPicture(fullPath, MsoTriState.msoFalse, MsoTriState.msoCTrue,
                (int)Math.Ceiling(X+offsetX), (int)Math.Ceiling(Y+offsetY), (int)(image.Width * 0.6), (int)(image.Height * 0.6));

            File.Delete(fullPath);
        }

        private void makeValues(int beginY, int rows, int columns, int updateInterval)
        {
            if (columns < 1) return;
            // VALUES
            Range range = worksheet.Range[Cells[beginY, 1], Cells[beginY + rows - 1, columns]];
            range.Borders.LineStyle = XlLineStyle.xlContinuous;
            range.Borders.Weight = XlBorderWeight.xlThin;

            if (OnStateChanged == null || updateInterval < 1)
            {
                range.Value = data.ToExcelArray();
                return;
            }

            Action<int, int> actionGUI = (current, max) => OnStateChanged.Invoke($"Filling Rows: {current}/{max}", current, max);
            Action<int, int> actionThrow = (a, b) => cancellationToken.ThrowIfCancellationRequested();

            range.Value = data.ToExcelArray(updateInterval, actionGUI + actionThrow);
        }

        private void makeFooter(int startY, int columns)
        {
            if (columns < 1) return;

            // COLUMNS FOOTERS
            Range range = worksheet.Range[Cells[startY, 1], Cells[startY, columns]];
            range.Borders.LineStyle = XlLineStyle.xlContinuous;
            range.Borders.Weight = XlBorderWeight.xlThin;
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.Value = data.Footers.ToExcelArray();

            // AUTOFIT COLUMNS
            for (int i = 1; i <= columns; i++)
            {
                Columns[i].AutoFit();
            }
        }

        private int makeHeader(int startY, int columns)
        {
            int y = startY;
            int width = columns > 0 ? columns : 1;

            // HEADER TITLE
            Range range = worksheet.Range[Cells[y, 1], Cells[y, width]];
            range.Merge();
            range.Value2 = "TSBExport_CSharp";
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.BorderAround();
            var font = range.Font;
            font.Bold = true;
            font.Size = 22;
            y++;

            // DATETIME SUBHEADER TITLE
            range = worksheet.Range[Cells[y, 1], Cells[y, width]];
            range.Merge();
            range.Value2 = DateTime.Now.ToString("dd.MM.yyyy - HH:mm:ss");
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.BorderAround();
            y++;

            // COLUMNS HEADERS
            range = worksheet.Range[Cells[y, 1], Cells[y, width]];
            range.Borders.LineStyle = XlLineStyle.xlContinuous;
            range.Borders.Weight = XlBorderWeight.xlThin;
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.Value = data.Headers.ToExcelArray();
            y++;

            Columns[1].AutoFit();
            return y - startY;
        }

        public void ForceClose()
        {
            // IF EXCEL ALREADY CLOSED BY USER
            try
            {
                book.Close(false);
            }
            catch (InvalidCastException ex)
            {
                if (ex.HResult == -2147467262) return;
                throw;
            }

            app.Quit();
        }
    }
}
