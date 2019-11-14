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
    public partial class frmPushPull : Form
    {
        
        public delegate void RefreshEventHandler(object sender, EventArgs e);
        public event RefreshEventHandler RefreshEvent;

        
        private delegate void Delegate_RcvDataToBufferDataTable(string data);

        
        DataGridViewButtonColumn edit1;
        DataGridViewButtonColumn edit2;
        DataGridViewButtonColumn edit3;
        DataGridViewButtonColumn edit4;
        DataGridViewButtonColumn edit5;
        DataGridViewButtonColumn Open;
        DataGridViewButtonColumn Delete;

        
        int vAdr;
        string hAdr;
        string _ip;

        const string push = "BE";
        const string pull = "BF";
        const string clr = "AE";
        const string rtn = "\r";
        string command;
        double upp;
        double low;
        bool editMode;
        DataTable dtBuffer;
        DataTable dtHistory;
        DataTable dtUpLowIns;
        int clmSet = 5;
        int rowSet = 1;
        bool adm_flag = false;
        bool fl;

        public frmPushPull()
        {
            InitializeComponent();
            txtComment.ForeColor = Color.Gray;
            txtComment.Text = "Comment the NG reason here";
            this.txtComment.Leave += new System.EventHandler(this.txtComment_Leave);
            this.txtComment.Enter += new System.EventHandler(this.txtComment_Enter);
        }

        
        private void Form3_Load(object sender, EventArgs e)
        {

            // 当フォームの表示場所を指定
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

            dtpSet10daysBefore(dtpLotFrom);

            
            dtpRoundUpHour(dtpLotTo);

            
            dtpRounddownHour(dtpLotInput);

            
            initializePort();

            
            dtBuffer = new DataTable();
            defineBufferAndHistoryTable(ref dtBuffer);
            dtHistory = new DataTable();
            defineBufferAndHistoryTable(ref dtHistory);
            readDtHistory(ref dtHistory);
            dtUpLowIns = new DataTable();
            setLimitSetAndCommand(ref dtUpLowIns);

            
            updateDataGripViews(dtBuffer, dtHistory, ref dgvBuffer, ref dgvHistory);

            
            addButtonsToDataGridView(dgvHistory);
        }

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


        private void readDtHistory(ref DataTable dt)
        {
            dt.Clear();

            string model = txtModel.Text;
            string process = txtProcess.Text;
            string inspect = txtInspect.Text;
            DateTime lotFrom = dtpLotFrom.Value;
            DateTime lotTo = dtpLotTo.Value;
            string line = txtLine.Text;

            string sql = "select inspect, lot, inspectdate, line, qc_user, " +
                                "m1, m2, m3, m4, m5, x, r FROM tbl_measure_history " + 
                         "WHERE model = '" + model + "' AND " +
                                "process = '" + process + "' AND " +
                                "inspect = '" + inspect + "' AND " +
                                "lot >= '" + lotFrom.ToString() + "' AND " +
                                "lot <= '" + lotTo.ToString() + "' AND " +
                                "line = '" + line + "' " +
                         "order by lot, inspectdate, row_set";

            System.Diagnostics.Debug.Print(sql);
            TfSQL tf = new TfSQL();
            tf.sqlDataAdapterFillDatatable(sql, ref dt);
        }

        
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
            if (dt.Rows[0]["instrument"].ToString() == "push") command = push;
            else if (dt.Rows[0]["instrument"].ToString() == "pull") command = pull;
        }
        
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            readDtHistory(ref dtHistory);
            updateDataGripViews(dtBuffer, dtHistory, ref dgvBuffer, ref dgvHistory);
        }

        // サブサブプロシージャ：グリットビュー右端にボタンを追加
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

        // 測定値の新規登録
        private void btnMeasure_Click(object sender, EventArgs e)
        {
            // 編集モードフラグを下し、登録・修正ボタンを「登録」の表示にする
            editMode = false;
            btnRegister.Text = "Register";
            dtpLotInput.Enabled = true;

            // ＨＩＳＴＯＲＹデータグリッドビューのマーキングをクリアする
            colorViewReset(ref dgvHistory);
            colorViewReset(ref dgvBuffer);

            // 新規登録用バッファーテーブル、バッファーグリットビューを初期化する
            dtBuffer.Clear();

            // セット数分の行の設定
            for (int i = 1; i <= rowSet; i++)
            {
                DataRow dr = dtBuffer.NewRow();
                dr["inspect"] = txtInspect.Text;
                dr["lot"] = dtpLotInput.Value;
                dr["inspectdate"] = DateTime.Now;
                dr["line"] = txtLine.Text;
                dr["qc_user"] = txtUser.Text;
                dtBuffer.Rows.Add(dr);
            }

            
            updateDataGripViews(dtBuffer, dtHistory, ref dgvBuffer, ref dgvHistory);

            // 新規登録用グリットビュー（バッファテーブル）へ、ボタンを追加する
            if (dgvBuffer.Columns.Count <= 13)
            {
                addButtonsToDgvBuffer(dgvBuffer, edit1, edit2, edit3, edit4, edit5);
            }
        }

        // 既存測定値の修正
        private void dgvHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int curRow = int.Parse(e.RowIndex.ToString());

            if (dgvHistory.Columns[e.ColumnIndex] == Open && curRow >= 0)
            {
                // 編集モードフラグを立て、登録・修正ボタンを「修正」の表示にする
                editMode = true;
                btnRegister.Text = "Update";
                dtpLotInput.Enabled = true;

                // 新規登録用バッファーテーブル、バッファーグリットビューを初期化し、ボタンに対応する値を格納する
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

                
                updateDataGripViews(dtBuffer, dtHistory, ref dgvBuffer, ref dgvHistory);

                // 新規登録用グリットビュー（バッファテーブル）へ、ボタンを追加する
                if (dgvBuffer.Columns.Count <= 13)
                {
                    addButtonsToDgvBuffer(dgvBuffer, edit1, edit2, edit3, edit4, edit5);
                }

                // 変更ターゲット行を表示する
                if (dgvHistory.Rows.Count >= 1)
                    dgvHistory.FirstDisplayedScrollingRowIndex = curRow;

                // サブプロシージャ：編集中の行をマーキングする
                colorViewForEdit(ref dgvHistory, curRow);
                colorViewForEdit(ref dgvBuffer, 0);
            }
        }

        // サブプロシージャ：編集中の行をマーキングする
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

        // サブプロシージャ：マーキングをクリアする
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

        // サブプロシージャ：スペック外のセルをマーキングする（履歴テーブル）
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

        // サブプロシージャ：スペック外のセルをマーキングする（一時テーブル）
        private void colorBufferViewBySpec(double value, ref DataGridView dgv)
        {
            // 返値の格納先データテーブル、セル番地を非ローカル変数へ格納
            int clm = 0;
            if (hAdr == "m1") clm = 7;
            else if (hAdr == "m2") clm = 9;
            else if (hAdr == "m3") clm = 11;
            else if (hAdr == "m4") clm = 13;
            else if (hAdr == "m5") clm = 15;

            if (value >= low && value <= upp)
                dgv[clm - 1, vAdr].Style.BackColor = Color.FromKnownColor(KnownColor.Window);
            else
                dgv[clm - 1, vAdr].Style.BackColor = Color.Red;
        }

        // 測定値の取り込みが終わったら、データベースへ登録する
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (dtBuffer.Rows.Count <= 0) return;

            string model = txtModel.Text;
            string process = txtProcess.Text;
            string inspect = txtInspect.Text;
            string status = string.Empty;
            DateTime lot = DateTime.Parse(dtBuffer.Rows[0]["lot"].ToString()); ;
            DateTime inspectdate = DateTime.Parse(dtBuffer.Rows[0]["inspectdate"].ToString()); ;
            string line = txtLine.Text;

            // ブァッファーテーブル内で、平均とレンジを計算する
            calculateAverageAndRangeInDataTable(ref dtBuffer);

            // ＩＰＱＣＤＢ 測定履歴テーブルに登録する
            TfSQL tf = new TfSQL();
            if (txtComment.Text != "Comment the NG reason here" && txtComment.Text != string.Empty)
            {
                status = txtComment.Text;
            }

            if (txtComment.Text != string.Empty && dtBuffer.Rows[0]["status"].ToString() != string.Empty) { status = dtBuffer.Rows[0]["status"].ToString(); }

            bool res = tf.sqlMultipleInsert(model, process, inspect, lot, inspectdate, line, status, dtBuffer);

            if (res)
            {
                // バックグラウンドでＰＱＭテーブルに登録する
                DataTable dtTemp = new DataTable();
                dtTemp = dtBuffer.Copy();
                registerMeasurementToPqmTable(dtTemp);

                // 登録済の状態を、当フォームに表示する
                dtBuffer.Clear();
                readDtHistory(ref dtHistory);
                updateDataGripViews(dtBuffer, dtHistory, ref dgvBuffer, ref dgvHistory);

                // 編集モードフラグを立て、登録・修正ボタンを「登録」の表示に戻す
                editMode = false;
                btnRegister.Text = "Register";
                dtpLotInput.Enabled = true;
                txtComment.ResetText();
            }
        }

        // サブプロシージャ: データテーブル内部で、平均とレンジ（最大－最小）を求め、格納する
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

        // サブプロシージャ: ＰＱＭテーブルへの登録（バックグラウンド処理）
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

        // 削除ボタン押下時の処理
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
                // データの削除
                string sql = "delete from tbl_measure_history where " +
                    "model='" + txtModel.Text + "' and " +
                    "inspect='" + txtInspect.Text + "' and " +
                    "lot ='" + dtBuffer.Rows[0]["lot"] + "' and " +
                    "inspectdate ='" + dtBuffer.Rows[0]["inspectdate"] + "' and " +
                    "line ='" + txtLine.Text + "'";

                System.Diagnostics.Debug.Print(sql);
                TfSQL tf = new TfSQL();
                int res = tf.sqlExecuteNonQueryInt(sql, false);

                // バックグラウンドでＰＱＭテーブル内の削除
                DataTable dtTemp = new DataTable();
                dtTemp = dtBuffer.Copy();
                deleteFromPqmTable(dtTemp);

                // 新規登録用バッファーテーブル、バッファーグリットビューを初期化する
                dtBuffer.Clear();

                // 削除後テーブルの再読み込み
                readDtHistory(ref dtHistory);

                // ＨＩＳＴＯＲＹデータグリッドビューのマーキングをクリアする
                colorViewReset(ref dgvHistory);

                
                updateDataGripViews(dtBuffer, dtHistory, ref dgvBuffer, ref dgvHistory);

                // 編集モードフラグを下し、登録・修正ボタンを「登録」の表示にする
                editMode = false;
                btnRegister.Text = "Register";
                dtpLotInput.Enabled = true;
            }
        }

        // サブプロシージャ: ＰＱＭテーブルでの削除（バックグラウンド処理）
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

        // キャンセルボタン押下時の処理
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // グリットビュー上のボタンクリック時、プッシュ＆プルゲージとの通信を開始する。処理部分はデレゲート。
        private void dgvBuffer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int curRow = int.Parse(e.RowIndex.ToString());
            int curClm = int.Parse(e.ColumnIndex.ToString());

            if ((curClm == 7 || curClm == 9 || curClm == 11 || curClm == 13 || curClm == 15) && curRow >= 0)
            {
                // 返値の格納先データテーブル、セル番地を非ローカル変数へ格納
                vAdr = curRow;

                if (curClm == 7) hAdr = "m1";
                else if (curClm == 9) hAdr = "m2";
                else if (curClm == 11) hAdr = "m3";
                else if (curClm == 13) hAdr = "m4";
                else if (curClm == 15) hAdr = "m5";

                // マイナスピーク値の送信要求を行う
                sendCmdPeakReuirement();
            }
        }

        // サブプロシージャ：マイナスピーク値の送信要求を行う
        private void sendCmdPeakReuirement()
        {
            // シリアルポートをオープンしていない場合、処理を行わない.
            if (serialPort1.IsOpen == false) return;

            try
            {
                string cmd = command + rtn;
                serialPort1.Write(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // データ受信が発生したときのイベント処理（デレゲート元）
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //! シリアルポートをオープンしていない場合、処理を行わない.
            if (serialPort1.IsOpen == false) return;

            try
            {
                //　スリープを入れることで、１セットの信号の送信が終わるまで待つ
                Thread.Sleep(100);

                //! 受信コマンドを読み込み、デレゲートする
                string cmd = serialPort1.ReadExisting();
                Invoke(new Delegate_RcvDataToBufferDataTable(RcvDataToBufferDataTable), new Object[] { cmd });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // データ受信が発生したときのイベント処理（デレゲート先）
        private void RcvDataToBufferDataTable(string cmd)
        {
            // マイナスピーク値の送信要求に対する返信の場合のみ処理
            if (cmd.Length != 12) return;

            if (cmd.Substring(0, 2) == command)
            {
                // マイナスピーク値のテキストを、ＤＯＵＢＬＥに変換してＢＵＦＦＥＲＴＡＢＬＥへ格納する
                double value = 0;
                bool b = double.TryParse(cmd.Substring(6, 5), out value);
                dtBuffer.Rows[vAdr][hAdr] = value;

                // ピーク値をクリアするコマンドを送信
                sendReset();

                
                updateDataGripViews(dtBuffer, dtHistory, ref dgvBuffer, ref dgvHistory);

                // スペック外のセルをマーキングする（一時テーブル）
                colorBufferViewBySpec(value, ref dgvBuffer);
            }
        }

        // サブプロシージャ: ピーク値をクリアするコマンドを送信
        private void sendReset()
        {
            // シリアルポートをオープンしていない場合、処理を行わない.
            if (serialPort1.IsOpen == false) return;

            try
            {
                string cmd = clr + rtn;
                serialPort1.Write(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // サブプロシージャ: 新規登録用グリットビュー（ＤＧＶバッファー）へ、ボタンを追加する
        private void addButtonsToDgvBuffer(DataGridView dgv, DataGridViewButtonColumn b1, DataGridViewButtonColumn b2,
            DataGridViewButtonColumn b3, DataGridViewButtonColumn b4, DataGridViewButtonColumn b5)
        {
            b1 = new DataGridViewButtonColumn();
            b1.Text = "Edit";
            b1.UseColumnTextForButtonValue = true;
            b1.Width = 80;
            dgv.Columns.Insert(7, b1);

            b2 = new DataGridViewButtonColumn();
            b2.Text = "Edit";
            b2.UseColumnTextForButtonValue = true;
            b2.Width = 80;
            dgv.Columns.Insert(9, b2);

            b3 = new DataGridViewButtonColumn();
            b3.Text = "Edit";
            b3.UseColumnTextForButtonValue = true;
            b3.Width = 80;
            dgv.Columns.Insert(11, b3);

            b4 = new DataGridViewButtonColumn();
            b4.Text = "Edit";
            b4.UseColumnTextForButtonValue = true;
            b4.Width = 80;
            dgv.Columns.Insert(13, b4);

            b5 = new DataGridViewButtonColumn();
            b5.Text = "Edit";
            b5.UseColumnTextForButtonValue = true;
            b5.Width = 80;
            dgv.Columns.Insert(15, b5);
        }

        // サブプロシージャ: 感知された１つ目のシリアルポートを選択し、オープンする
        private void initializePort()
        {
            // 利用可能なシリアルポート名の配列を取得する
            string[] PortList = SerialPort.GetPortNames();

            // シリアルポート名をコンボボックスにセットする
            cmbPortName.Items.Clear();

            foreach (string PortName in PortList)
                cmbPortName.Items.Add(PortName);

            // 感知されたポートの１つ目を選択し、コンボボックス変更イベントにより、シリアルポートをオープンする
            if (cmbPortName.Items.Count > 0)
                cmbPortName.SelectedIndex = 0;
        }

        // ポート選択用コンボボックス変更時、シリアルポートをオープンする
        private void cmbPortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //! 既にオープンしているポートがある場合は、処理を行わない
            if (serialPort1.IsOpen) return;

            //! オープンするシリアルポートをコンボボックスから取り出す.
            serialPort1.PortName = cmbPortName.SelectedItem.ToString();
            serialPort1.BaudRate = 9600;
            serialPort1.DataBits = 8;
            serialPort1.Parity = Parity.None;
            serialPort1.StopBits = StopBits.One;
            serialPort1.Encoding = Encoding.ASCII;

            try
            {
                serialPort1.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ダイアログの終了処理：シリアルポートをオープンしている場合、クローズする
        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
        }

        // サブサブプロシージャ：ＤＡＴＥＴＩＭＥＰＩＣＫＥＲを１０日前の日付にする
        private void dtpSet10daysBefore(DateTimePicker dtp)
        {
            DateTime dt = dtp.Value.Date.AddDays(-10);
            dtp.Value = dt;
        }

        // サブサブプロシージャ：ＤＡＴＥＴＩＭＥＰＩＣＫＥＲの分以下を切り上げる
        private void dtpRoundUpHour(DateTimePicker dtp)
        {
            DateTime dt = dtp.Value;
            int hour = dt.Hour;
            int minute = dt.Minute;
            int second = dt.Second;
            int millisecond = dt.Millisecond;
            dtp.Value = dt.AddHours(1).AddMinutes(-minute).AddSeconds(-second).AddMilliseconds(-millisecond);
        }

        // サブサブプロシージャ：ＤＡＴＥＴＩＭＥＰＩＣＫＥＲの分以下を下げる
        private void dtpRounddownHour(DateTimePicker dtp)
        {
            DateTime dt = dtp.Value;
            int hour = dt.Hour;
            int minute = dt.Minute;
            int second = dt.Second;
            int millisecond = dt.Millisecond;
            dtp.Value = dt.AddMinutes(-minute).AddSeconds(-second).AddMilliseconds(-millisecond);
        }

        // ＸＲグラフ作成ボタン押下時の処理  
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

        // サブサブプロシージャ：ＸＲ管理図用データテーブルの生成      
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