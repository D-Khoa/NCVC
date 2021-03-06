namespace JigQuick
{
    partial class frmDocking
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDocking));
            this.lblPartsLot = new System.Windows.Forms.Label();
            this.lblParent = new System.Windows.Forms.Label();
            this.txtParent = new System.Windows.Forms.TextBox();
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
            this.dgvChild = new System.Windows.Forms.DataGridView();
            this.btnClerChild = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.dgvParent = new System.Windows.Forms.DataGridView();
            this.lblChild2 = new System.Windows.Forms.Label();
            this.txtChild2 = new System.Windows.Forms.TextBox();
            this.Version_lbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartsLot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChild)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParent)).BeginInit();
            this.SuspendLayout();
            // 
            // lblPartsLot
            // 
            this.lblPartsLot.AutoSize = true;
            this.lblPartsLot.Location = new System.Drawing.Point(33, 89);
            this.lblPartsLot.Name = "lblPartsLot";
            this.lblPartsLot.Size = new System.Drawing.Size(55, 13);
            this.lblPartsLot.TabIndex = 50;
            this.lblPartsLot.Text = "Parts Lot: ";
            // 
            // lblParent
            // 
            this.lblParent.AutoSize = true;
            this.lblParent.Location = new System.Drawing.Point(460, 288);
            this.lblParent.Name = "lblParent";
            this.lblParent.Size = new System.Drawing.Size(44, 13);
            this.lblParent.TabIndex = 50;
            this.lblParent.Text = "Parent: ";
            // 
            // txtParent
            // 
            this.txtParent.Location = new System.Drawing.Point(582, 285);
            this.txtParent.Name = "txtParent";
            this.txtParent.Size = new System.Drawing.Size(215, 20);
            this.txtParent.TabIndex = 3;
            this.txtParent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtParent_KeyDown);
            // 
            // lblChild
            // 
            this.lblChild.AutoSize = true;
            this.lblChild.Location = new System.Drawing.Point(33, 288);
            this.lblChild.Name = "lblChild";
            this.lblChild.Size = new System.Drawing.Size(36, 13);
            this.lblChild.TabIndex = 50;
            this.lblChild.Text = "Child: ";
            // 
            // dgvPartsLot
            // 
            this.dgvPartsLot.AllowUserToAddRows = false;
            this.dgvPartsLot.AllowUserToOrderColumns = true;
            this.dgvPartsLot.BackgroundColor = System.Drawing.Color.LightSkyBlue;
            this.dgvPartsLot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPartsLot.Location = new System.Drawing.Point(28, 130);
            this.dgvPartsLot.MultiSelect = false;
            this.dgvPartsLot.Name = "dgvPartsLot";
            this.dgvPartsLot.ReadOnly = true;
            this.dgvPartsLot.RowTemplate.Height = 21;
            this.dgvPartsLot.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvPartsLot.Size = new System.Drawing.Size(872, 136);
            this.dgvPartsLot.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(460, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Process: ";
            // 
            // txtProcess
            // 
            this.txtProcess.Location = new System.Drawing.Point(582, 37);
            this.txtProcess.Name = "txtProcess";
            this.txtProcess.ReadOnly = true;
            this.txtProcess.Size = new System.Drawing.Size(215, 20);
            this.txtProcess.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "Model: ";
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(138, 38);
            this.txtModel.Name = "txtModel";
            this.txtModel.ReadOnly = true;
            this.txtModel.Size = new System.Drawing.Size(215, 20);
            this.txtModel.TabIndex = 20;
            // 
            // txtCount
            // 
            this.txtCount.Font = new System.Drawing.Font("MS UI Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtCount.Location = new System.Drawing.Point(582, 72);
            this.txtCount.Name = "txtCount";
            this.txtCount.ReadOnly = true;
            this.txtCount.Size = new System.Drawing.Size(69, 34);
            this.txtCount.TabIndex = 25;
            this.txtCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(460, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 50;
            this.label3.Text = "Count: ";
            // 
            // txtChild
            // 
            this.txtChild.Location = new System.Drawing.Point(138, 285);
            this.txtChild.Name = "txtChild";
            this.txtChild.Size = new System.Drawing.Size(215, 20);
            this.txtChild.TabIndex = 2;
            this.txtChild.TextChanged += new System.EventHandler(this.txtChild_TextChanged);
            this.txtChild.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChild_KeyDown);
            // 
            // txtPartsLot
            // 
            this.txtPartsLot.Location = new System.Drawing.Point(138, 86);
            this.txtPartsLot.Name = "txtPartsLot";
            this.txtPartsLot.Size = new System.Drawing.Size(215, 20);
            this.txtPartsLot.TabIndex = 1;
            this.txtPartsLot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPartsLot_KeyDown_1);
            // 
            // dgvChild
            // 
            this.dgvChild.AllowUserToAddRows = false;
            this.dgvChild.AllowUserToOrderColumns = true;
            this.dgvChild.BackgroundColor = System.Drawing.Color.LightSkyBlue;
            this.dgvChild.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChild.Location = new System.Drawing.Point(138, 328);
            this.dgvChild.MultiSelect = false;
            this.dgvChild.Name = "dgvChild";
            this.dgvChild.ReadOnly = true;
            this.dgvChild.RowTemplate.Height = 21;
            this.dgvChild.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvChild.Size = new System.Drawing.Size(215, 186);
            this.dgvChild.TabIndex = 20;
            // 
            // btnClerChild
            // 
            this.btnClerChild.Location = new System.Drawing.Point(822, 490);
            this.btnClerChild.Name = "btnClerChild";
            this.btnClerChild.Size = new System.Drawing.Size(85, 24);
            this.btnClerChild.TabIndex = 52;
            this.btnClerChild.Text = "Clear";
            this.btnClerChild.UseVisualStyleBackColor = true;
            this.btnClerChild.Click += new System.EventHandler(this.btnClerChild_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(712, 83);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(85, 24);
            this.btnRegister.TabIndex = 51;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // dgvParent
            // 
            this.dgvParent.AllowUserToAddRows = false;
            this.dgvParent.AllowUserToOrderColumns = true;
            this.dgvParent.BackgroundColor = System.Drawing.Color.LightSkyBlue;
            this.dgvParent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvParent.Location = new System.Drawing.Point(582, 328);
            this.dgvParent.MultiSelect = false;
            this.dgvParent.Name = "dgvParent";
            this.dgvParent.ReadOnly = true;
            this.dgvParent.RowTemplate.Height = 21;
            this.dgvParent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvParent.Size = new System.Drawing.Size(215, 186);
            this.dgvParent.TabIndex = 20;
            // 
            // lblChild2
            // 
            this.lblChild2.AutoSize = true;
            this.lblChild2.Location = new System.Drawing.Point(33, 574);
            this.lblChild2.Name = "lblChild2";
            this.lblChild2.Size = new System.Drawing.Size(45, 13);
            this.lblChild2.TabIndex = 50;
            this.lblChild2.Text = "Child 2: ";
            // 
            // txtChild2
            // 
            this.txtChild2.Location = new System.Drawing.Point(138, 571);
            this.txtChild2.Name = "txtChild2";
            this.txtChild2.Size = new System.Drawing.Size(215, 20);
            this.txtChild2.TabIndex = 4;
            this.txtChild2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtChild2_KeyDown);
            // 
            // Version_lbl
            // 
            this.Version_lbl.AutoSize = true;
            this.Version_lbl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Version_lbl.Location = new System.Drawing.Point(688, 584);
            this.Version_lbl.Name = "Version_lbl";
            this.Version_lbl.Size = new System.Drawing.Size(109, 15);
            this.Version_lbl.TabIndex = 53;
            this.Version_lbl.Tag = "";
            this.Version_lbl.Text = "VERSION : 1_0_00";
            // 
            // frmDocking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(929, 608);
            this.Controls.Add(this.Version_lbl);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnClerChild);
            this.Controls.Add(this.txtPartsLot);
            this.Controls.Add(this.dgvParent);
            this.Controls.Add(this.dgvChild);
            this.Controls.Add(this.dgvPartsLot);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.txtModel);
            this.Controls.Add(this.txtProcess);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtChild2);
            this.Controls.Add(this.txtChild);
            this.Controls.Add(this.txtParent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblChild2);
            this.Controls.Add(this.lblParent);
            this.Controls.Add(this.lblChild);
            this.Controls.Add(this.lblPartsLot);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmDocking";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JigQuick";
            this.Load += new System.EventHandler(this.frmInut_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPartsLot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChild)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvParent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPartsLot;
        private System.Windows.Forms.Label lblParent;
        private System.Windows.Forms.TextBox txtParent;
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
        private System.Windows.Forms.DataGridView dgvChild;
        private System.Windows.Forms.Button btnClerChild;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.DataGridView dgvParent;
        private System.Windows.Forms.Label lblChild2;
        private System.Windows.Forms.TextBox txtChild2;
        private System.Windows.Forms.Label Version_lbl;
    }
}

