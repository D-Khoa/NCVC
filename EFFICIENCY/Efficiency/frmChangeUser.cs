using System;
using System.Text;
using System.Windows.Forms;
using BOT;
using BLL;
using System.Data;

namespace Efficiency
{
    public partial class frmChangeUser : Form
    {
        user_bll userbll = new user_bll();
        user_bot userbot = new user_bot();

        public frmChangeUser()
        {
            InitializeComponent();
            userbot.place = frmLogin.place;
            cmbUserName.DataSource = userbll.loadUserID(userbot);
            cmbUserName.DisplayMember = "user_cd";
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnChange.PerformClick();
            }
        }

        private void frmChangeUser_Load(object sender, EventArgs e)
        {
            //cmbUserName.DisplayMember = "user_cd";
            cmbUserName.ResetText();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            userbot.place = txtSection.Text;
            userbot.user_cd = cmbUserName.Text;
            MessageBox.Show(userbll.UpdateSection(userbot), "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmbUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            userbot.user_cd = cmbUserName.Text;
            userbll.loadUserInfo(userbot);

            txtUserName.Text = userbot.user_name;
            txtSection.Text = userbot.place;
        }
    }
}
