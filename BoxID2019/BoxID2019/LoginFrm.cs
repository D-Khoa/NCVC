using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxID2019
{
    public partial class LoginFrm : CommonFrm
    {
        TfSQL SQL, SQL2;
        StringBuilder command;

        public LoginFrm()
        {
            InitializeComponent();
            SQL = new TfSQL("boxidcardb");
            SQL2 = new TfSQL("pqmdb");
            command = new StringBuilder();
        }

        private void LoginFrm_Load(object sender, EventArgs e)
        {
            command.Clear();
            command.Append("SELECT model FROM tbl_model_dbplace ORDER BY model");
            SQL2.getComboBoxData(command.ToString(), ref cmbModel);

            command.Clear();
            command.Append("SELECT suser FROM s_user ORDER BY suser");
            SQL.getComboBoxData(command.ToString(), ref cmbUser);

            cmbModel.Focus();
        }

        private void cmbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbUser.Focus();
        }

        private void cmbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbModel.Text))
            {
                command.Clear();
                command.Append("SELECT pass FROM s_user WHERE suser = '").Append(cmbUser.Text).Append("'");
                if (SQL.sqlExecuteScalarString(command.ToString()) == txtPassword.Text)
                {
                    BoxIDMainFrm boxIDfrm = new BoxIDMainFrm();
                    CommonFrm.UserName = cmbUser.Text;
                    CommonFrm.Model = cmbModel.Text;
                    this.Hide();
                    boxIDfrm.ShowDialog();
                    this.Show();
                }
                else
                    MessageBox.Show("Wrong password!!!", "Warring");
                txtPassword.Clear();
                txtPassword.Focus();
            }
            else
            {
                MessageBox.Show("Must choose model!!!", "Warring");
                cmbModel.Focus();
            }
        }

        private void LoginFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Warring", MessageBoxButtons.YesNo) != DialogResult.Yes)
                e.Cancel = true;
        }

        private void LoginFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

    }
}
