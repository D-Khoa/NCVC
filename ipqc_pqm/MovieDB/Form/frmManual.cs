using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Security.Permissions;
using System.Collections;
using Npgsql;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks; 

namespace IpqcDB
{
    public partial class frmManual : Form
    {
        //A delegate variable that notifies the occurrence of the event to the parent form
        public delegate void RefreshEventHandler(object sender, EventArgs e);
        public event RefreshEventHandler RefreshEvent;

        //Button for data grid view
        DataGridViewButtonColumn Open;

        //Other, non-local variables
        double upp;
        double low;
        bool editMode;
        DataTable dtBuffer;
        DataTable dtHistory;
        DataTable dtUpLowIns;
        int clmSet = 5;
        int rowSet = 1;
        string instrument;
        string _ip;
        bool adm_flag = false;
        bool fl;

        //Constructor
        public frmManual()
        {
            InitializeComponent();
            txtComment.ForeColor = Color.Gray;
            txtComment.Text = "Comment the NG reason here";
            this.txtComment.Leave += new System.EventHandler(this.txtComment_Leave);
            this.txtComment.Enter += new System.EventHandler(this.txtComment_Enter);
        }

        //Processing at load time
        private void Form6_Load(object sender, EventArgs e)
        {
            // Designate display location of this form
            this.Left = 300;
            this.Top = 15;

            //if (txtLine.Text == "") btnMeasure.Enabled = false;

            //Exit app if user has been log in by another device
            TfSQL flag = new TfSQL();
            fl = flag.sqlExecuteScalarBool("select admin_flag from qc_user_temp where qcuser = '" + txtUser.Text + "'");
            string ipadd = flag.sqlExecuteScalarString("select ip_address from qc_user_temp where qcuser = '" + txtUser.Text + "'");
            if (ipadd == "null") flag.sqlExecuteScalarString("UPDATE qc_user_temp SET loginstatus=true, ip_address = '" + _ip + "' where qcuser = '" + txtUser.Text + "'");
            if (ipadd != "null" && ipadd != _ip)
            {
                DialogResult res = MessageBox.Show("User is logged in " + _ip + "," + System.Environment.NewLine +
                                    "Do you want to log out and log in again ?", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Question);
                if (res == DialogResult.OK) Application.Exit();
            }

            //User permission
            if (txtUser.Text != "Admin")
            {
                string[] a = txtUser.Text.Split('_');

                if (a[0] != "IPQC" && txtUser.Text != "Admin")
                {
                    btnMeasure.Enabled = false;
                    btnRegister.Enabled = false;
                    btnDelete.Enabled = false;
                }
                if (a[0] != "VIEW" && txtUser.Text != "Admin" && fl == false)
                {
                    btnExport.Enabled = false;
                    btnExcelB.Enabled = false;
                }
            }

            // Make DATETIMEPICKER the date 10 days ago.
            dtpSet10daysBefore(dtpLotFrom);

            // Round up less than or equal to DATETIMEPICKER minutes
            dtpRoundUpHour(dtpLotTo);

            // Lower DATETIMEPICKER minutes or less
            dtpRounddownHour(dtpLotInput);

            // Generate tables for various processing and read data
            dtBuffer = new DataTable();
            defineBufferAndHistoryTable(ref dtBuffer);
            dtHistory = new DataTable();
            defineBufferAndHistoryTable(ref dtHistory);
            readDtHistory(ref dtHistory);
            dtUpLowIns = new DataTable();
            setLimitSetAndCommand(ref dtUpLowIns);

            // Update grid view
            updateDataGripViews(dtBuffer, dtHistory, ref dgvBuffer, ref dgvHistory);

            // Add a button to the right edge of the grid view (first time only)
            addButtonsToDataGridView(dgvHistory);
        }

        // Sub procedure: Call with parent form, store information of parent form in text box and take over
        //(Mode 1: View / Edit, Mode 2: New Registration)
        public void updateControls(string model, string process, string inspect, string line, string user, string ip)
        {
            txtModel.Text = model;
            txtProcess.Text = process;
            txtInspect.Text = inspect;
            txtLine.Text = line;
            txtUser.Text = user;
            _ip = ip;
            //string sql_line = "select line from tbl_model_line where model = '" + txtModel.Text + "' order by line";
            //TfSQL ln = new TfSQL();
            //ln.getComboBoxData(sql_line, ref cmbLine);
            //cmbLine.SelectedIndex = 0;
        }

