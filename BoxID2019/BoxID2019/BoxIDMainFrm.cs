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
            lbUsername.Text = UserName;
            lbModel.Text = Model;
        }

        private void updateDataGridViews(bool addButton)
        {
            dt.Clear();
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
                    command.Append("left join product_serial_rt b on a.boxid = b.boxid where b.serialno ='");
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
            SQL.sqlDataAdapterFillDatatable(command.ToString(), ref dt);
            tsRowsCount.Text = dt.Rows.Count.ToString();
            dgvBoxID.DataSource = dt;
            if (addButton) addButtonsDgv();
            dgvBoxID.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dgvBoxID.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvBoxID.Refresh();
        }

        private void addButtonsDgv()
        {
            dgvButtons = new DataGridViewButtonColumn();
            dgvButtons.HeaderText = "Open BoxId";
            dgvButtons.Text = "Open";
            dgvButtons.UseColumnTextForButtonValue = true;
            dgvBoxID.Columns.Add(dgvButtons);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            updateDataGridViews(true);
        }

        private void pnlBarcode_Paint(object sender, PaintEventArgs e)
        {
            DotNetBarcode

        }

    }
}
