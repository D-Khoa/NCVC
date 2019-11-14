using MatchingNO53;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingNO56
{
    public partial class MatchingForm : Form
    {
        TfSQL tf = new TfSQL();
        string tbl_motor, tbl_sub;
        public MatchingForm()
        {
            InitializeComponent();
        }

        private void MatchingForm_Load(object sender, EventArgs e)
        {
            tf.getComboBoxData("SELECT model FROM modeltbl WHERE eolmark = 'm'", ref cmbModel);
        }

        private void txtSubLenght_TextChanged(object sender, EventArgs e)
        {
            txtSubQR.MaxLength = String.IsNullOrEmpty(txtSubLenght.Text) ? 0 : int.Parse(txtSubLenght.Text);
        }

        private void txtMotorLenght_TextChanged(object sender, EventArgs e)
        {
            txtMotorQR.MaxLength = String.IsNullOrEmpty(txtMotorLenght.Text) ? 0 : int.Parse(txtMotorLenght.Text);
            txtMotorQR.Enabled = true;
        }
        public bool MotorCheck(string motorQR)
        {
            string motor_path = @"C:\testdata\" + cmbModel.Text + @"\" + motorQR + ".log";
            return File.Exists(motor_path);
        }
        public bool SubCheck(string subQR)
        {
            string sub_path = @"C:\testdata\" + VBS.Left(cmbModel.Text, 9) + @"\" + subQR + ".log";
            return File.Exists(sub_path);
        }
        public bool NO43Check(string subQR)
        {
            string no43_path = @"\\192.168.195.99\testdata\" + VBS.Left(cmbModel.Text, 9) + @"\" + txtSubQR.Text + ".log";
            return File.Exists(no43_path);
        }
        public void Reload()
        {
            lblMotorComp.ResetText();
            lblNO43.ResetText();
            lblSub.ResetText();
            lblMotorComp.BackColor = SystemColors.Control;
            lblNO43.BackColor = SystemColors.Control;
            lblSub.BackColor = SystemColors.Control;
        }
        private void txtMotorQR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //tbl_motor = cmbModel.Text + DateTime.Today.ToString("yyyyMM");

                //string motor_chk = tf.sqlExecuteScalarString("SELECT serno FROM " + tbl_motor + " WHERE serno = '" + txtMotorQR.Text + "'");
                bool motor_chk = MotorCheck(txtMotorQR.Text);

                if (motor_chk)
                {
                    lblMotorComp.Text = "MOTOR QR DUPLICATE";
                    lblMotorComp.BackColor = Color.Red;
                    txtMotorQR.SelectAll();
                    return;
                }
                else
                {
                    lblMotorComp.Text = "MOTOR QR OK";
                    lblMotorComp.BackColor = Color.LimeGreen;
                }

                txtSubQR.Enabled = true;
                txtSubQR.Focus();
                txtSubQR.SelectAll();
            }
        }

        private void txtSubQR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //tbl_motor = cmbModel.Text + DateTime.Today.ToString("yyyyMM");
                //tbl_sub = VBS.Left(cmbModel.Text, 9) + DateTime.Today.ToString("yyyyMM");

                //string sub_chk = tf.sqlExecuteScalarString("SELECT lot FROM " + tbl_motor + " WHERE lot = '" + txtSubQR.Text + "'");
                bool sub_chk = SubCheck(txtSubQR.Text);

                //string no43_path = @"\\192.168.195.99\testdata\" + VBS.Left(cmbModel.Text, 9) + @"\" + txtSubQR.Text + ".log";
                bool no43_chk = true; //NO43Check(txtSubQR.Text); //tf.sqlExecuteScalarString("SELECT tjudge FROM " + tbl_sub + " WHERE serno = '" + txtSubQR.Text + "' ORDER BY inspectdate desc LIMIT 1");

                if (sub_chk)
                {
                    lblSub.Text = "SUB QR DUPLICATE";
                    lblSub.BackColor = Color.Red;
                    txtSubQR.SelectAll();
                    lblNO43.Text = "";
                    lblNO43.BackColor = SystemColors.Control;
                    return;
                }
                else
                {
                    lblSub.Text = "SUB QR OK";
                    lblSub.BackColor = Color.LimeGreen;

                    if (no43_chk)
                    {
                        lblNO43.Text = "NO43 PASS";
                        lblNO43.BackColor = Color.LimeGreen;
                        //tf.sqlExecuteScalarString("INSERT INTO " + tbl_motor + "(serno,lot,model,site,factory,line,process,inspectdate,tjudge,tstatus,remark) VALUES ('" + txtMotorQR.Text + "','" + txtSubQR.Text + "','" + cmbModel.Text + "','NCVC','LA20','1','NO53',now(),'0','','')");
                        ExportLogFile(txtMotorQR.Text, cmbModel.Text, txtSubQR.Text);
                        ExportDataFile(txtMotorQR.Text, cmbModel.Text, txtSubQR.Text);

                        Reload();
                    }
                    else
                    {
                        lblNO43.Text = "NO43 SKIP PROCESS";
                        lblNO43.BackColor = Color.Red;
                        txtSubQR.SelectAll();
                        return;
                    }

                    txtMotorQR.ResetText();
                    txtSubQR.ResetText();
                    txtMotorQR.Focus();
                }
            }
        }
        public void ExportLogFile(string motorQR, string model, string subQR)
        {
            string path = @"C:\testdata\" + model + "\\";
            string sub_path = @"C:\testdata\" + VBS.Left(model, 9) + "\\";
            //Log content
            string[] names = new string[] { "Date,Time, RA", DateTime.Today.ToShortDateString() + "," + DateTime.Now.ToLongTimeString() + "," + subQR };

            //Check folder
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!Directory.Exists(sub_path))
            {
                Directory.CreateDirectory(sub_path);
            }

            //Create log file
            File.WriteAllLines(path + motorQR + ".log", names);
            File.WriteAllLines(sub_path + subQR + ".log", names);
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            UnlockForm unlk = new UnlockForm();
            if (btnUnlock.Text == "Lock Limit")
            {
                txtMotorLenght.Enabled = false;
                txtSubLenght.Enabled = false;
                btnUnlock.Text = "Unlock Limit";
            }
            else
            {
                unlk.ShowDialog();
                if (UnlockForm.pass == "NCVCME")
                {
                    txtMotorLenght.Enabled = true;
                    txtSubLenght.Enabled = true;
                    btnUnlock.Text = "Lock Limit";
                }
            }
        }

        public void ExportDataFile(string motorQR, string model, string subQR)
        {
            string path = @"C:\pqm\" + model + "\\";
            //Data content
            string[] names = new string[] { DateTime.Today.ToShortDateString() + "," + model, "S/N,LotNo,Date,Time,MA_SNo.,OK/NG,TotalJudge,MeasCondition", motorQR + "," + subQR + "," + DateTime.Today.ToShortDateString() + "," + DateTime.Now.ToLongTimeString() + ",,0,0," };
            //Data next
            string[] names_af = new string[] { motorQR + "," + subQR + "," + DateTime.Today.ToShortDateString() + "," + DateTime.Now.ToLongTimeString() + ",,0,0," };

            //Check folder
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Create data file
            if (!File.Exists(path + model + DateTime.Today.ToString("yyyyMM") + ".txt"))
            {
                File.AppendAllLines(path + model + DateTime.Today.ToString("yyyyMM") + ".txt", names);
                return;
            }
            File.AppendAllLines(path + model + DateTime.Today.ToString("yyyyMM") + ".txt", names_af);
        }
    }
}
