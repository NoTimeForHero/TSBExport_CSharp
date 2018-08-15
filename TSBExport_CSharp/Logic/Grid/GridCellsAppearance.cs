using System.Drawing;
using System.Windows.Forms;
using TSBExport_CSharp.Other;

namespace TSBExport_CSharp.Grid
{
    public class GridCellsAppearance
    {
        public string name = "Default Style";

        public Color gridColor = Color.DarkGoldenrod;
        public DataGridViewCellStyle styleHeader = Decorator.MakeStyle(Color.Black, Color.White);
        public DataGridViewCellStyle styleFooter = Decorator.MakeStyle(Color.Black, Color.White);

        // ------ analogue of "block code" in harbour ----
        // Signature declaration
        public delegate DataGridViewCellStyle DelegateColorize(int ColumnIndex, int RowIndex);
        // callback variable
        public DelegateColorize colorize;
    }
}