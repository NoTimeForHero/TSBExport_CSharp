namespace TSBExport_CSharp
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new TSBExport_CSharp.GUI.Controls.ExtendedDataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_Generate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_Table = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_Colors = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_excelNoStyle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_excel = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(192)))), ((int)(((byte)(211)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.FooterStyle = null;
            this.dataGridView1.HeaderStyle = null;
            this.dataGridView1.Location = new System.Drawing.Point(0, 52);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.Size = new System.Drawing.Size(794, 357);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.VirtualMode = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(94)))), ((int)(((byte)(116)))));
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.toolStrip_Generate,
            this.toolStrip_Table,
            this.toolStrip_Colors,
            this.toolStrip_Export,
            this.toolStrip_Exit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(10, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(794, 52);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.AutoSize = false;
            this.toolStripMenuItem4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(224)))), ((int)(((byte)(90)))));
            this.toolStripMenuItem4.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripMenuItem4.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(48, 38);
            this.toolStripMenuItem4.Text = "?";
            // 
            // toolStrip_Generate
            // 
            this.toolStrip_Generate.AutoSize = false;
            this.toolStrip_Generate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(224)))), ((int)(((byte)(90)))));
            this.toolStrip_Generate.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStrip_Generate.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.toolStrip_Generate.Name = "toolStrip_Generate";
            this.toolStrip_Generate.Size = new System.Drawing.Size(96, 38);
            this.toolStrip_Generate.Text = "Generate";
            this.toolStrip_Generate.Click += new System.EventHandler(this.toolStrip_Generate_Click);
            // 
            // toolStrip_Table
            // 
            this.toolStrip_Table.AutoSize = false;
            this.toolStrip_Table.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(224)))), ((int)(((byte)(90)))));
            this.toolStrip_Table.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStrip_Table.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.toolStrip_Table.Name = "toolStrip_Table";
            this.toolStrip_Table.Size = new System.Drawing.Size(78, 38);
            this.toolStrip_Table.Text = "Table";
            // 
            // toolStrip_Colors
            // 
            this.toolStrip_Colors.AutoSize = false;
            this.toolStrip_Colors.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(224)))), ((int)(((byte)(90)))));
            this.toolStrip_Colors.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStrip_Colors.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.toolStrip_Colors.Name = "toolStrip_Colors";
            this.toolStrip_Colors.Size = new System.Drawing.Size(78, 38);
            this.toolStrip_Colors.Text = "Color";
            this.toolStrip_Colors.DropDownOpening += new System.EventHandler(this.toolStripMenu_Colors_DropDownOpening);
            // 
            // toolStrip_Export
            // 
            this.toolStrip_Export.AutoSize = false;
            this.toolStrip_Export.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(224)))), ((int)(((byte)(90)))));
            this.toolStrip_Export.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStrip_excelNoStyle,
            this.toolStrip_excel});
            this.toolStrip_Export.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStrip_Export.ForeColor = System.Drawing.Color.Black;
            this.toolStrip_Export.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.toolStrip_Export.Name = "toolStrip_Export";
            this.toolStrip_Export.Size = new System.Drawing.Size(78, 38);
            this.toolStrip_Export.Text = "Export";
            // 
            // toolStrip_Exit
            // 
            this.toolStrip_Exit.AutoSize = false;
            this.toolStrip_Exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(224)))), ((int)(((byte)(90)))));
            this.toolStrip_Exit.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStrip_Exit.ForeColor = System.Drawing.Color.Black;
            this.toolStrip_Exit.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.toolStrip_Exit.Name = "toolStrip_Exit";
            this.toolStrip_Exit.Size = new System.Drawing.Size(78, 38);
            this.toolStrip_Exit.Text = "Exit";
            this.toolStrip_Exit.Click += new System.EventHandler(this.toolStrip_Exit_Click);
            // 
            // toolStrip_excelNoStyle
            // 
            this.toolStrip_excelNoStyle.Name = "toolStrip_excelNoStyle";
            this.toolStrip_excelNoStyle.Size = new System.Drawing.Size(212, 30);
            this.toolStrip_excelNoStyle.Text = "Excel (no style)";
            this.toolStrip_excelNoStyle.Click += new System.EventHandler(this.toolStrip_excel_Click);
            // 
            // toolStrip_excel
            // 
            this.toolStrip_excel.Name = "toolStrip_excel";
            this.toolStrip_excel.Size = new System.Drawing.Size(212, 30);
            this.toolStrip_excel.Text = "Excel";
            this.toolStrip_excel.Click += new System.EventHandler(this.toolStrip_excel_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 409);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormMain";
            this.Text = "TSBExport_CSharp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeBegin += new System.EventHandler(this.FormMain_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TSBExport_CSharp.GUI.Controls.ExtendedDataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_Exit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_Colors;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_Table;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_Export;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_Generate;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_excelNoStyle;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_excel;
    }
}

