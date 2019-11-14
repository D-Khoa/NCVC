using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Security.Permissions;
using Npgsql;

namespace IpqcDB
{
    public partial class frmItem : Form
    {
        //親フォームForm5へ、イベント発生を連絡（デレゲート）
        public delegate void RefreshEventHandler(object sender, EventArgs e);
        public event RefreshEventHandler RefreshEvent;
        public static string mdl_p;
        public static string mdl;

        //データテーブル
        DataTable dtInspectItems;
        DataTable dtLine;

        
        DataGridViewButtonColumn line1;
        DataGridViewButtonColumn line2;
        DataGridViewButtonColumn line3;

        //その他非ローカル変数
        bool load_cmb = true;
        bool adm_flag = false;

        public frmItem()
        {
            InitializeComponent();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            //フォームの場所を指定
            this.Left = 0;
            this.Top = 0;
            dtInspectItems = new DataTable();
            dtLine = new DataTable();
            defineItemTable(ref dtInspectItems);
            defineLineTable(ref dtLine);
            getComboListFromDB(ref cmbModel);
            updateDataGripViews(ref dgvMeasureItem, true);
            load_cmb = false;
            //  loadline();
            TfSQL flag = new TfSQL();
            bool fl = flag.sqlExecuteScalarBool("select admin_flag from qc_user_temp where qcuser = '" + txtUser.Text + "'");
            if (fl == true) adm_flag = true;
            

            //Exit app if user has been log in by another device and log in again
            string ipadd = flag.sqlExecuteScalarString("select ip_address from qc_user_temp where qcuser = '" + txtUser.Text + "'");
            if (ipadd == "null") flag.sqlExecuteScalarString("UPDATE qc_user_temp SET ip_address = '" + _ip + "' where qcuser = '" + txtUser.Text + "'");
            if (ipadd != "null" && ipadd != _ip)
            {
                DialogResult res = MessageBox.Show("User is logged in " + _ip + "," + System.Environment.NewLine +
                                    "Do you want to log out and log in again ?", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Question);
                if (res == DialogResult.OK) Application.Exit();
            }
        }

        public string _ip;
        public void updateControls(string user, string ip)
        {
            txtUser.Text = user;
            _ip = ip;
        }

        // サブプロシージャ：検査項目テーブルを定義する
        private void defineItemTable(ref DataTable dt)
        {
            dt.Columns.Add("no", Type.GetType("System.String"));
            dt.Columns.Add("model", Type.GetType("System.String"));
            dt.Columns.Add("process", Type.GetType("System.String"));
            dt.Columns.Add("inspect", Type.GetType("System.String"));
            dt.Columns.Add("description", Type.GetType("System.String"));
            dt.Columns.Add("instrument", Type.GetType("System.String"));
        }

        // サブプロシージャ：ラインテーブルを定義する
        private void defineLineTable(ref DataTable dt)
        {
            dt.Columns.Add("model", Type.GetType("System.String"));
            dt.Columns.Add("line", Type.GetType("System.String"));
        }

        // サブプロシージャ：型式コンボボックスへ候補を取り込む
        public void getComboListFromDB(ref ComboBox cmb)
        {
            string sql = "select model from tbl_model_dbplace order by model";
            System.Diagnostics.Debug.Print(sql);
            TfSQL tf = new TfSQL();
            tf.getComboBoxData(sql, ref cmb);

            if (cmbModel.Items.Count > 0)
                cmbModel.SelectedIndex = 0;
        }

        
        public void updateDataGripViews(ref DataGridView dgv, bool load)
        {
            dtInspectItems.Clear();
            string model = cmbModel.Text;
            string sql = "select no, model, process, inspect, description, instrument from tbl_measure_item_2 where model='"
                + model + "' order by no, process, inspect";
            System.Diagnostics.Debug.Print(sql);
            TfSQL tf = new TfSQL();
            tf.sqlDataAdapterFillDatatable(sql, ref dtInspectItems);

            
            dgv.DataSource = dtInspectItems;

            // グリットビュー右端にボタンを追加
            addButtonsToDataGridView(dgv);

            // 列幅の調整
            //dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.Columns["no"].Width = 50;
            dgv.Columns["model"].Width = 100;
            dgv.Columns["process"].Width = 50;
            dgv.Columns["inspect"].Width = 100;
            dgv.Columns["description"].Width = 600;
            dgv.Columns["instrument"].Width = 80;
         }
        
