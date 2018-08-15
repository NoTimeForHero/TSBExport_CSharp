using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TSBExport_CSharp.GUI.Controls
{
    partial class ControlRangeValues
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.mtbValue2 = new TSBExport_CSharp.GUI.Controls.MaskedTextBoxWithBorder();
            this.chkRandom = new System.Windows.Forms.CheckBox();
            this.labelInfo = new System.Windows.Forms.Label();
            this.mtbValue1 = new TSBExport_CSharp.GUI.Controls.MaskedTextBoxWithBorder();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.3125F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.6875F));
            this.tableLayoutPanel1.Controls.Add(this.cbType, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.mtbValue2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkRandom, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelInfo, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.mtbValue1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(597, 59);
            this.tableLayoutPanel1.TabIndex = 26;
            // 
            // cbType
            // 
            this.cbType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(3, 3);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(94, 21);
            this.cbType.TabIndex = 1;
            // 
            // mtbValue2
            // 
            this.mtbValue2.BorderColor = this.ForeColor;
            this.mtbValue2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mtbValue2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtbValue2.ForeColor = this.ForeColor;
            this.mtbValue2.Location = new System.Drawing.Point(378, 5);
            this.mtbValue2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.mtbValue2.Mask = "00/00/0000  -  00:00:00";
            this.mtbValue2.Name = "mtbValue2";
            this.mtbValue2.PromptChar = '#';
            this.mtbValue2.Size = new System.Drawing.Size(215, 20);
            this.mtbValue2.TabIndex = 4;
            this.mtbValue2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtbValue2.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MaskedTextBox_PreviewKeyDown);
            this.mtbValue2.Validating += new System.ComponentModel.CancelEventHandler(this.MaskedTextBox_Validating);
            // 
            // chkRandom
            // 
            this.chkRandom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRandom.Location = new System.Drawing.Point(3, 33);
            this.chkRandom.Name = "chkRandom";
            this.chkRandom.Size = new System.Drawing.Size(94, 21);
            this.chkRandom.TabIndex = 2;
            this.chkRandom.Text = "Random range";
            this.chkRandom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkRandom.UseVisualStyleBackColor = true;
            // 
            // labelInfo
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.labelInfo, 2);
            this.labelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelInfo.ForeColor = this.ForeColor;
            this.labelInfo.Location = new System.Drawing.Point(103, 30);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(491, 27);
            this.labelInfo.TabIndex = 21;
            this.labelInfo.Text = "dd.MM.yyy  -  HH:mm:ss";
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mtbValue1
            // 
            this.mtbValue1.BorderColor = this.ForeColor;
            this.mtbValue1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mtbValue1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtbValue1.ForeColor = this.ForeColor;
            this.mtbValue1.Location = new System.Drawing.Point(104, 5);
            this.mtbValue1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.mtbValue1.Mask = "00/00/0000  -  00:00:00";
            this.mtbValue1.Name = "mtbValue1";
            this.mtbValue1.PromptChar = '#';
            this.mtbValue1.Size = new System.Drawing.Size(266, 20);
            this.mtbValue1.TabIndex = 3;
            this.mtbValue1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtbValue1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MaskedTextBox_PreviewKeyDown);
            this.mtbValue1.Validating += new System.ComponentModel.CancelEventHandler(this.MaskedTextBox_Validating);
            // 
            // ControlRangeValues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ControlRangeValues";
            this.Size = new System.Drawing.Size(597, 59);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        private ComboBox cbType;
        private CheckBox chkRandom;
        private Label labelInfo;
        private MaskedTextBoxWithBorder mtbValue2;
        private MaskedTextBoxWithBorder mtbValue1;
    }
}
