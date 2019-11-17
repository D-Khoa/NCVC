namespace BoxID2019
{
    partial class AddBoxIDFrm
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.lbLimit = new System.Windows.Forms.Label();
            this.btnChangeLimit = new System.Windows.Forms.Button();
            this.lbUsername = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbOKCount = new System.Windows.Forms.Label();
            this.txtCartonNo = new System.Windows.Forms.TextBox();
            this.txtInvoice = new System.Windows.Forms.TextBox();
            this.txtProductSerial = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbModel = new System.Windows.Forms.ComboBox();
            this.txtBoxID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnRegBoxID = new System.Windows.Forms.Button();
            this.btnReprint = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnDelProduct = new System.Windows.Forms.Button();
            this.btnDelBoxID = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvBoxPackage = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoxPackage)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.lbLimit);
            this.panel3.Controls.Add(this.btnChangeLimit);
            this.panel3.Controls.Add(this.lbUsername);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.txtCartonNo);
            this.panel3.Controls.Add(this.txtInvoice);
            this.panel3.Controls.Add(this.txtProductSerial);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.cmbModel);
            this.panel3.Controls.Add(this.txtBoxID);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Location = new System.Drawing.Point(0, 76);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(664, 111);
            this.panel3.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(270, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Limit";
            // 
            // lbLimit
            // 
            this.lbLimit.AutoSize = true;
            this.lbLimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLimit.Location = new System.Drawing.Point(334, 78);
            this.lbLimit.Name = "lbLimit";
            this.lbLimit.Size = new System.Drawing.Size(46, 17);
            this.lbLimit.TabIndex = 18;
            this.lbLimit.Text = "None";
            // 
            // btnChangeLimit
            // 
            this.btnChangeLimit.Location = new System.Drawing.Point(452, 71);
            this.btnChangeLimit.Name = "btnChangeLimit";
            this.btnChangeLimit.Size = new System.Drawing.Size(55, 26);
            this.btnChangeLimit.TabIndex = 17;
            this.btnChangeLimit.Text = "Change";
            this.btnChangeLimit.UseVisualStyleBackColor = true;
            this.btnChangeLimit.Visible = false;
            // 
            // lbUsername
            // 
            this.lbUsername.AutoSize = true;
            this.lbUsername.Location = new System.Drawing.Point(585, 13);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(33, 13);
            this.lbUsername.TabIndex = 14;
            this.lbUsername.Text = "None";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbOKCount);
            this.groupBox1.Location = new System.Drawing.Point(540, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(101, 50);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "OK Count";
            // 
            // lbOKCount
            // 
            this.lbOKCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbOKCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOKCount.Location = new System.Drawing.Point(3, 16);
            this.lbOKCount.Name = "lbOKCount";
            this.lbOKCount.Size = new System.Drawing.Size(95, 31);
            this.lbOKCount.TabIndex = 8;
            this.lbOKCount.Text = "0/0";
            this.lbOKCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCartonNo
            // 
            this.txtCartonNo.Enabled = false;
            this.txtCartonNo.Location = new System.Drawing.Point(334, 12);
            this.txtCartonNo.Name = "txtCartonNo";
            this.txtCartonNo.Size = new System.Drawing.Size(173, 20);
            this.txtCartonNo.TabIndex = 12;
            this.txtCartonNo.TextChanged += new System.EventHandler(this.txtCartonNo_TextChanged);
            // 
            // txtInvoice
            // 
            this.txtInvoice.Enabled = false;
            this.txtInvoice.Location = new System.Drawing.Point(334, 45);
            this.txtInvoice.Name = "txtInvoice";
            this.txtInvoice.Size = new System.Drawing.Size(173, 20);
            this.txtInvoice.TabIndex = 11;
            // 
            // txtProductSerial
            // 
            this.txtProductSerial.Enabled = false;
            this.txtProductSerial.Location = new System.Drawing.Point(91, 77);
            this.txtProductSerial.Name = "txtProductSerial";
            this.txtProductSerial.Size = new System.Drawing.Size(173, 20);
            this.txtProductSerial.TabIndex = 10;
            this.txtProductSerial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductSerial_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(544, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "User :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(270, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Invoice";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(270, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Carton No.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Product Serial";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Model";
            // 
            // cmbModel
            // 
            this.cmbModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModel.FormattingEnabled = true;
            this.cmbModel.Location = new System.Drawing.Point(91, 44);
            this.cmbModel.Name = "cmbModel";
            this.cmbModel.Size = new System.Drawing.Size(173, 21);
            this.cmbModel.TabIndex = 2;
            this.cmbModel.SelectedIndexChanged += new System.EventHandler(this.cmbModel_SelectedIndexChanged);
            // 
            // txtBoxID
            // 
            this.txtBoxID.Enabled = false;
            this.txtBoxID.Location = new System.Drawing.Point(91, 12);
            this.txtBoxID.Name = "txtBoxID";
            this.txtBoxID.Size = new System.Drawing.Size(173, 20);
            this.txtBoxID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Box ID";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAddProduct);
            this.groupBox2.Controls.Add(this.btnRegBoxID);
            this.groupBox2.Location = new System.Drawing.Point(0, 193);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(199, 39);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Location = new System.Drawing.Point(103, 10);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(91, 23);
            this.btnAddProduct.TabIndex = 2;
            this.btnAddProduct.Text = "Add Product";
            this.btnAddProduct.UseVisualStyleBackColor = true;
            this.btnAddProduct.Visible = false;
            // 
            // btnRegBoxID
            // 
            this.btnRegBoxID.Enabled = false;
            this.btnRegBoxID.Location = new System.Drawing.Point(6, 10);
            this.btnRegBoxID.Name = "btnRegBoxID";
            this.btnRegBoxID.Size = new System.Drawing.Size(91, 23);
            this.btnRegBoxID.TabIndex = 0;
            this.btnRegBoxID.Text = "Register Box ID";
            this.btnRegBoxID.UseVisualStyleBackColor = true;
            // 
            // btnReprint
            // 
            this.btnReprint.Location = new System.Drawing.Point(87, 9);
            this.btnReprint.Name = "btnReprint";
            this.btnReprint.Size = new System.Drawing.Size(75, 23);
            this.btnReprint.TabIndex = 1;
            this.btnReprint.Text = "Reprint";
            this.btnReprint.UseVisualStyleBackColor = true;
            this.btnReprint.Visible = false;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(6, 9);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // btnDelProduct
            // 
            this.btnDelProduct.Location = new System.Drawing.Point(6, 10);
            this.btnDelProduct.Name = "btnDelProduct";
            this.btnDelProduct.Size = new System.Drawing.Size(91, 23);
            this.btnDelProduct.TabIndex = 4;
            this.btnDelProduct.Text = "Delete Product";
            this.btnDelProduct.UseVisualStyleBackColor = true;
            this.btnDelProduct.Visible = false;
            // 
            // btnDelBoxID
            // 
            this.btnDelBoxID.Location = new System.Drawing.Point(106, 10);
            this.btnDelBoxID.Name = "btnDelBoxID";
            this.btnDelBoxID.Size = new System.Drawing.Size(91, 23);
            this.btnDelBoxID.TabIndex = 5;
            this.btnDelBoxID.Text = "Delete Box ID";
            this.btnDelBoxID.UseVisualStyleBackColor = true;
            this.btnDelBoxID.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDelProduct);
            this.groupBox3.Controls.Add(this.btnDelBoxID);
            this.groupBox3.Location = new System.Drawing.Point(461, 193);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(203, 39);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnClose);
            this.groupBox4.Controls.Add(this.btnExport);
            this.groupBox4.Controls.Add(this.btnReprint);
            this.groupBox4.Location = new System.Drawing.Point(205, 193);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(250, 39);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            // 
            // dgvBoxPackage
            // 
            this.dgvBoxPackage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBoxPackage.Location = new System.Drawing.Point(6, 238);
            this.dgvBoxPackage.Name = "dgvBoxPackage";
            this.dgvBoxPackage.Size = new System.Drawing.Size(658, 209);
            this.dgvBoxPackage.TabIndex = 8;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(168, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 24);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // AddBoxIDFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 459);
            this.Controls.Add(this.dgvBoxPackage);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel3);
            this.Name = "AddBoxIDFrm";
            this.Text = "Product Serial";
            this.Title_Name = "Box Package";
            this.Load += new System.EventHandler(this.AddBoxIDFrm_Load);
            this.Controls.SetChildIndex(this.panel3, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.groupBox3, 0);
            this.Controls.SetChildIndex(this.groupBox4, 0);
            this.Controls.SetChildIndex(this.dgvBoxPackage, 0);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBoxPackage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbModel;
        private System.Windows.Forms.Label lbOKCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtProductSerial;
        private System.Windows.Forms.Label lbUsername;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCartonNo;
        private System.Windows.Forms.TextBox txtInvoice;
        private System.Windows.Forms.Label lbLimit;
        private System.Windows.Forms.Button btnChangeLimit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Button btnReprint;
        private System.Windows.Forms.Button btnRegBoxID;
        private System.Windows.Forms.Button btnDelProduct;
        private System.Windows.Forms.Button btnDelBoxID;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgvBoxPackage;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnClose;
    }
}