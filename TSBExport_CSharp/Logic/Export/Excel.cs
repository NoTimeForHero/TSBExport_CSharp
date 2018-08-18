using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using TSBExport_CSharp.Grid;
using TSBExport_CSharp.Other;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace TSBExport_CSharp.Logic.Export
{
    class ToExcel : IDisposable
    {
        private readonly Workbook book;
        private readonly Application app;
        private readonly Worksheet worksheet;

        private dynamic Cells => worksheet.Cells;
        private dynamic Columns => worksheet.Columns;

        private readonly GridDataTable data;
        private int headerLen;

        public bool Visible
        {
            get => app.Visible;
            set => app.Visible = value;
        }

        public ToExcel(GridDataTable data)
        {
            this.data = data ?? throw new ArgumentNullException(nameof(data));

            app = new Application();
            app.Visible = false;
            book = app.Workbooks.Add();
            worksheet = app.Worksheets.Add();
        }

        public async Task AsyncFill()
        {
            await Task.Run(() =>
            {
                int columns = data.Columns.Count;
                int rows = data.Rows.Count;

                headerLen = makeHeader(1, columns);
                makeValues(headerLen + 1, rows, columns);
                makeFooter(headerLen + rows + 1, columns);
                Thread.Sleep(1000);
            });
        }

        public async Task AsyncColorize(GridCellsAppearance appearance)
        {
            await Task.Run(() => _colorizeValues(appearance, 1));
        }

        private void _colorizeValues(GridCellsAppearance appearance, int startY)
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
            }
        }

        private void makeValues(int beginY, int rows, int columns)
        {
            if (columns < 1) return;
            // VALUES
            Range range = worksheet.Range[Cells[beginY, 1], Cells[beginY + rows - 1, columns]];
            range.Borders.LineStyle = XlLineStyle.xlContinuous;
            range.Borders.Weight = XlBorderWeight.xlThin;
            range.Value = data.ToExcelArray();
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
            for (int i = 1; i < columns; i++)
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
            range.BorderAround2();
            var font = range.Font;
            font.Bold = true;
            font.Size = 22;
            y++;

            // DATETIME SUBHEADER TITLE
            range = worksheet.Range[Cells[y, 1], Cells[y, width]];
            range.Merge();
            range.Value2 = DateTime.Now.ToString("dd.MM.yyyy - HH:mm:ss");
            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            range.BorderAround2();
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


        ~ToExcel()
        {
            Dispose();
        }

        protected bool disposed;
        public void Dispose()
        {
            if (disposed) return;

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
            disposed = true;
        }
    }
}
