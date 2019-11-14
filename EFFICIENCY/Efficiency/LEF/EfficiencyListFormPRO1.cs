using System;
using System.Drawing;
using BOT;
using BLL;
using System.Windows.Forms;
//using System.Drawing;

namespace Efficiency
{
    public partial class EfficiencyListFormPRO1 : Form
    {
        model_line_bot model_bot = new model_line_bot();
        model_line_bll model_bll = new model_line_bll();
        efficiency_bot eff_bot = new efficiency_bot();
        efficiency_bll eff_bll = new efficiency_bll();
        user_bot usr_bot = new user_bot();
        user_bll usr_bll = new user_bll();

        public static string m_model, m_line, m_subAssy, title, m_shift;
        public static string effNo, createDate, Shift, User, Check, Approve;
        public int time = 0;

        public EfficiencyListFormPRO1()
        {
            InitializeComponent();
            //initFormEffList();
        }

        private void dgvEfficiencyList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnView.Enabled = true;
            if (String.IsNullOrEmpty(dgvEfficiencyList.CurrentRow.Cells["col_check"].Value.ToString()) && frmLogin.permission == "1" || !String.IsNullOrEmpty(dgvEfficiencyList.CurrentRow.Cells["col_check"].Value.ToString()) && frmLogin.flagAdminUser == true || String.IsNullOrEmpty(dgvEfficiencyList.CurrentRow.Cells["col_check"].Value.ToString()) && frmLogin.permission == "user")
            {
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
            }
            else if (frmLogin.flagAdminUser == true)
            {
                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
            }
            else
            {
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
            }

            if (frmLogin.permission == "user") btnDelete.Enabled = false;
            if (!String.IsNullOrEmpty(dgvEfficiencyList.CurrentRow.Cells["col_check"].Value.ToString()) && !String.IsNullOrEmpty(dgvEfficiencyList.CurrentRow.Cells["col_appr"].Value.ToString()))
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void ColorView()
        {
            for (int i = 0; i < dgvEfficiencyList.RowCount; i++)
            {
                if (!String.IsNullOrEmpty(dgvEfficiencyList.Rows[i].Cells["col_check"].Value.ToString()) && !String.IsNullOrEmpty(dgvEfficiencyList.Rows[i].Cells["col_appr"].Value.ToString()))
                {
                    dgvEfficiencyList.Rows[i].DefaultCellStyle.BackColor = Color.PaleGreen;
                    dgvEfficiencyList.Rows[i].DefaultCellStyle.ForeColor = Color.Blue;
                }
                else if (!String.IsNullOrEmpty(dgvEfficiencyList.Rows[i].Cells["col_check"].Value.ToString()))
                {
                    dgvEfficiencyList.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
                }
            }
        }

        private void EfficiencyListForm_Load(object sender, EventArgs e)
        {
            if (frmLogin.flagAdminUser == true || frmLogin.permission == "1")
            {
                btnUpdateST.Enabled = true;
                btnChangeUser.Visible = true;
            }

            if (frmLogin.flagAdminUser == true)
            {
                btnChangeUser.Visible = true;
            }

            if (frmLogin.flagAdminUser == true && String.IsNullOrEmpty(dgvEfficiencyList.CurrentRow.Cells["col_check"].Value.ToString()) && String.IsNullOrEmpty(dgvEfficiencyList.CurrentRow.Cells["col_appr"].Value.ToString()))
            {
                btnEdit.Enabled = true;
            }
            
            ColorView();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            grbChangePass.Visible = false;
            btnChangePass.Text = "Change Password";
        }

        private void cmbSubAssy_SelectedIndexChanged(object sender, EventArgs e)
        {
            eff_bot.SubAssy = cmbSubAssy.Text;
            txtSubAssyName.Text = eff_bll.GetSubAssyName(eff_bot);
        }

        private void btnUpdateST_Click(object sender, EventArgs e)
        {
            EfficiencyAddST add_st = new EfficiencyAddST();
            add_st.ShowDialog();
        }

