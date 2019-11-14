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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.lblCount = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDetail4257 = new System.Windows.Forms.TextBox();
            this.pnl4257 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartsLot)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPartsLot
            // 
            this.lblPartsLot.AutoSize = true;
            this.lblPartsLot.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblPartsLot.Location = new System.Drawing.Point(26, 95);
            this.lblPartsLot.Name = "lblPartsLot";
            this.lblPartsLot.Size = new System.Drawing.Size(67, 13);
            this.lblPartsLot.TabIndex = 50;
            this.lblPartsLot.Text = "Parts Lot: ";
            // 
            // lblChild
            // 
            this.lblChild.AutoSize = true;
            this.lblChild.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblChild.Location = new System.Drawing.Point(26, 62);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPartsLot.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPartsLot.Location = new System.Drawing.Point(12, 147);
            this.dgvPartsLot.MultiSelect = false;
            this.dgvPartsLot.Name = "dgvPartsLot";
            this.dgvPartsLot.ReadOnly = true;
            this.dgvPartsLot.RowTemplate.Height = 21;
            this.dgvPartsLot.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvPartsLot.Size = new System.Drawing.Size(1049, 102);
            this.dgvPartsLot.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(569, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Process: ";
            // 
            // txtProcess
            // 
            this.txtProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtProcess.Location = new System.Drawing.Point(691, 20);
            this.txtProcess.Name = "txtProcess";
            this.txtProcess.ReadOnly = true;
            this.txtProcess.Size = new System.Drawing.Size(215, 23);
            this.txtProcess.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(26, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Model: ";
            // 
            // txtModel
            // 
            this.txtModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtModel.Location = new System.Drawing.Point(131, 20);
            this.txtModel.Name = "txtModel";
            this.txtModel.ReadOnly = true;
            this.txtModel.Size = new System.Drawing.Size(372, 23);
            this.txtModel.TabIndex = 20;
            // 
            // txtCount
            // 
            this.txtCount.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtCount.Location = new System.Drawing.Point(691, 58);
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
            this.label3.Location = new System.Drawing.Point(569, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 50;
            this.label3.Text = "Today\'s PASS: ";
            // 
            // txtChild
            // 
            this.txtChild.Enabled = false;
            this.txtChild.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtChild.Location = new System.Drawing.Point(131, 54);
            this.txtChild.Name = "txtChild";
            this.txtChild.Size = new System.Drawing.Size(372, 23);
            this.txtChild.TabIndex = 2;
            this.txtChild.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChild_KeyDown);
            // 
            // txtPartsLot
            // 
            this.txtPartsLot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtPartsLot.Location = new System.Drawing.Point(131, 88);
            this.txtPartsLot.Name = "txtPartsLot";
            this.txtPartsLot.Size = new System.Drawing.Size(372, 23);
            this.txtPartsLot.TabIndex = 1;
            this.txtPartsLot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartsLot_KeyDown_1);
            // 
            // txtResultDetail
            // 
            this.txtResultDetail.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtResultDetail.Location = new System.Drawing.Point(305, 259);
            this.txtResultDetail.Multiline = true;
            this.txtResultDetail.Name = "txtResultDetail";
            this.txtResultDetail.ReadOnly = true;
            this.txtResultDetail.Size = new System.Drawing.Size(505, 76);
            this.txtResultDetail.TabIndex = 51;
            // 
            // pnlResult
            // 
            this.pnlResult.BackColor = System.Drawing.Color.White;
            this.pnlResult.Location = new System.Drawing.Point(305, 50);
            this.pnlResult.Name = "pnlResult";
            this.pnlResult.Size = new System.Drawing.Size(257, 203);
            this.pnlResult.TabIndex = 52;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(568, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 14);
            this.label4.TabIndex = 53;
            this.label4.Text = "Check Item: ";
            // 
            // txtSubAssy
            // 
            this.txtSubAssy.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtSubAssy.Location = new System.Drawing.Point(660, 36);
            this.txtSubAssy.Multiline = true;
            this.txtSubAssy.Name = "txtSubAssy";
            this.txtSubAssy.ReadOnly = true;
            this.txtSubAssy.Size = new System.Drawing.Size(150, 51);
            this.txtSubAssy.TabIndex = 54;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(577, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 14);
            this.label5.TabIndex = 55;
            this.label5.Text = "NG Count: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.Location = new System.Drawing.Point(577, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 14);
            this.label6.TabIndex = 56;
            this.label6.Text = "OK Count: ";
            // 
            // txtNgCount
            // 
            this.txtNgCount.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtNgCount.Location = new System.Drawing.Point(660, 126);
            this.txtNgCount.Name = "txtNgCount";
            this.txtNgCount.ReadOnly = true;
            this.txtNgCount.Size = new System.Drawing.Size(99, 27);
            this.txtNgCount.TabIndex = 57;
            this.txtNgCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtOkCount
            // 
            this.txtOkCount.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtOkCount.Location = new System.Drawing.Point(660, 93);
            this.txtOkCount.Name = "txtOkCount";
            this.txtOkCount.ReadOnly = true;
            this.txtOkCount.Size = new System.Drawing.Size(99, 27);
            this.txtOkCount.TabIndex = 58;
            this.txtOkCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(572, 163);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(118, 44);
            this.btnReset.TabIndex = 59;
            this.btnReset.Text = "Reset Judge";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSetZero
            // 
            this.btnSetZero.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetZero.Location = new System.Drawing.Point(572, 63);
            this.btnSetZero.Name = "btnSetZero";
            this.btnSetZero.Size = new System.Drawing.Size(76, 24);
            this.btnSetZero.TabIndex = 60;
            this.btnSetZero.Text = "Set Zero";
            this.btnSetZero.UseVisualStyleBackColor = true;
            this.btnSetZero.Click += new System.EventHandler(this.btnSetZero_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(305, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 15);
            this.label7.TabIndex = 61;
            this.label7.Text = "Judge:";
            // 
            // btnResetPartsLot
            // 
            this.btnResetPartsLot.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.btnResetPartsLot.Location = new System.Drawing.Point(796, 62);
            this.btnResetPartsLot.Name = "btnResetPartsLot";
            this.btnResetPartsLot.Size = new System.Drawing.Size(96, 24);
            this.btnResetPartsLot.TabIndex = 59;
            this.btnResetPartsLot.Text = "Reset Parts";
            this.btnResetPartsLot.UseVisualStyleBackColor = true;
            this.btnResetPartsLot.Click += new System.EventHandler(this.btnResetParts_Click);
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
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.Location = new System.Drawing.Point(8, 104);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(0, 16);
            this.lblResult.TabIndex = 64;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 16);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 255);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(213, 308);
            this.groupBox1.TabIndex = 67;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DUPLICATE CHECK";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.lblSearch);
            this.panel2.Controls.Add(this.dtpDate);
            this.panel2.Location = new System.Drawing.Point(7, 170);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 128);
            this.panel2.TabIndex = 62;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(3, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(137, 16);
            this.label10.TabIndex = 70;
            this.label10.Text = "SEARCH OUTPUT";
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(9, 46);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(0, 16);
            this.lblSearch.TabIndex = 66;
            // 
            // dtpDate
            // 
            this.dtpDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(6, 78);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(187, 21);
            this.dtpDate.TabIndex = 67;
            this.dtpDate.Value = new System.DateTime(2017, 11, 11, 0, 0, 0, 0);
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
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
            this.panel1.Size = new System.Drawing.Size(200, 135);
            this.panel1.TabIndex = 62;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtDetail4257);
            this.groupBox2.Controls.Add(this.pnl4257);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtResultDetail);
            this.groupBox2.Controls.Add(this.btnSetZero);
            this.groupBox2.Controls.Add(this.pnlResult);
            this.groupBox2.Controls.Add(this.txtSubAssy);
            this.groupBox2.Controls.Add(this.btnReset);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtOkCount);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtNgCount);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(240, 255);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(821, 347);
            this.groupBox2.TabIndex = 68;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "NTRS CHECK";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.lblPartsLot);
            this.groupBox3.Controls.Add(this.btnResetPartsLot);
            this.groupBox3.Controls.Add(this.lblChild);
            this.groupBox3.Controls.Add(this.txtPartsLot);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtChild);
            this.groupBox3.Controls.Add(this.txtCount);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtModel);
            this.groupBox3.Controls.Add(this.txtProcess);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1049, 128);
            this.groupBox3.TabIndex = 69;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "JIGQUICK";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.Location = new System.Drawing.Point(18, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 15);
            this.label9.TabIndex = 64;
            this.label9.Text = "NO42 - NO57:";
            // 
            // txtDetail4257
            // 
            this.txtDetail4257.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtDetail4257.Location = new System.Drawing.Point(18, 259);
            this.txtDetail4257.Multiline = true;
            this.txtDetail4257.Name = "txtDetail4257";
            this.txtDetail4257.ReadOnly = true;
            this.txtDetail4257.Size = new System.Drawing.Size(257, 76);
            this.txtDetail4257.TabIndex = 62;
            // 
            // pnl4257
            // 
            this.pnl4257.BackColor = System.Drawing.Color.White;
            this.pnl4257.Location = new System.Drawing.Point(18, 50);
            this.pnl4257.Name = "pnl4257";
            this.pnl4257.Size = new System.Drawing.Size(257, 203);
            this.pnl4257.TabIndex = 63;
            // 
            // frmOmni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1070, 611);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvPartsLot);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmOmni";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JigQuick + NTRS";
            this.Load += new System.EventHandler(this.frmInut_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartsLot)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDetail4257;
        private System.Windows.Forms.Panel pnl4257;
    }
}