        // Subprocedure: Reading from DB into DtHISTORY
        private void readDtHistory(ref DataTable dt)
        {
            dt.Clear();

            string model = txtModel.Text;
            string process = txtProcess.Text;
            string inspect = txtInspect.Text;
            DateTime lotFrom = dtpLotFrom.Value;
            DateTime lotTo = dtpLotTo.Value;
            string line = txtLine.Text;

            string sql = "select inspect, lot, inspectdate, line, qc_user, status, " +
                                "m1, m2, m3, m4, m5, x, r FROM tbl_measure_history " +
                         "WHERE model = '" + model + "' AND " +
                                "process = '" + process + "' AND " +
                                "inspect = '" + inspect + "' AND " +
                                "lot >= '" + lotFrom.ToString() + "' AND " +
                                "lot <= '" + lotTo.ToString() + "' AND " +
                                "line = '" + line + "' AND " +
                                "qc_user != '1. Upper' AND qc_user != '2. Lower' " +
                         "order by lot, inspectdate, row_set";

            System.Diagnostics.Debug.Print(sql);
            TfSQL tf = new TfSQL();
            tf.sqlDataAdapterFillDatatable(sql, ref dt);
        }

        // Subprocedure: definition of history table
        private void defineBufferAndHistoryTable(ref DataTable dt)
        {
            dt.Columns.Add("inspect", Type.GetType("System.String"));
            dt.Columns.Add("lot", Type.GetType("System.DateTime"));
            dt.Columns.Add("inspectdate", Type.GetType("System.DateTime"));
            dt.Columns.Add("line", Type.GetType("System.String"));
            dt.Columns.Add("qc_user", Type.GetType("System.String"));
            dt.Columns.Add("status", Type.GetType("System.String"));
            dt.Columns.Add("m1", Type.GetType("System.Double"));
            dt.Columns.Add("m2", Type.GetType("System.Double"));
            dt.Columns.Add("m3", Type.GetType("System.Double"));
            dt.Columns.Add("m4", Type.GetType("System.Double"));
            dt.Columns.Add("m5", Type.GetType("System.Double"));
            dt.Columns.Add("x", Type.GetType("System.Double"));
            dt.Columns.Add("r", Type.GetType("System.Double"));
        }

        // Subprocedure: setting upper / lower limit, row set / column set, command set
        private void setLimitSetAndCommand(ref DataTable dt)
        {
            dt.Clear();
            string sql = "select upper, lower, clm_set, row_set, instrument from tbl_measure_item_2 " +
                "where model = '" + txtModel.Text + "' and " +
                      "inspect = '" + txtInspect.Text + "'";
            System.Diagnostics.Debug.Print(sql);
            TfSQL tf = new TfSQL();
            tf.sqlDataAdapterFillDatatable(sql, ref dt);

            upp = (double)dt.Rows[0]["upper"];
            txtUsl.Text = upp.ToString();
            low = (double)dt.Rows[0]["lower"];
            txtLsl.Text = low.ToString();
            rowSet = (int)dt.Rows[0]["row_set"];
            clmSet = (int)dt.Rows[0]["clm_set"];
            instrument = (string)dt.Rows[0]["instrument"];
        }

        // Subprocedure: Updating the data grid view
        private void updateDataGripViews(DataTable dt1, DataTable dt2, ref DataGridView dgv1, ref DataGridView dgv2)
        {
            // Store DATATABLE in data grid view
            dgv1.DataSource = dt1;
            dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DisableSortMode(dgvBuffer);

            dgv2.DataSource = dt2;
            dgv2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv2.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DisableSortMode(dgvHistory);

            // Mark cells outside specifications
            colorHistoryViewBySpec(dtHistory, ref dgvHistory);

            // Display the bottom line
            if (dgv2.Rows.Count >= 1)
                dgv2.FirstDisplayedScrollingRowIndex = dgv2.Rows.Count - 1;
        }

