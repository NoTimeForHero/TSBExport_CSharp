﻿using System.Drawing;
using System.Windows.Forms;

namespace TSBExport_CSharp
{
    public class GridCellsAppearance
    {
        public string name = "Default Style";

        public DataGridViewCellStyle styleHeader = Decorator.MakeStyle(Color.Black, Color.White);
        public DataGridViewCellStyle styleFooter = Decorator.MakeStyle(Color.Black, Color.White);

        // ------ analogue of "block code" in harbour ----
        // Signature declaration
        public delegate DataGridViewCellStyle DelegateColorize(int ColumnIndex, int RowIndex);
        // callback variable
        public DelegateColorize colorize;
    }
}