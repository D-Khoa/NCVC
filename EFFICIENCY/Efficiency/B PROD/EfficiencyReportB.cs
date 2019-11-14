using BLL;
using BOT;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Efficiency
{
    public partial class EfficiencyReportB : Form
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

        public EfficiencyReportB()
        {
            InitializeComponent();
            initFormEfficiencyReport();
        }

        private void initFormEfficiencyReport()
        {
            txtLine.Text = EfficiencyListFormB.m_line;
            txtModel.Text = EfficiencyListFormB.m_model;
            txtSubAssy.Text = EfficiencyListFormB.m_subAssy;
            txtShift.Text = EfficiencyListFormB.m_shift;
            txtLeader.Text = frmLogin.userName;
            lblDate.Text = DateTime.Now.ToString();
            txtDept.Text = frmLogin.place;

            eff_bot.Model = txtModel.Text;
            eff_bot.SubAssy = txtSubAssy.Text;
            txtMtp.Text = sub_ie_bll.getMTP(eff_bot);

            cmbProdStatus.DataSource = pro_bll.loadProdStatus();
            cmbProdStatus.DisplayMember = "status";
            cmbProdStatus.ValueMember = "prod_status_id";
        }

        private string getNewEff()
        {
            string subAssyNo = txtSubAssy.Text; //jljkljkl
            string modelNo = txtModel.Text; ;

            eff_bot.EffNo = modelNo;
            string effOld = eff_bll.getEffNo(eff_bot);

            DateTime dateOld = new DateTime(0);
            long numberOld = 0;
            string batchNew;

            if (effOld != string.Empty)
            {
                string[] d = effOld.Split('#');
                dateOld = DateTime.ParseExact(d[2], "yyMMdd", CultureInfo.InvariantCulture);
                numberOld = long.Parse(d[3]);
            }

            if (dateOld != DateTime.Today)
            {
                batchNew = modelNo + "#" + subAssyNo + "#" + DateTime.Today.ToString("yyMMdd") + "#" + "001";
            }
            else
            {
                batchNew = modelNo + "#" + subAssyNo + "#" + DateTime.Today.ToString("yyMMdd") + "#" + (numberOld + 1).ToString("000");
            }

            return batchNew;
        }

        private void dgvMedical_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex < dgvMedical.Rows.Count - 1)
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
            if (e.RowIndex < dgvNGItem.Rows.Count - 1)
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

        private void dgvOPQty_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex < dgvOPQty.Rows.Count - 1)
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
            if (e.RowIndex < dgvEarly.Rows.Count - 1)
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

        private void EfficiencyReport_Load(object sender, EventArgs e)
        {
            cmbProdStatus.ResetText();
            txtEfficiencyNo.Text = getNewEff();
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
                    switch (cmbOverTime.Text)
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
                    switch (cmbOverTime.Text)
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
                    switch (cmbOverTime.Text)
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

        private void cmbOverTime_SelectedIndexChanged(object sender, EventArgs e)
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
            if (txtAddTime.Text != "-")
            {
                NormalTime();
            }
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Insert Efficiency table
            eff_bot.EffNo = txtEfficiencyNo.Text;
            eff_bot.Dept = txtDept.Text;
            eff_bot.Model = txtModel.Text;
            eff_bot.SubAssy = txtSubAssy.Text;
            eff_bot.CreateDate = DateTime.Parse(lblDate.Text);
            switch (txtShift.Text)
            {
                case "3":
                    if (eff_bot.CreateDate.Hour < 7)
                    {
                        eff_bot.ActDate = eff_bot.CreateDate.AddDays(-1).ToShortDateString();
                    }
                    else eff_bot.ActDate = eff_bot.CreateDate.ToShortDateString();
                    break;
                default:
                    eff_bot.ActDate = eff_bot.CreateDate.ToShortDateString();
                    break;
            } 
            eff_bot.Shift = txtShift.Text;
            eff_bot.Line = txtLine.Text;
            eff_bot.UserName = txtLeader.Text;
            eff_bot.StatusID = int.Parse(cmbProdStatus.SelectedValue.ToString());
            eff_bot.OT = float.Parse(cmbOverTime.Text);
            eff_bot.AddTime = String.IsNullOrEmpty(txtAddTime.Text) ? 0 : double.Parse(txtAddTime.Text);
            eff_bot.PlanQty = String.IsNullOrEmpty(txtPlanQty.Text) ? 0 : double.Parse(txtPlanQty.Text);
            eff_bot.Item1_1 = String.IsNullOrEmpty(txt1_1.Text) ? 0 : double.Parse(txt1_1.Text);
            eff_bot.Item1_2 = String.IsNullOrEmpty(txt1_2.Text) ? 0 : double.Parse(txt1_2.Text);
            eff_bot.Item2_1 = String.IsNullOrEmpty(txt2_1.Text) ? 0 : double.Parse(txt2_1.Text);
            eff_bot.Item2_2 = String.IsNullOrEmpty(txt2_2.Text) ? 0 : double.Parse(txt2_2.Text);
            eff_bot.Item2_3 = String.IsNullOrEmpty(txt2_3.Text) ? 0 : double.Parse(txt2_3.Text);
            eff_bot.Item3_1 = String.IsNullOrEmpty(txt3_1.Text) ? 0 : double.Parse(txt3_1.Text);
            eff_bot.Item3_2 = String.IsNullOrEmpty(txt3_2.Text) ? 0 : double.Parse(txt3_2.Text);
            eff_bot.Item3_3 = String.IsNullOrEmpty(txt3_3.Text) ? 0 : double.Parse(txt3_3.Text);
            eff_bot.Item3_4 = String.IsNullOrEmpty(txt3_4.Text) ? 0 : double.Parse(txt3_4.Text);
            eff_bot.Input = String.IsNullOrEmpty(txtInput.Text) ? 0 : int.Parse(txtInput.Text);
            eff_bot.Output = String.IsNullOrEmpty(txtOutput.Text) ? 0 : int.Parse(txtOutput.Text);
            eff_bot.NG = String.IsNullOrEmpty(txtNG.Text) ? 0 : int.Parse(txtNG.Text);
            eff_bot.OTTime = String.IsNullOrEmpty(txtOTTime.Text) ? 0 : double.Parse(txtOTTime.Text);
            eff_bot.NormalTime = String.IsNullOrEmpty(txtNormalTime.Text) ? 0 : double.Parse(txtNormalTime.Text);
            eff_bot.PlanManPower = String.IsNullOrEmpty(txtPlanMPower.Text) ? 0 : double.Parse(txtPlanMPower.Text);
            eff_bot.Lot = txtLot.Text;
            eff_bot.Remark = txtRemark.Text;
            eff_bot.ST = String.IsNullOrEmpty(txtMtp.Text) ? 0 : double.Parse(txtMtp.Text);

            //Insert OP Time
            if (dgvOPQty.Rows.Count > 1)
            {
                op_bot.EffNo = eff_bot.EffNo;
                for (int i = 0; i < dgvOPQty.Rows.Count - 1; i++)
                {
                    op_bot.OPQty = String.IsNullOrEmpty(dgvOPQty.Rows[i].Cells["col_op_no"].Value.ToString()) ? 0 : int.Parse(dgvOPQty.Rows[i].Cells["col_op_no"].Value.ToString());
                    op_bot.OPTime = String.IsNullOrEmpty(dgvOPQty.Rows[i].Cells["col_min"].Value.ToString()) ? 0 : int.Parse(dgvOPQty.Rows[i].Cells["col_min"].Value.ToString());
                    op_bll.insertOPTime(op_bot);
                }
            }

            //Insert OP Early Time
            if (dgvEarly.Rows.Count > 1)
            {
                op_bot.EffNo = eff_bot.EffNo;
                for (int i = 0; i < dgvEarly.Rows.Count - 1; i++)
                {
                    op_bot.NoiDung = String.IsNullOrEmpty(dgvEarly.Rows[i].Cells["col_noidung"].Value.ToString()) ? "" : dgvEarly.Rows[i].Cells["col_noidung"].Value.ToString();
                    op_bot.OPQtyE = String.IsNullOrEmpty(dgvEarly.Rows[i].Cells["col_e_op"].Value.ToString()) ? 0 : int.Parse(dgvEarly.Rows[i].Cells["col_e_op"].Value.ToString());
                    op_bot.OPTimeE = String.IsNullOrEmpty(dgvEarly.Rows[i].Cells["col_e_min"].Value.ToString()) ? 0 : int.Parse(dgvEarly.Rows[i].Cells["col_e_min"].Value.ToString());
                    op_bll.insertOPEarly(op_bot);
                }
            }

            //Insert Medical
            if (dgvMedical.Rows.Count > 1)
            {
                med_bot.EffNo = eff_bot.EffNo;
                for (int i = 0; i < dgvMedical.Rows.Count - 1; i++)
                {
                    med_bot.OPIdNo = dgvMedical.Rows[i].Cells["colOPno"].Value.ToString();
                    med_bot.InTime = dgvMedical.Rows[i].Cells["colFrom"].Value.ToString();
                    med_bot.OutTime = dgvMedical.Rows[i].Cells["colTo"].Value.ToString();
                    med_bot.MedTime = dgvMedical.Rows[i].Cells["colTotal"].Value.ToString();
                    med_bll.inserMedical(med_bot);
                }
            }

            //Insert NG
            if (dgvNGItem.Rows.Count > 1)
            {
                ng_bot.EffNo = eff_bot.EffNo;
                for (int i = 0; i < dgvNGItem.Rows.Count - 1; i++)
                {
                    ng_bot.NGItem = dgvNGItem.Rows[i].Cells["ng_item"].Value.ToString();
                    ng_bot.NGBefore = String.IsNullOrEmpty(dgvNGItem.Rows[i].Cells["ng_before"].Value.ToString()) ? 0 : double.Parse(dgvNGItem.Rows[i].Cells["ng_before"].Value.ToString());
                    ng_bot.NGAfter = String.IsNullOrEmpty(dgvNGItem.Rows[i].Cells["ng_after"].Value.ToString()) ? 0 : double.Parse(dgvNGItem.Rows[i].Cells["ng_after"].Value.ToString());
                    ng_bot.NGRate = String.IsNullOrEmpty(dgvNGItem.Rows[i].Cells["ng_rate"].Value.ToString()) ? 0 : double.Parse(dgvNGItem.Rows[i].Cells["ng_rate"].Value.ToString());
                    ng_bll.insertNG(ng_bot);
                }
            }

            MessageBox.Show(eff_bll.insertEfficiency(eff_bot), "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnSave.Enabled = false;
        }

        private void dgvNGItem_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNGItem.CurrentCell.ColumnIndex == 2)
            {
                dgvNGItem.CurrentRow.Cells["ng_rate"].Value = ((String.IsNullOrEmpty(dgvNGItem.CurrentRow.Cells["ng_after"].Value.ToString()) ? 0 : double.Parse(dgvNGItem.CurrentRow.Cells["ng_after"].Value.ToString())) / (String.IsNullOrEmpty(txtInput.Text) ? 0 : double.Parse(txtInput.Text)) * 100).ToString();
            }
        }

        private void txtTotalMin_TextChanged(object sender, EventArgs e)
        {
            if (txtTotalMin.Text == "-")
            {
                return;
            }
            else
            {
                txtAddTime.Text = txtTotalMin.Text;
            }
        }

        private void dgvMedical_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMedical.CurrentCell.ColumnIndex == 2)
            {
                dgvMedical.CurrentRow.Cells["colTotal"].Value = DateTime.Parse(dgvMedical.CurrentRow.Cells["colTo"].Value.ToString()) - DateTime.Parse(dgvMedical.CurrentRow.Cells["colFrom"].Value.ToString());
            }
        }

        private void txtOTTime_TextChanged(object sender, EventArgs e)
        {
            txtTTLTime.Text = ((String.IsNullOrEmpty(txtNormalTime.Text) ? 0 : double.Parse(txtNormalTime.Text)) + (String.IsNullOrEmpty(txtOTTime.Text) ? 0 : double.Parse(txtOTTime.Text))).ToString("0.###");
        }

        private void dgvEarly_EndEdit()
        {
            tEarly = 0;
            tNormal = 0;
            for (int i = 0; i < dgvEarly.Rows.Count - 1; i++)
            {
                //tEarly += (String.IsNullOrEmpty(dgvEarly.Rows[i].Cells["col_e_min"].Value.ToString()) ? 0 : double.Parse(dgvEarly.Rows[i].Cells["col_e_min"].Value.ToString()));
                tNormal += (String.IsNullOrEmpty(dgvEarly.Rows[i].Cells["col_e_min"].Value.ToString()) ? 0 : double.Parse(dgvEarly.Rows[i].Cells["col_e_min"].Value.ToString())) * (String.IsNullOrEmpty(dgvEarly.Rows[i].Cells["col_e_op"].Value.ToString()) ? 0 : double.Parse(dgvEarly.Rows[i].Cells["col_e_op"].Value.ToString()));
            }
            txtEarlySum.Text = tNormal.ToString();
        }

        private void dgvEarly_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEarly.CurrentCell.ColumnIndex == 2)
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
            for (int i = 0; i < dgvOPQty.Rows.Count - 1; i++)
            {
                tOP += (String.IsNullOrEmpty(dgvOPQty.Rows[i].Cells["col_op_no"].Value.ToString()) ? 0 : double.Parse(dgvOPQty.Rows[i].Cells["col_op_no"].Value.ToString()));
                if (txtShift.Text == "3")
                {
                    OT += ((String.IsNullOrEmpty(dgvOPQty.Rows[i].Cells["col_min"].Value.ToString()) ? 0 : double.Parse(dgvOPQty.Rows[i].Cells["col_min"].Value.ToString())) - 425) * (String.IsNullOrEmpty(dgvOPQty.Rows[i].Cells["col_op_no"].Value.ToString()) ? 0 : double.Parse(dgvOPQty.Rows[i].Cells["col_op_no"].Value.ToString()));
                }
                else
                {
                    OT += ((String.IsNullOrEmpty(dgvOPQty.Rows[i].Cells["col_min"].Value.ToString()) ? 0 : double.Parse(dgvOPQty.Rows[i].Cells["col_min"].Value.ToString())) - 435) * (String.IsNullOrEmpty(dgvOPQty.Rows[i].Cells["col_op_no"].Value.ToString()) ? 0 : double.Parse(dgvOPQty.Rows[i].Cells["col_op_no"].Value.ToString()));
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

        private void dgvOPQty_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dgvOPQty_EndEdit();
        }

        private void dgvEarly_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dgvEarly_EndEdit();
        }
    }
}