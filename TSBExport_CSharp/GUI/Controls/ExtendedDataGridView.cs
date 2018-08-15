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
            dataTable = (DataTable)bindingSrc.DataSource;
            if (dataTable == null) throw new ArgumentException("BindingSource DataSource must be DataTable!");
            ColumnCount = bindingSrc.GetItemProperties(null).Count;
            RowCount = bindingSrc.Count + HeaderHeight + FooterHeight;
        }

        [DefaultValue(typeof(bool), "false")]
        public bool HeaderVisible
        {
            get => Rows[HeaderIndex].Visible;
            set
            {
                Rows[HeaderIndex].Visible = value;
                if (FirstDisplayedScrollingRowIndex < 2) FirstDisplayedScrollingRowIndex = value ? HeaderIndex : HeaderIndex+1;
            }
        }

        [DefaultValue(typeof(bool), "false")]
        public bool FooterVisible {
            get => Rows[FooterIndex].Visible;
            set => Rows[FooterIndex].Visible = value;
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

        // ======== Failed idea with merged headers =======
        // DataGridView events are not enough to realize this idea
        // Need direct changes inside control sources
        // TODO: Remove this section after next pushed commit, to save it in history
        /*
        private bool needRedraw = true;
        private Rectangle bound;
        private Font mergedHeaderFont;

        private void Event_Invalidated(object sender, InvalidateEventArgs e)
        {
            if (!needRedraw && e.InvalidRect.IntersectsWith(bound))
            {
                needRedraw = true;

                // Each of next operation causes notable perfomance issues
                Invalidate(bound);
                InvalidateCell(ColumnCount - 1, 0);
            }
        }

        private void Event_Paint(object sender, PaintEventArgs e)
        {
            // Flicking because event "Paint" called too late then "CellPainting"
            if (needRedraw && false)
            {
                var width = Columns.OfType<DataGridViewColumn>().Take(ColumnCount).Sum(x => x.Width);
                var height = Rows.OfType<DataGridViewRow>().Take(1).Sum(x => x.Height);

                Rectangle xxx = GetCellDisplayRectangle(1, 1, false);
                Console.WriteLine($"{xxx.Left}, {xxx.Top}, {xxx.Right}, {xxx.Bottom}");

                bound = new Rectangle(0, 0, width, height);
                //Console.WriteLine($"{bound.Left}, {bound.Top}, {bound.Right}, {bound.Bottom}");

                if (mergedHeaderFont == null) mergedHeaderFont = new Font(DefaultCellStyle.Font.FontFamily, DefaultCellStyle.Font.SizeInPoints + 3, FontStyle.Bold);
                TextRenderer.DrawText(e.Graphics, "THIS IS SUPERHEADER", mergedHeaderFont, bound, Color.Yellow, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }

        }

        private void Event_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (InFooterOrHeader(e.RowIndex))
            {
                var copy = new DataGridViewAdvancedBorderStyle();
                copy.Bottom = e.AdvancedBorderStyle.Bottom;
                copy.Top = e.AdvancedBorderStyle.Top;
                copy.Left = e.AdvancedBorderStyle.Left;
                copy.Right = e.AdvancedBorderStyle.Right;

                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                e.AdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
                e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;

                #region MainBlock
                if (e.RowIndex == 0)
                    e.AdvancedBorderStyle.Top = copy.Top;

                if (e.ColumnIndex == 0)
                    e.AdvancedBorderStyle.Left = copy.Left;

                if (e.ColumnIndex == ColumnCount - 1)
                    e.AdvancedBorderStyle.Right = copy.Right;

                if (e.RowIndex == HeaderTotalHeight - 1)
                    e.AdvancedBorderStyle.Bottom = copy.Bottom;
                #endregion

                // SECOND ROW
                if (e.RowIndex == 0)
                    e.AdvancedBorderStyle.Bottom = copy.Bottom;

                e.PaintBackground(e.ClipBounds, true);
                e.Handled = true;

                if (needRedraw && e.RowIndex == 0 && e.ColumnIndex == ColumnCount - 1)
                {
                    var width = Columns.OfType<DataGridViewColumn>().Take(ColumnCount).Sum(x => x.Width);
                    var height = Rows.OfType<DataGridViewRow>().Take(1).Sum(x => x.Height);

                    bound = new Rectangle(0, 0, width, height);
                    if (mergedHeaderFont == null) mergedHeaderFont = new Font(DefaultCellStyle.Font.FontFamily, DefaultCellStyle.Font.SizeInPoints + 3, FontStyle.Bold);
                    TextRenderer.DrawText(e.Graphics, "MERGED HEADER", mergedHeaderFont, bound, Color.Yellow, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    needRedraw = false;
                }
            }
        }
        */

        private void Event_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex == HeaderIndex)
            {
                if (HeaderValues.Count > e.ColumnIndex)
                    e.Value = HeaderValues[e.ColumnIndex];
                return;
            }

            if (e.RowIndex == FooterIndex)
            {
                if (FooterValues.Count > e.ColumnIndex)
                    e.Value = HeaderValues[e.ColumnIndex];
                return;
            }

            e.Value = dataTable.Rows[e.RowIndex - HeaderHeight][e.ColumnIndex];
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
            {
                Rows[HeaderIndex].Cells[x].Style = HeaderStyle;
            }
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
                for (int y = HeaderIndex+1; y < FooterIndex-1; y++)
                {
                    Rows[y].Cells[x].Style = colorize(x, y - HeaderHeight);
                }
            }
        }
    }
}
