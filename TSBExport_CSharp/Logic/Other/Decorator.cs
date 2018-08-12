using System;
using System.Drawing;
using System.Windows.Forms;

namespace TSBExport_CSharp
{
    public class Decorator
    {
        public static Color errorColor = Color.Red;
        public static Random random = new Random(322);

        public delegate void OnChecked(bool newState);

        public static ToolStripItem CreateCheckboxToolStrip(bool IsChecked, String value, OnChecked onChanged)
        {
            CheckBox cb = new CheckBox { BackColor = Color.Transparent };
            ToolStripControlHost host = new ToolStripControlHost(cb)
            {
                AutoSize = false,
                Text = value,
                Height = 40,
                Width = 250
            };
            cb.Checked = IsChecked;
            cb.CheckedChanged += (e, v) => onChanged(cb.Checked);
            return host;
        }

        public static ToolStripButton CreateButtonToolStrip(String value, EventHandler click)
        {
            ToolStripButton button = new ToolStripButton {Text = value};
            button.Click += click;
            return button;
        }

        public static bool Chance(int chance)
        {
            return random.Next(0, 100) < chance;
        }

        public static DataGridViewCellStyle MakeStyle(Color text, Color background, Font font = null)
        {
            var style = new DataGridViewCellStyle();
            style.ForeColor = text;
            style.BackColor = background;
            // Optional argument, so we must check for null
            if (font != null) style.Font = font;
            return style;
        }
    }
}