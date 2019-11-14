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
            this.btnInvoice = new System.Windows.Forms.Button();
            this.colInv = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colBoxid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShaft = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOverlay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrintdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShipDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReturn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_invoice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.txtInvoice = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoxId)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddBoxId
            // 
            this.btnAddBoxId.Location = new System.Drawing.Point(173, 195);
            this.btnAddBoxId.Name = "btnAddBoxId";
            this.btnAddBoxId.Size = new System.Drawing.Size(80, 25);
            this.btnAddBoxId.TabIndex = 6;
            this.btnAddBoxId.Text = "Add Box ID";
            this.btnAddBoxId.UseVisualStyleBackColor = true;
            this.btnAddBoxId.Click += new System.EventHandler(this.btnAddBoxId_Click);
            // 
            // btnSearchBoxId
            // 
            this.btnSearchBoxId.Location = new System.Drawing.Point(87, 195);
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
            this.dgvBoxId.BackgroundColor = System.Drawing.Color.LightSalmon;
            this.dgvBoxId.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBoxId.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colInv,
            this.colBoxid,
            this.colShaft,
            this.colOverlay,
            this.colUser,
            this.colPrintdate,
            this.colShipDate,
            this.colReturn,
            this.col_invoice});
            this.dgvBoxId.Location = new System.Drawing.Point(12, 234);
            this.dgvBoxId.Name = "dgvBoxId";
            this.dgvBoxId.RowTemplate.Height = 21;
            this.dgvBoxId.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvBoxId.Size = new System.Drawing.Size(730, 361);
            this.dgvBoxId.TabIndex = 9;
            this.dgvBoxId.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBoxId_CellContentClick);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(50, 50);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Print Date: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Box ID from: ";
            // 
            // txtBoxIdFrom
            // 
            this.txtBoxIdFrom.Location = new System.Drawing.Point(148, 10);
            this.txtBoxIdFrom.Name = "txtBoxIdFrom";
            this.txtBoxIdFrom.Size = new System.Drawing.Size(166, 20);
            this.txtBoxIdFrom.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Product Serial: ";
            // 
            // txtProductSerial
            // 
            this.txtProductSerial.Location = new System.Drawing.Point(148, 81);
            this.txtProductSerial.Name = "txtProductSerial";
            this.txtProductSerial.Size = new System.Drawing.Size(166, 20);
            this.txtProductSerial.TabIndex = 1;
            // 
            // dtpPrintDate
            // 
            this.dtpPrintDate.CustomFormat = "yyyy/MM/dd";
            this.dtpPrintDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPrintDate.Location = new System.Drawing.Point(148, 44);
            this.dtpPrintDate.Name = "dtpPrintDate";
            this.dtpPrintDate.Size = new System.Drawing.Size(166, 20);
            this.dtpPrintDate.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(463, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "User: ";
            // 
            // txtUser
            // 
            this.txtUser.Enabled = false;
            this.txtUser.Location = new System.Drawing.Point(504, 47);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(166, 20);
            this.txtUser.TabIndex = 1;
            // 
            // pnlBarcode
            // 
            this.pnlBarcode.BackColor = System.Drawing.Color.White;
            this.pnlBarcode.Location = new System.Drawing.Point(464, 85);
            this.pnlBarcode.Name = "pnlBarcode";
            this.pnlBarcode.Size = new System.Drawing.Size(206, 57);
            this.pnlBarcode.TabIndex = 11;
            this.pnlBarcode.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBarcode_Paint);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(603, 195);
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
            this.rdbBoxId.Location = new System.Drawing.Point(335, 12);
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
            this.rdbPrintDate.Location = new System.Drawing.Point(336, 48);
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
            this.rdbProductSerial.Location = new System.Drawing.Point(336, 82);
            this.rdbProductSerial.Name = "rdbProductSerial";
            this.rdbProductSerial.Size = new System.Drawing.Size(14, 13);
            this.rdbProductSerial.TabIndex = 12;
            this.rdbProductSerial.UseVisualStyleBackColor = true;
            this.rdbProductSerial.CheckedChanged += new System.EventHandler(this.rdbProductSerial_CheckedChanged);
            // 
            // btnAddReturn
            // 
            this.btnAddReturn.Location = new System.Drawing.Point(345, 195);
            this.btnAddReturn.Name = "btnAddReturn";
            this.btnAddReturn.Size = new System.Drawing.Size(80, 25);
            this.btnAddReturn.TabIndex = 6;
            this.btnAddReturn.Text = "Add Return";
            this.btnAddReturn.UseVisualStyleBackColor = true;
            this.btnAddReturn.Click += new System.EventHandler(this.btnAddReturn_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(259, 195);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(80, 25);
            this.btnExport.TabIndex = 42;
            this.btnExport.Text = "Excel Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnEditShipping
            // 
            this.btnEditShipping.Location = new System.Drawing.Point(431, 195);
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
            this.rdbShipDate.Location = new System.Drawing.Point(336, 119);
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
            this.dtpShipDate.Location = new System.Drawing.Point(148, 119);
            this.dtpShipDate.Name = "dtpShipDate";
            this.dtpShipDate.ShowUpDown = true;
            this.dtpShipDate.Size = new System.Drawing.Size(166, 20);
            this.dtpShipDate.TabIndex = 45;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 44;
            this.label4.Text = "OQC Ship Date: ";
            // 
            // txtBoxIdTo
            // 
            this.txtBoxIdTo.Location = new System.Drawing.Point(504, 10);
            this.txtBoxIdTo.Name = "txtBoxIdTo";
            this.txtBoxIdTo.Size = new System.Drawing.Size(166, 20);
            this.txtBoxIdTo.TabIndex = 47;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(462, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 48;
            this.label5.Text = "to: ";
            // 
            // btnInvoice
            // 
            this.btnInvoice.Location = new System.Drawing.Point(517, 187);
            this.btnInvoice.Name = "btnInvoice";
            this.btnInvoice.Size = new System.Drawing.Size(80, 41);
            this.btnInvoice.TabIndex = 43;
            this.btnInvoice.Text = "Update Invoice";
            this.btnInvoice.UseVisualStyleBackColor = true;
            this.btnInvoice.Click += new System.EventHandler(this.btnInvoice_Click);
            // 
            // colInv
            // 
            this.colInv.HeaderText = "";
            this.colInv.Name = "colInv";
            // 
            // colBoxid
            // 
            this.colBoxid.DataPropertyName = "boxid";
            this.colBoxid.HeaderText = "BoxID";
            this.colBoxid.Name = "colBoxid";
            // 
            // colShaft
            // 
            this.colShaft.DataPropertyName = "shaft";
            this.colShaft.HeaderText = "Shaft";
            this.colShaft.Name = "colShaft";
            // 
            // colOverlay
            // 
            this.colOverlay.DataPropertyName = "over_lay";
            this.colOverlay.HeaderText = "Overlay";
            this.colOverlay.Name = "colOverlay";
            // 
            // colUser
            // 
            this.colUser.DataPropertyName = "suser";
            this.colUser.HeaderText = "User";
            this.colUser.Name = "colUser";
            // 
            // colPrintdate
            // 
            this.colPrintdate.DataPropertyName = "printdate";
            this.colPrintdate.HeaderText = "Print Date";
            this.colPrintdate.Name = "colPrintdate";
            // 
            // colShipDate
            // 
            this.colShipDate.DataPropertyName = "shipdate";
            this.colShipDate.HeaderText = "Ship Date";
            this.colShipDate.Name = "colShipDate";
            // 
            // colReturn
            // 
            this.colReturn.DataPropertyName = "return";
            this.colReturn.HeaderText = "Return";
            this.colReturn.Name = "colReturn";
            // 
            // col_invoice
            // 
            this.col_invoice.DataPropertyName = "invoice";
            this.col_invoice.HeaderText = "Invoice";
            this.col_invoice.Name = "col_invoice";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 161);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Invoice: ";
            // 
            // txtInvoice
            // 
            this.txtInvoice.Location = new System.Drawing.Point(148, 157);
            this.txtInvoice.Name = "txtInvoice";
            this.txtInvoice.Size = new System.Drawing.Size(166, 20);
            this.txtInvoice.TabIndex = 1;
            // 
            // frmBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(755, 607);
            this.Controls.Add(this.txtBoxIdTo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.rdbShipDate);
            this.Controls.Add(this.dtpShipDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnInvoice);
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
            this.Controls.Add(this.txtInvoice);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtProductSerial);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnAddReturn);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddBoxId);
            this.Controls.Add(this.btnSearchBoxId);
            this.Controls.Add(this.dgvBoxId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Box ID";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmBox_FormClosed);
            this.Load += new System.EventHandler(this.frmBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoxId)).EndInit();
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
        private System.Windows.Forms.Button btnInvoice;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colInv;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBoxid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShaft;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOverlay;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrintdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShipDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReturn;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_invoice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtInvoice;
    }
}

