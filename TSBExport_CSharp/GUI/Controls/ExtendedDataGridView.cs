using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSBExport_CSharp.Grid;

namespace TSBExport_CSharp.GUI.Controls
{

    public class ExtendedDataGridView : DataGridView
    {
        private BindingSource bindingSrc;
        private DataTable dataTable;

        public ExtendedDataGridView()
        {
            VirtualMode = true;
            CellValueNeeded += Event_CellValueNeeded;
        }


        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            HitTestInfo hti = HitTest(e.X, e.Y);
            base.OnMouseMove(e);

            if (hti.Type == DataGridViewHitTestType.Cell && InFooterOrHeader(hti.RowIndex))
            {
                if (e.Button != MouseButtons.Left)
                {
                    Cursor = Cursors.Arrow;
                }
            }
        }

        public void UpdateData(BindingSource bindingSrc)
        {
            this.bindingSrc = bindingSrc;
            VirtualMode = false;
            dataTable = (DataTable)bindingSrc.DataSource;
            if (dataTable == null) throw new ArgumentException("BindingSource DataSource must be DataTable!");
            RowCount = bindingSrc.Count + HeaderHeight + FooterHeight;
            ColumnCount = bindingSrc.GetItemProperties(null).Count;
            Console.WriteLine("CURRENT COLUMNS: " + ColumnCount);
            VirtualMode = true;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool HeaderVisible
        {
            get => RowCount > HeaderHeight && Rows[HeaderIndex].Visible;
            set
            {
                if (RowCount <= HeaderHeight) return;
                Rows[HeaderIndex].Visible = value;
                if (FirstDisplayedScrollingRowIndex < 2) FirstDisplayedScrollingRowIndex = value ? HeaderIndex : HeaderIndex+1;
            }
        }

        private bool _multiline = true;

        public bool Multiline
        {
            get => _multiline;
            set
            {
                bool old_value = _multiline;
                _multiline = value;
                if (old_value != value) Refresh();
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool FooterVisible
        {
            get => RowCount > FooterHeight && Rows[FooterIndex].Visible;
            set
            {
                if (RowCount <= HeaderHeight) return;
                Rows[FooterIndex].Visible = value;
            }
        }

        private DataGridViewCellStyle _headerStyle;
        private DataGridViewCellStyle _footerStyle;

        public DataGridViewCellStyle HeaderStyle
        {
            get => _headerStyle;
            set
            {
                _headerStyle = value;
                UpdateHeader();
            }
        }

        public DataGridViewCellStyle FooterStyle
        {
            get => _footerStyle;
            set
            {
                _footerStyle = value;
                UpdateFooter();
            }
        }

        public List<String> HeaderValues = new List<string>();
        public List<String> FooterValues = new List<string>();

        private const int HeaderHeight = 1;
        private const int FooterHeight = 1;

        private int HeaderIndex = 0;
        private int FooterIndex => RowCount - FooterHeight;

        private void Event_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.ColumnIndex > ColumnCount) return;

            if (e.RowIndex == HeaderIndex)
            {
                if (HeaderValues.Count > e.ColumnIndex)
                    e.Value = HeaderValues[e.ColumnIndex];
                return;
            }

            if (e.RowIndex == FooterIndex)
            {
                if (FooterValues.Count > e.ColumnIndex)
                    e.Value = FooterValues[e.ColumnIndex];
                return;
            }

            int RowIndex = e.RowIndex - HeaderHeight;
            if (RowIndex > RowCount) return;

            object value = dataTable.Rows[RowIndex][e.ColumnIndex];

            if (!Multiline && value is string s)
            {
                int newLineIndex = s.IndexOf('\n');
                if (newLineIndex >= 0) value = s.Substring(0, newLineIndex);
            }

            e.Value = value;
        }

        public void ColumnsAutoFit()
        {
            for (int i = 0; i < ColumnCount; i++)
                Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Columns[ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            for (int i = 0; i < ColumnCount; i++)
                Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool InFooterOrHeader(int RowIndex)
        {
            return RowIndex == HeaderIndex || RowIndex == FooterIndex;
        }

        private void UpdateHeader()
        {
            if (HeaderStyle == null) return;
            for (int x = 0; x < ColumnCount; x++)
                Rows[HeaderIndex].Cells[x].Style = HeaderStyle;
        }

        private void UpdateFooter()
        {
            if (FooterStyle == null) return;
            for (int x = 0; x < ColumnCount; x++)
                Rows[FooterIndex].Cells[x].Style = FooterStyle;
        }

        public void Colorize(GridCellsAppearance.DelegateColorize colorize)
        {
            for (int x = 0; x < ColumnCount; x++)
            {
                for (int y = HeaderIndex+1; y < FooterIndex; y++)
                {
                    Rows[y].Cells[x].Style = colorize(x, y - HeaderHeight);
                }
            }
        }
    }
}