        //DisableSortMode
        public void DisableSortMode(DataGridView dgv)
        {
            foreach (DataGridViewColumn col in dgv.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        // When the search button is pressed, data is read and DATAGRIDVIEW is updated
        private void btnSearch_Click(object sender, EventArgs e)
        {
            readDtHistory(ref dtHistory);
            updateDataGripViews(dtBuffer, dtHistory, ref dgvBuffer, ref dgvHistory);
        }

        // Sub-sub procedure: Add a button to the right end of the grid view
        private void addButtonsToDataGridView(DataGridView dgv)
        {
            if (fl == true) adm_flag = true;
            Open = new DataGridViewButtonColumn();
            Open.Text = "Open";
            Open.UseColumnTextForButtonValue = true;
            Open.Width = 45;
            dgv.Columns.Add(Open);

            if (adm_flag == true)
            {
                if (frmItem.mdl.Contains(frmItem.mdl_p))
                {
                    btnDelete.Visible = true;
                }
                else if (frmItem.mdl_p == "all")
                {
                    btnDelete.Visible = true;
                }
                else btnDelete.Visible = false;
            }
        }

        // New registration of measured values
        private void btnMeasure_Click(object sender, EventArgs e)
        {
            // Decrease the edit mode flag and set the registration / correction button to "registration"
            editMode = false;
            btnRegister.Text = "Register";
            dtpLotInput.Enabled = true;

            // Clear the marking of HISTORY datagridview
            colorViewReset(ref dgvHistory);
            colorViewReset(ref dgvBuffer);

            // Initialize the new registration buffer table and buffer grid view
            dtBuffer.Clear();

            // Set the number of rows as many as the number of sets (special items use variable rowSet 2)
            int rowSet2;
            bool specialItem = txtInspect.Text == "MCWCWP";
            if (specialItem) rowSet2 = 3;
            else rowSet2 = rowSet;

            for (int i = 1; i <= rowSet2; i++)
            {
                DataRow dr = dtBuffer.NewRow();
                dr["inspect"] = txtInspect.Text;
                dr["lot"] = dtpLotInput.Value;
                dr["inspectdate"] = DateTime.Now;
                dr["line"] = txtLine.Text;
                dr["qc_user"] = txtUser.Text;

                if (specialItem && i == 1) dr["qc_user"] = "1. Upper";
                else if (specialItem && i == 2) dr["qc_user"] = "2. Lower";
                else if (specialItem && i == 3) dr["qc_user"] = txtUser.Text;

                dtBuffer.Rows.Add(dr);
            }

            // Update grid view
            updateDataGripViews(dtBuffer, dtHistory, ref dgvBuffer, ref dgvHistory);
        }

        // 既存測定値の修正
        private void dgvHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int curRow = int.Parse(e.RowIndex.ToString());

            if (dgvHistory.Columns[e.ColumnIndex] == Open && curRow >= 0)
            {
                // 編集モードフラグを立て、Registration・修正ボタンを「修正」の表示にする
                editMode = true;
                btnRegister.Text = "Update";
                dtpLotInput.Enabled = true;

                // 新規Registration用バッファーテーブル、バッファーグリットビューを初期化し、ボタンに対応する値を格納する
                dtBuffer.Clear();

                string sql = "select inspect, lot, inspectdate, line, qc_user, status, " +
                                    "m1, m2, m3, m4, m5 FROM tbl_measure_history WHERE " +
                             "model = '" + txtModel.Text + "' AND " +
                             "inspect = '" + dgvHistory["inspect", curRow].Value.ToString() + "' AND " +
                             "lot = '" + (DateTime)dgvHistory["lot", curRow].Value + "' AND " +
                             "inspectdate = '" + (DateTime)dgvHistory["inspectdate", curRow].Value + "' AND " +
                             "line = '" + dgvHistory["line", curRow].Value.ToString() + "' " +
                             "order by qc_user";
                System.Diagnostics.Debug.Print(sql);
                TfSQL tf = new TfSQL();
                tf.sqlDataAdapterFillDatatable(sql, ref dtBuffer);

                // Update grid view
                updateDataGripViews(dtBuffer, dtHistory, ref dgvBuffer, ref dgvHistory);

                // Display change target line
                if (dgvHistory.Rows.Count >= 1)
                    dgvHistory.FirstDisplayedScrollingRowIndex = curRow;

                // Subprocedure: Marking the row being edited
                colorViewForEdit(ref dgvHistory, curRow);
                colorViewForEdit(ref dgvBuffer, 0);
            }
        }

        // Subprocedure: Marking the row being edited
        private void colorViewForEdit(ref DataGridView dgv, int row)
        {
            if (dgv.Rows.Count == 0) return;

            int rowCount = dgv.RowCount;
            int clmCount = dgv.ColumnCount;
            DateTime inspectdate = (DateTime)dgv["inspectdate", row].Value;

            for (int i = 0; i < rowCount; ++i)
            {
                if ((DateTime)dgv["inspectdate", i].Value == inspectdate)
                {
                    for (int j = 0; j < clmCount; ++j)
                        dgv[j, i].Style.BackColor = Color.Yellow;
                }
                else
                {
                    for (int k = 0; k < clmCount; ++k)
                        dgv[k, i].Style.BackColor = Color.FromKnownColor(KnownColor.Window);
                }
            }
        }

        // Sub procedure: clear marking
        private void colorViewReset(ref DataGridView dgv)
        {
            int rowCount = dgv.RowCount;
            int clmCount = dgv.ColumnCount;

            for (int i = 0; i < rowCount; ++i)
            {
                for (int k = 0; k < clmCount; ++k)
                    dgv[k, i].Style.BackColor = Color.FromKnownColor(KnownColor.Window);
            }
        }

        // Subprocedure: Mark cells outside specifications (history table)
        private void colorHistoryViewBySpec(DataTable dt, ref DataGridView dgv)
        {
            int rowCount = dgv.RowCount;
            int clmStart = 6;
            int clmEnd = 10;

            for (int i = 0; i < rowCount; ++i)
            {
                for (int j = clmStart; j <= clmEnd; ++j)
                {
                    double m = 0;
                    bool b = double.TryParse(dt.Rows[i][j].ToString(), out m);
                    if (m >= low && m <= upp)
                        dgv[j, i].Style.BackColor = Color.FromKnownColor(KnownColor.Window);
                    else
                        dgv[j, i].Style.BackColor = Color.Red;
                }
            }
        }

        // At the completion of editing, Mark cells outside specifications (temporary table)
        private void dgvBuffer_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            if (txtInspect.Text == "MCWCWP")
            {
                if (dgv[e.ColumnIndex, 0].Value.ToString() != string.Empty && 
                        dgv[e.ColumnIndex, 1].Value.ToString() != string.Empty)
                {
                    double d1 = 0;
                    double d2 = 0;
                    double d3 = 0;
                    double.TryParse(dgv[e.ColumnIndex, 0].Value.ToString(), out d1);
                    double.TryParse(dgv[e.ColumnIndex, 1].Value.ToString(), out d2);
                    d3 = Math.Round(Math.Abs(d1 - d2), 4);
                    dgv[e.ColumnIndex, 2].Value = d3;

                    if (d3 >= low && d3 <= upp)
                        dgv[e.ColumnIndex, 2].Style.BackColor = Color.FromKnownColor(KnownColor.Window);
                    else
                        dgv[e.ColumnIndex, 2].Style.BackColor = Color.Red;
                }
            }
            else if (txtInspect.Text == "CAMMPAD")
            {
                if (dgv[e.ColumnIndex, 0].Value.ToString() != string.Empty &&
                        dgv[e.ColumnIndex, 1].Value.ToString() != string.Empty &&
                        dgv[e.ColumnIndex, 2].Value.ToString() != string.Empty &&
                        dgv[e.ColumnIndex, 3].Value.ToString() != string.Empty &&
                        dgv[e.ColumnIndex, 4].Value.ToString() != string.Empty &&
                        dgv[e.ColumnIndex, 5].Value.ToString() != string.Empty)
                {
                    double min;
                    int i;
                    double d1 = 0, d2 = 0, d3 = 0, d4 = 0, d5 = 0, d6 = 0, d7 = 0;
                    double.TryParse(dgv[e.ColumnIndex, 0].Value.ToString(), out d1);
                    double.TryParse(dgv[e.ColumnIndex, 1].Value.ToString(), out d2);
                    double.TryParse(dgv[e.ColumnIndex, 2].Value.ToString(), out d3);
                    double.TryParse(dgv[e.ColumnIndex, 3].Value.ToString(), out d4);
                    double.TryParse(dgv[e.ColumnIndex, 4].Value.ToString(), out d5);
                    double.TryParse(dgv[e.ColumnIndex, 5].Value.ToString(), out d6);
                    double[] mang = new double[] { d1, d2, d3, d4, d5, d6 };

                    min = mang[0];
                    for (i = 0; i <= 5; i++)
                    {
                        if (min > mang[i]) min = mang[i];
                    }
                    d7 = min;
                    dgv[e.ColumnIndex, 6].Value = d7;

                    if (d7 >= low && d7 <= upp)
                        dgv[e.ColumnIndex, 6].Style.BackColor = Color.FromKnownColor(KnownColor.Window);
                    else
                        dgv[e.ColumnIndex, 6].Style.BackColor = Color.Red;
                }
            }
            else
            {
                double d = 0;
                double.TryParse(dgv[e.ColumnIndex, e.RowIndex].Value.ToString(), out d);

                if (d >= low && d <= upp)
                    dgv[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.FromKnownColor(KnownColor.Window);
                else
                    dgv[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Red;
            }
        }

        // Only columns of measured values can be edited
        private void dgvBuffer_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            //Judge whether you can edit (UPPER · LOWER · Difference input for specific inspection item, Difference can not be input directly)
            if (e.ColumnIndex <= 5 || e.ColumnIndex >= 11 || (txtInspect.Text == "MCWCWP" && e.RowIndex == 2))
            {
                //Disable editing
                e.Cancel = true;
            }
        }

        // After importing the measurement value, register it in the database
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (dtBuffer.Rows.Count <= 0) return;

            string model = txtModel.Text;
            string process = txtProcess.Text;
            string inspect = txtInspect.Text;
            string status = string.Empty;
            DateTime lot = DateTime.Parse(dtBuffer.Rows[0]["lot"].ToString());
            DateTime inspectdate = DateTime.Parse(dtBuffer.Rows[0]["inspectdate"].ToString());
            string line = txtLine.Text;

            // Calculate the mean and range in the buffer table
            calculateAverageAndRangeInDataTable(ref dtBuffer);

            // Register in the IPQCDB measurement history table
            TfSQL tf = new TfSQL();

            if (txtComment.Text != "Comment the NG reason here" && txtComment.Text != string.Empty)
            {
                status = txtComment.Text;
            }

            if (txtComment.Text != string.Empty && dtBuffer.Rows[0]["status"].ToString() != string.Empty) { status = dtBuffer.Rows[0]["status"].ToString(); }

            bool res = tf.sqlMultipleInsert(model, process, inspect, lot, inspectdate, line, status, dtBuffer);

            if (res)
            {
                // Register in the PQM table in the background
                DataTable dtTemp = new DataTable();
                dtTemp = dtBuffer.Copy();
                registerMeasurementToPqmTable(dtTemp);

                // Display the registered status on the form
                dtBuffer.Clear();
                readDtHistory(ref dtHistory);
                updateDataGripViews(dtBuffer, dtHistory, ref dgvBuffer, ref dgvHistory);

                // Set the edit mode flag and return the registration / correction button to the display of "registration"
                editMode = false;
                btnRegister.Text = "Register";
                dtpLotInput.Enabled = true;
                txtComment.ResetText();
            }
        }

