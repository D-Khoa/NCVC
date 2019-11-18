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
        string serial;
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
            cmbModel.SelectedIndex = cmbModel.Items.IndexOf(Model);
            lbUsername.Text = UserName;
            if (UserRole == "admin")
            {
                grAdmin.Visible = true;
                btnChangeLimit.Visible = true;
                btnAddProduct.Visible = true;
                btnReprint.Visible = true;
            }
        }
        //Add value to Box ID, Product Serial, Invoice
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

        //Update Box ID = Model + Date + Carton Number
        #region UPDATE BOX ID
        private void cmbModel_TextChanged(object sender, EventArgs e)
        {
            Model = cmbModel.Text;
            if (!string.IsNullOrEmpty(Model))
            {
                if (editMode)
                    txtCartonNo.Enabled = true;
                else
                    txtCartonNo.Enabled = false;
                txtProductSerial.Enabled = true;
                string[] s = Model.Split('_');
                modelTail = s[1];
            }
        }

        private void txtCartonNo_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(regdate))
                regdate = DateTime.Now.ToString("yyyyMMdd");
            txtBoxID.Text = modelTail + "-" + regdate + "-" + txtCartonNo.Text;
        }
        #endregion

        //Event when input barcode of product
        private void txtProductSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(cmbModel.Text))
                {
                    MessageBox.Show("Please choose model!", "Warring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbModel.Focus();
                    return;
                }
                txtProductSerial.Enabled = false;
                serial = txtProductSerial.Text;
            }
        }

        private void DefineTableName()
        {

        }

        private void btnRegBoxID_Click(object sender, EventArgs e)
        {

        }

    }
}
