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
        bool editMode;
        string regdate;
        string modelTail;

        public AddBoxIDFrm(bool edit_Mode)
        {
            InitializeComponent();
            editMode = edit_Mode;
            SQL = new TfSQL("boxidcardb");
            SQL2 = new TfSQL("pqmdb");
            command = new StringBuilder();
            if (editMode)
                this.Text += "Edit Mode";
            else
                this.Text += "View Mode";
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
            if (editMode)
                txtCartonNo.Enabled = true;
            else
                txtCartonNo.Enabled = false;
            txtProductSerial.Enabled = true;
            string[] s = cmbModel.Text.Split('_');
            modelTail = s[1];
        }

        private void txtCartonNo_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(regdate))
                regdate = DateTime.Now.ToString("yyyyMMdd");
            txtBoxID.Text = modelTail + "-" + regdate + "-" + txtCartonNo.Text;
        }

        private void txtProductSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtProductSerial.Enabled = false;

            }
        }

        public void UpdateControl(string boxid, string productserial, string invoice)
        {
            string[] s = boxid.Split('-');
            foreach (string model in cmbModel.Items)
            {
                if (model.Contains(s[0]))
                {
                    cmbModel.SelectedItem = model;
                    break;
                }
            }
            regdate = s[1];
            txtCartonNo.Text = s[2];
            txtInvoice.Text = invoice;
            txtProductSerial.Text = productserial;
        }

    }
}