        // Subprocedure: Find and store average and range (maximum - minimum) inside the data table
        private void calculateAverageAndRangeInDataTable(ref DataTable dt)
        {
            if (dt.Rows.Count == 0) return;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double[] ary = new double[5];
                double max = double.MinValue;
                double min = double.MaxValue;
                double sum = 0;
                double avg = 0;
                int cnt = 0;
                string idx = string.Empty;

                for (int j = 0; j < 5; j++)
                {
                    idx = "m" + (j + 1);
                    if (!string.IsNullOrEmpty(dt.Rows[i][idx].ToString()))
                    {
                        ary[j] = (double)dt.Rows[i][idx];
                        if (max < ary[j]) max = ary[j];
                        if (min > ary[j]) min = ary[j];
                        sum += ary[j];
                        cnt += 1;
                    }
                }
                avg = sum / cnt;
                dt.Rows[i]["x"] = Math.Round(avg, 4);
                dt.Rows[i]["r"] = Math.Abs(max - min);
            }
        }

        // Sub procedure: Register to PQM table (background process)
        private void registerMeasurementToPqmTable(DataTable dt)
        {
            var task = Task.Factory.StartNew(() =>
            {
                string model = txtModel.Text;
                string process = txtProcess.Text;
                string inspect = txtInspect.Text;
                DateTime lot = DateTime.Parse(dt.Rows[0]["lot"].ToString());
                DateTime inspectdate = DateTime.Parse(dt.Rows[0]["inspectdate"].ToString());
                string line = txtLine.Text;

                TfSqlPqm Tfc = new TfSqlPqm();
                Tfc.sqlMultipleInsertMeasurementToPqmTable(model, process, inspect, lot, inspectdate, line, dt, upp, low);
            }); 
        }

