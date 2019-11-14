

namespace NTRS
{
    partial class frmNTRS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNTRS));
            this.txtSubAssy = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtModuleId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOkCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSetZero = new System.Windows.Forms.Button();
            this.txtNgCount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlResult = new System.Windows.Forms.Panel();
            this.btnTodaysCount = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.txtResultDetail = new System.Windows.Forms.TextBox();
            this.grMain = new System.Windows.Forms.GroupBox();
            this.Version_lbl = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.txtOpId1 = new System.Windows.Forms.TextBox();
            this.txtOpId2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ckbOpId = new System.Windows.Forms.CheckBox();
            this.grMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSubAssy
            // 
            this.txtSubAssy.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtSubAssy.Location = new System.Drawing.Point(124, 23);
            this.txtSubAssy.Name = "txtSubAssy";
            this.txtSubAssy.ReadOnly = true;
            this.txtSubAssy.Size = new System.Drawing.Size(289, 22);
            this.txtSubAssy.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label2.Location = new System.Drawing.Point(45, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Sub Assy: ";
            // 
            // txtModuleId
            // 
            this.txtModuleId.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtModuleId.Location = new System.Drawing.Point(124, 69);
            this.txtModuleId.Name = "txtModuleId";
            this.txtModuleId.Size = new System.Drawing.Size(289, 22);
            this.txtModuleId.TabIndex = 1;
            this.txtModuleId.TextChanged += new System.EventHandler(this.txtModuleId_TextChanged);
            this.txtModuleId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtModuleId_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label3.Location = new System.Drawing.Point(45, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Module: ";
            // 
            // txtOkCount
            // 
            this.txtOkCount.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtOkCount.Location = new System.Drawing.Point(523, 19);
            this.txtOkCount.Name = "txtOkCount";
            this.txtOkCount.ReadOnly = true;
            this.txtOkCount.Size = new System.Drawing.Size(99, 27);
            this.txtOkCount.TabIndex = 10;
            this.txtOkCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label4.Location = new System.Drawing.Point(437, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "OK Count: ";
            // 
            // btnSetZero
            // 
            this.btnSetZero.Location = new System.Drawing.Point(648, 74);
            this.btnSetZero.Name = "btnSetZero";
            this.btnSetZero.Size = new System.Drawing.Size(110, 24);
            this.btnSetZero.TabIndex = 3;
            this.btnSetZero.Text = "Set Zero";
            this.btnSetZero.UseVisualStyleBackColor = true;
            this.btnSetZero.Click += new System.EventHandler(this.btnSetZero_Click);
            // 
            // txtNgCount
            // 
            this.txtNgCount.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtNgCount.Location = new System.Drawing.Point(523, 65);
            this.txtNgCount.Name = "txtNgCount";
            this.txtNgCount.ReadOnly = true;
            this.txtNgCount.Size = new System.Drawing.Size(99, 27);
            this.txtNgCount.TabIndex = 10;
            this.txtNgCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label5.Location = new System.Drawing.Point(436, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "NG Count: ";
            // 
            // pnlResult
            // 
            this.pnlResult.BackColor = System.Drawing.Color.White;
            this.pnlResult.Location = new System.Drawing.Point(257, 156);
            this.pnlResult.Name = "pnlResult";
            this.pnlResult.Size = new System.Drawing.Size(295, 277);
            this.pnlResult.TabIndex = 10;
            // 
            // btnTodaysCount
            // 
            this.btnTodaysCount.Location = new System.Drawing.Point(648, 27);
            this.btnTodaysCount.Name = "btnTodaysCount";
            this.btnTodaysCount.Size = new System.Drawing.Size(110, 24);
            this.btnTodaysCount.TabIndex = 4;
            this.btnTodaysCount.Text = "Show Today\'s";
            this.btnTodaysCount.UseVisualStyleBackColor = true;
            this.btnTodaysCount.Click += new System.EventHandler(this.btnTodaysCount_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label6.Location = new System.Drawing.Point(232, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Judge:";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(648, 120);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(110, 40);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // txtResultDetail
            // 
            this.txtResultDetail.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtResultDetail.Location = new System.Drawing.Point(45, 460);
            this.txtResultDetail.Multiline = true;
            this.txtResultDetail.Name = "txtResultDetail";
            this.txtResultDetail.ReadOnly = true;
            this.txtResultDetail.Size = new System.Drawing.Size(713, 82);
            this.txtResultDetail.TabIndex = 6;
            // 
            // grMain
            // 
            this.grMain.BackColor = System.Drawing.SystemColors.Control;
            this.grMain.Controls.Add(this.ckbOpId);
            this.grMain.Controls.Add(this.Version_lbl);
            this.grMain.Controls.Add(this.pnlResult);
            this.grMain.Controls.Add(this.label7);
            this.grMain.Controls.Add(this.label1);
            this.grMain.Controls.Add(this.label2);
            this.grMain.Controls.Add(this.txtResultDetail);
            this.grMain.Controls.Add(this.btnSetZero);
            this.grMain.Controls.Add(this.btnReset);
            this.grMain.Controls.Add(this.label3);
            this.grMain.Controls.Add(this.btnTodaysCount);
            this.grMain.Controls.Add(this.label6);
            this.grMain.Controls.Add(this.txtOkCount);
            this.grMain.Controls.Add(this.txtNgCount);
            this.grMain.Controls.Add(this.label5);
            this.grMain.Controls.Add(this.txtSubAssy);
            this.grMain.Controls.Add(this.label4);
            this.grMain.Controls.Add(this.txtOpId2);
            this.grMain.Controls.Add(this.txtOpId1);
            this.grMain.Controls.Add(this.txtModuleId);
            this.grMain.Controls.Add(this.groupBox1);
            this.grMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grMain.Location = new System.Drawing.Point(0, 0);
            this.grMain.Name = "grMain";
            this.grMain.Size = new System.Drawing.Size(805, 593);
            this.grMain.TabIndex = 11;
            this.grMain.TabStop = false;
            // 
            // Version_lbl
            // 
            this.Version_lbl.AutoSize = true;
            this.Version_lbl.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.Version_lbl.Location = new System.Drawing.Point(565, 568);
            this.Version_lbl.Name = "Version_lbl";
            this.Version_lbl.Size = new System.Drawing.Size(132, 19);
            this.Version_lbl.TabIndex = 69;
            this.Version_lbl.Tag = "";
            this.Version_lbl.Text = "VERSION : 1_00";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(583, 215);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 183);
            this.groupBox1.TabIndex = 68;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DUPLICATE CHECK";
            this.groupBox1.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.lblCount);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtProduct);
            this.panel1.Controls.Add(this.lblResult);
            this.panel1.Location = new System.Drawing.Point(7, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 148);
            this.panel1.TabIndex = 62;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ForeColor = System.Drawing.Color.Blue;
            this.lblCount.Location = new System.Drawing.Point(6, 8);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 24);
            this.lblCount.TabIndex = 65;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label8.Location = new System.Drawing.Point(8, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 13);
            this.label8.TabIndex = 63;
            this.label8.Text = "Product Serial";
            // 
            // txtProduct
            // 
            this.txtProduct.Enabled = false;
            this.txtProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.txtProduct.Location = new System.Drawing.Point(9, 74);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(187, 21);
            this.txtProduct.TabIndex = 62;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.Location = new System.Drawing.Point(8, 104);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(0, 16);
            this.lblResult.TabIndex = 64;
            // 
            // txtOpId1
            // 
            this.txtOpId1.Enabled = false;
            this.txtOpId1.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtOpId1.Location = new System.Drawing.Point(124, 152);
            this.txtOpId1.Name = "txtOpId1";
            this.txtOpId1.Size = new System.Drawing.Size(289, 22);
            this.txtOpId1.TabIndex = 1;
            this.txtOpId1.Visible = false;
            this.txtOpId1.Click += new System.EventHandler(this.txtOpId1_Click);
            this.txtOpId1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOpId_KeyDown);
            // 
            // txtOpId2
            // 
            this.txtOpId2.Enabled = false;
            this.txtOpId2.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtOpId2.Location = new System.Drawing.Point(124, 191);
            this.txtOpId2.Name = "txtOpId2";
            this.txtOpId2.Size = new System.Drawing.Size(289, 22);
            this.txtOpId2.TabIndex = 1;
            this.txtOpId2.Visible = false;
            this.txtOpId2.Click += new System.EventHandler(this.txtOpId2_Click);
            this.txtOpId2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOpId2_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.Location = new System.Drawing.Point(121, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "OP App 1: ";
            this.label1.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label7.Location = new System.Drawing.Point(123, 177);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "OP App 2: ";
            this.label7.Visible = false;
            // 
            // ckbOpId
            // 
            this.ckbOpId.AutoSize = true;
            this.ckbOpId.Location = new System.Drawing.Point(27, 156);
            this.ckbOpId.Name = "ckbOpId";
            this.ckbOpId.Size = new System.Drawing.Size(75, 17);
            this.ckbOpId.TabIndex = 70;
            this.ckbOpId.Text = "OP ID No:";
            this.ckbOpId.UseVisualStyleBackColor = true;
            this.ckbOpId.Visible = false;
            this.ckbOpId.CheckedChanged += new System.EventHandler(this.ckbOpId_CheckedChanged);
            // 
            // frmNTRS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(805, 593);
            this.Controls.Add(this.grMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmNTRS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NTRS: Product - Data Matching";
            this.Load += new System.EventHandler(this.frmModule_Load);
            this.grMain.ResumeLayout(false);
            this.grMain.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtSubAssy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtModuleId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOkCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSetZero;
        private System.Windows.Forms.TextBox txtNgCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlResult;
        private System.Windows.Forms.Button btnTodaysCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.TextBox txtResultDetail;
        private System.Windows.Forms.GroupBox grMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label Version_lbl;
        private System.Windows.Forms.CheckBox ckbOpId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOpId2;
        private System.Windows.Forms.TextBox txtOpId1;
    }
}

