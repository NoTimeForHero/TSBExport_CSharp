using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TSBExport_CSharp.GUI.Forms
{
    public partial class DialogExport : Form
    {
        public event Action ButtonCancelClick;

        public DialogExport()
        {
            InitializeComponent();
        }

        private void _setStatus(string text, int value, int max)
        {
            if (value < 0 || max < 0)
            {
                progressBar1.Style = ProgressBarStyle.Marquee;
                return;
            }

            if (progressBar1.Style == ProgressBarStyle.Marquee)
            {
                progressBar1.Style = ProgressBarStyle.Blocks;
            }

            label1.Text = text;
            progressBar1.Value = value;
            progressBar1.Maximum = max;
        }

        public void setStatus(string text, int value, int max)
        {
            try
            {
                Invoke((Action<string, int, int>) _setStatus, text, value, max);
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine("ObjectDisposedException: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("InvalidOperationException: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ButtonCancelClick?.Invoke();
        }
    }
}
