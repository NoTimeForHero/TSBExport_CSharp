using TSBExport_CSharp.GUI.Controls;

namespace TSBExport_CSharp
{
    partial class FormGenerate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGenerate));
            this.panelFields = new System.Windows.Forms.Panel();
            this.panelExample = new System.Windows.Forms.Panel();
            this.protoFieldDelete = new System.Windows.Forms.Button();
            this.protoField = new TSBExport_CSharp.GUI.Controls.ControlRangeValues();
            this.buttonAddField = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSeed = new TSBExport_CSharp.GUI.Controls.MaskedTextBoxWithBorder();
            this.tbRecords = new TSBExport_CSharp.GUI.Controls.MaskedTextBoxWithBorder();
            this.panelFields.SuspendLayout();
            this.panelExample.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFields
            // 
            this.panelFields.AutoScroll = true;
            this.panelFields.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(241)))), ((int)(((byte)(192)))));
            this.panelFields.Controls.Add(this.panelExample);
            this.panelFields.Location = new System.Drawing.Point(12, 67);
            this.panelFields.Name = "panelFields";
            this.panelFields.Size = new System.Drawing.Size(703, 368);
            this.panelFields.TabIndex = 3;
            // 
            // panelExample
            // 
            this.panelExample.BackColor = System.Drawing.Color.Transparent;
            this.panelExample.Controls.Add(this.protoFieldDelete);
            this.panelExample.Controls.Add(this.protoField);
            this.panelExample.Location = new System.Drawing.Point(3, 3);
            this.panelExample.Name = "panelExample";
            this.panelExample.Size = new System.Drawing.Size(683, 97);
            this.panelExample.TabIndex = 5;
            this.panelExample.Visible = false;
            // 
            // protoFieldDelete
            // 
            this.protoFieldDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(118)))), ((int)(((byte)(98)))));
            this.protoFieldDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(219)))), ((int)(((byte)(80)))));
            this.protoFieldDelete.Location = new System.Drawing.Point(599, 22);
            this.protoFieldDelete.Name = "protoFieldDelete";
            this.protoFieldDelete.Size = new System.Drawing.Size(71, 35);
            this.protoFieldDelete.TabIndex = 4;
            this.protoFieldDelete.Text = "Delete";
            this.protoFieldDelete.UseVisualStyleBackColor = false;
            // 
            // protoField
            // 
            this.protoField.BackColor = System.Drawing.Color.Transparent;
            this.protoField.EnabledRandomValue = true;
            this.protoField.ForeColor = System.Drawing.Color.Black;
            this.protoField.Location = new System.Drawing.Point(9, 18);
            this.protoField.Margin = new System.Windows.Forms.Padding(14, 18, 14, 18);
            this.protoField.Name = "protoField";
            this.protoField.Size = new System.Drawing.Size(559, 75);
            this.protoField.TabIndex = 3;
            this.protoField.Text1 = null;
            this.protoField.Text2 = null;
            this.protoField.ValueType = TSBExport_CSharp.GUI.Controls.ControlRangeValues.EnumValueType.Date;
            // 
            // buttonAddField
            // 
            this.buttonAddField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(118)))), ((int)(((byte)(98)))));
            this.buttonAddField.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddField.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(219)))), ((int)(((byte)(80)))));
            this.buttonAddField.Location = new System.Drawing.Point(12, 441);
            this.buttonAddField.Name = "buttonAddField";
            this.buttonAddField.Size = new System.Drawing.Size(143, 52);
            this.buttonAddField.TabIndex = 9000;
            this.buttonAddField.Text = "New field";
            this.buttonAddField.UseVisualStyleBackColor = false;
            this.buttonAddField.Click += new System.EventHandler(this.buttonAddField_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(118)))), ((int)(((byte)(98)))));
            this.buttonExit.CausesValidation = false;
            this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(219)))), ((int)(((byte)(80)))));
            this.buttonExit.Location = new System.Drawing.Point(587, 441);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(128, 47);
            this.buttonExit.TabIndex = 9010;
            this.buttonExit.Text = "Cancel";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(118)))), ((int)(((byte)(98)))));
            this.buttonOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(219)))), ((int)(((byte)(80)))));
            this.buttonOK.Location = new System.Drawing.Point(453, 441);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(128, 47);
            this.buttonOK.TabIndex = 9005;
            this.buttonOK.Text = "Confirm";
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Records to generate:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(295, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(245, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Random generator seed for data:";
            // 
            // tbSeed
            // 
            this.tbSeed.BorderColor = System.Drawing.SystemColors.WindowText;
            this.tbSeed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSeed.Location = new System.Drawing.Point(546, 30);
            this.tbSeed.Name = "tbSeed";
            this.tbSeed.Size = new System.Drawing.Size(152, 26);
            this.tbSeed.TabIndex = 2;
            this.tbSeed.Text = "1011010";
            this.tbSeed.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Validating);
            // 
            // tbRecords
            // 
            this.tbRecords.BorderColor = System.Drawing.SystemColors.WindowText;
            this.tbRecords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRecords.Location = new System.Drawing.Point(177, 27);
            this.tbRecords.Name = "tbRecords";
            this.tbRecords.Size = new System.Drawing.Size(100, 26);
            this.tbRecords.TabIndex = 1;
            this.tbRecords.Text = "500";
            this.tbRecords.Validating += new System.ComponentModel.CancelEventHandler(this.TextBox_Validating);
            // 
            // FormGenerate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(219)))), ((int)(((byte)(175)))));
            this.ClientSize = new System.Drawing.Size(727, 495);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbSeed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbRecords);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonAddField);
            this.Controls.Add(this.panelFields);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGenerate";
            this.Text = "Generate dynamic data";
            this.panelFields.ResumeLayout(false);
            this.panelExample.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelFields;
        private System.Windows.Forms.Button buttonAddField;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Panel panelExample;
        private System.Windows.Forms.Button protoFieldDelete;
        private ControlRangeValues protoField;
        private MaskedTextBoxWithBorder tbRecords;
        private MaskedTextBoxWithBorder tbSeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}