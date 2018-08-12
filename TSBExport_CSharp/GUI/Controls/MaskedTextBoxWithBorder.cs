using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSBExport_CSharp
{
    public class MaskedTextBoxWithBorder : MaskedTextBox
    {
        private Color mColor = SystemColors.WindowText;
        public MaskedTextBoxWithBorder()
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            Invalidate();
        }

        [SuppressMessage("ReSharper", "ArrangeAccessorOwnerBody")] // Visual Studio has a strange bug with lambda expression getter
        [DefaultValue(typeof(SystemColors), "WindowText")]
        public Color BorderColor
        {
            get { return mColor; }
            set { mColor = value; Invalidate(); }
        }

        private void drawBorder()
        {
            using (Graphics gr = this.CreateGraphics())
            {
                ControlPaint.DrawBorder(gr, this.DisplayRectangle, mColor, ButtonBorderStyle.Solid);
            }
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 15) drawBorder();
        }
    }
}
