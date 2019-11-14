using System;
using System.Text;
using System.Windows.Forms;
using BOT;
using BLL;

namespace Efficiency
{
    public partial class frmLogin : Form
    {
        user_bll userbll = new user_bll();
        user_bot userbot = new user_bot();
        public static bool flagAdminUser = false;
        public static string userCode;
        public static string userName;
        public static string permission;
        public static string currentPassword, place;

        public frmLogin(string section)
        {
            InitializeComponent();
            userbot.place = place = section;
            cmbUserName.DisplayMember = "user_cd";
            cmbUserName.DataSource = userbll.loadUserID(userbot);
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !cbShowPassword.Checked;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Equals("") || cmbUserName.Text.Equals(""))
            {
                MessageBox.Show(Properties.Resources.llce00004, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //userbot.user_name = cmbUserName.Text;
                userbll.DeleteZero();
                userbot.pass = txtPassword.Text;
                userbot.user_cd = userCode = cmbUserName.Text;
                if (userbll.CheckUser(userbot).Equals(0))
                {
                    MessageBox.Show(Properties.Resources.llci00002, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    flagAdminUser = userbot.admin_flag;
                    currentPassword = userbot.pass;
                    userName = userbot.user_name;
                    permission = userbot.permission;
                    switch (place)
                    {
                        case "LS":
                            EfficiencyListForm fmain = new EfficiencyListForm();
                            this.Hide();
                            fmain.ShowDialog();
                            this.Show();
                            break;
                        case "B":
                            EfficiencyListFormB fmainB = new EfficiencyListFormB();
                            this.Hide();
                            fmainB.ShowDialog();
                            this.Show();
                            break;
                        case "LEF":
                            EfficiencyListFormPRO1 fmainPRO1 = new EfficiencyListFormPRO1();
                            this.Hide();
                            fmainPRO1.ShowDialog();
                            this.Show();
                            break;
                    }
                    txtPassword.ResetText();
                    cmbUserName.ResetText();
                }
            }
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            //cmbUserName.DisplayMember = "user_cd";
            cmbUserName.ResetText();
        }

        private void cmbUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            userbot.user_cd = cmbUserName.Text;
            
            txtUserName.Text = userbll.loadUserName(userbot);
        }
    }
}
