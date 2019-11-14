namespace Efficiency
{
    partial class EfficiencyAddST
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EfficiencyAddST));
            this.dgvST = new System.Windows.Forms.DataGridView();
            this.col_model_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_sub_assy_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_eff_prefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_eff_period = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_eff_st = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSubAssyName = new System.Windows.Forms.TextBox();
            this.cmbSubAssy = new System.Windows.Forms.ComboBox();
            this.cmbModel = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.rdSTM = new System.Windows.Forms.RadioButton();
            this.rdLBT = new System.Windows.Forms.RadioButton();
            this.rdFan = new System.Windows.Forms.RadioButton();
            this.grGroup = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvST)).BeginInit();
            this.grGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvST
            // 
            this.dgvST.AllowUserToAddRows = false;
            this.dgvST.AllowUserToDeleteRows = false;
            this.dgvST.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvST.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_model_no,
            this.col_sub_assy_no,
            this.col_eff_prefix,
            this.col_eff_period,
            this.col_eff_st});
            this.dgvST.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvST.Location = new System.Drawing.Point(0, 119);
            this.dgvST.Name = "dgvST";
            this.dgvST.Size = new System.Drawing.Size(665, 450);
            this.dgvST.TabIndex = 0;
            this.dgvST.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvST_RowPostPaint);
            // 
            // col_model_no
            // 
            this.col_model_no.DataPropertyName = "model_no";
            this.col_model_no.HeaderText = "Model";
            this.col_model_no.Name = "col_model_no";
            // 
            // col_sub_assy_no
            // 
            this.col_sub_assy_no.DataPropertyName = "sub_assy_no";
            this.col_sub_assy_no.HeaderText = "Sub Assy";
            this.col_sub_assy_no.Name = "col_sub_assy_no";
            // 
            // col_eff_prefix
            // 
            this.col_eff_prefix.DataPropertyName = "eff_prefix";
            this.col_eff_prefix.HeaderText = "Prefix";
            this.col_eff_prefix.Name = "col_eff_prefix";
            // 
            // col_eff_period
            // 
            this.col_eff_period.DataPropertyName = "eff_period";
            this.col_eff_period.HeaderText = "Period";
            this.col_eff_period.Name = "col_eff_period";
            // 
            // col_eff_st
            // 
            this.col_eff_st.DataPropertyName = "eff_st";
            this.col_eff_st.HeaderText = "ST";
            this.col_eff_st.Name = "col_eff_st";
            // 
            // txtSubAssyName
            // 
            this.txtSubAssyName.Enabled = false;
            this.txtSubAssyName.Location = new System.Drawing.Point(92, 61);
            this.txtSubAssyName.Name = "txtSubAssyName";
            this.txtSubAssyName.Size = new System.Drawing.Size(154, 20);
            this.txtSubAssyName.TabIndex = 373;
            this.txtSubAssyName.Visible = false;
            // 
            // cmbSubAssy
            // 
            this.cmbSubAssy.FormattingEnabled = true;
            this.cmbSubAssy.Location = new System.Drawing.Point(92, 34);
            this.cmbSubAssy.Name = "cmbSubAssy";
            this.cmbSubAssy.Size = new System.Drawing.Size(154, 21);
            this.cmbSubAssy.TabIndex = 372;
            this.cmbSubAssy.Visible = false;
            this.cmbSubAssy.SelectedIndexChanged += new System.EventHandler(this.cmbSubAssy_SelectedIndexChanged);
            // 
            // cmbModel
            // 
            this.cmbModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbModel.FormattingEnabled = true;
            this.cmbModel.Location = new System.Drawing.Point(92, 7);
            this.cmbModel.Name = "cmbModel";
            this.cmbModel.Size = new System.Drawing.Size(154, 21);
            this.cmbModel.TabIndex = 371;
            this.cmbModel.SelectedIndexChanged += new System.EventHandler(this.cmbModel_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 368;
            this.label3.Text = "Sub Assy Name";
            this.label3.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 369;
            this.label5.Text = "Sub Assy No";
            this.label5.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 370;
            this.label4.Text = "Model";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(467, 74);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 37);
            this.btnUpdate.TabIndex = 375;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(386, 74);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 37);
            this.btnSearch.TabIndex = 374;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // rdSTM
            // 
            this.rdSTM.AutoSize = true;
            this.rdSTM.Location = new System.Drawing.Point(11, 58);
            this.rdSTM.Name = "rdSTM";
            this.rdSTM.Size = new System.Drawing.Size(48, 17);
            this.rdSTM.TabIndex = 378;
            this.rdSTM.TabStop = true;
            this.rdSTM.Text = "STM";
            this.rdSTM.UseVisualStyleBackColor = true;
            this.rdSTM.CheckedChanged += new System.EventHandler(this.rdSTM_CheckedChanged);
            // 
            // rdLBT
            // 
            this.rdLBT.AutoSize = true;
            this.rdLBT.Location = new System.Drawing.Point(11, 35);
            this.rdLBT.Name = "rdLBT";
            this.rdLBT.Size = new System.Drawing.Size(45, 17);
            this.rdLBT.TabIndex = 377;
            this.rdLBT.TabStop = true;
            this.rdLBT.Text = "LBT";
            this.rdLBT.UseVisualStyleBackColor = true;
            this.rdLBT.CheckedChanged += new System.EventHandler(this.rdLBT_CheckedChanged);
            // 
            // rdFan
            // 
            this.rdFan.AutoSize = true;
            this.rdFan.Location = new System.Drawing.Point(11, 12);
            this.rdFan.Name = "rdFan";
            this.rdFan.Size = new System.Drawing.Size(46, 17);
            this.rdFan.TabIndex = 376;
            this.rdFan.TabStop = true;
            this.rdFan.Text = "FAN";
            this.rdFan.UseVisualStyleBackColor = true;
            this.rdFan.CheckedChanged += new System.EventHandler(this.rdFan_CheckedChanged);
            // 
            // grGroup
            // 
            this.grGroup.Controls.Add(this.rdFan);
            this.grGroup.Controls.Add(this.rdSTM);
            this.grGroup.Controls.Add(this.rdLBT);
            this.grGroup.Location = new System.Drawing.Point(285, 1);
            this.grGroup.Name = "grGroup";
            this.grGroup.Size = new System.Drawing.Size(66, 85);
            this.grGroup.TabIndex = 379;
            this.grGroup.TabStop = false;
            this.grGroup.Visible = false;
            // 
            // EfficiencyAddST
            // 
            this.ClientSize = new System.Drawing.Size(665, 569);
            this.Controls.Add(this.grGroup);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSubAssyName);
            this.Controls.Add(this.cmbSubAssy);
            this.Controls.Add(this.cmbModel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvST);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EfficiencyAddST";
            this.Text = "Update ST";
            ((System.ComponentModel.ISupportInitialize)(this.dgvST)).EndInit();
            this.grGroup.ResumeLayout(false);
            this.grGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvST;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_model_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_sub_assy_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_eff_prefix;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_eff_period;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_eff_st;
        private System.Windows.Forms.TextBox txtSubAssyName;
        private System.Windows.Forms.ComboBox cmbSubAssy;
        private System.Windows.Forms.ComboBox cmbModel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.RadioButton rdSTM;
        private System.Windows.Forms.RadioButton rdLBT;
        private System.Windows.Forms.RadioButton rdFan;
        private System.Windows.Forms.GroupBox grGroup;
    }
}