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

        TfSQL SQL;
        StringBuilder command;
        DataTable dtBoxPackage;
        bool editMode;
        int limit;
        int OKcount;
        string serial;
        string regdate;
        string modelTail;
        string processThisMonthTbl, processLastMonthTbl, dataThisMonthTbl, dataLastMonthTbl;

        public AddBoxIDFrm(bool edit_Mode)
        {
            InitializeComponent();
            editMode = edit_Mode;
            SQL = new TfSQL("boxidcardb");
            dtBoxPackage = new DataTable();
            command = new StringBuilder();
            OKcount = 0;
            if (editMode)
                this.Text += "[Edit Mode]";
            else
                this.Text += "[View Mode]";
        }

        private void AddBoxIDFrm_Load(object sender, EventArgs e)
        {
            command.Clear();
            command.Append("SELECT model FROM tbl_model_box_limit ORDER BY model");
            SQL.getComboBoxData(command.ToString(), ref cmbModel);
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
            command.Clear();
            command.Append("SELECT box_limit FROM tbl_model_box_limit WHERE model ='").Append(Model).Append("'");
            limit = int.Parse(SQL.sqlExecuteScalarString(command.ToString()));
        }

        private void txtCartonNo_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(regdate))
                regdate = DateTime.Now.ToString("yyyyMMdd");
            txtBoxID.Text = modelTail + "-" + regdate + "-" + txtCartonNo.Text;
        }
        #endregion

        //Change and set limit each model
        #region CHANGE LIMIT
        private void btnChangeLimit_Click(object sender, EventArgs e)
        {
            if (btnChangeLimit.Text == "Change")
            {
                btnChangeLimit.Text = "Apply";
                txtChangeLimit.Visible = true;
                txtChangeLimit.Text = limit.ToString();
            }
            else
            {
                if (MessageBox.Show("Do you want change limit of this model?", "Change limit", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    limit = int.Parse(txtChangeLimit.Text);
                    command.Clear();
                    command.Append("Update tbl_model_box_limit set box_limit ='").Append(limit).Append("' ");
                    command.Append("Where model ='").Append(Model).Append("'");
                    SQL.sqlExecuteNonQuery(command.ToString(), true);
                }
                btnChangeLimit.Text = "Change";
                txtChangeLimit.Visible = false;
            }
            lbOKCount.Text = OKcount + "/" + limit;
        }

        private void txtChangeLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please enter numbers only!", "Warring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private void DefineTableName(DateTime date)
        {
            processThisMonthTbl = Model + date.ToString("yyyyMM");
            dataThisMonthTbl = processThisMonthTbl + "data";
            processLastMonthTbl = Model + date.AddMonths(-1).ToString("yyyyMM");
            dataLastMonthTbl = processLastMonthTbl + "data";
        }

        private void btnRegBoxID_Click(object sender, EventArgs e)
        {
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = @"Excel documents (*.xlsx)|*.xlsx|
                          Excel 97-2003 documents (*.xls)|*.xls|
                          All file (*.*)|*.*";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                ExcelClass excel = new ExcelClass(sf.FileName);
                DataTable dt = new DataTable();
                excel.DatagridviewToDatatable(dgvBoxPackage, ref dt);
                excel.CreateWorkBook();
                excel.AddDatatable(dt);
                excel.SaveAndExit();
                MessageBox.Show("Export to Excel completed!", "Excel Class", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (dtBoxPackage.Rows.Count == 0 || !editMode)
            {
                this.Close();
            }
            else
            {
                string caution = @"The current data has not been saved."
                               + Environment.NewLine +
                               "Do you really cancel?";
                if (MessageBox.Show(caution, "Warring", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    == DialogResult.Yes)
                {
                    dtBoxPackage.Clear();
                    caution = "Temporary data has been deleted!";
                    MessageBox.Show(caution, "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                    return;
            }
        }
    }
}
