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
        TfSQL SQL;
        DataTable dt;
        StringBuilder command;
        DataGridViewButtonColumn dgvButtons;

        public BoxIDMainFrm()
        {
            InitializeComponent();
            dt = new DataTable();
            SQL = new TfSQL("boxidcardb");
            command = new StringBuilder();
        }

        private void BoxIDMainFrm_Load(object sender, EventArgs e)
        {
            lbModel.Text = Model;
            lbUsername.Text = UserName;
        }

        #region SETTING AND OPTIONS DATAGRIDVIEW
        private bool checkProductSerialTable(string db_table)
        {
            command.Clear();
            command.Append("select model from ").Append(db_table);
            command.Append("where model ='").Append(lbModel.Text).Append("'");
            return (SQL.sqlExecuteNonQuery(command.ToString(), false));
        }

        private string getProductSerialTable()
        {
            if (checkProductSerialTable("product_serial_rt"))
                return "product_serial_rt";
            else if (checkProductSerialTable("product_serial_rtcd"))
                return "product_serial_rtcd";
            else if (checkProductSerialTable("product_serial_517eb"))
                return "product_serial_517eb";
            else
                return "product_serial_" + lbModel.Text;
        }

        private void getDataIntoDatatable(ref DataTable table)
        {
            table.Clear();
            command.Clear();
            command.Append("Select a.boxid, a.suser, a.regist_date, a.shipdate, a.invoice from box_id_rt a ");
            if (rdbBoxIDFrom.Checked)
            {
                command.Append("where 1 = 1 ");
                if (!string.IsNullOrEmpty(txtBoxIDFrom.Text))
                    command.Append("and a.boxid like '").Append(txtBoxIDFrom.Text).Append("%' ");
            }
            else if (rdbProductSerial.Checked)
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
            if (addButton) addButtonsDgv();
            dgvBoxID.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dgvBoxID.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvBoxID.DataSource = dt;
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
        #endregion

        //Click Search for view Box ID
        private void btnSearch_Click(object sender, EventArgs e)
        {
            updateDataGridViews(false);
        }

        //Draw Barcode into pnlBarcode
        private void pnlBarcode_Paint(object sender, PaintEventArgs e)
        {
            DotNetBarcode barCode = new DotNetBarcode(DotNetBarcode.Types.Jan13);
            Single x1, x2, y1, y2;
            string barCodeNo = txtBoxIDFrom.Text;
            x1 = 0;
            y1 = 0;
            x2 = pnlBarcode.Size.Width;
            y2 = pnlBarcode.Size.Height;
            if (!string.IsNullOrEmpty(barCodeNo))
                barCode.WriteBar(barCodeNo, x1, y1, x2, y2, e.Graphics);
        }

        private void dgvBoxID_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int currentRow = e.RowIndex;
            if (dgvBoxID.Columns[e.ColumnIndex] == dgvButtons && currentRow >= 0)
            {
                bool inUse = TfGeneral.checkOpenFormExists("");
                if (inUse)
                {
                    MessageBox.Show("Please close the other opened window", "Notice");
                    return;
                }
                string boxID = dgvBoxID["boxid", currentRow].Value.ToString();
                string serialNo = txtProductSerial.Text;
                string user = lbUsername.Text;
                string inVoice = dgvBoxID["invoice", currentRow].Value.ToString();
                DateTime printDate = DateTime.Parse(dgvBoxID["regist_date", currentRow].Value.ToString());
            }
        }
    }
}
