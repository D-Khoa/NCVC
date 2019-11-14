namespace BoxIdDb
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnOpenSerchMenu = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Version_lbl = new System.Windows.Forms.Label();
            this.cmbUserName = new System.Windows.Forms.ComboBox();
            this.btnPcLogIn = new System.Windows.Forms.Button();
            this.btnOqcLogIn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(102, 87);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(147, 20);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(22, 50);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "User Name: ";
            // 
            // btnOpenSerchMenu
            // 
            this.btnOpenSerchMenu.Location = new System.Drawing.Point(76, 37);
            this.btnOpenSerchMenu.Name = "btnOpenSerchMenu";
            this.btnOpenSerchMenu.Size = new System.Drawing.Size(149, 25);
            this.btnOpenSerchMenu.TabIndex = 5;
            this.btnOpenSerchMenu.Text = "Open Search Menu";
            this.btnOpenSerchMenu.UseVisualStyleBackColor = true;
            this.btnOpenSerchMenu.Click += new System.EventHandler(this.btnOpenSerchMenu_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Version_lbl);
            this.groupBox1.Controls.Add(this.cmbUserName);
            this.groupBox1.Controls.Add(this.btnPcLogIn);
            this.groupBox1.Controls.Add(this.btnOqcLogIn);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Location = new System.Drawing.Point(12, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 224);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log in and make Box ID";
            // 
            // Version_lbl
            // 
            this.Version_lbl.AutoSize = true;
            this.Version_lbl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.Version_lbl.Location = new System.Drawing.Point(182, 204);
            this.Version_lbl.Name = "Version_lbl";
            this.Version_lbl.Size = new System.Drawing.Size(94, 15);
            this.Version_lbl.TabIndex = 72;
            this.Version_lbl.Tag = "";
            this.Version_lbl.Text = "VERSION : 1.0.0";
            // 
            // cmbUserName
            // 
            this.cmbUserName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUserName.FormattingEnabled = true;
            this.cmbUserName.Location = new System.Drawing.Point(102, 47);
            this.cmbUserName.Name = "cmbUserName";
            this.cmbUserName.Size = new System.Drawing.Size(147, 21);
            this.cmbUserName.TabIndex = 1;
            // 
            // btnPcLogIn
            // 
            this.btnPcLogIn.Location = new System.Drawing.Point(76, 174);
            this.btnPcLogIn.Name = "btnPcLogIn";
            this.btnPcLogIn.Size = new System.Drawing.Size(149, 25);
            this.btnPcLogIn.TabIndex = 4;
            this.btnPcLogIn.Text = "PC Log In";
            this.btnPcLogIn.UseVisualStyleBackColor = true;
            this.btnPcLogIn.Click += new System.EventHandler(this.btnPcLogIn_Click);
            // 
            // btnOqcLogIn
            // 
            this.btnOqcLogIn.Location = new System.Drawing.Point(76, 132);
            this.btnOqcLogIn.Name = "btnOqcLogIn";
            this.btnOqcLogIn.Size = new System.Drawing.Size(149, 25);
            this.btnOqcLogIn.TabIndex = 3;
            this.btnOqcLogIn.Text = "OQC Log In";
            this.btnOqcLogIn.UseVisualStyleBackColor = true;
            this.btnOqcLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Password: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOpenSerchMenu);
            this.groupBox2.Location = new System.Drawing.Point(12, 248);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 85);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Open search menu";
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnOqcLogIn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 342);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Box ID System";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnOpenSerchMenu;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOqcLogIn;
        private System.Windows.Forms.ComboBox cmbUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPcLogIn;
        private System.Windows.Forms.Label Version_lbl;
    }
}

