using System;
using System.Windows.Forms;
using BOT;
using BLL;
using System.Text;

namespace Efficiency
{
    public partial class frmSection : Form
    {
        user_bll userbll = new user_bll();
        user_bot userbot = new user_bot();

        public frmSection()
        {
            InitializeComponent();
        }

        private void btnLS_Click(object sender, EventArgs e)
        {
            frmLogin Login = new frmLogin(btnLS.Text);
            this.Hide();
            Login.ShowDialog();
            this.Show();
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnPro1_Click(object sender, EventArgs e)
        {
            frmLogin Login = new frmLogin(btnPro1.Text);
            this.Hide();
            Login.ShowDialog();
            this.Show();
        }

        private void frmSection_Load(object sender, EventArgs e)
        {
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                Version deploy = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;

                StringBuilder version = new StringBuilder();
                version.Append("VERSION: ");
                //version.Append("VERSION_");
                version.Append(deploy.Major);
                version.Append("_");
                //version.Append(deploy.Minor);
                //version.Append("_");
                //version.Append(deploy.Build);
                //version.Append("_");
                version.Append(deploy.Revision);

                Version_lbl.Text = version.ToString();
            }
        }

        private void btnBPro_Click(object sender, EventArgs e)
        {
            frmLogin Login = new frmLogin(btnBPro.Text);
            this.Hide();
            Login.ShowDialog();
            this.Show();
        }
    }
}
