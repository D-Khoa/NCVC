using KeepDynamic.Barcode.Generator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxID2019
{
    public partial class BoxIDMainFrm : CommonFrm
    {
        TfSQL SQL, SQL2;
        DataTable dt;
        CheckBox ckbInvoice;
        CheckBox ckbShipDate;
        StringBuilder command;
        DataGridViewButtonColumn dgvButtons;
        string productTable;

        public BoxIDMainFrm()
        {
            InitializeComponent();
            dt = new DataTable();
            SQL = new TfSQL("boxidcardb");
            SQL2 = new TfSQL("pqmdb");
            command = new StringBuilder();
        }

        private void BoxIDMainFrm_Load(object sender, EventArgs e)
        {
            command.Clear();
            command.Append("SELECT model FROM tbl_model_dbplace ORDER BY model");
            SQL2.getComboBoxData(command.ToString(), ref cmbModel);
            lbUsername.Text = UserName;
            if (UserRole == "admin")
            {
                pnlAdmin.Visible = true;
            }
        }

        #region SETTING AND OPTIONS DATAGRIDVIEW
        private bool checkProductSerialTable(string db_table)
        {
            command.Clear();
            command.Append("select model from ").Append(db_table);
            command.Append("where model ='").Append(cmbModel.Text).Append("'");
            return (SQL.sqlExecuteNonQuery(command.ToString(), false));
        }

        private string getProductSerialTable()
        {
            string[] s = Model.Split('_');
            productTable = "product_serial_rt" + s[1];
            if (checkProductSerialTable("product_serial_rtcd"))
                return "product_serial_rtcd";
            else if (checkProductSerialTable("product_serial_517eb"))
                return "product_serial_517eb";
            else if (checkProductSerialTable("product_serial_rt"))
                return "product_serial_rt";
            else
                return productTable;
        }

        private void getDataIntoDatatable(ref DataTable table)
        {
            table.Clear();
            command.Clear();
            command.Append("Select a.boxid, a.suser, a.regist_date, a.shipdate, a.invoice from box_id_rt a ");
            if (rdbBoxID.Checked)
            {
                command.Append("where 1 = 1 ");
                if (!string.IsNullOrEmpty(txtBoxIDFrom.Text))
                    command.Append("and a.boxid like '").Append(txtBoxIDFrom.Text).Append("%' ");
            }
            else if (rdbProductSerial.Checked)
            {
                if (!string.IsNullOrEmpty(cmbModel.Text))
                {
                    if (!string.IsNullOrEmpty(txtProductSerial.Text))
                    {
                        command.Append("left join ").Append(getProductSerialTable()).Append(" b ");
                        command.Append("on a.boxid = b.boxid ");
                        command.Append("where b.serialno ='");
                        command.Append(txtProductSerial.Text).Append("' ");
                    }
                    else
                        command.Append("where 1 = 1 ");
                }
                else
                {
                    MessageBox.Show("Please choose model!", "Warring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbModel.Focus();
                }
            }
            if (dtpRegDate.Checked)
                command.Append("and a.regist_date between '").Append(dtpRegDate.Value).Append("' and '").Append(dtpRegDate.Value.AddDays(1)).Append("' ");
            if (dtpShipDate.Checked)
                command.Append("and a.shipdate between '").Append(dtpShipDate.Value).Append("' and '").Append(dtpShipDate.Value.AddDays(1)).Append("' ");
            command.Append("order by a.boxid");
            SQL.sqlDataAdapterFillDatatable(command.ToString(), ref table);
        }

        private void updateDataGridViews(bool addButton)
        {
            getDataIntoDatatable(ref dt);
            dgvBoxID.DataSource = dt;
            if (addButton) addButtonsDgv();
            if (dgvBoxID.Columns.Count >= 5)
            {
                addCheckBoxDgv();
            }
            dgvBoxID.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvBoxID.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dgvBoxID.Refresh();
            pnlBarcode.Refresh();
        }

        private void addButtonsDgv()
        {
            dgvButtons = new DataGridViewButtonColumn();
            dgvButtons.HeaderText = "Open BoxId";
            dgvButtons.Text = "Open";
            dgvButtons.UseColumnTextForButtonValue = true;
            dgvBoxID.Columns.Add(dgvButtons);
        }

        private void addCheckBoxDgv()
        {
            DataGridViewColumn dc = new DataGridViewColumn();
            dc.ValueType = typeof(bool);
            ckbShipDate = new CheckBox();
            //Get the column header cell bounds
            Rectangle rect1 = this.dgvBoxID.GetCellDisplayRectangle(3, -1, true);
            ckbShipDate.Size = new Size(14, 14);
            //Change the location of the CheckBox to make it stay on the header
            ckbShipDate.Location = rect1.Location;
            ckbShipDate.CheckedChanged += CkbShipDate_CheckedChanged;
            //Add the CheckBox into the DataGridView
            dc.HeaderText = "Edit Shipdate";
            dgvBoxID.Columns.Add(dc);
            //dgvBoxID.Columns.Insert(3, dc);
            dgvBoxID.Controls.Add(ckbShipDate);
            //dgvBoxID.Columns[3].HeaderText = "Edit ShipDate";

            ckbInvoice = new CheckBox();
            //Get the column header cell bounds
            Rectangle rect = this.dgvBoxID.GetCellDisplayRectangle(5, -1, true);
            ckbInvoice.Size = new Size(14, 14);
            //Change the location of the CheckBox to make it stay on the header
            ckbInvoice.Location = rect.Location;
            ckbInvoice.CheckedChanged += CkbInvoice_CheckedChanged;
            //Add the CheckBox into the DataGridView
            dc.HeaderText = "Update Invoice";
            //dgvBoxID.Columns.Insert(5, dc);
            dgvBoxID.Controls.Add(ckbInvoice);
            //dgvBoxID.Columns[5].HeaderText = "Update Invoice";
        }

        private void CkbShipDate_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgvBoxID.Rows)
            {
                if (ckbShipDate.Checked)
                {
                    if (string.IsNullOrEmpty(dr.Cells["shipdate"].Value.ToString()))
                        dr.Cells[3].Value = true;
                }
                else
                    dr.Cells[3].Value = false;
            }
        }

        private void CkbInvoice_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgvBoxID.Rows)
            {
                if (ckbShipDate.Checked)
                {
                    if (string.IsNullOrEmpty(dr.Cells["invoice"].Value.ToString()))
                        dr.Cells[5].Value = true;
                }
                else
                    dr.Cells[5].Value = false;
            }
        }
        #endregion

        //Click Search for view Box ID
        private void btnSearch_Click(object sender, EventArgs e)
        {
            updateDataGridViews(true);
        }

        private void btnAddBoxId_Click(object sender, EventArgs e)
        {
            if (TfGeneral.checkOpenFormExists("AddBoxIDFrm"))
            {
                MessageBox.Show("Please close the Box Package form or finish edit current form.", "BoxID DB", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            }
            else
            {
                AddBoxIDFrm packFrm = new AddBoxIDFrm(true);
                packFrm.RefreshEvent += delegate (object sndr, EventArgs excp)
                {
                    updateDataGridViews(false);
                    Focus();
                };
                packFrm.Show();
            }
        }

        private void dgvBoxID_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int currentRow = e.RowIndex;
            if (dgvBoxID.Columns[e.ColumnIndex] == dgvButtons && currentRow >= 0)
            {
                bool inUse = TfGeneral.checkOpenFormExists("AddBoxIDFrm");
                if (inUse)
                {
                    MessageBox.Show("Please close the other opened window", "Notice");
                    return;
                }
                string serialNo = txtProductSerial.Text;
                string boxID = dgvBoxID["boxid", currentRow].Value.ToString();
                string inVoice = dgvBoxID["invoice", currentRow].Value.ToString();
                AddBoxIDFrm viewFrm = new AddBoxIDFrm(false);
                viewFrm.UpdateControl(boxID, serialNo, inVoice);
            }
        }

        private void btnUpInvoice_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgvBoxID.Rows)
            {
                if (dr.Cells[5].Value != null && (bool)dr.Cells[5].Value == true
                    && string.IsNullOrEmpty(dr.Cells["invoice"].Value.ToString()))
                {
                    command.Clear();
                    command.Append("UPDATE box_id_rt SET invoice = '").Append(txtInvoice.Text);
                    command.Append("WHERE boxid = '").Append(txtBoxIDFrom.Text).Append("'");
                    SQL.sqlExecuteNonQuery(command.ToString(), false);
                }
            }
            MessageBox.Show("Updated Invoice!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            updateDataGridViews(false);
        }

        private void btnEditShipping_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dgvBoxID.Rows)
            {
                if (dr.Cells[3].Value != null && (bool)dr.Cells[3].Value == true
                    && string.IsNullOrEmpty(dr.Cells["shipdate"].Value.ToString()))
                {
                    command.Clear();
                    command.Append("UPDATE box_id_rt SET shipdate = '").Append(dtpShipDate.Value.ToString());
                    command.Append("WHERE boxid = '").Append(txtBoxIDFrom.Text).Append("'");
                    SQL.sqlExecuteNonQuery(command.ToString(), false);
                }
            }
            MessageBox.Show("Updated Ship Date!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            updateDataGridViews(false);
        }

        private void btnAddModel_Click(object sender, EventArgs e)
        {
            AddModelFrm addModelfrm = new AddModelFrm();
            addModelfrm.ShowDialog();
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
                excel.DatagridviewToDatatable(dgvBoxID, ref dt);
                excel.CreateWorkBook();
                excel.AddDatatable(dt);
                excel.SaveAndExit();
                MessageBox.Show("Export to Excel completed!", "Excel Class", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Model = cmbModel.Text;
        }

        private void txtBoxIDFrom_TextChanged(object sender, EventArgs e)
        {
            pnlBarcode.Refresh();
        }

        //Draw Barcode into pnlBarcode
        private void pnlBarcode_Paint(object sender, PaintEventArgs e)
        {
            BarCode barCode = new BarCode();
            barCode.SymbologyType = SymbologyType.Code128;
            barCode.CodeText = txtBoxIDFrom.Text;
            barCode.BarCodeWidth = pnlBarcode.Width;
            barCode.BarCodeHeight = pnlBarcode.Height;
            if (!string.IsNullOrEmpty(barCode.CodeText))
                barCode.drawBarcode(e.Graphics);
        }

        private void BoxIDMainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Warring", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
