namespace JigQuick
{
    partial class frmOmni
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOmni));
            this.lblPartsLot = new System.Windows.Forms.Label();
            this.lblChild = new System.Windows.Forms.Label();
            this.dgvPartsLot = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProcess = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtChild = new System.Windows.Forms.TextBox();
            this.txtPartsLot = new System.Windows.Forms.TextBox();
            this.txtResultDetail = new System.Windows.Forms.TextBox();
            this.pnlResult = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSubAssy = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNgCount = new System.Windows.Forms.TextBox();
            this.txtOkCount = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSetZero = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnResetPartsLot = new System.Windows.Forms.Button();
            this.Version_lbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartsLot)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPartsLot
            // 
            this.lblPartsLot.AutoSize = true;
            this.lblPartsLot.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblPartsLot.Location = new System.Drawing.Point(33, 115);
            this.lblPartsLot.Name = "lblPartsLot";
            this.lblPartsLot.Size = new System.Drawing.Size(67, 13);
            this.lblPartsLot.TabIndex = 50;
            this.lblPartsLot.Text = "Parts Lot: ";
            // 
            // lblChild
            // 
            this.lblChild.AutoSize = true;
            this.lblChild.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblChild.Location = new System.Drawing.Point(33, 74);
            this.lblChild.Name = "lblChild";
            this.lblChild.Size = new System.Drawing.Size(42, 13);
            this.lblChild.TabIndex = 50;
            this.lblChild.Text = "Child: ";
            // 
            // dgvPartsLot
            // 
            this.dgvPartsLot.AllowUserToAddRows = false;
            this.dgvPartsLot.AllowUserToOrderColumns = true;
            this.dgvPartsLot.BackgroundColor = System.Drawing.Color.LightSkyBlue;
            this.dgvPartsLot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPartsLot.Location = new System.Drawing.Point(35, 155);
            this.dgvPartsLot.MultiSelect = false;
            this.dgvPartsLot.Name = "dgvPartsLot";
            this.dgvPartsLot.ReadOnly = true;
            this.dgvPartsLot.RowTemplate.Height = 21;
            this.dgvPartsLot.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvPartsLot.Size = new System.Drawing.Size(872, 176);
            this.dgvPartsLot.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(460, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Process: ";
            // 
            // txtProcess
            // 
            this.txtProcess.Location = new System.Drawing.Point(582, 33);
            this.txtProcess.Name = "txtProcess";
            this.txtProcess.ReadOnly = true;
            this.txtProcess.Size = new System.Drawing.Size(215, 20);
            this.txtProcess.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(33, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Model: ";
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(138, 34);
            this.txtModel.Name = "txtModel";
            this.txtModel.ReadOnly = true;
            this.txtModel.Size = new System.Drawing.Size(215, 20);
            this.txtModel.TabIndex = 20;
            // 
            // txtCount
            // 
            this.txtCount.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtCount.Location = new System.Drawing.Point(582, 99);
            this.txtCount.Name = "txtCount";
            this.txtCount.ReadOnly = true;
            this.txtCount.Size = new System.Drawing.Size(69, 34);
            this.txtCount.TabIndex = 25;
            this.txtCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(460, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 50;
            this.label3.Text = "Today\'s PASS: ";
            // 
            // txtChild
            // 
            this.txtChild.Enabled = false;
            this.txtChild.Location = new System.Drawing.Point(138, 70);
            this.txtChild.Name = "txtChild";
            this.txtChild.Size = new System.Drawing.Size(215, 20);
            this.txtChild.TabIndex = 2;
            this.txtChild.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChild_KeyDown);
            // 
            // txtPartsLot
            // 
            this.txtPartsLot.Location = new System.Drawing.Point(138, 112);
            this.txtPartsLot.Name = "txtPartsLot";
            this.txtPartsLot.Size = new System.Drawing.Size(215, 20);
            this.txtPartsLot.TabIndex = 1;
            this.txtPartsLot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartsLot_KeyDown_1);
            // 
            // txtResultDetail
            // 
            this.txtResultDetail.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtResultDetail.Location = new System.Drawing.Point(138, 612);
            this.txtResultDetail.Multiline = true;
            this.txtResultDetail.Name = "txtResultDetail";
            this.txtResultDetail.ReadOnly = true;
            this.txtResultDetail.Size = new System.Drawing.Size(689, 46);
            this.txtResultDetail.TabIndex = 51;
            // 
            // pnlResult
            // 
            this.pnlResult.BackColor = System.Drawing.Color.White;
            this.pnlResult.Location = new System.Drawing.Point(172, 368);
            this.pnlResult.Name = "pnlResult";
            this.pnlResult.Size = new System.Drawing.Size(285, 209);
            this.pnlResult.TabIndex = 52;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(490, 380);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 14);
            this.label4.TabIndex = 53;
            this.label4.Text = "Check Item: ";
            // 
            // txtSubAssy
            // 
            this.txtSubAssy.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtSubAssy.Location = new System.Drawing.Point(577, 380);
            this.txtSubAssy.Name = "txtSubAssy";
            this.txtSubAssy.ReadOnly = true;
            this.txtSubAssy.Size = new System.Drawing.Size(250, 22);
            this.txtSubAssy.TabIndex = 54;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(490, 481);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 14);
            this.label5.TabIndex = 55;
            this.label5.Text = "NG Count: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(491, 429);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 14);
            this.label6.TabIndex = 56;
            this.label6.Text = "OK Count: ";
            // 
            // txtNgCount
            // 
            this.txtNgCount.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtNgCount.Location = new System.Drawing.Point(577, 481);
            this.txtNgCount.Name = "txtNgCount";
            this.txtNgCount.ReadOnly = true;
            this.txtNgCount.Size = new System.Drawing.Size(99, 27);
            this.txtNgCount.TabIndex = 57;
            this.txtNgCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtOkCount
            // 
            this.txtOkCount.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtOkCount.Location = new System.Drawing.Point(577, 428);
            this.txtOkCount.Name = "txtOkCount";
            this.txtOkCount.ReadOnly = true;
            this.txtOkCount.Size = new System.Drawing.Size(99, 27);
            this.txtOkCount.TabIndex = 58;
            this.txtOkCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(493, 533);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(118, 44);
            this.btnReset.TabIndex = 59;
            this.btnReset.Text = "Reset Judge";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSetZero
            // 
            this.btnSetZero.Location = new System.Drawing.Point(717, 429);
            this.btnSetZero.Name = "btnSetZero";
            this.btnSetZero.Size = new System.Drawing.Size(110, 24);
            this.btnSetZero.TabIndex = 60;
            this.btnSetZero.Text = "Set Zero";
            this.btnSetZero.UseVisualStyleBackColor = true;
            this.btnSetZero.Click += new System.EventHandler(this.btnSetZero_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(93, 368);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 15);
            this.label7.TabIndex = 61;
            this.label7.Text = "Judge:";
            // 
            // btnResetPartsLot
            // 
            this.btnResetPartsLot.Location = new System.Drawing.Point(731, 112);
            this.btnResetPartsLot.Name = "btnResetPartsLot";
            this.btnResetPartsLot.Size = new System.Drawing.Size(96, 24);
            this.btnResetPartsLot.TabIndex = 59;
            this.btnResetPartsLot.Text = "Reset Parts";
            this.btnResetPartsLot.UseVisualStyleBackColor = true;
            this.btnResetPartsLot.Click += new System.EventHandler(this.btnResetParts_Click);
            // 
            // Version_lbl
            // 
            this.Version_lbl.AutoSize = true;
            this.Version_lbl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Version_lbl.Location = new System.Drawing.Point(688, 663);
            this.Version_lbl.Name = "Version_lbl";
            this.Version_lbl.Size = new System.Drawing.Size(109, 15);
            this.Version_lbl.TabIndex = 62;
            this.Version_lbl.Tag = "";
            this.Version_lbl.Text = "VERSION : 1_0_00";
            // 
            // frmOmni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(937, 689);
            this.Controls.Add(this.Version_lbl);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnSetZero);
            this.Controls.Add(this.btnResetPartsLot);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtNgCount);
            this.Controls.Add(this.txtOkCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSubAssy);
            this.Controls.Add(this.pnlResult);
            this.Controls.Add(this.txtResultDetail);
            this.Controls.Add(this.txtPartsLot);
            this.Controls.Add(this.dgvPartsLot);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.txtProcess);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtChild);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblChild);
            this.Controls.Add(this.lblPartsLot);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmOmni";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JigQuick + NTRS";
            this.Load += new System.EventHandler(this.frmInut_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartsLot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPartsLot;
        private System.Windows.Forms.Label lblChild;
        private System.Windows.Forms.DataGridView dgvPartsLot;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProcess;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtChild;
        private System.Windows.Forms.TextBox txtPartsLot;
        private System.Windows.Forms.TextBox txtResultDetail;
        private System.Windows.Forms.Panel pnlResult;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSubAssy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNgCount;
        private System.Windows.Forms.TextBox txtOkCount;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSetZero;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnResetPartsLot;
        private System.Windows.Forms.Label Version_lbl;
    }
}

