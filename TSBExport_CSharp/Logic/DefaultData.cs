using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSBExport_CSharp.Grid;
using TSBExport_CSharp.GUI.Controls;
using TSBExport_CSharp.GUI.Forms;
using TSBExport_CSharp.Logic.Export;
using TSBExport_CSharp.Other;

namespace TSBExport_CSharp
{
    public static class DefaultData
    {
        public static async void ExportExcel(Form sender, GridDataTable data, GridCellsAppearance style)
        {
            ToExcel excel = new ToExcel(data);
            try
            {
                CancellationTokenSource token = new CancellationTokenSource();
                DialogExport dialog = new DialogExport();
                dialog.FormClosing += (s, ev) => token.Cancel();
                dialog.ButtonCancelClick += () =>
                {
                    token.Cancel();
                    dialog.Close();
                };
                dialog.Show(sender);

                excel.cancellationToken = token.Token;
                excel.OnStateChanged += dialog.setStatus;

                await excel.AsyncCreate();

                if (style != null)
                    await excel.AsyncColorize(style,1);

                dialog.Close();
                excel.Visible = true;
            }
            catch (Exception ex)
            {
                excel.ForceClose();

                bool shouldThrow = !(ex is OperationCanceledException);
                if (ex is COMException exCom)
                {
                    MessageBox.Show(sender, "Excel Error Occured: " + exCom.Message, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    shouldThrow = false;
                }

                if (shouldThrow) throw;
            }
        }

        public static void InitTableSettings(ConfigSettings settings)
        {
            GridSettings ts = new GridSettings
            {
                rndSeed = 851649,
                records = 100
            };

            ts.columns.AddRange(new[] {
                new GridColumn("{row}."),
                new GridColumn(10000d, 99999d, "N3"),
                new GridColumn(10000, 99999),
                new GridColumn(new DateTime(2010, 1, 1), new DateTime(2020, 12, 30), "dd MMMM yyyy, dddd"),
                new GridColumn(new DateTime(2010, 1, 1, 0, 0, 0), new DateTime(2010, 1, 1, 23, 59, 59), "HH:mm:ss")
            });

            settings.GridSettings = ts;
            settings.CurrentApperance = "Standart";
            settings.Save();
        }

        public static void AddTableControls(FormMain parent, ToolStripMenuItem menu, ExtendedDataGridView dataGridView, BindingSource bindingSource)
        {

            menu.DropDownItems.Add(Decorator.CreateCheckboxToolStrip(true, "Header", isVisible => dataGridView.HeaderVisible = isVisible));
            menu.DropDownItems.Add(Decorator.CreateCheckboxToolStrip(true, "Footer", isVisible => dataGridView.FooterVisible = isVisible));
            menu.DropDownItems.Add(Decorator.CreateCheckboxToolStrip(false, "Selector", isVisible => dataGridView.RowHeadersVisible = isVisible));

            menu.DropDownItems.Add(Decorator.CreateButtonToolStrip("Data Format", (sender, args) =>
            {
                DataGridView grid = new DataGridView();
                grid.Dock = DockStyle.Fill;
                grid.AllowUserToAddRows = false;
                grid.RowHeadersVisible = false;
                grid.RowTemplate.DefaultCellStyle.Padding = new Padding(3, 5, 3, 5);
                grid.ColumnHeadersHeight += 10;
                grid.RowTemplate.Height += 10;

                grid.Columns.Add(new DataGridViewColumn { Name = "№", CellTemplate = new DataGridViewTextBoxCell(), Width = 80, ReadOnly = true });
                grid.Columns.Add(new DataGridViewColumn { Name = "Type", CellTemplate = new DataGridViewTextBoxCell(), Width = 150, ReadOnly = true });
                grid.Columns.Add(new DataGridViewColumn
                {
                    Name = "Format (Editable)",
                    CellTemplate = new DataGridViewTextBoxCell(),
                    Width = 300,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        NullValue = "[NULL]"
                    }
                });

                int index = 0;
                foreach (var column in parent.Columns)
                {
                    grid.Rows.Add(++index, column.type.Name, column.Format);
                }

                grid.CellValueChanged += (o, ev) => parent.Columns[ev.RowIndex].Format = grid.Rows[ev.RowIndex].Cells[ev.ColumnIndex].Value.ToString();

                Form dialog = new Form();
                dialog.Text = "Data rows format";
                dialog.Size = new Size(400, 200);
                dialog.MinimizeBox = false;
                dialog.MaximizeBox = false;
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.AutoScaleMode = AutoScaleMode.Font;
                dialog.Font = parent.Font;

                dialog.Controls.Add(grid);
                dialog.Show(parent);
            }));

        }

        public static void AddStyles(List<GridCellsAppearance> viewSettings)
        {
            DataGridViewCellStyle headersAndfooters = Decorator.MakeStyle(Color.White, Color.FromArgb(blue: 64, red: 64, green: 64));
            DataGridViewCellStyle gray = Decorator.MakeStyle(Color.Black, Color.LightGray);
            DataGridViewCellStyle white = Decorator.MakeStyle(Color.Black, Color.White);

            viewSettings.Add(new GridCellsAppearance
            {
                name = "Standart",
                colorize = delegate { return white; },
                styleHeader = headersAndfooters,
                styleFooter = headersAndfooters
            });

            viewSettings.Add(new GridCellsAppearance
            {
                name = "Zebra",
                colorize = delegate (int x, int y)
                {
                    if (y % 2 == 0) return gray;
                    return white;
                },
                styleHeader = headersAndfooters,
                styleFooter = headersAndfooters
            });

            viewSettings.Add(new GridCellsAppearance
            {
                name = "Chess",
                colorize = delegate (int x, int y)
                {
                    if (y % 2 == x % 2) return gray;
                    return white;
                },
                styleHeader = headersAndfooters,
                styleFooter = headersAndfooters
            });


            // Color mode
            List<DataGridViewCellStyle> colors = new List<DataGridViewCellStyle>
            {
                Decorator.MakeStyle(Color.Black, Color.CornflowerBlue),
                Decorator.MakeStyle(Color.Black, Color.Cyan),
                Decorator.MakeStyle(Color.Black, Color.Chartreuse),
                Decorator.MakeStyle(Color.Black, Color.Orange),
                Decorator.MakeStyle(Color.Black, Color.HotPink),
            };
            int rowsPerColor = 4;

            viewSettings.Add(new GridCellsAppearance
            {
                name = "RAINBOW",
                colorize = (x, y) => colors[y / rowsPerColor % colors.Count],
                styleHeader = headersAndfooters,
                styleFooter = headersAndfooters,
                gridColor = Color.Azure
            });


            DataGridViewCellStyle anotherFont = Decorator.MakeStyle(Color.DarkRed, Color.White, new Font("Comic Sans MS", 12f, FontStyle.Bold));
            DataGridViewCellStyle anotherFont2 = Decorator.MakeStyle(Color.DarkMagenta, Color.White, new Font("Arial Black", 9f, FontStyle.Bold));
            viewSettings.Add(new GridCellsAppearance
            {
                name = "Random Fonts",
                colorize = delegate
                {
                    if (Decorator.Chance(10)) return anotherFont; // 10% chance
                    if (Decorator.Chance(10)) return anotherFont2; // 10% chance
                    return white;
                },
                styleHeader = headersAndfooters,
                styleFooter = headersAndfooters
            });

        }
    }
}