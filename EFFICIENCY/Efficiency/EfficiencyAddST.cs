using System;
using System.Drawing;
using BOT;
using BLL;
using System.Windows.Forms;
//using System.Drawing;

namespace Efficiency
{
    public partial class EfficiencyAddST : Form
    {
        model_line_bot model_bot = new model_line_bot();
        model_line_bll model_bll = new model_line_bll();
        efficiency_bot eff_bot = new efficiency_bot();
        efficiency_bll eff_bll = new efficiency_bll();
        user_bot usr_bot = new user_bot();
        user_bll usr_bll = new user_bll();
        sub_assy_ie_bot sub_ie_bot = new sub_assy_ie_bot();
        sub_assy_ie_bll sub_ie_bll = new sub_assy_ie_bll();

        public static string effNo, createDate, Shift, User, Check, Approve;

        public static string m_model, m_line, m_subAssy, title, m_shift;

        private void dgvST_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex < dgvST.Rows.Count)
            {
                string strRowNumber = (e.RowIndex + 1).ToString();
                //while (strRowNumber.Length < dgvST.RowCount.ToString().Length)
                //{
                //    strRowNumber = "0" + strRowNumber;
                //}
                SizeF Size = e.Graphics.MeasureString(strRowNumber, dgvST.Font);

                Brush b = SystemBrushes.ControlText;
                e.Graphics.DrawString(strRowNumber, dgvST.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y +
                          ((e.RowBounds.Height - Size.Height) / 2));
            }
        }

        public EfficiencyAddST()
        {
            InitializeComponent();
            initFormEffList();
        }

        public void initFormEffList()
        {
            if (frmLogin.place != "LEF")
            {
                model_bot.Place = frmLogin.place;
                cmbModel.DataSource = model_bll.LoadModel(model_bot);
                cmbModel.DisplayMember = "model_no";
                cmbModel.ResetText();

                dgvST.DataSource = sub_ie_bll.getListST(sub_ie_bot, model_bot.Place);
            }
            else
            {
                grGroup.Visible = true;
            }
        }

        public void loadEffList(string plc)
        {
            model_bot.Place = plc;
            cmbModel.DataSource = model_bll.LoadModel(model_bot);
            cmbModel.DisplayMember = "model_no";
            cmbModel.ResetText();

            dgvST.DataSource = sub_ie_bll.getListST(sub_ie_bot, model_bot.Place);
        }

        private void rdFan_CheckedChanged(object sender, EventArgs e)
        {
            loadEffList("FAN");
        }

        private void rdLBT_CheckedChanged(object sender, EventArgs e)
        {
            loadEffList("LBT");
        }

        private void rdSTM_CheckedChanged(object sender, EventArgs e)
        {
            loadEffList("STM");
        }

        private void cmbSubAssy_SelectedIndexChanged(object sender, EventArgs e)
        {
            //eff_bot.SubAssy = cmbSubAssy.Text;
            //txtSubAssyName.Text = eff_bll.GetSubAssyName(eff_bot);
        }

        private void cmbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            model_bot.ModelNo = cmbModel.Text;

            cmbSubAssy.DataSource = model_bll.LoadSubAssy(model_bot);
            cmbSubAssy.DisplayMember = "sub_assy_no";
            cmbSubAssy.ResetText();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            sub_ie_bot.Model = cmbModel.Text;
            sub_ie_bot.SubAssy = cmbSubAssy.Text;
            dgvST.DataSource = sub_ie_bll.searchListST(sub_ie_bot);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvST.Rows.Count; i++)
            {
                sub_ie_bot.SubAssy = dgvST.Rows[i].Cells["col_sub_assy_no"].Value.ToString();
                sub_ie_bot.EffPeriod = dgvST.Rows[i].Cells["col_eff_period"].Value.ToString();
                sub_ie_bot.EffST = dgvST.Rows[i].Cells["col_eff_st"].Value.ToString();
                sub_ie_bll.UpdateST(sub_ie_bot);
            }
            //btnSearch.PerformClick();
            //MessageBox.Show(Properties.Resources.llci00003, "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}