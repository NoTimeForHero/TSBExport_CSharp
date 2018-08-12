using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TSBExport_CSharp
{
    public static class DefaultData
    {

        public static void InitTableSettings(ConfigSettings settings)
        {
            GridSettings ts = new GridSettings
            {
                rndSeed = 322,
                records = 500
            };

            ts.columns.AddRange(new[] {
                new GridColumn("{row}."),
                new GridColumn("Example Text\nWith multiline support\nIn DataGridView Cell"),
                new GridColumn(50, 8000),
                new GridColumn(new DateTime(2010, 1, 1), new DateTime(2020, 12, 30)),
                new GridColumn(new DateTime(2010, 1, 1, 0, 0, 0), new DateTime(2010, 1, 1, 23, 59, 59)),
                new GridColumn(10000d, 99999d)
            });

            settings.GridSettings = ts;
            settings.Save();
        }

        public static void AddTableControls(Form parent, ToolStripMenuItem menu, DataGridView dataGridView, BindingSource bindingSource)
        {

            menu.DropDownItems.Add(Decorator.CreateCheckboxToolStrip(true, "Header", isVisible =>
            {
                bindingSource.SuspendBinding();
                dataGridView.Rows[0].Visible = isVisible;
                if (dataGridView.FirstDisplayedScrollingRowIndex < 2)
                    dataGridView.FirstDisplayedScrollingRowIndex = isVisible ? 0 : 1; // Must be visible to scroll or exception throwed
                bindingSource.ResumeBinding();
            }));

            menu.DropDownItems.Add(Decorator.CreateCheckboxToolStrip(true, "Footer", isVisible =>
            {
                int lastRow = dataGridView.RowCount - 1; // Latest row index

                bindingSource.SuspendBinding();
                dataGridView.Rows[lastRow].Visible = isVisible;
                if (dataGridView.FirstDisplayedScrollingRowIndex > lastRow - 2)
                    dataGridView.FirstDisplayedScrollingRowIndex = isVisible ? lastRow : lastRow - 1; // Must be visible to scroll or exception throwed
                bindingSource.ResumeBinding();
            }));

            menu.DropDownItems.Add(Decorator.CreateCheckboxToolStrip(true, "Multiline", on =>
            {
                MessageBox.Show("Not Implemented Yet!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }));

            menu.DropDownItems.Add(Decorator.CreateCheckboxToolStrip(false, "Selector", on => dataGridView.RowHeadersVisible = on));

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

                grid.Rows.Add("1", "String");
                grid.Rows.Add("2", "String");
                grid.Rows.Add("3", "Date", "dd.MM.yyyy");
                grid.Rows.Add("4", "Integer", "c");

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
            DataGridViewCellStyle white = new DataGridViewCellStyle();

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
                styleFooter = headersAndfooters
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