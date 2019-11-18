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
        TfSQL SQL;
        StringBuilder command;

        public LoginFrm()
        {
            InitializeComponent();
            SQL = new TfSQL("boxidcardb");
            command = new StringBuilder();
        }

        private void LoginFrm_Load(object sender, EventArgs e)
        {
            command.Clear();
            command.Append("SELECT suser FROM s_user ORDER BY suser");
            SQL.getComboBoxData(command.ToString(), ref cmbUser);

            cmbUser.Focus();
        }

        private void LoginFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void cmbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            command.Clear();
            command.Append("SELECT pass FROM s_user WHERE suser = '").Append(cmbUser.Text).Append("'");
            string pass = SQL.sqlExecuteScalarString(command.ToString());
            command.Clear();
            command.Append("SELECT loginstatus FROM s_user WHERE suser = '").Append(cmbUser.Text).Append("'");
            bool login = SQL.sqlExecuteScalarBool(command.ToString());
            if (pass == txtPassword.Text)
            {
                if (login)
                {
                    DialogResult reply = MessageBox.Show("This user account is currently used by other user," + System.Environment.NewLine + "or the log out last time had a problem." + System.Environment.NewLine + "Do you log in with this account ?", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (reply == DialogResult.No)
                    {
                        return;
                    }
                }
                LoginStatus(true);
                BoxIDMainFrm boxIDfrm = new BoxIDMainFrm();
                CommonFrm.UserName = cmbUser.Text;
                CommonFrm.UserRole = CheckRole();
                this.Hide();
                boxIDfrm.ShowDialog();
                LoginStatus(false);
                this.Show();
                this.Invalidate();
            }
            else
                MessageBox.Show("Wrong password!!!", "Warring");
            txtPassword.Clear();
            txtPassword.Focus();
        }

        private bool LoginStatus(bool state)
        {
            command.Clear();
            command.Append("UPDATE s_user SET loginstatus=").Append(state);
            command.Append(" WHERE suser='").Append(cmbUser.Text).Append("'");
            return SQL.sqlExecuteNonQuery(command.ToString(), false);
        }

        private string CheckRole()
        {
            command.Clear();
            command.Append("SELECT user_role FROM s_user WHERE suser = '").Append(cmbUser.Text).Append("'");
            return SQL.sqlExecuteScalarString(command.ToString());
        }
    }
}
