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
        string thisMonthTbl, lastMonthTbl;

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
            UpdateLimitAndCounter();
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
            UpdateLimitAndCounter();
        }

        private void txtChangeLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please enter numbers only!", "Warring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateLimitAndCounter()
        {
            lbLimit.Text = limit.ToString();
            lbOKCount.Text = OKcount.ToString() + '/' + limit.ToString();
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
                DefineTableName(DateTime.Now);
                dtBoxPackage = DataProductSerial(serial);
                dtBoxPackage.Columns.RemoveAt(dtBoxPackage.Columns.Count - 1);
                dgvBoxPackage.DataSource = dtBoxPackage;
            }
        }

        private DataTable DataOQC(string inserial)
        {
            string sql = @"select serno, tjudge, inspectdate, " +
            "MAX(case inspect when 'CG_CCW' then inspectdata else null end) as CG_CCW, " +
            "MAX(case inspect when 'CIO_CCW' then inspectdata else null end) as CIO_CCW, " +
            "MAX(case inspect when 'CNO_CCW' then inspectdata else null end) as CNO_CCW " +
            "FROM " +
            "(select d.serno, d.tjudge, c.inspectdate, c.inspect, c.inspectdata, c.judge " +
            "from (select SERNO, INSPECTDATE, INSPECT, INSPECTDATA, JUDGE " +
            "from (select SERNO, INSPECT, INSPECTDATA, JUDGE, max(inspectdate) as inspectdate, " +
            "row_number() OVER(PARTITION BY inspect ORDER BY max(inspectdate) desc) as flag " +
            "from (select * from " + thisMonthTbl + "data " +
            "WHERE serno = (select serno from " + thisMonthTbl +
            " where process = 'NMT2' and serno = '" + serial + "' LIMIT 1) " +
            "and inspect in ('CG_CCW','CIO_CCW','CNO_CCW')) a " +
            "group by SERNO, INSPECTDATE , INSPECT , INSPECTDATA , JUDGE ) b where flag = 1) c, " +
            "(select serno, tjudge from " + thisMonthTbl +
            " where serno = '" + inserial + "' and process = 'NMT2' and tjudge = '0' " +
            "order by inspectdate desc LIMIT 1) d " +
            "group by d.serno, d.tjudge, c.inspectdate, c.inspect, c.inspectdata, c.judge) e " +
            "GROUP BY serno, tjudge, inspectdate " +
            "UNION ALL " +
            "select serno, tjudge, inspectdate, " +
            "MAX(case inspect when 'CG_CCW' then inspectdata else null end) as CG_CCW, " +
            "MAX(case inspect when 'CIO_CCW' then inspectdata else null end) as CIO_CCW, " +
            "MAX(case inspect when 'CNO_CCW' then inspectdata else null end) as CNO_CCW " +
            "FROM " +
            "(select d.serno, d.tjudge, c.inspectdate, c.inspect, c.inspectdata, c.judge " +
            "from (select SERNO, INSPECTDATE, INSPECT, INSPECTDATA, JUDGE " +
            "from (select SERNO, INSPECT, INSPECTDATA, JUDGE, max(inspectdate) as inspectdate, " +
            "row_number() OVER(PARTITION BY inspect ORDER BY max(inspectdate) desc) as flag " +
            "from (select * from " + lastMonthTbl + "data " +
            "WHERE serno = (select serno from " + lastMonthTbl +
            " where process = 'NMT2' and serno = '" + inserial + "' LIMIT 1) " +
            "and inspect in ('CG_CCW','CIO_CCW','CNO_CCW')) a " +
            "group by SERNO, INSPECTDATE , INSPECT , INSPECTDATA , JUDGE ) b where flag = 1) c, " +
            "(select serno, tjudge from " + lastMonthTbl +
            " where serno = '" + inserial + "' and process = 'NMT2' and tjudge = '0' " +
            "order by inspectdate desc LIMIT 1) d " +
            "group by d.serno, d.tjudge, c.inspectdate, c.inspect, c.inspectdata, c.judge) e " +
            "GROUP BY serno, tjudge, inspectdate";
            System.Diagnostics.Debug.Print(System.Environment.NewLine + sql);
            DataTable dt = new DataTable();
            SQL.sqlDataAdapterFillDatatable(sql, ref dt);
            return dt;
        }

        private DataTable DataInLine(string inserial)
        {
            string sql = "select serno, tjudge as tjudge_line, inspectdate as date_line, " +
            "MAX(case inspect when 'AIO_CCW' then inspectdata else null end) as AIO_CCW, " +
            "MAX(case inspect when 'ANO_CCW' then inspectdata else null end) as ANO_CCW, " +
            "MAX(case inspect when 'AIR_CCW' then inspectdata else null end) as AIR_CCW, " +
            "MAX(case inspect when 'ANR_CCW' then inspectdata else null end) as ANR_CCW, " +
            "MAX(case inspect when 'AIS_CCW' then inspectdata else null end) as AIS_CCW " +
            "FROM " +
            "(select d.serno, d.tjudge, c.inspectdate, c.inspect, c.inspectdata, c.judge " +
            "FROM(select SERNO, INSPECTDATE, INSPECT, INSPECTDATA, JUDGE " +
            "FROM (select SERNO, INSPECT, INSPECTDATA, JUDGE, max(inspectdate) as inspectdate, " +
            "row_number() OVER(PARTITION BY inspect ORDER BY max(inspectdate) desc) as flag " +
            "FROM (select * from " + thisMonthTbl + "data " +
            "WHERE serno = (select lot from " + thisMonthTbl + " where process = 'NO53' " +
            "and serno = '" + inserial + "' LIMIT 1) " +
            "and inspect in ('AIO_CCW','AIR_CCW','AIS_CCW','ANO_CCW','ANR_CCW')) a " +
            "group by SERNO, INSPECTDATE , INSPECT , INSPECTDATA , JUDGE ) b where flag = 1) c, " +
            "(select serno, tjudge from " + thisMonthTbl + " where serno = (select lot from " + thisMonthTbl +
            " where process = 'NO53' and serno = '" + inserial + "' LIMIT 1) " +
            "and process = 'NO41' order by inspectdate desc LIMIT 1) d " +
            "group by d.serno, d.tjudge, c.inspectdate, c.inspect, c.inspectdata, c.judge) e " +
            "GROUP BY serno, tjudge, inspectdate " +
            "UNION ALL " +
            "select serno, tjudge as tjudge_line, inspectdate as date_line, " +
            "MAX(case inspect when 'AIO_CCW' then inspectdata else null end) as AIO_CCW, " +
            "MAX(case inspect when 'ANO_CCW' then inspectdata else null end) as ANO_CCW, " +
            "MAX(case inspect when 'AIR_CCW' then inspectdata else null end) as AIR_CCW, " +
            "MAX(case inspect when 'ANR_CCW' then inspectdata else null end) as ANR_CCW, " +
            "MAX(case inspect when 'AIS_CCW' then inspectdata else null end) as AIS_CCW " +
            "FROM " +
            "(select d.serno, d.tjudge, c.inspectdate, c.inspect, c.inspectdata, c.judge " +
            "FROM(select SERNO, INSPECTDATE, INSPECT, INSPECTDATA, JUDGE " +
            "FROM (select SERNO, INSPECT, INSPECTDATA, JUDGE, max(inspectdate) as inspectdate, " +
            "row_number() OVER(PARTITION BY inspect ORDER BY max(inspectdate) desc) as flag " +
            "FROM (select * from " + lastMonthTbl + "data " +
            "WHERE serno = (select lot from " + lastMonthTbl + " where process = 'NO53' " +
            "and serno = '" + inserial + "' LIMIT 1) " +
            "and inspect in ('AIO_CCW','AIR_CCW','AIS_CCW','ANO_CCW','ANR_CCW')) a " +
            "group by SERNO, INSPECTDATE , INSPECT , INSPECTDATA , JUDGE ) b where flag = 1) c, " +
            "(select serno, tjudge from " + lastMonthTbl + " where serno = (select lot from " + lastMonthTbl +
            " where process = 'NO53' and serno = '" + inserial + "' LIMIT 1) and process = 'NO41' " +
            "order by inspectdate desc LIMIT 1) d " +
            "group by d.serno, d.tjudge, c.inspectdate, c.inspect, c.inspectdata, c.judge) e " +
            "GROUP BY serno, tjudge, inspectdate ";
            System.Diagnostics.Debug.Print(System.Environment.NewLine + sql);
            DataTable dt = new DataTable();
            SQL.sqlDataAdapterFillDatatable(sql, ref dt);
            return dt;
        }

        private DataTable DataProductSerial(string inserial)
        {
            string sql = @"(select * from (select a.serno, a.inspectdate, a.tjudge,
            MAX(case inspect when 'CG_CCW' then inspectdata else null end) as CG_CCW,
            MAX(case inspect when 'CIO_CCW' then inspectdata else null end) as CIO_CCW,
            MAX(case inspect when 'CNO_CCW' then inspectdata else null end) as CNO_CCW
            from " + thisMonthTbl + " a left join " + thisMonthTbl + @"data b
            on a.serno = b.serno and a.inspectdate = b.inspectdate
            where a.process = 'NMT3' group by a.serno, a.inspectdate, a.tjudge
            order by a.inspectdate desc) c left join (select a.inspectdate, a.tjudge,
            MAX(case inspect when 'AIO_CCW' then inspectdata else null end) as AIO_CCW,
            MAX(case inspect when 'ANO_CCW' then inspectdata else null end) as ANO_CCW,
            MAX(case inspect when 'AIR_CCW' then inspectdata else null end) as AIR_CCW,
            MAX(case inspect when 'ANR_CCW' then inspectdata else null end) as ANR_CCW,
            MAX(case inspect when 'AIS_CCW' then inspectdata else null end) as AIS_CCW,
            a.serno from " + thisMonthTbl + " a left join " + thisMonthTbl + @"data b
            on a.lot = b.serno where a.process in ('NO53')
            group by a.serno, a.inspectdate, a.tjudge order by inspectdate desc) d
            on c.serno = d.serno where c.serno = '" + inserial + @"' limit 1)
            UNION ALL
            (select * from(select a.serno, a.inspectdate, a.tjudge,
            MAX(case inspect when 'CG_CCW' then inspectdata else null end) as CG_CCW,
            MAX(case inspect when 'CIO_CCW' then inspectdata else null end) as CIO_CCW,
            MAX(case inspect when 'CNO_CCW' then inspectdata else null end) as CNO_CCW
            from " + lastMonthTbl + " a left join " + lastMonthTbl + @"data b
            on a.serno = b.serno and a.inspectdate = b.inspectdate
            where a.process = 'NMT3' group by a.serno, a.inspectdate, a.tjudge
            order by a.inspectdate desc) c left join(select a.inspectdate, a.tjudge,
            MAX(case inspect when 'AIO_CCW' then inspectdata else null end) as AIO_CCW,
            MAX(case inspect when 'ANO_CCW' then inspectdata else null end) as ANO_CCW,
            MAX(case inspect when 'AIR_CCW' then inspectdata else null end) as AIR_CCW,
            MAX(case inspect when 'ANR_CCW' then inspectdata else null end) as ANR_CCW,
            MAX(case inspect when 'AIS_CCW' then inspectdata else null end) as AIS_CCW,
            a.serno from " + lastMonthTbl + " a left join " + lastMonthTbl + @"data b
            on a.lot = b.serno where a.process in ('NO53')
            group by a.serno, a.inspectdate, a.tjudge order by inspectdate desc) d
            on c.serno = d.serno where c.serno = '" + inserial + "' limit 1)";
            System.Diagnostics.Debug.Print(System.Environment.NewLine + sql);
            DataTable dt = new DataTable();
            SQL.sqlDataAdapterFillDatatable(sql, ref dt);
            return dt;
        }

        private void DefineTableName(DateTime date)
        {
            thisMonthTbl = Model + date.ToString("yyyyMM");
            lastMonthTbl = Model + date.AddMonths(-1).ToString("yyyyMM");
        }

        private void btnReprint_Click(object sender, EventArgs e)
        {

        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {

        }

        private void btnDelProduct_Click(object sender, EventArgs e)
        {

        }

        private void btnRegBoxID_Click(object sender, EventArgs e)
        {

        }

        private void btnDelBoxID_Click(object sender, EventArgs e)
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
