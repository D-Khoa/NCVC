namespace BoxIdDb
{
    partial class frmBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBox));
            this.btnAddBoxId = new System.Windows.Forms.Button();
            this.btnSearchBoxId = new System.Windows.Forms.Button();
            this.dgvBoxId = new System.Windows.Forms.DataGridView();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxIdFrom = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProductSerial = new System.Windows.Forms.TextBox();
            this.dtpPrintDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.pnlBarcode = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rdbBoxId = new System.Windows.Forms.RadioButton();
            this.rdbPrintDate = new System.Windows.Forms.RadioButton();
            this.rdbProductSerial = new System.Windows.Forms.RadioButton();
            this.btnAddReturn = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnEditShipping = new System.Windows.Forms.Button();
            this.rdbShipDate = new System.Windows.Forms.RadioButton();
            this.dtpShipDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBoxIdTo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnModelEcd = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.contactUsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoxId)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddBoxId
            // 
            this.btnAddBoxId.Location = new System.Drawing.Point(114, 196);
            this.btnAddBoxId.Name = "btnAddBoxId";
            this.btnAddBoxId.Size = new System.Drawing.Size(80, 25);
            this.btnAddBoxId.TabIndex = 6;
            this.btnAddBoxId.Text = "Add Box ID";
            this.btnAddBoxId.UseVisualStyleBackColor = true;
            this.btnAddBoxId.Click += new System.EventHandler(this.btnAddBoxId_Click);
            // 
            // btnSearchBoxId
            // 
            this.btnSearchBoxId.Location = new System.Drawing.Point(28, 196);
            this.btnSearchBoxId.Name = "btnSearchBoxId";
            this.btnSearchBoxId.Size = new System.Drawing.Size(80, 25);
            this.btnSearchBoxId.TabIndex = 2;
            this.btnSearchBoxId.Text = "Search";
            this.btnSearchBoxId.UseVisualStyleBackColor = true;
            this.btnSearchBoxId.Click += new System.EventHandler(this.btnSearchBoxId_Click);
            // 
            // dgvBoxId
            // 
            this.dgvBoxId.AllowUserToAddRows = false;
            this.dgvBoxId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBoxId.BackgroundColor = System.Drawing.Color.LightSkyBlue;
            this.dgvBoxId.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBoxId.Location = new System.Drawing.Point(3, 242);
            this.dgvBoxId.Name = "dgvBoxId";
            this.dgvBoxId.ReadOnly = true;
            this.dgvBoxId.RowTemplate.Height = 21;
            this.dgvBoxId.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvBoxId.Size = new System.Drawing.Size(645, 477);
            this.dgvBoxId.TabIndex = 9;
            this.dgvBoxId.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBoxId_CellContentClick);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(50, 81);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Print Date: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Box ID from: ";
            // 
            // txtBoxIdFrom
            // 
            this.txtBoxIdFrom.Location = new System.Drawing.Point(148, 41);
            this.txtBoxIdFrom.Name = "txtBoxIdFrom";
            this.txtBoxIdFrom.Size = new System.Drawing.Size(166, 20);
            this.txtBoxIdFrom.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Product Serial: ";
            // 
            // txtProductSerial
            // 
            this.txtProductSerial.Location = new System.Drawing.Point(148, 112);
            this.txtProductSerial.Name = "txtProductSerial";
            this.txtProductSerial.Size = new System.Drawing.Size(166, 20);
            this.txtProductSerial.TabIndex = 1;
            // 
            // dtpPrintDate
            // 
            this.dtpPrintDate.CustomFormat = "yyyy/MM/dd";
            this.dtpPrintDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPrintDate.Location = new System.Drawing.Point(148, 75);
            this.dtpPrintDate.Name = "dtpPrintDate";
            this.dtpPrintDate.Size = new System.Drawing.Size(166, 20);
            this.dtpPrintDate.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(394, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "User: ";
            // 
            // txtUser
            // 
            this.txtUser.Enabled = false;
            this.txtUser.Location = new System.Drawing.Point(435, 78);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(166, 20);
            this.txtUser.TabIndex = 1;
            // 
            // pnlBarcode
            // 
            this.pnlBarcode.BackColor = System.Drawing.Color.White;
            this.pnlBarcode.Location = new System.Drawing.Point(395, 116);
            this.pnlBarcode.Name = "pnlBarcode";
            this.pnlBarcode.Size = new System.Drawing.Size(206, 57);
            this.pnlBarcode.TabIndex = 11;
            this.pnlBarcode.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBarcode_Paint);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(544, 196);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rdbBoxId
            // 
            this.rdbBoxId.AutoSize = true;
            this.rdbBoxId.Location = new System.Drawing.Point(335, 43);
            this.rdbBoxId.Name = "rdbBoxId";
            this.rdbBoxId.Size = new System.Drawing.Size(14, 13);
            this.rdbBoxId.TabIndex = 12;
            this.rdbBoxId.UseVisualStyleBackColor = true;
            this.rdbBoxId.CheckedChanged += new System.EventHandler(this.rdbBoxId_CheckedChanged);
            // 
            // rdbPrintDate
            // 
            this.rdbPrintDate.AutoSize = true;
            this.rdbPrintDate.Checked = true;
            this.rdbPrintDate.Location = new System.Drawing.Point(336, 79);
            this.rdbPrintDate.Name = "rdbPrintDate";
            this.rdbPrintDate.Size = new System.Drawing.Size(14, 13);
            this.rdbPrintDate.TabIndex = 12;
            this.rdbPrintDate.TabStop = true;
            this.rdbPrintDate.UseVisualStyleBackColor = true;
            this.rdbPrintDate.CheckedChanged += new System.EventHandler(this.rdbPrintDate_CheckedChanged);
            // 
            // rdbProductSerial
            // 
            this.rdbProductSerial.AutoSize = true;
            this.rdbProductSerial.Location = new System.Drawing.Point(336, 113);
            this.rdbProductSerial.Name = "rdbProductSerial";
            this.rdbProductSerial.Size = new System.Drawing.Size(14, 13);
            this.rdbProductSerial.TabIndex = 12;
            this.rdbProductSerial.UseVisualStyleBackColor = true;
            this.rdbProductSerial.CheckedChanged += new System.EventHandler(this.rdbProductSerial_CheckedChanged);
            // 
            // btnAddReturn
            // 
            this.btnAddReturn.Location = new System.Drawing.Point(286, 196);
            this.btnAddReturn.Name = "btnAddReturn";
            this.btnAddReturn.Size = new System.Drawing.Size(80, 25);
            this.btnAddReturn.TabIndex = 6;
            this.btnAddReturn.Text = "Add Return";
            this.btnAddReturn.UseVisualStyleBackColor = true;
            this.btnAddReturn.Click += new System.EventHandler(this.btnAddReturn_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(458, 196);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(80, 25);
            this.btnExport.TabIndex = 42;
            this.btnExport.Text = "Excel Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnEditShipping
            // 
            this.btnEditShipping.Location = new System.Drawing.Point(372, 196);
            this.btnEditShipping.Name = "btnEditShipping";
            this.btnEditShipping.Size = new System.Drawing.Size(80, 25);
            this.btnEditShipping.TabIndex = 43;
            this.btnEditShipping.Text = "Edit Shipping";
            this.btnEditShipping.UseVisualStyleBackColor = true;
            this.btnEditShipping.Click += new System.EventHandler(this.btnEditShipping_Click);
            // 
            // rdbShipDate
            // 
            this.rdbShipDate.AutoSize = true;
            this.rdbShipDate.Location = new System.Drawing.Point(336, 150);
            this.rdbShipDate.Name = "rdbShipDate";
            this.rdbShipDate.Size = new System.Drawing.Size(14, 13);
            this.rdbShipDate.TabIndex = 46;
            this.rdbShipDate.UseVisualStyleBackColor = true;
            this.rdbShipDate.CheckedChanged += new System.EventHandler(this.rdbShipDate_CheckedChanged_1);
            // 
            // dtpShipDate
            // 
            this.dtpShipDate.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dtpShipDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpShipDate.Location = new System.Drawing.Point(148, 150);
            this.dtpShipDate.Name = "dtpShipDate";
            this.dtpShipDate.ShowUpDown = true;
            this.dtpShipDate.Size = new System.Drawing.Size(166, 20);
            this.dtpShipDate.TabIndex = 45;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 44;
            this.label4.Text = "OQC Ship Date: ";
            // 
            // txtBoxIdTo
            // 
            this.txtBoxIdTo.Location = new System.Drawing.Point(435, 41);
            this.txtBoxIdTo.Name = "txtBoxIdTo";
            this.txtBoxIdTo.Size = new System.Drawing.Size(166, 20);
            this.txtBoxIdTo.TabIndex = 47;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(393, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 48;
            this.label5.Text = "to: ";
            // 
            // btnModelEcd
            // 
            this.btnModelEcd.Location = new System.Drawing.Point(200, 196);
            this.btnModelEcd.Name = "btnModelEcd";
            this.btnModelEcd.Size = new System.Drawing.Size(80, 25);
            this.btnModelEcd.TabIndex = 49;
            this.btnModelEcd.Text = "Add E-code";
            this.btnModelEcd.UseVisualStyleBackColor = true;
            this.btnModelEcd.Click += new System.EventHandler(this.btnModelEcd_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contactUsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(651, 24);
            this.menuStrip1.TabIndex = 50;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // contactUsToolStripMenuItem
            // 
            this.contactUsToolStripMenuItem.Name = "contactUsToolStripMenuItem";
            this.contactUsToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.contactUsToolStripMenuItem.Text = "Contact us";
            this.contactUsToolStripMenuItem.Click += new System.EventHandler(this.contactUsToolStripMenuItem_Click);
            // 
            // frmBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(651, 721);
            this.Controls.Add(this.btnModelEcd);
            this.Controls.Add(this.txtBoxIdTo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rdbShipDate);
            this.Controls.Add(this.dtpShipDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnEditShipping);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.rdbProductSerial);
            this.Controls.Add(this.rdbPrintDate);
            this.Controls.Add(this.rdbBoxId);
            this.Controls.Add(this.pnlBarcode);
            this.Controls.Add(this.dtpPrintDate);
            this.Controls.Add(this.txtBoxIdFrom);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtProductSerial);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnAddReturn);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddBoxId);
            this.Controls.Add(this.btnSearchBoxId);
            this.Controls.Add(this.dgvBoxId);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Box ID";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmBox_FormClosed);
            this.Load += new System.EventHandler(this.frmBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoxId)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridView dgvBoxId;
        private System.Windows.Forms.Button btnSearchBoxId;
        private System.Windows.Forms.Button btnAddBoxId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxIdFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProductSerial;
        private System.Windows.Forms.DateTimePicker dtpPrintDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Panel pnlBarcode;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rdbBoxId;
        private System.Windows.Forms.RadioButton rdbPrintDate;
        private System.Windows.Forms.RadioButton rdbProductSerial;
        private System.Windows.Forms.Button btnAddReturn;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnEditShipping;
        private System.Windows.Forms.RadioButton rdbShipDate;
        private System.Windows.Forms.DateTimePicker dtpShipDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxIdTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnModelEcd;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem contactUsToolStripMenuItem;
    }
}