        private void dgvEfficiencyList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex < dgvEfficiencyList.Rows.Count)
            {
                string strRowNumber = (e.RowIndex + 1).ToString();
                //while (strRowNumber.Length < dgvEfficiencyList.RowCount.ToString().Length)
                //{
                //    strRowNumber = strRowNumber;
                //}
                SizeF Size = e.Graphics.MeasureString(strRowNumber, dgvEfficiencyList.Font);

                Brush b = SystemBrushes.ControlText;
                e.Graphics.DrawString(strRowNumber, dgvEfficiencyList.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - Size.Height) / 2));
            }
        }

        private void rdFan_CheckedChanged(object sender, EventArgs e)
        {
            grChoice.Enabled = true;
            initFormEffList("FAN");
        }

        private void rdLBT_CheckedChanged(object sender, EventArgs e)
        {
            grChoice.Enabled = true;
            initFormEffList("LBT");
        }

        private void rdSTM_CheckedChanged(object sender, EventArgs e)
        {
            grChoice.Enabled = true;
            initFormEffList("STM");
        }

        private void btnChangeUser_Click(object sender, EventArgs e)
        {
            frmChangeUser frmChgUsr = new frmChangeUser();
            frmChgUsr.ShowDialog();
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            if (btnChangePass.Text != "OK")
            {
                grbChangePass.Visible = true;
                txtUser.Text = frmLogin.userCode;
                btnChangePass.Text = "OK";
            }
            else
            {
                if (String.IsNullOrEmpty(txtNewPass.Text) || String.IsNullOrEmpty(txtConfirm.Text) || txtNewPass.Text != txtConfirm.Text)
                {
                    MessageBox.Show(Properties.Resources.llce00009, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtNewPass.Text == frmLogin.currentPassword)
                {
                    MessageBox.Show(Properties.Resources.llce00012, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                usr_bot.user_cd = txtUser.Text;
                usr_bot.oldpass = txtNewPass.Text;
                usr_bot.pass = txtConfirm.Text;
                MessageBox.Show(usr_bll.UpdatePassword(usr_bot), "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                grbChangePass.Visible = false;
                btnChangePass.Text = "Change Password";
                this.Close();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            m_line = dgvEfficiencyList.CurrentRow.Cells["col_line"].Value.ToString();
            m_model = dgvEfficiencyList.CurrentRow.Cells["col_model_no"].Value.ToString();
            m_subAssy = dgvEfficiencyList.CurrentRow.Cells["col_sub_assy"].Value.ToString();
            effNo = dgvEfficiencyList.CurrentRow.Cells["col_eff_no"].Value.ToString();
            createDate = dgvEfficiencyList.CurrentRow.Cells["col_date"].Value.ToString();
            Shift = dgvEfficiencyList.CurrentRow.Cells["col_shift"].Value.ToString();
            User = dgvEfficiencyList.CurrentRow.Cells["col_user"].Value.ToString();
            Check = dgvEfficiencyList.CurrentRow.Cells["col_check"].Value.ToString();
            title = "View";

            EfficiencyUpdatePRO1 effU = new EfficiencyUpdatePRO1();
            effU.ShowDialog();

            dgvEfficiencyList.DataSource = eff_bll.LoadEffList(eff_bot, time);
            ColorView();
        }
        
        public void initFormEffList(string plc)
        {
            model_bot.Place = plc;
            cmbModel.DataSource = model_bll.LoadModel(model_bot);
            cmbModel.DisplayMember = "model_no";
            cmbModel.ResetText();
           
            dgvEfficiencyList.DataSource = eff_bll.LoadEffList(eff_bot, time);
        }

        private void cmbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            model_bot.ModelNo = cmbModel.Text;
           
            cmbLine.DataSource = model_bll.LoadLine(model_bot);
            cmbLine.DisplayMember = "line";

            cmbSubAssy.DataSource = model_bll.LoadSubAssy(model_bot);
            cmbSubAssy.DisplayMember = "sub_assy_no";
            cmbSubAssy.ResetText();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            m_line = dgvEfficiencyList.CurrentRow.Cells["col_line"].Value.ToString();
            m_model = dgvEfficiencyList.CurrentRow.Cells["col_model_no"].Value.ToString();
            m_subAssy = dgvEfficiencyList.CurrentRow.Cells["col_sub_assy"].Value.ToString();
            effNo = dgvEfficiencyList.CurrentRow.Cells["col_eff_no"].Value.ToString();
            createDate = dgvEfficiencyList.CurrentRow.Cells["col_date"].Value.ToString();
            Shift = dgvEfficiencyList.CurrentRow.Cells["col_shift"].Value.ToString();
            User = dgvEfficiencyList.CurrentRow.Cells["col_user"].Value.ToString();
            Check = dgvEfficiencyList.CurrentRow.Cells["col_check"].Value.ToString();
            Approve = dgvEfficiencyList.CurrentRow.Cells["col_appr"].Value.ToString();
            title = "View";

            EfficiencyViewPRO1 effV = new EfficiencyViewPRO1();
            effV.ShowDialog();
            btnView.Enabled = false;

            dgvEfficiencyList.DataSource = eff_bll.LoadEffList(eff_bot, time);
            ColorView();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            eff_bot.EffNo = dgvEfficiencyList.CurrentRow.Cells["col_eff_no"].Value.ToString();
            DialogResult re = MessageBox.Show("Are you sure to delete this report ?", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (re == DialogResult.Yes)
            {
                MessageBox.Show(eff_bll.DeleteEff(eff_bot), "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else return;

            dgvEfficiencyList.DataSource = eff_bll.LoadEffList(eff_bot, time);
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (ckbModel.Checked == true)
            {
                eff_bot.Model = cmbModel.Text;
            }
            else eff_bot.Model = "";

            if (ckbSubAssy.Checked == true)
            {
                eff_bot.SubAssy = cmbSubAssy.Text;
            }
            else eff_bot.SubAssy = "";

            if (ckbLine.Checked == true)
            {
                eff_bot.Line = cmbLine.Text;
            }
            else eff_bot.Line = "";

            if (ckbDate.Checked == true)
            {
                eff_bot.CreateDate = dtpFrom.Value.Date;
                time = 0;
            }
            else time = 1;

            if (ckbShift.Checked == true)
            {
                eff_bot.Shift = cmbShift.Text;
            }
            else eff_bot.Shift = "";

            if (rdFan.Checked == true)
            {
                eff_bot.Dept = rdFan.Text;
            }
            else if (rdLBT.Checked == true)
            {
                eff_bot.Dept = rdLBT.Text;
            }
            else if (rdSTM.Checked == true)
            {
                eff_bot.Dept = rdSTM.Text;
            }

            dgvEfficiencyList.DataSource = eff_bll.LoadEffList(eff_bot, time);
            ColorView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DialogResult re = new DialogResult();
            re = MessageBox.Show("Model is " + cmbModel.Text + "\n" + "SubAssy is " + cmbSubAssy.Text + "\n" + "ARE YOU SURE TO ADD NEW REPORT ?", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (re == DialogResult.Yes)
            {
                if (!String.IsNullOrEmpty(cmbModel.Text) && !String.IsNullOrEmpty(cmbSubAssy.Text) && !String.IsNullOrEmpty(cmbLine.Text) && !String.IsNullOrEmpty(cmbShift.Text))
                {
                    m_line = cmbLine.Text;
                    m_model = cmbModel.Text;
                    m_subAssy = cmbSubAssy.Text;
                    m_shift = cmbShift.Text;
                    title = "New";

                    EfficiencyReportPRO1 effR = new EfficiencyReportPRO1();
                    effR.ShowDialog();

                    dgvEfficiencyList.DataSource = eff_bll.LoadEffList(eff_bot, time);
                    ColorView();
                }
                else MessageBox.Show(Properties.Resources.ffce00001, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}