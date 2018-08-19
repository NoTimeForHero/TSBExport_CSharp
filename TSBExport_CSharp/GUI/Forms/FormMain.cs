using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TSBExport_CSharp.Grid;
using TSBExport_CSharp.GUI.Controls;
using TSBExport_CSharp.Other;

namespace TSBExport_CSharp
{
    public partial class FormMain : Form
    {
        public IList<GridColumn> Columns => gridSettings.columns.AsReadOnly();

        private readonly List<GridCellsAppearance> viewSettings = new List<GridCellsAppearance>();
        private GridCellsAppearance currentGridCellsAppearance
        {
            get => viewSettings.FirstOrDefault(x => x.name == settings.CurrentApperance);
            set => settings.CurrentApperance = value.name;
        }

        private readonly BindingSource dataGridBindingSource = new BindingSource();
        private readonly ConfigSettings settings;
        private GridDataTable dataTable;
        private GridSettings gridSettings;

        public delegate void OnAddStyles(List<GridCellsAppearance> viewSettings);
        public delegate void OnAddTableControls(FormMain parent, ToolStripMenuItem menu, ExtendedDataGridView dataGridView, BindingSource bindingSource);
        public delegate void OnExportExcel(Form host, GridDataTable data, GridCellsAppearance style, bool throwAnyway);

        public OnAddTableControls AddTableControls;
        public OnAddStyles AddStyles;
        public OnExportExcel ExportExcel;

        public FormMain(ConfigSettings settings)
        {
            this.settings = settings;
            this.gridSettings = settings.GridSettings;
            InitializeComponent();
        }

        private void UpdateDataGridFormat()
        {
            for (int i = 0; i < dataGridView1.ColumnCount && i < gridSettings.columns.Count; i++)
            {
                dataGridView1.Columns[i].DefaultCellStyle.Format = gridSettings.columns[i].Format;
            }
        }

        private void BindFormatChangeEvent()
        {
            foreach (var column in gridSettings.columns)
            {
                column.formatChanged += (o, ev) => UpdateDataGridFormat();
            }
        }

        private void UpdateDataGridColor(GridCellsAppearance settings)
        {
            if (settings == null) return;
            currentGridCellsAppearance = settings;
            if (dataGridView1.Rows.Count < 1) return;

            // Setting style for header and footer
            dataGridView1.HeaderStyle = settings.styleHeader;
            dataGridView1.FooterStyle = settings.styleFooter;
            dataGridView1.GridColor = settings.gridColor;

            dataGridView1.DefaultCellStyle.SelectionBackColor = settings.selection.BackColor;
            dataGridView1.DefaultCellStyle.SelectionForeColor = settings.selection.ForeColor;

            if (settings.colorize != null)
            {
                dataGridView1.Colorize(settings.colorize);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (settings.WindowLocation.HasValue) Location = settings.WindowLocation.Value;
            if (settings.WindowSize.HasValue) Size = settings.WindowSize.Value;
            Text = Text + " (Version " + Assembly.GetExecutingAssembly().GetName().Version + ")";

            dataGridView1.ColumnHeadersVisible = false; // Hide real headers
            dataGridView1.RowHeadersVisible = false; // Hide left system column before first data
            dataGridView1.ReadOnly = false; // Not allowed to edit values
            dataGridView1.AllowUserToAddRows = false; // Not allowed to add new rows by user
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically; // No way to edit row
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Select full row instead one cell
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True; // Allow multilining

            AddTableControls?.Invoke(this, toolStrip_Table, dataGridView1, dataGridBindingSource);
            AddStyles?.Invoke(viewSettings);

            RebuildDataGrid();

            if (settings.ColumnsWidth != null)
            {
                for (int i = 0; i < dataGridView1.ColumnCount && i < settings.ColumnsWidth.Count; i++)
                    dataGridView1.Columns[i].Width = settings.ColumnsWidth[i];
            }

            FormMain_ResizeBegin(null, null);
            Form1_ResizeEnd(null, null);
        }

        private void RebuildDataGrid()
        {
            SuspendLayout();
            dataTable = gridSettings.getActualData();
            dataGridBindingSource.DataSource = dataTable;
            dataGridView1.UpdateData(dataGridBindingSource);
            dataGridView1.Refresh();
            dataGridView1.HeaderValues.AddRange(dataTable.Headers);
            dataGridView1.FooterValues.AddRange(dataTable.Footers);

            // AUTO FIT NEW DATA
            dataGridView1.ColumnsAutoFit();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            UpdateDataGridColor(currentGridCellsAppearance);
            UpdateDataGridFormat();
            BindFormatChangeEvent();
            dataGridView1.ClearSelection();
            ResumeLayout(true);
        }

        private int oldWidth;

        private void FormMain_ResizeBegin(object sender, EventArgs e)
        {
            oldWidth = Width;
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Width = (int)(column.Width * ((double)Width / oldWidth));
            }
        }

        // Code for dynamic width calculating
        private void toolStripMenu_Colors_DropDownOpening(object sender, EventArgs e)
        {
            if (viewSettings.Count < 1) return;

            // Removes all old styles
            toolStrip_Colors.DropDownItems.Clear();

            int maxWidth;

            // Finding largest width in styles
            using (Graphics g = this.CreateGraphics())
            {
                var largestSizeF = viewSettings.Select(x => g.MeasureString(x.name, toolStrip_Colors.Font)).OrderByDescending(y => y.Width).First();
                maxWidth = (int)Math.Ceiling(largestSizeF.Width);
            }

            // Iterating array of styles
            foreach (var view in viewSettings)
            {
                ToolStripItem item = new ToolStripButton(view.name);
                item.Click += (obj, ev) => UpdateDataGridColor(view);
                item.Width = maxWidth;
                toolStrip_Colors.DropDownItems.Add(item);
            }
        }

        private void toolStrip_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStrip_Generate_Click(object sender, EventArgs e)
        {
            FormGenerate generate = new FormGenerate(gridSettings);
            generate.ShowDialog(this);

            if (generate.ResultSettings != null)
            {
                gridSettings = generate.ResultSettings;
                RebuildDataGrid();
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            settings.ColumnsWidth = dataGridView1.Columns.OfType<DataGridViewColumn>().Select(x => x.Width).ToList();
            settings.WindowLocation = Location;
            settings.WindowSize = Size;
            settings.Save();

            if (settings.GridSettings.Equals(gridSettings)) return;
            var result = MessageBox.Show("Do you want save changes?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            switch (result)
            {
                case DialogResult.Yes:
                    settings.GridSettings = gridSettings;
                    settings.Save();
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
            }
        }

        private void toolStrip_excel_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripMenuItem toolStrip)) return;

            GridCellsAppearance style = null;
            if (toolStrip == toolStrip_excel) style = currentGridCellsAppearance;

            ExportExcel(this, dataTable, style, settings.ForceThrowExcelExceptions);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            string msg = "Example created by NoTimeForHero\nSource: https://github.com/NoTimeForHero/TSBExport_CSharp\n\nIcon by Aha-Soft\nLicense: Creative Commons (Attribution 3.0 Unported)\nSource: https://bit.ly/2PjyVXs";
            MessageBox.Show(this,msg, "Credits", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }


}
