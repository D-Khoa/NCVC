namespace BoxID2019
{
    partial class BoxIDMainFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BoxIDMainFrm));
            this.txtBoxIDFrom = new System.Windows.Forms.TextBox();
            this.txtProductSerial = new System.Windows.Forms.TextBox();
            this.txtInvoice = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpRegDate = new System.Windows.Forms.DateTimePicker();
            this.dtpShipDate = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvBoxID = new System.Windows.Forms.DataGridView();
            this.lbUsername = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rdbProductSerial = new System.Windows.Forms.RadioButton();
            this.rdbBoxIDFrom = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmbModel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddBoxId = new System.Windows.Forms.Button();
            this.pnlBarcode = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlInvoice = new System.Windows.Forms.Panel();
            this.btnUpInvoice = new System.Windows.Forms.Button();
            this.btnEditShipping = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoxID)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlInvoice.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBoxIDFrom
            // 
            this.txtBoxIDFrom.Location = new System.Drawing.Point(122, 10);
            this.txtBoxIDFrom.Name = "txtBoxIDFrom";
            this.txtBoxIDFrom.Size = new System.Drawing.Size(146, 20);
            this.txtBoxIDFrom.TabIndex = 1;
            this.txtBoxIDFrom.TextChanged += new System.EventHandler(this.txtBoxIDFrom_TextChanged);
            // 
            // txtProductSerial
            // 
            this.txtProductSerial.Location = new System.Drawing.Point(122, 45);
            this.txtProductSerial.Name = "txtProductSerial";
            this.txtProductSerial.Size = new System.Drawing.Size(146, 20);
            this.txtProductSerial.TabIndex = 2;
            // 
            // txtInvoice
            // 
            this.txtInvoice.Location = new System.Drawing.Point(122, 140);
            this.txtInvoice.Name = "txtInvoice";
            this.txtInvoice.Size = new System.Drawing.Size(146, 20);
            this.txtInvoice.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Registration Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "OQC Ship Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Invoice";
            // 
            // dtpRegDate
            // 
            this.dtpRegDate.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dtpRegDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRegDate.Location = new System.Drawing.Point(122, 75);
            this.dtpRegDate.Name = "dtpRegDate";
            this.dtpRegDate.ShowCheckBox = true;
            this.dtpRegDate.ShowUpDown = true;
            this.dtpRegDate.Size = new System.Drawing.Size(146, 20);
            this.dtpRegDate.TabIndex = 9;
            // 
            // dtpShipDate
            // 
            this.dtpShipDate.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dtpShipDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpShipDate.Location = new System.Drawing.Point(122, 106);
            this.dtpShipDate.Name = "dtpShipDate";
            this.dtpShipDate.ShowCheckBox = true;
            this.dtpShipDate.ShowUpDown = true;
            this.dtpShipDate.Size = new System.Drawing.Size(146, 20);
            this.dtpShipDate.TabIndex = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(12, 257);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgvBoxID
            // 
            this.dgvBoxID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBoxID.Location = new System.Drawing.Point(12, 286);
            this.dgvBoxID.Name = "dgvBoxID";
            this.dgvBoxID.Size = new System.Drawing.Size(517, 126);
            this.dgvBoxID.TabIndex = 12;
            this.dgvBoxID.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBoxID_CellContentClick);
            // 
            // lbUsername
            // 
            this.lbUsername.AutoSize = true;
            this.lbUsername.Location = new System.Drawing.Point(70, 10);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(33, 13);
            this.lbUsername.TabIndex = 16;
            this.lbUsername.Text = "None";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Username :";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rdbProductSerial);
            this.panel3.Controls.Add(this.rdbBoxIDFrom);
            this.panel3.Controls.Add(this.txtBoxIDFrom);
            this.panel3.Controls.Add(this.txtProductSerial);
            this.panel3.Controls.Add(this.txtInvoice);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.dtpShipDate);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.dtpRegDate);
            this.panel3.Location = new System.Drawing.Point(0, 73);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(303, 178);
            this.panel3.TabIndex = 18;
            // 
            // rdbProductSerial
            // 
            this.rdbProductSerial.AutoSize = true;
            this.rdbProductSerial.Location = new System.Drawing.Point(12, 46);
            this.rdbProductSerial.Name = "rdbProductSerial";
            this.rdbProductSerial.Size = new System.Drawing.Size(91, 17);
            this.rdbProductSerial.TabIndex = 12;
            this.rdbProductSerial.Text = "Product Serial";
            this.rdbProductSerial.UseVisualStyleBackColor = true;
            // 
            // rdbBoxIDFrom
            // 
            this.rdbBoxIDFrom.AutoSize = true;
            this.rdbBoxIDFrom.Checked = true;
            this.rdbBoxIDFrom.Location = new System.Drawing.Point(12, 11);
            this.rdbBoxIDFrom.Name = "rdbBoxIDFrom";
            this.rdbBoxIDFrom.Size = new System.Drawing.Size(83, 17);
            this.rdbBoxIDFrom.TabIndex = 11;
            this.rdbBoxIDFrom.TabStop = true;
            this.rdbBoxIDFrom.Text = "Box ID From";
            this.rdbBoxIDFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdbBoxIDFrom.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.cmbModel);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.lbUsername);
            this.panel4.Location = new System.Drawing.Point(325, 76);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(204, 72);
            this.panel4.TabIndex = 19;
            // 
            // cmbModel
            // 
            this.cmbModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModel.FormattingEnabled = true;
            this.cmbModel.Location = new System.Drawing.Point(64, 39);
            this.cmbModel.Name = "cmbModel";
            this.cmbModel.Size = new System.Drawing.Size(129, 21);
            this.cmbModel.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Model :";
            // 
            // btnAddBoxId
            // 
            this.btnAddBoxId.Location = new System.Drawing.Point(93, 257);
            this.btnAddBoxId.Name = "btnAddBoxId";
            this.btnAddBoxId.Size = new System.Drawing.Size(75, 23);
            this.btnAddBoxId.TabIndex = 21;
            this.btnAddBoxId.Text = "Add Box ID";
            this.btnAddBoxId.UseVisualStyleBackColor = true;
            this.btnAddBoxId.Click += new System.EventHandler(this.btnAddBoxId_Click);
            // 
            // pnlBarcode
            // 
            this.pnlBarcode.BackColor = System.Drawing.Color.White;
            this.pnlBarcode.Location = new System.Drawing.Point(325, 154);
            this.pnlBarcode.Name = "pnlBarcode";
            this.pnlBarcode.Size = new System.Drawing.Size(204, 52);
            this.pnlBarcode.TabIndex = 22;
            this.pnlBarcode.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBarcode_Paint);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(174, 257);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 23;
            this.btnExport.Text = "Export Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(255, 257);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 24;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // pnlInvoice
            // 
            this.pnlInvoice.Controls.Add(this.btnEditShipping);
            this.pnlInvoice.Controls.Add(this.btnUpInvoice);
            this.pnlInvoice.Location = new System.Drawing.Point(325, 213);
            this.pnlInvoice.Name = "pnlInvoice";
            this.pnlInvoice.Size = new System.Drawing.Size(204, 38);
            this.pnlInvoice.TabIndex = 25;
            this.pnlInvoice.Visible = false;
            // 
            // btnUpInvoice
            // 
            this.btnUpInvoice.Location = new System.Drawing.Point(6, 7);
            this.btnUpInvoice.Name = "btnUpInvoice";
            this.btnUpInvoice.Size = new System.Drawing.Size(89, 23);
            this.btnUpInvoice.TabIndex = 0;
            this.btnUpInvoice.Text = "Update Invoice";
            this.btnUpInvoice.UseVisualStyleBackColor = true;
            this.btnUpInvoice.Click += new System.EventHandler(this.btnUpInvoice_Click);
            // 
            // btnEditShipping
            // 
            this.btnEditShipping.Location = new System.Drawing.Point(104, 7);
            this.btnEditShipping.Name = "btnEditShipping";
            this.btnEditShipping.Size = new System.Drawing.Size(89, 23);
            this.btnEditShipping.TabIndex = 1;
            this.btnEditShipping.Text = "Edit Shipping";
            this.btnEditShipping.UseVisualStyleBackColor = true;
            this.btnEditShipping.Click += new System.EventHandler(this.btnEditShipping_Click);
            // 
            // BoxIDMainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 414);
            this.Controls.Add(this.pnlInvoice);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.pnlBarcode);
            this.Controls.Add(this.btnAddBoxId);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.dgvBoxID);
            this.Controls.Add(this.btnSearch);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BoxIDMainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BoxID Main";
            this.Title_Name = "Box ID System";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BoxIDMainFrm_FormClosing);
            this.Load += new System.EventHandler(this.BoxIDMainFrm_Load);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.dgvBoxID, 0);
            this.Controls.SetChildIndex(this.panel3, 0);
            this.Controls.SetChildIndex(this.panel4, 0);
            this.Controls.SetChildIndex(this.btnAddBoxId, 0);
            this.Controls.SetChildIndex(this.pnlBarcode, 0);
            this.Controls.SetChildIndex(this.btnExport, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.pnlInvoice, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoxID)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.pnlInvoice.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoxIDFrom;
        private System.Windows.Forms.TextBox txtProductSerial;
        private System.Windows.Forms.TextBox txtInvoice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpRegDate;
        private System.Windows.Forms.DateTimePicker dtpShipDate;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvBoxID;
        private System.Windows.Forms.Label lbUsername;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rdbProductSerial;
        private System.Windows.Forms.RadioButton rdbBoxIDFrom;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddBoxId;
        private System.Windows.Forms.Panel pnlBarcode;
        private System.Windows.Forms.ComboBox cmbModel;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlInvoice;
        private System.Windows.Forms.Button btnEditShipping;
        private System.Windows.Forms.Button btnUpInvoice;
    }
}