        // Processing when the delete button is pressed
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dtBuffer.Rows.Count <= 0) return;

            DialogResult result = MessageBox.Show("Do you really want to delete the selected row?",
                "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
            {
                MessageBox.Show("Delete process was canceled.",
                "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
            }
            else if (result == DialogResult.Yes)
            {
                // Delete data
                string sql = "delete from tbl_measure_history where " +
                    "model='" + txtModel.Text + "' and " +
                    "inspect='" + txtInspect.Text + "' and " +
                    "lot ='" + dtBuffer.Rows[0]["lot"] + "' and " +
                    "inspectdate ='" + dtBuffer.Rows[0]["inspectdate"] + "' and " +
                    "line ='" + txtLine.Text + "'";

                System.Diagnostics.Debug.Print(sql);
                TfSQL tf = new TfSQL();
                int res = tf.sqlExecuteNonQueryInt(sql, false);

                // Delete in the PQM table in the background
                DataTable dtTemp = new DataTable();
                dtTemp = dtBuffer.Copy();
                deleteFromPqmTable(dtTemp);

                // Initialize the new registration buffer table and buffer grid view
                dtBuffer.Clear();
                
                // Reload the table after deleting
                readDtHistory(ref dtHistory);

                // Clear the marking of HISTORY datagridview
                colorViewReset(ref dgvHistory);

                // Update grid view
                updateDataGripViews(dtBuffer, dtHistory, ref dgvBuffer, ref dgvHistory);

                // Decrease the edit mode flag and set the registration / correction button to "registration"
                editMode = false;
                btnRegister.Text = "Register";
                dtpLotInput.Enabled = true;
            }
        }

        // Sub procedure: Delete in PQM table (background process)
        private void deleteFromPqmTable(DataTable dt)
        {
            var task = Task.Factory.StartNew(() =>
            {
                string model = txtModel.Text;
                string process = txtProcess.Text;
                string inspect = txtInspect.Text;
                DateTime lot = DateTime.Parse(dt.Rows[0]["lot"].ToString());
                DateTime inspectdate = DateTime.Parse(dt.Rows[0]["inspectdate"].ToString());
                string line = txtLine.Text;

                TfSqlPqm Tfc = new TfSqlPqm();
                Tfc.sqlDeleteFromPqmTable(model, process, inspect, lot, inspectdate, line);
            });
        }

        // Processing when the cancel button is pressed
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Sub-sub procedure: Make DATETIMEPICKER the date 10 days ago
        private void dtpSet10daysBefore(DateTimePicker dtp)
        {
            DateTime dt = dtp.Value.Date.AddDays(-10);
            dtp.Value = dt;
        }

        // Sub-sub procedure: rounds up to the minute of DATETIMEPICKER
        private void dtpRoundUpHour(DateTimePicker dtp)
        {
            DateTime dt = dtp.Value;
            int hour = dt.Hour;
            int minute = dt.Minute;
            int second = dt.Second;
            int millisecond = dt.Millisecond;
            dtp.Value = dt.AddHours(1).AddMinutes(-minute).AddSeconds(-second).AddMilliseconds(-millisecond);
        }

        // Sub-sub procedure: lower DATETIMEPICKER minutes or less
        private void dtpRounddownHour(DateTimePicker dtp)
        {
            DateTime dt = dtp.Value;
            int hour = dt.Hour;
            int minute = dt.Minute;
            int second = dt.Second;
            int millisecond = dt.Millisecond;
            dtp.Value = dt.AddMinutes(-minute).AddSeconds(-second).AddMilliseconds(-millisecond);
        }

        // Processing when the XR graph creation button is pressed    
        private void btnExport_Click(object sender, EventArgs e)
        {
            TfSQL sampl = new TfSQL();
            string sample = sampl.sqlExecuteScalarString("select clm_set from tbl_measure_item_2 where inspect = '" + txtInspect.Text + "' and model = '" + txtModel.Text + "'");
            string descrip = sampl.sqlExecuteScalarString("select description from tbl_measure_item_2 where inspect = '" + txtInspect.Text + "' and model = '" + txtModel.Text + "'");
            ExcelClassnew xl = new ExcelClassnew();

            string dtpFrom = dtpLotFrom.Value.ToString("yyyy/MM/dd");
            string dtpTo = dtpLotTo.Value.ToString("yyyy/MM/dd");

            xl.exportExcel(txtModel.Text, txtLine.Text, txtUser.Text, txtUsl.Text, txtLsl.Text, txtProcess.Text, txtInspect.Text, sample, descrip, dgvHistory, dtpFrom, dtpTo);
        }

        // Sub-sub procedure: Generate data table for XR chart     
        private DataTable returnXrChartData()
        {
            DataTable dt = new DataTable();
            dt = ((DataTable)dgvHistory.DataSource).Copy();
            dt.Columns.Add("llim", Type.GetType("System.Double"));
            dt.Columns.Add("ulim", Type.GetType("System.Double"));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["llim"] = double.Parse(txtLsl.Text);
                dt.Rows[i]["ulim"] = double.Parse(txtUsl.Text);
            }

            return dt;
        }

        private void txtComment_Enter(object sender, EventArgs e)
        {
            if (txtComment.Text == "Comment the NG reason here")
            {
                txtComment.Text = "";
                txtComment.ForeColor = Color.Black;
            }
        }

        private void txtComment_Leave(object sender, EventArgs e)
        {
            if (txtComment.Text == "")
            {
                txtComment.Text = "Comment the NG reason here";
                txtComment.ForeColor = Color.Gray;
            }
        }

        private void btnExcelB_Click(object sender, EventArgs e)
        {
            //ExcelClass ex = new ExcelClass();
            //ex.ExportToExcelWithXrChart(dtHistory);
            TfSQL sampl = new TfSQL();
            string sample = sampl.sqlExecuteScalarString("select clm_set from tbl_measure_item_2 where inspect = '" + txtInspect.Text + "' and model = '" + txtModel.Text + "'");
            string descrip = sampl.sqlExecuteScalarString("select description from tbl_measure_item_2 where inspect = '" + txtInspect.Text + "' and model = '" + txtModel.Text + "'");
            ExcelClassnew xl = new ExcelClassnew();

            string dtpFrom = dtpLotFrom.Value.ToString("yyyy/MM/dd");
            string dtpTo = dtpLotTo.Value.ToString("yyyy/MM/dd");

            xl.exportExcelB(txtModel.Text, txtLine.Text, txtUser.Text, txtUsl.Text, txtLsl.Text, txtProcess.Text, txtInspect.Text, sample, descrip, dgvHistory, dtpFrom, dtpTo);
        }
    }
}