        // サブサブプロシージャ：グリットビュー右端にボタンを追加
        private void addButtonsToDataGridView(DataGridView dgv)
        {
            dtLine.Clear();
            string model = cmbModel.Text;
            string sql = "select line FROM tbl_model_line where model='" + model + "' order by line";
            System.Diagnostics.Debug.Print(sql);
            TfSQL tf = new TfSQL();
            tf.sqlDataAdapterFillDatatable(sql, ref dtLine);

            if (dtLine.Rows.Count == 0) return;

            if (line1 != null)
            {
                dgv.Columns.Remove(line1);
                line1 = null;
            }

            if (line2 != null)
            {
                dgv.Columns.Remove(line2);
                line2 = null;
            }

            if (line3 != null)
            {
                dgv.Columns.Remove(line3);
                line3 = null;
            } 

            if (dtLine.Rows.Count >= 1)
            {
                line1 = new DataGridViewButtonColumn();
                line1.Name = "line";
                line1.Text = dtLine.Rows[0]["line"].ToString();
                line1.UseColumnTextForButtonValue = true;
                line1.Width = 45;
                dgv.Columns.Add(line1);
            }

            if (dtLine.Rows.Count >= 2)
            {
                line2 = new DataGridViewButtonColumn();
                line2.Name = "line";
                line2.Text = dtLine.Rows[1]["line"].ToString();
                line2.UseColumnTextForButtonValue = true;
                line2.Width = 45;
                dgv.Columns.Add(line2);            
            }

            if (dtLine.Rows.Count >= 3)
            {
                line3 = new DataGridViewButtonColumn();
                line3.Name = "line";
                line3.Text = dtLine.Rows[2]["line"].ToString();
                line3.UseColumnTextForButtonValue = true;
                line3.Width = 45;
                dgv.Columns.Add(line3);
            }
        }

        // 型式コンボボックス変更時の処理
        private void cmbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (load_cmb) return; //ロード時は処理を行わない

            TfSQL mdlp = new TfSQL();
            mdl_p = mdlp.sqlExecuteScalarString("select mdl_per from qc_user_temp where qcuser = '" + txtUser.Text + "'");
            mdl = mdlp.sqlExecuteScalarString("select dbplace from tbl_model_dbplace where model = '" + cmbModel.Text + "'");
            if (adm_flag == true)
            {
                if (mdl.Contains(mdl_p))
                {
                    btnEditMaster.Enabled = true;
                }
                else if (mdl_p == "all")
                {
                    btnEditMaster.Enabled = true;
                }
                else btnEditMaster.Enabled = false;
            }

            updateDataGripViews(ref dgvMeasureItem, false);
        }

        // グリットビュー上のボタンクリック時、フォーム２・３・４を開く（フォーム２はデレゲートあり）
        private void dgvBoxId_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (!(senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)) return;

            //選択したボタンに該当する情報を保持する
            int curRow = int.Parse(e.RowIndex.ToString());
            string model = dgvMeasureItem["model", curRow].Value.ToString();
            string process = dgvMeasureItem["process", curRow].Value.ToString();
            string inspect = dgvMeasureItem["inspect", curRow].Value.ToString();
            string line = ((DataGridViewButtonColumn)senderGrid.Columns[e.ColumnIndex]).Text;
            string instrument = dgvMeasureItem["instrument", curRow].Value.ToString();
            string user = txtUser.Text;

            //測定器に該当するフォームを開く
            //if (instrument == "mmx" || instrument == "mmy")
            //{
            //    frmMmAuto fA = new frmMmAuto();
            //    fA.updateControls(model, process, inspect, line, user);
            //    fA.Show();
            //}
            if (instrument == "push" || instrument == "pull")
            {
                frmPushPull fP = new frmPushPull();
                fP.updateControls(model, process, inspect, line, user, _ip);
                fP.Show();
            }
            else if (instrument == "hr-20")
            {
                frmScale fS = new frmScale();
                fS.updateControls(model, process, inspect, line, user, _ip);
                fS.Show();
            }
            else if (instrument == "hr-21")
            {
                frmScale fS = new frmScale();
                fS.updateControls(model, process, inspect, line, user, _ip);
                fS.Show();
            }
            else if (instrument == "hiohm")
            {
                frmHioki fH = new frmHioki();
                fH.updateControls(model, process, inspect, line, user, _ip);
                fH.Show();
            }
            else
            {
                frmManual fM = new frmManual();
                fM.updateControls(model, process, inspect, line, user, _ip);
                fM.Show();
            }
        }

        //Form1を閉じる際、非表示になっている親フォームForm5を閉じる
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //親フォームForm5を閉じるよう、デレゲートイベントを発生させる
            this.RefreshEvent(this, new EventArgs());
        }

        // キャンセルボタン押下時の処理
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // 検査項目マスター編集用フォームの呼び出し
        private void btnEditMaster_Click(object sender, EventArgs e)
        {
            if (adm_flag == false) return;
            if (TfGeneral.checkOpenFormExists("frmItemMaster")) return;

            frmItemMaster fM = new frmItemMaster(cmbModel.Text);
            //子イベントをキャッチして、データグリッドを更新する
            fM.RefreshEvent += delegate (object sndr, EventArgs excp)
            {
                updateDataGripViews(ref dgvMeasureItem, false);
            };
            fM.Show();
        }
    }
}