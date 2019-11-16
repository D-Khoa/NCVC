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
    public partial class AddBoxIDFrm : CommonFrm
    {
        public delegate void RefreshEventHandler(object sender, EventArgs e);
        public event RefreshEventHandler RefreshEvent;

        TfSQL SQL, SQL2;
        StringBuilder command;

        public AddBoxIDFrm()
        {
            InitializeComponent();
            SQL = new TfSQL("boxidcardb");
            SQL2 = new TfSQL("pqmdb");
            command = new StringBuilder();
        }

        private void AddBoxIDFrm_Load(object sender, EventArgs e)
        {
            command.Clear();
            command.Append("SELECT model FROM tbl_model_dbplace ORDER BY model");
            SQL2.getComboBoxData(command.ToString(), ref cmbModel);
            lbUsername.Text = UserName;
        }

        private void cmbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCartonNo.Enabled = true;
        }

        private void txtCartonNo_TextChanged(object sender, EventArgs e)
        {
            string[] s = cmbModel.Text.Split('_');
            txtBoxID.Text = s[1] + "-" + DateTime.Now.ToString("yyyyMMdd") + "-" + txtCartonNo.Text;
        }

    }
}
