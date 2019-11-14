﻿using BLL;
using BOT;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Efficiency
{
    public partial class EfficiencyView : Form
    {
        production_status_bll pro_bll = new production_status_bll();
        production_status_bot pro_bot = new production_status_bot();
        efficiency_bll eff_bll = new efficiency_bll();
        efficiency_bot eff_bot = new efficiency_bot();
        medical_bll med_bll = new medical_bll();
        medical_bot med_bot = new medical_bot();
        ng_item_bll ng_bll = new ng_item_bll();
        ng_item_bot ng_bot = new ng_item_bot();
        op_time_bll op_bll = new op_time_bll();
        op_time_bot op_bot = new op_time_bot();
        sub_assy_ie_bll sub_ie_bll = new sub_assy_ie_bll();
        sub_assy_ie_bot sub_ie_bot = new sub_assy_ie_bot();
        
        public EfficiencyView()
        {
            InitializeComponent();
            initFormEfficiencyView();
        }

        private void initFormEfficiencyView()
        {
            txtLine.Text = EfficiencyListForm.m_line;
            txtModel.Text = EfficiencyListForm.m_model;
            txtSubAssy.Text = EfficiencyListForm.m_subAssy;
            txtEfficiencyNo.Text = EfficiencyListForm.effNo;
            lblDate.Text = EfficiencyListForm.createDate;
            txtShift.Text = EfficiencyListForm.Shift;
            txtLeader.Text = EfficiencyListForm.User;

            eff_bot.Model = txtModel.Text;
            eff_bot.SubAssy = txtSubAssy.Text;

            DataTable dtE = new DataTable();
            defineDataTable(ref dtE);
            eff_bot.EffNo = txtEfficiencyNo.Text;
            dtE = eff_bll.loadEfficiencyInfo(eff_bot);

            txtMtp.Text = dtE.Rows[0]["eff_st"].ToString();
            txtDept.Text = dtE.Rows[0]["dept"].ToString();
            txtProdStatus.Text = dtE.Rows[0]["status"].ToString();
            txtOverTime.Text = dtE.Rows[0]["ot"].ToString();
            txtTotalMin.Text = dtE.Rows[0]["add_time"].ToString();
            txtPlanQty.Text = dtE.Rows[0]["plan_qty"].ToString();
            txt1_1.Text = dtE.Rows[0]["item1_1"].ToString();
            txt1_2.Text = dtE.Rows[0]["item1_2"].ToString();
            txt2_1.Text = dtE.Rows[0]["item2_1"].ToString();
            txt2_2.Text = dtE.Rows[0]["item2_2"].ToString();
            txt2_3.Text = dtE.Rows[0]["item2_3"].ToString();
            txt3_1.Text = dtE.Rows[0]["item3_1"].ToString();
            txt3_2.Text = dtE.Rows[0]["item3_2"].ToString();
            txt3_3.Text = dtE.Rows[0]["item3_3"].ToString();
            txt3_4.Text = dtE.Rows[0]["item3_4"].ToString();
            txtInput.Text = dtE.Rows[0]["in_qty"].ToString();
            txtOutput.Text = dtE.Rows[0]["out_qty"].ToString();
            txtNG.Text = dtE.Rows[0]["ng_qty"].ToString();
            txtLot.Text = dtE.Rows[0]["lot"].ToString();
            txtRemark.Text = dtE.Rows[0]["remark"].ToString();

            lblCheck.Text = "Checked by: " + EfficiencyListForm.Check;
            lblApprove.Text = "Approved by: " + EfficiencyListForm.Approve;
            if (!String.IsNullOrEmpty(EfficiencyListForm.Check))
            {
                btnCheck.Enabled = false;
            }

            dgvEarly.DataSource = eff_bll.loadOPEarlyQty(eff_bot);
            dgvOPQty.DataSource = eff_bll.loadOPQty(eff_bot);
            dgvMedical.DataSource = med_bll.loadMedical(eff_bot);
            dgvNGItem.DataSource = ng_bll.loadNG(eff_bot);

            dgvEarly_EndEdit();
            dgvOPQty_EndEdit();
        }

        private void defineDataTable(ref DataTable dt)
        {
            dt.Columns.Add("dept", Type.GetType("System.String"));
            dt.Columns.Add("status", Type.GetType("System.String"));
            dt.Columns.Add("ot", Type.GetType("System.String"));
            dt.Columns.Add("add_time", Type.GetType("System.String"));
            dt.Columns.Add("plan_qty", Type.GetType("System.String"));
            dt.Columns.Add("item1_1", Type.GetType("System.String"));
            dt.Columns.Add("item1_2", Type.GetType("System.String"));
            dt.Columns.Add("item2_1", Type.GetType("System.String"));
            dt.Columns.Add("item2_2", Type.GetType("System.String"));
            dt.Columns.Add("item2_3", Type.GetType("System.String"));
            dt.Columns.Add("item3_1", Type.GetType("System.String"));
            dt.Columns.Add("item3_2", Type.GetType("System.String"));
            dt.Columns.Add("item3_3", Type.GetType("System.String"));
            dt.Columns.Add("item3_4", Type.GetType("System.String"));
            dt.Columns.Add("in_qty", Type.GetType("System.String"));
            dt.Columns.Add("out_qty", Type.GetType("System.String"));
            dt.Columns.Add("ng_qty", Type.GetType("System.String"));
            dt.Columns.Add("lot", Type.GetType("System.String"));
            dt.Columns.Add("remark", Type.GetType("System.String"));
            dt.Columns.Add("eff_st", Type.GetType("System.String"));
        }

        private void dgvMedical_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex < dgvMedical.Rows.Count)
            {
                string strRowNumber = (e.RowIndex + 1).ToString();
                while (strRowNumber.Length < dgvMedical.RowCount.ToString().Length)
                {
                    strRowNumber = "0" + strRowNumber;
                }
                SizeF Size = e.Graphics.MeasureString(strRowNumber, dgvMedical.Font);

                Brush b = SystemBrushes.ControlText;
                e.Graphics.DrawString(strRowNumber, dgvMedical.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y +
                          ((e.RowBounds.Height - Size.Height) / 2));
            }
        }

        private void dgvNGItem_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex < dgvNGItem.Rows.Count)
            {
                string strRowNumber = (e.RowIndex + 1).ToString();
                while (strRowNumber.Length < dgvNGItem.RowCount.ToString().Length)
                {
                    strRowNumber = "0" + strRowNumber;
                }
                SizeF Size = e.Graphics.MeasureString(strRowNumber, dgvNGItem.Font);

                Brush b = SystemBrushes.ControlText;
                e.Graphics.DrawString(strRowNumber, dgvNGItem.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y +
                          ((e.RowBounds.Height - Size.Height) / 2));
            }
        }

        private void setTime()
        {
            switch (txtShift.Text)
            {
                case "1":
                    txtShiftOut.Text = txtShift.Text;
                    txtWorkTimeFr.Text = "6:00";
                    txtWorkTimeTo.Text = "14:00";
                    txtWorkTimeFrOut.Text = txtWorkTimeFr.Text;
                    txtWorkTimeToOut.Text = txtWorkTimeTo.Text;
                    switch (txtOverTime.Text)
                    {
                        case "0.5":
                            DateTime tBegin = Convert.ToDateTime(txtWorkTimeTo.Text);
                            DateTime overTime = tBegin.AddMinutes(30);

                            txtWorkTimeTo.Text = overTime.ToString("HH:mm");
                            txtWorkTimeToOut.Text = txtWorkTimeTo.Text;
                            txtPlanTime.Text = "455";
                            break;
                        case "1":
                            DateTime tBegin1 = Convert.ToDateTime(txtWorkTimeTo.Text);
                            DateTime overTime1 = tBegin1.AddMinutes(60);

                            txtWorkTimeTo.Text = overTime1.ToString("HH:mm");
                            txtWorkTimeToOut.Text = txtWorkTimeTo.Text;
                            txtPlanTime.Text = "485";
                            break;
                        case "1.5":
                            DateTime tBegin2 = Convert.ToDateTime(txtWorkTimeTo.Text);
                            DateTime overTime2 = tBegin2.AddMinutes(90);

                            txtWorkTimeTo.Text = overTime2.ToString("HH:mm");
                            txtWorkTimeToOut.Text = txtWorkTimeTo.Text;
                            txtPlanTime.Text = "515";
                            break;
                        case "2":
                            DateTime tBegin3 = Convert.ToDateTime(txtWorkTimeTo.Text);
                            DateTime overTime3 = tBegin3.AddMinutes(120);

                            txtWorkTimeTo.Text = overTime3.ToString("HH:mm");
                            txtWorkTimeToOut.Text = txtWorkTimeTo.Text;
                            txtPlanTime.Text = "525";
                            break;
                        case "2.5":
                            DateTime tBegin4 = Convert.ToDateTime(txtWorkTimeTo.Text);
                            DateTime overTime4 = tBegin4.AddMinutes(150);

                            txtWorkTimeTo.Text = overTime4.ToString("HH:mm");
                            txtWorkTimeToOut.Text = txtWorkTimeTo.Text;
                            txtPlanTime.Text = "555";
                            break;
                        case "3":
                            DateTime tBegin5 = Convert.ToDateTime(txtWorkTimeTo.Text);
                            DateTime overTime5 = tBegin5.AddMinutes(180);

                            txtWorkTimeTo.Text = overTime5.ToString("HH:mm");
                            txtWorkTimeToOut.Text = txtWorkTimeTo.Text;
                            txtPlanTime.Text = "585";
                            break;
                        case "3.5":
                            DateTime tBegin6 = Convert.ToDateTime(txtWorkTimeTo.Text);
                            DateTime overTime6 = tBegin6.AddMinutes(210);

                            txtWorkTimeTo.Text = overTime6.ToString("HH:mm");
                            txtWorkTimeToOut.Text = txtWorkTimeTo.Text;
                            txtPlanTime.Text = "615";
                            break;
                        case "4":
                            DateTime tBegin7 = Convert.ToDateTime(txtWorkTimeTo.Text);
                            DateTime overTime7 = tBegin7.AddMinutes(240);

                            txtWorkTimeTo.Text = overTime7.ToString("HH:mm");
                            txtWorkTimeToOut.Text = txtWorkTimeTo.Text;
                            txtPlanTime.Text = "645";
                            break;
                        default:
                            txtPlanTime.Text = "435";
                            break;
                    }
                    break;
                case "2":
                    txtShiftOut.Text = txtShift.Text;
                    txtWorkTimeFr.Text = "14:00";
                    txtWorkTimeTo.Text = "22:00";
                    txtWorkTimeFrOut.Text = txtWorkTimeFr.Text;
                    txtWorkTimeToOut.Text = txtWorkTimeTo.Text;
                    switch (txtOverTime.Text)
                    {
                        case "0.5":
                            DateTime tBegin = Convert.ToDateTime(txtWorkTimeFr.Text);
                            DateTime overTime = tBegin.AddMinutes(-30);

                            txtWorkTimeFr.Text = overTime.ToString("HH:mm");
                            txtWorkTimeFrOut.Text = txtWorkTimeFr.Text;
                            txtPlanTime.Text = "455";
                            break;
                        case "1":
                            DateTime tBegin1 = Convert.ToDateTime(txtWorkTimeFr.Text);
                            DateTime overTime1 = tBegin1.AddMinutes(-60);

                            txtWorkTimeFr.Text = overTime1.ToString("HH:mm");
                            txtWorkTimeFrOut.Text = txtWorkTimeFr.Text;
                            txtPlanTime.Text = "485";
                            break;
                        case "1.5":
                            DateTime tBegin2 = Convert.ToDateTime(txtWorkTimeFr.Text);
                            DateTime overTime2 = tBegin2.AddMinutes(-90);

                            txtWorkTimeFr.Text = overTime2.ToString("HH:mm");
                            txtWorkTimeFrOut.Text = txtWorkTimeFr.Text;
                            txtPlanTime.Text = "515";
                            break;
                        case "2":
                            DateTime tBegin3 = Convert.ToDateTime(txtWorkTimeFr.Text);
                            DateTime overTime3 = tBegin3.AddMinutes(-120);

                            txtWorkTimeFr.Text = overTime3.ToString("HH:mm");
                            txtWorkTimeFrOut.Text = txtWorkTimeFr.Text;
                            txtPlanTime.Text = "525";
                            break;
                        case "2.5":
                            DateTime tBegin4 = Convert.ToDateTime(txtWorkTimeFr.Text);
                            DateTime overTime4 = tBegin4.AddMinutes(-150);

                            txtWorkTimeFr.Text = overTime4.ToString("HH:mm");
                            txtWorkTimeFrOut.Text = txtWorkTimeFr.Text;
                            txtPlanTime.Text = "555";
                            break;
                        case "3":
                            DateTime tBegin5 = Convert.ToDateTime(txtWorkTimeFr.Text);
                            DateTime overTime5 = tBegin5.AddMinutes(-180);

                            txtWorkTimeFr.Text = overTime5.ToString("HH:mm");
                            txtWorkTimeFrOut.Text = txtWorkTimeFr.Text;
                            txtPlanTime.Text = "585";
                            break;
                        case "3.5":
                            DateTime tBegin6 = Convert.ToDateTime(txtWorkTimeFr.Text);
                            DateTime overTime6 = tBegin6.AddMinutes(-210);

                            txtWorkTimeFr.Text = overTime6.ToString("HH:mm");
                            txtWorkTimeFrOut.Text = txtWorkTimeFr.Text;
                            txtPlanTime.Text = "615";
                            break;
                        case "4":
                            DateTime tBegin7 = Convert.ToDateTime(txtWorkTimeFr.Text);
                            DateTime overTime7 = tBegin7.AddMinutes(-240);

                            txtWorkTimeFr.Text = overTime7.ToString("HH:mm");
                            txtWorkTimeFrOut.Text = txtWorkTimeFr.Text;
                            txtPlanTime.Text = "645";
                            break;
                        default:
                            txtPlanTime.Text = "435";
                            break;
                    }
                    break;
                case "3":
                    txtShiftOut.Text = txtShift.Text;
                    txtWorkTimeFr.Text = "22:00";
                    txtWorkTimeTo.Text = "6:00";
                    txtWorkTimeFrOut.Text = txtWorkTimeFr.Text;
                    txtWorkTimeToOut.Text = txtWorkTimeTo.Text;
                    switch (txtOverTime.Text)
                    {
                        case "0.5":
                            DateTime tBegin = Convert.ToDateTime(txtWorkTimeFr.Text);
                            DateTime overTime = tBegin.AddMinutes(-30);

                            txtWorkTimeFr.Text = overTime.ToString("HH:mm");
                            txtWorkTimeFrOut.Text = txtWorkTimeFr.Text;
                            txtPlanTime.Text = "445";
                            break;
                        case "1":
                            DateTime tBegin1 = Convert.ToDateTime(txtWorkTimeFr.Text);
                            DateTime overTime1 = tBegin1.AddMinutes(-60);

                            txtWorkTimeFr.Text = overTime1.ToString("HH:mm");
                            txtWorkTimeFrOut.Text = txtWorkTimeFr.Text;
                            txtPlanTime.Text = "475";
                            break;
                        case "1.5":
                            DateTime tBegin2 = Convert.ToDateTime(txtWorkTimeFr.Text);
                            DateTime overTime2 = tBegin2.AddMinutes(-90);

                            txtWorkTimeFr.Text = overTime2.ToString("HH:mm");
                            txtWorkTimeFrOut.Text = txtWorkTimeFr.Text;
                            txtPlanTime.Text = "505";
                            break;
                        case "2":
                            DateTime tBegin3 = Convert.ToDateTime(txtWorkTimeFr.Text);
                            DateTime overTime3 = tBegin3.AddMinutes(-120);

                            txtWorkTimeFr.Text = overTime3.ToString("HH:mm");
                            txtWorkTimeFrOut.Text = txtWorkTimeFr.Text;
                            txtPlanTime.Text = "515";
                            break;
                        case "2.5":
                            DateTime tBegin4 = Convert.ToDateTime(txtWorkTimeFr.Text);
                            DateTime overTime4 = tBegin4.AddMinutes(-150);

                            txtWorkTimeFr.Text = overTime4.ToString("HH:mm");
                            txtWorkTimeFrOut.Text = txtWorkTimeFr.Text;
                            txtPlanTime.Text = "545";
                            break;
                        case "3":
                            DateTime tBegin5 = Convert.ToDateTime(txtWorkTimeFr.Text);
                            DateTime overTime5 = tBegin5.AddMinutes(-180);

                            txtWorkTimeFr.Text = overTime5.ToString("HH:mm");
                            txtWorkTimeFrOut.Text = txtWorkTimeFr.Text;
                            txtPlanTime.Text = "575";
                            break;
                        case "3.5":
                            DateTime tBegin6 = Convert.ToDateTime(txtWorkTimeFr.Text);
                            DateTime overTime6 = tBegin6.AddMinutes(-210);

                            txtWorkTimeFr.Text = overTime6.ToString("HH:mm");
                            txtWorkTimeFrOut.Text = txtWorkTimeFr.Text;
                            txtPlanTime.Text = "605";
                            break;
                        case "4":
                            DateTime tBegin7 = Convert.ToDateTime(txtWorkTimeFr.Text);
                            DateTime overTime7 = tBegin7.AddMinutes(-240);

                            txtWorkTimeFr.Text = overTime7.ToString("HH:mm");
                            txtWorkTimeFrOut.Text = txtWorkTimeFr.Text;
                            txtPlanTime.Text = "635";
                            break;
                        default:
                            txtPlanTime.Text = "425";
                            break;
                    }
                    break;
            }
        }

        private void txtShift_TextChanged(object sender, EventArgs e)
        {
            setTime();
        }

        #region MPL
        private void PlanMPower()
        {
            eff_bot.Model = txtModel.Text;
            eff_bot.SubAssy = txtSubAssy.Text;
            double st = double.Parse(sub_ie_bll.getST(eff_bot));
            txtPlanMPower.Text = (st * double.Parse(txtPlanQty.Text) / int.Parse(txtPlanTime.Text)).ToString("0");
        }

        private void txtPlanQty_TextChanged(object sender, EventArgs e)
        {
            if (txtPlanQty.Text.Length > 0)
            {
                PlanMPower();
            }
        }

        private void txtPlanTime_TextChanged(object sender, EventArgs e)
        {
            if (txtPlanQty.Text.Length > 0)
            {
                PlanMPower();
            }
        }
        #endregion

        #region TTL1,2,3
        private void TTL1()
        {
            txtTTL1.Text = ((String.IsNullOrEmpty(txt1_1.Text) ? 0 : double.Parse(txt1_1.Text)) + (String.IsNullOrEmpty(txt1_2.Text) ? 0 : double.Parse(txt1_2.Text))).ToString("0.###");
        }

        private void TTL2()
        {
            txtTTL2.Text = ((String.IsNullOrEmpty(txt2_1.Text) ? 0 : double.Parse(txt2_1.Text)) + (String.IsNullOrEmpty(txt2_2.Text) ? 0 : double.Parse(txt2_2.Text)) + (String.IsNullOrEmpty(txt2_3.Text) ? 0 : double.Parse(txt2_3.Text))).ToString("0.###");
        }

        private void TTL3()
        {
            txtTTL3.Text = ((String.IsNullOrEmpty(txt3_1.Text) ? 0 : double.Parse(txt3_1.Text)) + (String.IsNullOrEmpty(txt3_2.Text) ? 0 : double.Parse(txt3_2.Text)) + (String.IsNullOrEmpty(txt3_3.Text) ? 0 : double.Parse(txt3_3.Text)) + (String.IsNullOrEmpty(txt3_4.Text) ? 0 : double.Parse(txt3_4.Text))).ToString("0.###");
        }

        private void txt1_1_TextChanged(object sender, EventArgs e)
        {
            TTL1();
        }

        private void txt1_2_TextChanged(object sender, EventArgs e)
        {
            TTL1();
        }

        private void txt2_1_TextChanged(object sender, EventArgs e)
        {
            TTL2();
        }

        private void txt2_2_TextChanged(object sender, EventArgs e)
        {
            TTL2();
        }

        private void txt2_3_TextChanged(object sender, EventArgs e)
        {
            TTL2();
        }

        private void txt3_1_TextChanged(object sender, EventArgs e)
        {
            TTL3();
        }

        private void txt3_2_TextChanged(object sender, EventArgs e)
        {
            TTL3();
        }

        private void txt3_3_TextChanged(object sender, EventArgs e)
        {
            TTL3();
        }

        private void txt3_4_TextChanged(object sender, EventArgs e)
        {
            TTL3();
        }
        #endregion

        private void NG()
        {
            txtNG.Text = ((String.IsNullOrEmpty(txtInput.Text) ? 0 : double.Parse(txtInput.Text)) - (String.IsNullOrEmpty(txtOutput.Text) ? 0 : double.Parse(txtOutput.Text))).ToString("0.###");
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            NG();
        }

        private void txtOutput_TextChanged(object sender, EventArgs e)
        {
            NG();

            Protivity();

            if (!String.IsNullOrEmpty(txtOutput.Text))
            {
                txtTotalPlanTime.Text = (double.Parse(txtOutput.Text) * double.Parse(txtMtp.Text)).ToString("0.###");
            }
        }

        #region Protivity
        private void Protivity()
        {
            double st = double.Parse(sub_ie_bll.getST(eff_bot));
            txtProtivity.Text = (st * (String.IsNullOrEmpty(txtOutput.Text) ? 0 : double.Parse(txtOutput.Text)) / ((String.IsNullOrEmpty(txtTTLTime.Text) ? 0 : double.Parse(txtTTLTime.Text)) - (String.IsNullOrEmpty(txtTTL1.Text) ? 0 : double.Parse(txtTTL1.Text)) - (String.IsNullOrEmpty(txtTTL2.Text) ? 0 : double.Parse(txtTTL2.Text))) * 100).ToString("0.##") + "%";
        }

        private void txtTTL1_TextChanged(object sender, EventArgs e)
        {
            Protivity();
        }

        private void txtTTL2_TextChanged(object sender, EventArgs e)
        {
            Protivity();
        }
        #endregion

        public double tQty, tEarly, tOP, OT, tNormal;

        #region Normal
        private void NormalTime()
        {
            if (txtShift.Text == "3")
            {
                txtNormalTime.Text = ((String.IsNullOrEmpty(txtMPLOP.Text) ? 0 : double.Parse(txtMPLOP.Text)) * 419 + (String.IsNullOrEmpty(txtAddTime.Text) ? 0 : double.Parse(txtAddTime.Text)) - tNormal).ToString("0.###");
            }
            else
            {
                txtNormalTime.Text = ((String.IsNullOrEmpty(txtMPLOP.Text) ? 0 : double.Parse(txtMPLOP.Text)) * 429 + (String.IsNullOrEmpty(txtAddTime.Text) ? 0 : double.Parse(txtAddTime.Text)) - tNormal).ToString("0.###");
            }
        }

        private void txtMPLOP_TextChanged(object sender, EventArgs e)
        {
            NormalTime();
        }

        private void txtAddTime_TextChanged(object sender, EventArgs e)
        {
            NormalTime();
        }
        #endregion

        private void txtNormalTime_TextChanged(object sender, EventArgs e)
        {
            txtTTLTime.Text = ((String.IsNullOrEmpty(txtNormalTime.Text) ? 0 : double.Parse(txtNormalTime.Text)) + (String.IsNullOrEmpty(txtOTTime.Text) ? 0 : double.Parse(txtOTTime.Text))).ToString("0.###");
        }

        #region Efficiency
        private void Efficiency()
        {
            txtEfficiency.Text = ((String.IsNullOrEmpty(txtTotalPlanTime.Text) ? 0 : double.Parse(txtTotalPlanTime.Text)) * (String.IsNullOrEmpty(txtEffStand.Text) ? 0 : double.Parse(txtEffStand.Text)) / (String.IsNullOrEmpty(txtTTLTime.Text) ? 0 : double.Parse(txtTTLTime.Text))).ToString("0.##") + "%";
        }

        private void txtTTLTime_TextChanged(object sender, EventArgs e)
        {
            Protivity();
            Efficiency();
        }

        private void txtTotalPlanTime_TextChanged(object sender, EventArgs e)
        {
            Efficiency();
        }

        private void txtEffStand_TextChanged(object sender, EventArgs e)
        {
            Efficiency();
        }
        #endregion

        private void dgvNGItem_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNGItem.CurrentCell.ColumnIndex == 2)
            {
                dgvNGItem.CurrentRow.Cells["ng_rate"].Value = (double.Parse(dgvNGItem.CurrentRow.Cells["ng_after"].Value.ToString()) / double.Parse(txtInput.Text) * 100).ToString();
            }
        }

        private void txtTotalMin_TextChanged(object sender, EventArgs e)
        {
            txtAddTime.Text = txtTotalMin.Text;
        }

        private void dgvMedical_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMedical.CurrentCell.ColumnIndex == 2)
            {
                dgvMedical.CurrentRow.Cells["colTotal"].Value = DateTime.Parse(dgvMedical.CurrentRow.Cells["colTo"].Value.ToString()) - DateTime.Parse(dgvMedical.CurrentRow.Cells["colFrom"].Value.ToString());
            }
        }

        private void txtOverTime_TextChanged(object sender, EventArgs e)
        {
            setTime();
        }

        private void txtOTTime_TextChanged(object sender, EventArgs e)
        {
            txtTTLTime.Text = ((String.IsNullOrEmpty(txtNormalTime.Text) ? 0 : double.Parse(txtNormalTime.Text)) + (String.IsNullOrEmpty(txtOTTime.Text) ? 0 : double.Parse(txtOTTime.Text))).ToString("0.###");
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            eff_bot.Check = frmLogin.userName;
            eff_bot.EffNo = txtEfficiencyNo.Text;
            eff_bll.CheckReport(eff_bot);
            lblCheck.Text = "Checked by: " + frmLogin.userName;
            btnCheck.Enabled = false;
        }

        private void EfficiencyView_Load(object sender, EventArgs e)
        {
            if (frmLogin.permission == "1" && String.IsNullOrEmpty(EfficiencyListForm.Check))
            {
                btnCheck.Enabled = true;
                btnApprove.Enabled = false;
            }
            else if (frmLogin.flagAdminUser == true && !String.IsNullOrEmpty(EfficiencyListForm.Check))
            {
                btnApprove.Enabled = true;
            }

            if (!String.IsNullOrEmpty(EfficiencyListForm.Approve))
            {
                btnApprove.Enabled = false;
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            eff_bot.Approve = frmLogin.userName;
            eff_bot.EffNo = txtEfficiencyNo.Text;
            eff_bll.ApproveReport(eff_bot);
            lblApprove.Text = "Approved by: " + frmLogin.userName;
            btnApprove.Enabled = false;
        }

        private void dgvOPQty_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex < dgvOPQty.Rows.Count)
            {
                string strRowNumber = (e.RowIndex + 1).ToString();
                while (strRowNumber.Length < dgvOPQty.RowCount.ToString().Length)
                {
                    strRowNumber = "0" + strRowNumber;
                }
                SizeF Size = e.Graphics.MeasureString(strRowNumber, dgvOPQty.Font);

                Brush b = SystemBrushes.ControlText;
                e.Graphics.DrawString(strRowNumber, dgvOPQty.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y +
                          ((e.RowBounds.Height - Size.Height) / 2));
            }
        }

        private void dgvEarly_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex < dgvEarly.Rows.Count)
            {
                string strRowNumber = (e.RowIndex + 1).ToString();
                while (strRowNumber.Length < dgvEarly.RowCount.ToString().Length)
                {
                    strRowNumber = "0" + strRowNumber;
                }
                SizeF Size = e.Graphics.MeasureString(strRowNumber, dgvEarly.Font);

                Brush b = SystemBrushes.ControlText;
                e.Graphics.DrawString(strRowNumber, dgvEarly.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y +
                          ((e.RowBounds.Height - Size.Height) / 2));
            }
        }

        private void dgvEarly_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvEarly_EndEdit();

            NormalTime();
        }

        private void dgvEarly_EndEdit()
        {
            tEarly = 0;
            tNormal = 0;
            for (int i = 0; i < dgvEarly.Rows.Count; i++)
            {
                //tEarly += double.Parse(dgvEarly.Rows[i].Cells["col_e_min"].Value.ToString());
                tNormal += double.Parse(dgvEarly.Rows[i].Cells["col_e_min"].Value.ToString()) * double.Parse(dgvEarly.Rows[i].Cells["col_e_op"].Value.ToString());
            }
            txtEarlySum.Text = tNormal.ToString();
        }

        private void dgvEarly_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEarly.CurrentCell.ColumnIndex == 1)
            {
                dgvEarly_EndEdit();

                NormalTime();
            }
        }

        private void dgvOPQty_EndEdit()
        {
            tQty = 0;
            tOP = 0;
            OT = 0;
            for (int i = 0; i <= dgvOPQty.Rows.Count - 1; i++)
            {
                tOP += double.Parse(dgvOPQty.Rows[i].Cells["col_op_no"].Value.ToString());
                if (txtShift.Text == "3")
                {
                    OT += (double.Parse(dgvOPQty.Rows[i].Cells["col_min"].Value.ToString()) - 425) * double.Parse(dgvOPQty.Rows[i].Cells["col_op_no"].Value.ToString());
                }
                else
                {
                    OT += (double.Parse(dgvOPQty.Rows[i].Cells["col_min"].Value.ToString()) - 435) * double.Parse(dgvOPQty.Rows[i].Cells["col_op_no"].Value.ToString());
                }
            }
            txtMPLOP.Text = tOP.ToString();
            txtOTTime.Text = OT.ToString();
        }

        private void dgvOPQty_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvOPQty.CurrentCell.ColumnIndex == 1)
            {
                dgvOPQty_EndEdit();
            }
        }
    }
}