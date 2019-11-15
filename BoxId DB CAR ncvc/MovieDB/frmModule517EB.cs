using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Linq;


namespace BoxIdDb
{
    public partial class frmModule517EB : Form
    {
        //�e�t�H�[��frmBox�փC�x���g������A���i�f���Q�[�g�j
        public delegate void RefreshEventHandler(object sender, EventArgs e);
        public event RefreshEventHandler RefreshEvent;

        // �v�����g�p�e�L�X�g�t�@�C���̕ۑ��p�t�H���_���A��{�ݒ�t�@�C���Őݒ肷��
        string appconfig = @"\\192.168.193.1\barcode$\BoxId Printer vc5\info.ini";
        string directory = @"C:\Users\takusuke.fujii\Desktop\Auto Print\\";

        //���̑��A�񃍁[�J���ϐ�
        bool formEditMode;
        bool formReturnMode;
        bool formAddMode;
        string user;
        string m_model;
        string m_lot, m_comp_ser;
        int okCount;
        bool inputBoxModeOriginal;
        string testerTableThisMonth, testerThisMonth;
        string testerTableLastMonth, testerLastMonth;
        string tableThisMonth;
        string tableLastMonth;
        DataTable dtOverall;
        //DataTable dtTester;
        int limit1 = 60;
        public int limit2 = 0;
        bool sound;

        // �R���X�g���N�^
        public frmModule517EB()
        {
            InitializeComponent();
        }

        // ���[�h���̏���
        private void frmModule_Load(object sender, EventArgs e)
        {
            //txtCarton.Enabled = false;
            // �ҏW���[�h�p���[�U�[����ێ�����
            user = txtUser.Text;

            // ���[�U�[�X���ݒ肷��k�h�l�h�s���A�e�L�X�g�{�b�N�X�֕\��
            txtLimit.Text = limit2.ToString();

            // �v�����g�p�t�@�C���̕ۑ���t�H���_�A���̑��ݒ���A�ǂݍ���
            directory =  /*@"C:\Users\mt-qc20\Desktop\print\";*/ readIni("TARGET DIRECTORY", "DIR", appconfig);

            // ���t�H�[���̕\���ꏊ���w��
            this.Left = 250;
            this.Top = 20;

            // �e�폈���p�̃e�[�u���𐶐�
            dtOverall = new DataTable();
            defineAndReadDtOverall(ref dtOverall);
            //dtTester = new DataTable();
            //defineAndReaddtTester(ref dtTester);

            // �k�h�l�h�s�̐������Œ����K�v����
            if (!formEditMode)
            {
                // �f�[�^�e�[�u���̐擪�s�̃V���A������A�k�h�l�h�s�𔻒f����
                if (dtOverall.Rows.Count >= 0)
                {
                    limit1 = 60;
                }
            }

            // �O���b�g�r���[�̍X�V
            updateDataGridViews(dtOverall, ref dgvInline);

            // �V���A���p�e�L�X�g�{�b�N�X�̐������Œ����K�v����
            if (!formEditMode)
            {
                txtProductSerial.Enabled = false;
            }
        }

        // �ݒ�e�L�X�g�t�@�C���̓ǂݍ���
        private string readIni(string s, string k, string cfs)
        {
            StringBuilder retVal = new StringBuilder(255);
            string section = s;
            string key = k;
            string def = String.Empty;
            int size = 255;
            //get the value from the key in section
            int strref = GetPrivateProfileString(section, key, def, retVal, size, cfs);
            return retVal.ToString();
        }
        // Windows API ���C���|�[�g
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filepath);

        // �T�u�v���V�[�W���F�e�t�H�[���ŌĂяo���A�e�t�H�[���̏����A�e�L�X�g�{�b�N�X�֊i�[���Ĉ����p��
        public void updateControls(string frmName, string boxId, DateTime registDate, string serialNo, string invoice, string user, bool editMode, bool returnMode)
        {
            lblFrmName.Text = frmName;
            txtBoxId.Text = boxId;
            txtProductSerial.Text = serialNo;
            txtUser.Text = user;
            txtInvoice.Text = invoice;
            if (boxId != "")
            {
                string[] box_arr = boxId.Split('-');

                limit1 = 60;
                txtCarton.Text = box_arr[2];
            }

            //txtCarton.Enabled = editMode;
            txtProductSerial.Enabled = editMode;
            //cmbModel.Enabled = editMode;
            btnRegisterBoxId.Enabled = !editMode;
            btnPrint.Visible = !editMode;
            //btnDeleteSelection.Visible = editMode;
            formEditMode = editMode;
            formReturnMode = returnMode;

            this.Text = editMode ? "Product Serial - Edit Mode" : "Product Serial - Browse Mode";
            if (editMode && user == "admin" || editMode && user == "User_9")
            {
                btnChangeLimit.Visible = true;
                txtLimit.Visible = true;
            }
            if (!editMode && user == "admin" || !editMode && user == "User_9")
            {
                //btnAddSerial.Visible = true;
                btnCancelBoxid.Visible = true;
                btnChangeLimit.Visible = true;
                //btnDeleteSerial.Visible = true;
            }
        }

        // �T�u�v���V�[�W���F�c�a����̂c�s�n�u�d�q�`�k�k�ւ̓ǂݍ���
        private void defineAndReadDtOverall(ref DataTable dt)
        {
            string boxId = txtBoxId.Text;

            dt.Columns.Add("serialno", Type.GetType("System.String"));
            dt.Columns.Add("model", Type.GetType("System.String"));
            dt.Columns.Add("lot", Type.GetType("System.String"));
            dt.Columns.Add("cir_ccw", Type.GetType("System.String"));
            dt.Columns.Add("cg_ccw", Type.GetType("System.String"));
            dt.Columns.Add("cnr_ccw", Type.GetType("System.String"));
            dt.Columns.Add("aio_ccw", Type.GetType("System.String"));
            dt.Columns.Add("ano_ccw", Type.GetType("System.String"));
            dt.Columns.Add("air_ccw", Type.GetType("System.String"));
            dt.Columns.Add("anr_ccw", Type.GetType("System.String"));
            dt.Columns.Add("ais_ccw", Type.GetType("System.String"));
            dt.Columns.Add("judge", Type.GetType("System.String"));
            dt.Columns.Add("return", Type.GetType("System.String"));

            if (!formEditMode)
            {
                string sql;
                //if (VBS.Left(boxId, 4) == "517C" || VBS.Left(boxId, 4) == "517D" || VBS.Left(boxId, 4) == "517E")
                //{
                sql = "select serialno, model, lot, cir_ccw, cg_ccw, cnr_ccw, aio_ccw, ano_ccw, air_ccw, anr_ccw, ais_ccw, judge, return " +
                    "FROM product_serial_517eb WHERE boxid='" + boxId + "'";
                //}
                //else
                //{
                //    sql = "select serialno, model, lot, current_ma, vibration_g, vibration_m_s2, vibration10, frequency_hz, aio_cw, ano_cw, air_cw, anr_cw, ais_cw, judge, return " +
                //        "FROM product_serial_517eb WHERE boxid='" + boxId + "'";
                //}
                TfSQL tf = new TfSQL();
                System.Diagnostics.Debug.Print(sql);
                tf.sqlDataAdapterFillDatatable(sql, ref dt);
            }
        }

        // �T�u�v���V�[�W���F�f�[�^�O���b�g�r���[�̍X�V
        private void updateDataGridViews(DataTable dt1, ref DataGridView dgv1)
        {
            // ���͗p�{�b�N�X�̗L���E������ێ����A����̏ꍇ���ꎞ�I�ɖ����ɂ���
            inputBoxModeOriginal = txtProductSerial.Enabled;
            txtProductSerial.Enabled = false;

            // �f�[�^�O���b�g�r���[�ւc�s�`�`�s�`�a�k�d���i�[
            updateDataGridViewsSub(dt1, ref dgv1);

            // �e�X�g���ʂ��e�`�h�k�܂��̓��R�[�h�Ȃ��̃V���A�����}�[�L���O���� 
            colorViewForFailAndBlank(ref dgv1);
            // colorViewForFailAndBlank(ref dgv2);

            // �d�����R�[�h�A����тP�Z���Q�d���͂��}�[�L���O����
            colorViewForDuplicateSerial(ref dgv1);
            // colorViewForDuplicateSerial(ref dgv2);

            // �Q�ȏ�̃R���t�B�O�����݂���ꍇ�Ɍx�����A�f�[�^�O���b�g�r���[���}�[�N����

            //colorMixedLot(dt1, ref dgv1);

            //�s�w�b�_�[�ɍs�ԍ���\������i�C�����C���j
            for (int i = 0; i < dgv1.Rows.Count; i++)
                dgv1.Rows[i].HeaderCell.Value = (i + 1).ToString();

            //�s�w�b�_�[�̕����������߂���i�C�����C���j
            dgv1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            // ��ԉ��̍s��\������i�C�����C���j
            if (dgv1.Rows.Count >= 1)
                dgv1.FirstDisplayedScrollingRowIndex = dgv1.Rows.Count - 1;

            // ���͗p�{�b�N�X�̗L���E���������֖߂�
            txtProductSerial.Enabled = inputBoxModeOriginal;

            // ���݂̈ꎞ�o�^������ϐ��֕ێ�����
            okCount = getOkCount(dt1);
            txtOkCount.Text = okCount.ToString() + "/" + limit1.ToString();

            // �X�L�������������ɂk�h�l�h�s�ɒB���Ă���ꍇ�́A���͗p�{�b�N�X�𖳌��ɂ���
            if (okCount == limit1)
            {
                txtProductSerial.Enabled = false;
            }
            else
            {
                txtProductSerial.Enabled = true;
            }

            // �O���b�g���R�[�h�����ƁAokCount������v���Ă���ꍇ�ɁA�o�^�{�^����L���ɂ���
            if (okCount == limit1 && dgv1.Rows.Count == limit1)
            {
                btnRegisterBoxId.Enabled = true;
            }
            else
            {
                btnRegisterBoxId.Enabled = false;
            }
        }

        // �T�u�v���V�[�W���F�V���A���ԍ��d���Ȃ��̂o�`�r�r�����擾����
        private int getOkCount(DataTable dt)
        {
            if (dt.Rows.Count <= 0) return 0;
            DataTable distinct = dt.DefaultView.ToTable(true, new string[] { "serialno", "judge" });
            DataRow[] dr = distinct.Select("judge = 'PASS'");
            int dist = dr.Length;
            return dist;
        }

        // �T�u�v���V�[�W���F���C���f�[�^�O���b�g�r���[�փf�[�^�e�[�u�����i�[�A����яW�v�O���b�h�r���[�̍쐬
        private void updateDataGridViewsSub(DataTable dt1, ref DataGridView dgv1)
        {
            dgv1.DataSource = dt1;
            dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            string[] criteriaDateCode = getLotArray(dt1);
            makeDatatableSummary(dt1, ref dgvDateCode, criteriaDateCode, "lot");
        }

        private string[] getLotArray(DataTable dt0)
        {
            DataTable dt1 = dt0.Copy();
            DataView dv = dt1.DefaultView;
            dv.Sort = "lot";
            DataTable dt2 = dv.ToTable(true, "lot");
            string[] array = new string[dt2.Rows.Count + 1];
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                array[i] = dt2.Rows[i]["lot"].ToString();
            }
            array[dt2.Rows.Count] = "Total";
            return array;
        }

        // �T�u�T�u�v���V�[�W���F�W�v�p�̃f�[�^�e�[�u�����A�f�[�^�O���b�h�r���[�Ɋi�[
        public void makeDatatableSummary(DataTable dt0, ref DataGridView dgv, string[] criteria, string header)
        {
            DataTable dt1 = new DataTable();
            DataRow dr = dt1.NewRow();
            Int32 count;
            Int32 total = 0;
            string condition;

            for (int i = 0; i < criteria.Length; i++)
            {
                dt1.Columns.Add(criteria[i], typeof(Int32));
                condition = header + " = '" + criteria[i] + "'";
                count = dt0.Select(condition).Length;
                total += count;
                dr[criteria[i]] = count;
                if (criteria[i] == "Total") dr[criteria[i]] = total;
                if (criteria[i] == "No Data") dr[criteria[i]] = dgvInline.Rows.Count - total;
            }
            dt1.Rows.Add(dr);

            dgv.Columns.Clear();
            dgv.DataSource = dt1;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void colorMixedLot(DataTable dt, ref DataGridView dgv)
        {
            if (dt.Rows.Count <= 0) return;

            DataTable distinct1 = dt.DefaultView.ToTable(true, new string[] { "lot" });

            if (distinct1.Rows.Count == 1)
                m_lot = distinct1.Rows[0]["lot"].ToString();

            if (distinct1.Rows.Count >= 2)
            {
                string A = distinct1.Rows[0]["lot"].ToString();
                string B = distinct1.Rows[1]["lot"].ToString();
                int a = distinct1.Select("lot = '" + A + "'").Length;
                int b = distinct1.Select("lot = '" + B + "'").Length;

                // �����̑����R���t�B�O���A���̔��̃��C�����f���Ƃ���
                m_lot = a > b ? A : B;

                // �����̏��Ȃ��ق��̃��C�����f���������擾���A�Z���Ԓn����肵�ă}�[�N����
                string C = a < b ? A : B;
                int c = -1;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["lot"].ToString() == C) { c = i; }
                }

                if (c != -1)
                {
                    dgv["col_lot", c].Style.BackColor = Color.Red;
                    soundAlarm();
                }
                else
                {
                    dgv.Columns["col_lot"].DefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Window);
                }
            }
        }

        // �T�u�v���V�[�W���F�e�X�g���ʂ��e�`�h�k�܂��̓��R�[�h�Ȃ��̃V���A�����}�[�L���O����
        private void colorViewForFailAndBlank(ref DataGridView dgv)
        {
            int row = dgv.Rows.Count;
            for (int i = 0; i < row; ++i)
            {
                // �e�X�g���ʂ̃}�[�L���O
                if (dgv["col_judge", i].Value.ToString() == "FAIL" || dgv["col_judge", i].Value.ToString() == "PLS NG" || String.IsNullOrEmpty(dgv["col_judge", i].Value.ToString()))
                {
                    dgv[3, i].Style.BackColor = Color.Red;
                    dgv[4, i].Style.BackColor = Color.Red;
                    dgv[5, i].Style.BackColor = Color.Red;
                    dgv[6, i].Style.BackColor = Color.Red;
                    dgv[7, i].Style.BackColor = Color.Red;
                    dgv[8, i].Style.BackColor = Color.Red;
                    dgv[9, i].Style.BackColor = Color.Red;
                    dgv[10, i].Style.BackColor = Color.Red;

                    if (dgv.Name == "dgvInline") tabControl1.SelectedIndex = 1;
                    else tabControl1.SelectedIndex = 0;

                    soundAlarm();
                }
                else
                {
                    dgv.Rows[i].InheritedStyle.BackColor = Color.FromKnownColor(KnownColor.Window);

                    tabControl1.SelectedIndex = 0;
                }
            }
        }

        // �T�u�v���V�[�W���F�d�����R�[�h�A�܂��͂P�Z���Q�d���͂��}�[�L���O����
        private void colorViewForDuplicateSerial(ref DataGridView dgv)
        {
            DataTable dt = ((DataTable)dgv.DataSource).Copy();
            if (dt.Rows.Count <= 0) return;

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                string serial;
                //if (cmbModel.Text == "LA20_517CB")
                //{
                //    serial = VBS.Mid(dgv["col_serial_no", i].Value.ToString(), 2, 21);
                //}
                //else 
                serial = dgv["col_serial_no", i].Value.ToString();

                DataRow[] dr = dt.Select("serialno = '" + serial + "'");
                if (dr.Length >= 2 || dgv["col_serial_no", i].Value.ToString().Length >= 25)
                {
                    if (dgv.Name == "dgvInline") tabControl1.SelectedIndex = 1;
                    else tabControl1.SelectedIndex = 0;

                    dgv["col_serial_no", i].Style.BackColor = Color.Red;
                    soundAlarm();
                }
                else
                {
                    dgv["col_serial_no", i].Style.BackColor = Color.FromKnownColor(KnownColor.Window);
                    tabControl1.SelectedIndex = 0;
                }
            }
        }

        // �V���A�����X�L�������ꂽ���̏���
        private void txtProductSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbModel.Text == "")
                {
                    MessageBox.Show("Please select model name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbModel.Focus();
                    return;
                }

                // ���͗p�e�L�X�g�{�b�N�X��ҏW�s�ɂ��āA�������̃X�L�������u���b�N����
                txtProductSerial.Enabled = false;
                string serial = txtProductSerial.Text;

                decideReferenceTable();

                if (serial != String.Empty)
                {
                    string model = cmbModel.Text;
                    //Data OQC
                    string sql2 = "SELECT inspect, inspectdata, judge, inspectdate FROM " + testerThisMonth + " WHERE serno = '" + serial + "' AND inspect in ('CIR_CCW', 'CG_CCW', 'CNR_CCW') AND judge = '0' order by inspectdate desc, inspect asc";
                    string sql3 = "SELECT inspect, inspectdata, judge, inspectdate FROM " + testerLastMonth + " WHERE serno = '" + serial + "' AND inspect in ('CIR_CCW', 'CG_CCW', 'CNR_CCW') AND judge = '0' order by inspectdate desc, inspect asc";

                    //string sql5 = "(SELECT serno FROM " + testerTableThisMonth + " WHERE lot = '" + comp_ser + "' and process = 'NO53' limit 1) UNION ALL (SELECT serno FROM " + testerTableLastMonth + " WHERE lot = '" + comp_ser + "' and process = 'NO53' limit 1)";

                    System.Diagnostics.Debug.Print(System.Environment.NewLine + sql2);
                    DataTable dt2 = new DataTable();
                    TfSQL tf = new TfSQL();
                    tf.sqlDataAdapterFillDatatableOqc517EB(sql2, ref dt2);

                    if (dt2.Rows.Count == 0)
                    {
                        tf.sqlDataAdapterFillDatatableOqc517EB(sql3, ref dt2);
                    }
                    string sql1;
                    //Data INLINE
                    //switch (VBS.Mid(model, 6, 4))
                    //{
                    //    case "517D":
                    //    case "517C":
                    //    case "517E":
                    sql1 = "select serno, " +
                    "MAX(case inspect when 'AIO_CCW' then inspectdata else null end) as AIO_CCW," +
                    "MAX(case inspect when 'ANO_CCW' then inspectdata else null end) as ANO_CCW," +
                    "MAX(case inspect when 'AIR_CCW' then inspectdata else null end) as AIR_CCW," +
                    "MAX(case inspect when 'ANR_CCW' then inspectdata else null end) as ANR_CCW," +
                    "MAX(case inspect when 'AIS_CCW' then inspectdata else null end) as AIS_CCW" +
                    " FROM" +
                    " (select d.serno, c.inspectdate, c.inspect, c.inspectdata, c.judge from (select SERNO, INSPECTDATE, INSPECT, INSPECTDATA, JUDGE from (select SERNO, INSPECT, INSPECTDATA, JUDGE, max(inspectdate) as inspectdate, row_number() OVER(PARTITION BY inspect ORDER BY max(inspectdate) desc) as flag from (select * from " + tableThisMonth +
                    " WHERE serno = (select lot from " + testerTableThisMonth + " where process = 'NO53' and serno = '" + serial + "' LIMIT 1) and inspect in ('AIO_CCW','AIR_CCW','AIS_CCW','ANO_CCW','ANR_CCW') AND JUDGE = '0')" + "a group by SERNO, INSPECTDATE , INSPECT , INSPECTDATA , JUDGE ) b where flag = 1) c," + "(select serno from " + testerTableThisMonth + " where serno = '" + serial + "')d" +
                    " group by d.serno, c.inspectdate, c.inspect, c.inspectdata, c.judge) e " +
                    " GROUP BY serno" +

                    " UNION ALL" +

                    " select serno, " +
                    "MAX(case inspect when 'AIO_CCW' then inspectdata else null end) as AIO_CCW," +
                    "MAX(case inspect when 'ANO_CCW' then inspectdata else null end) as ANO_CCW," +
                    "MAX(case inspect when 'AIR_CCW' then inspectdata else null end) as AIR_CCW," +
                    "MAX(case inspect when 'ANR_CCW' then inspectdata else null end) as ANR_CCW," +
                    "MAX(case inspect when 'AIS_CCW' then inspectdata else null end) as AIS_CCW" +
                    " FROM" +
                    " (select d.serno, c.inspectdate, c.inspect, c.inspectdata, c.judge from (select SERNO, INSPECTDATE, INSPECT, INSPECTDATA, JUDGE from (select SERNO, INSPECT, INSPECTDATA, JUDGE, max(inspectdate) as inspectdate, row_number() OVER(PARTITION BY inspect ORDER BY max(inspectdate) desc) as flag from (select * from " + tableLastMonth +
                    " WHERE serno = (select lot from " + testerTableThisMonth + " where process = 'NO53' and serno = '" + serial + "' LIMIT 1) and inspect in ('AIO_CCW','AIR_CCW','AIS_CCW','ANO_CCW','ANR_CCW') AND JUDGE = '0')" + "a group by SERNO, INSPECTDATE , INSPECT , INSPECTDATA , JUDGE ) b where flag = 1) c," + "(select serno from " + testerTableThisMonth + " where serno = '" + serial + "')d" +
                    " group by d.serno, c.inspectdate, c.inspect, c.inspectdata, c.judge) e " +
                    " GROUP BY serno" +

                    " UNION ALL" +

                    " select serno, " +
                    "MAX(case inspect when 'AIO_CCW' then inspectdata else null end) as AIO_CCW," +
                    "MAX(case inspect when 'ANO_CCW' then inspectdata else null end) as ANO_CCW," +
                    "MAX(case inspect when 'AIR_CCW' then inspectdata else null end) as AIR_CCW," +
                    "MAX(case inspect when 'ANR_CCW' then inspectdata else null end) as ANR_CCW," +
                    "MAX(case inspect when 'AIS_CCW' then inspectdata else null end) as AIS_CCW" +
                    " FROM" +
                    " (select d.serno, c.inspectdate, c.inspect, c.inspectdata, c.judge from (select SERNO, INSPECTDATE, INSPECT, INSPECTDATA, JUDGE from (select SERNO, INSPECT, INSPECTDATA, JUDGE, max(inspectdate) as inspectdate, row_number() OVER(PARTITION BY inspect ORDER BY max(inspectdate) desc) as flag from (select * from " + tableLastMonth +
                    " WHERE serno = (select lot from " + testerTableLastMonth + " where process = 'NO53' and serno = '" + serial + "' LIMIT 1) and inspect in ('AIO_CCW','AIR_CCW','AIS_CCW','ANO_CCW','ANR_CCW') AND JUDGE = '0')" + "a group by SERNO, INSPECTDATE , INSPECT , INSPECTDATA , JUDGE ) b where flag = 1) c," + "(select serno from " + testerTableLastMonth + " where serno = '" + serial + "')d" +
                    " group by d.serno, c.inspectdate, c.inspect, c.inspectdata, c.judge) e " +
                    " GROUP BY serno";

                    //    break;
                    ////case "517E":
                    //default:
                    //    sql1 = "select serno, MC, " +
                    //    "MAX(case inspect when 'AIO_CCW' then inspectdata else null end) as AIO_CCW," +
                    //    "MAX(case inspect when 'ANO_CCW' then inspectdata else null end) as ANO_CCW," +
                    //    "MAX(case inspect when 'AIR_CCW' then inspectdata else null end) as AIR_CCW," +
                    //    "MAX(case inspect when 'ANR_CCW' then inspectdata else null end) as ANR_CCW," +
                    //    "MAX(case inspect when 'AIS_CCW' then inspectdata else null end) as AIS_CCW" +
                    //    " FROM" +
                    //    " (select c.serno, d.lot as MC, c.inspectdate, c.inspect, c.inspectdata, c.judge from (select SERNO, INSPECTDATE, INSPECT, INSPECTDATA, JUDGE from (select SERNO, INSPECT, INSPECTDATA, JUDGE, max(inspectdate) as inspectdate, row_number() OVER(PARTITION BY inspect ORDER BY max(inspectdate) desc) as flag from (select * from " + tableThisMonth +
                    //    " WHERE serno = '" + serial + "' and inspect in ('AIO_CCW','AIR_CCW','AIS_CCW','ANO_CCW','ANR_CCW') AND JUDGE = '0') a group by SERNO, INSPECTDATE , INSPECT , INSPECTDATA , JUDGE ) b where flag = 1) c,(select lot from " + testerTableThisMonth + " where serno = '" + serial + "' and process = 'NO53' limit 1) d" +
                    //    " group by c.serno, d.lot, c.inspectdate, c.inspect, c.inspectdata, c.judge) e " +
                    //    " GROUP BY serno, MC" +

                    //    " UNION ALL" +

                    //    " select serno, MC, " +
                    //    "MAX(case inspect when 'AIO_CCW' then inspectdata else null end) as AIO_CCW," +
                    //    "MAX(case inspect when 'ANO_CCW' then inspectdata else null end) as ANO_CCW," +
                    //    "MAX(case inspect when 'AIR_CCW' then inspectdata else null end) as AIR_CCW," +
                    //    "MAX(case inspect when 'ANR_CCW' then inspectdata else null end) as ANR_CCW," +
                    //    "MAX(case inspect when 'AIS_CCW' then inspectdata else null end) as AIS_CCW" +
                    //    " FROM" +
                    //    " (select c.serno, d.lot as MC, c.inspectdate, c.inspect, c.inspectdata, c.judge from (select SERNO, INSPECTDATE, INSPECT, INSPECTDATA, JUDGE from (select SERNO, INSPECT, INSPECTDATA, JUDGE, max(inspectdate) as inspectdate, row_number() OVER(PARTITION BY inspect ORDER BY max(inspectdate) desc) as flag from (select * from " + tableLastMonth +
                    //    " WHERE serno = '" + serial + "' and inspect in ('AIO_CCW','AIR_CCW','AIS_CCW','ANO_CCW','ANR_CCW') AND JUDGE = '0') a group by SERNO, INSPECTDATE , INSPECT , INSPECTDATA , JUDGE ) b where flag = 1) c, (select lot from " + testerTableThisMonth + " where serno = '" + serial + "' and process = 'NO53' limit 1) d" +
                    //    " group by c.serno, d.lot, c.inspectdate, c.inspect, c.inspectdata, c.judge) e " +
                    //    " GROUP BY serno, MC";
                    //    break;

                    // }

                    System.Diagnostics.Debug.Print(System.Environment.NewLine + sql1);
                    DataTable dt1 = new DataTable();

                    tf.sqlDataAdapterFillDatatablePqm(sql1, ref dt1);



                    DataRow dr = dtOverall.NewRow();

                    //if (model == "LA20_517CB")
                    //{
                    //    dr["serialno"] = "'" + serial;
                    //}
                    //else dr["serialno"] = serial;
                    //if (model == "LA20_517EB")
                    //{
                    //    dr["comp_ser"] = comp_ser;
                    //}
                    dr["model"] = model.Substring(0, 4) + "V" + model.Substring(4);

                    switch (model)
                    {
                        case "LA20_517CC":
                        case "LA20_517CC1":
                        case "LA20_517CC3":
                        case "LA20_517DB":
                            dr["serialno"] = serial;
                            dr["lot"] = VBS.Mid(serial, 13, 3).Length < 3 ? "Error" : VBS.Mid(serial, 13, 3);
                            break;
                        case "LA20_517CC2":
                        case "LA20_517DC":
                            dr["serialno"] = serial;
                            dr["lot"] = VBS.Mid(serial, 8, 3).Length < 3 ? "Error" : VBS.Mid(serial, 8, 3);
                            break;
                        case "LA20_517CB":
                            dr["serialno"] = serial;
                            dr["lot"] = VBS.Mid(serial, 9, 3).Length < 3 ? "Error" : VBS.Mid(serial, 9, 3);
                            break;
                        case "LA20_517EB":
                            dr["serialno"] = serial;
                            dr["lot"] = VBS.Mid(serial, 12, 6).Length < 3 ? "Error" : VBS.Mid(serial, 12, 6);
                            break;
                        default:
                            dr["serialno"] = serial;
                            dr["lot"] = VBS.Mid(serial, 9, 3).Length < 3 ? "Error" : VBS.Mid(serial, 9, 3);
                            break;
                    }

                    dr["return"] = formReturnMode ? "R" : "N";

                    if (dt2.Rows.Count != 0)
                    {
                        string linepass = String.Empty;
                        string buff = dt2.Rows[0]["judge"].ToString();
                        if (buff == "0") linepass = "PASS";
                        else if (buff == "1") linepass = "FAIL";
                        else linepass = "ERROR";

                        //switch (VBS.Mid(model, 6, 4))
                        //{
                        //case "517D":
                        //case "517E":

                        //break;
                        //default:
                        //dr["aio_cw"] = dt1.Rows[0]["aio_cw"].ToString();
                        //dr["ano_cw"] = dt1.Rows[0]["ano_cw"].ToString();
                        //dr["air_cw"] = dt1.Rows[0]["air_cw"].ToString();
                        //dr["anr_cw"] = dt1.Rows[0]["anr_cw"].ToString();
                        //dr["ais_cw"] = dt1.Rows[0]["ais_cw"].ToString();
                        //break;

                        //}
                        dr["judge"] = linepass;
                    }

                    if (dt1.Rows.Count != 0)
                    {
                        dr["aio_ccw"] = dt1.Rows[0]["aio_ccw"].ToString();
                        dr["ano_ccw"] = dt1.Rows[0]["ano_ccw"].ToString();
                        dr["air_ccw"] = dt1.Rows[0]["air_ccw"].ToString();
                        dr["anr_ccw"] = dt1.Rows[0]["anr_ccw"].ToString();
                        dr["ais_ccw"] = dt1.Rows[0]["ais_ccw"].ToString();
                    }

                    if (dt2.Rows.Count != 0)
                    {
                        dr["cg_ccw"] = dt2.Rows[0]["inspectdata"].ToString();

                        dr["cir_ccw"] = dt2.Rows[1]["inspectdata"].ToString();

                        dr["cnr_ccw"] = dt2.Rows[2]["inspectdata"].ToString();
                    }

                    dtOverall.Rows.Add(dr);

                    // �f�[�^�O���b�g�r���[�̍X�V
                    updateDataGridViews(dtOverall, ref dgvInline);
                }
                // ���͗p�e�L�X�g�{�b�N�X��ҏW�\�֖߂��A�A�����ăX�L�����ł���悤�A�e�L�X�g��I����Ԃɂ���
                if (okCount >= limit1 && !formAddMode)
                {
                    txtProductSerial.Enabled = false;
                }
                else
                {
                    txtProductSerial.Enabled = true;
                    txtProductSerial.Focus();
                    txtProductSerial.SelectAll();

                }
            }
        }

        // �T�u�v���V�[�W���F�V���A������A�Q�Ƃ��ׂ��o�p�l�e�[�u��������肷��
        private void decideReferenceTable()
        {
            string model = VBS.Mid(cmbModel.Text, 6, 4);
            string model_c = VBS.Left(cmbModel.Text, 9);
            switch (model)
            {
                case "517C":
                    testerTableThisMonth = cmbModel.Text + DateTime.Today.ToString("yyyyMM");
                    tableThisMonth = model_c + DateTime.Today.ToString("yyyyMM") + "data";
                    testerTableLastMonth = cmbModel.Text + ((VBS.Right(DateTime.Today.ToString("yyyyMM"), 2) != "01") ?
                        (long.Parse(DateTime.Today.ToString("yyyyMM")) - 1).ToString() : (long.Parse(DateTime.Today.ToString("yyyy")) - 1).ToString() + "12");
                    tableLastMonth = model_c + ((VBS.Right(DateTime.Today.ToString("yyyyMM"), 2) != "01") ?
                        (long.Parse(DateTime.Today.ToString("yyyyMM")) - 1).ToString() : (long.Parse(DateTime.Today.ToString("yyyy")) - 1).ToString() + "12") + "data";
                    break;
                case "517D":
                    testerTableThisMonth = cmbModel.Text + DateTime.Today.ToString("yyyyMM");
                    tableThisMonth = model_c + DateTime.Today.ToString("yyyyMM") + "data";
                    testerTableLastMonth = cmbModel.Text + ((VBS.Right(DateTime.Today.ToString("yyyyMM"), 2) != "01") ?
                        (long.Parse(DateTime.Today.ToString("yyyyMM")) - 1).ToString() : (long.Parse(DateTime.Today.ToString("yyyy")) - 1).ToString() + "12");
                    tableLastMonth = model_c + ((VBS.Right(DateTime.Today.ToString("yyyyMM"), 2) != "01") ?
                        (long.Parse(DateTime.Today.ToString("yyyyMM")) - 1).ToString() : (long.Parse(DateTime.Today.ToString("yyyy")) - 1).ToString() + "12") + "data";
                    break;
                default:
                    testerTableThisMonth = cmbModel.Text + DateTime.Today.ToString("yyyyMM");
                    testerThisMonth = cmbModel.Text + DateTime.Today.ToString("yyyyMM") + "data";
                    tableThisMonth = model_c + DateTime.Today.ToString("yyyyMM") + "data";
                    testerTableLastMonth = cmbModel.Text + ((VBS.Right(DateTime.Today.ToString("yyyyMM"), 2) != "01") ?
                        (long.Parse(DateTime.Today.ToString("yyyyMM")) - 1).ToString() : (long.Parse(DateTime.Today.ToString("yyyy")) - 1).ToString() + "12");
                    testerLastMonth = cmbModel.Text + ((VBS.Right(DateTime.Today.ToString("yyyyMM"), 2) != "01") ?
                        (long.Parse(DateTime.Today.ToString("yyyyMM")) - 1).ToString() : (long.Parse(DateTime.Today.ToString("yyyy")) - 1).ToString() + "12") + "data";
                    tableLastMonth = model_c + ((VBS.Right(DateTime.Today.ToString("yyyyMM"), 2) != "01") ?
                        (long.Parse(DateTime.Today.ToString("yyyyMM")) - 1).ToString() : (long.Parse(DateTime.Today.ToString("yyyy")) - 1).ToString() + "12") + "data";
                    break;
            }
        }

        // �r���[���[�h�ōĈ�����s��
        private void btnPrint_Click(object sender, EventArgs e)
        {
            string boxId = txtBoxId.Text;
            string model = cmbModel.Text.Substring(0, 4) + "V" + cmbModel.Text.Substring(4);
            string shipKind = dtOverall.Rows[0]["return"].ToString();
            printBarcode(directory, boxId, model, dgvDateCode, ref dgvDateCode2, ref txtBoxIdPrint, shipKind);
        }

        // �e��m�F��A�{�b�N�X�h�c�̔��s�A�V���A���̓o�^�A�o�[�R�[�h���x���̃v�����g���s��
        private void btnRegisterBoxId_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCarton.Text))
            {
                btnRegisterBoxId.Enabled = false;
                btnDeleteSelection.Enabled = false;
                btnCancel.Enabled = false;

                string boxId = txtBoxId.Text;

                //�ꎞ�e�[�u���̃V���A���S�Ăɂ��āA�{�ԃe�[�u���ɓo�^���Ȃ����A�`�F�b�N
                string checkResult = checkDataTableWithRealTable(dtOverall);

                if (checkResult != String.Empty)
                {
                    MessageBox.Show("The following serials are already registered with box id:" + Environment.NewLine +
                        checkResult + Environment.NewLine + "Please check and delete.", "Notice",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    btnRegisterBoxId.Enabled = true;
                    btnDeleteSelection.Enabled = true;
                    btnCancel.Enabled = true;
                    return;
                }

                //�{�b�N�X�h�c�̐V�K�̔�
                //string boxIdNew = box_m[1] + "-" + DateTime.Today.ToString("yyyyMMdd") + "-" + txtCarton.Text;
                TfSQL yn = new TfSQL();
                string sql_box = "INSERT INTO box_id_rt(" +
                    "boxid," +
                    "suser," +
                    "regist_date) " +
                    "VALUES(" +
                    "'" + boxId + "'," +
                    "'" + user + "'," +
                    "'" + DateTime.Now.ToString() + "')";
                System.Diagnostics.Debug.Print(sql_box);
                yn.sqlExecuteNonQuery(sql_box, false);

                //�悸�́ADataTble�Ƀ{�b�N�X�h�c��o�^
                DataTable dt = dtOverall.Copy();
                dt.Columns.Add("boxid", Type.GetType("System.String"));
                dt.Columns.Add("carton", Type.GetType("System.String"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["boxid"] = boxId;
                    dt.Rows[i]["carton"] = txtCarton.Text;
                }

                //DataTable����{�ԃe�[�u���ֈꊇ�o�^
                TfSQL tf = new TfSQL();
                bool res1 = tf.sqlMultipleInsert517EB(dt);

                if (res1)
                {
                    // �o�[�R�[�h���󎚁i�O�̂��߃��C�����f��������x�擾������j
                    //m_model = dtOverall.Rows[0]["model"].ToString();
                    string shipKind = dtOverall.Rows[0]["return"].ToString();
                    string prt_model = cmbModel.Text.Substring(0, 4) + "V" + cmbModel.Text.Substring(4);
                    //printBarcode(directory, boxId, prt_model, dgvDateCode, ref dgvDateCode2, ref txtBoxIdPrint, shipKind);

                    //�f�[�^�e�[�u���̃��R�[�h����
                    dtOverall.Clear();
                    dt = null;

                    txtBoxId.Text = boxId;
                    //dtpPrintDate.Value = DateTime.ParseExact(VBS.Mid(boxIdNew, 3, 6), "yyMMdd", CultureInfo.InvariantCulture);

                    //�e�t�H�[��frmBox�̃f�[�^�O���b�g�r���[���X�V���邽�߁A�f���Q�[�g�C�x���g�𔭐�������
                    this.RefreshEvent(this, new EventArgs());

                    this.Focus();
                    MessageBox.Show("The box id " + boxId + " and " + Environment.NewLine +
                        "its product serials were registered.", "Process Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBoxId.Text = String.Empty;
                    txtProductSerial.Text = String.Empty;
                    updateDataGridViews(dtOverall, ref dgvInline);
                    btnRegisterBoxId.Enabled = false;
                    btnDeleteSelection.Enabled = true;
                    btnCancel.Enabled = true;
                }
                else
                {
                    //��U�o�^�����a�n�w�h�c����������
                    string sql = "delete from box_id_rt WHERE boxid= '" + boxId + "'";
                    int res = tf.sqlExecuteNonQueryInt(sql, false);

                    MessageBox.Show("Box id and product serials were not registered.", "Process Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnRegisterBoxId.Enabled = true;
                    btnDeleteSelection.Enabled = true;
                    btnCancel.Enabled = true;
                }
            }
            else MessageBox.Show("Please input the carton number!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // �T�u�v���V�[�W���F�f�[�^�e�[�u���̃V���A���S�Ăɂ��āA�{�e�[�u���ɓo�^���Ȃ����ꊇ�m�F
        private string checkDataTableWithRealTable(DataTable dt1)
        {
            string serial;
            string result = String.Empty;
            if (formReturnMode) return result;

            string sql = "select serialno, boxid FROM product_serial_517eb";

            DataTable dt2 = new DataTable();
            TfSQL tf = new TfSQL();
            tf.sqlDataAdapterFillDatatable(sql, ref dt2);

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (cmbModel.Text == "LA20_517CB")
                {
                    serial = VBS.Mid(dt1.Rows[i]["serialno"].ToString(), 2, 21);
                }
                else serial = dt1.Rows[i]["serialno"].ToString();
                DataRow[] dr = dt2.Select("serialno = '" + serial + "'");
                if (dr.Length >= 1)
                {
                    string boxid = dr[0]["boxId"].ToString();
                    result += (i + 1 + ": " + serial + " / " + boxid + Environment.NewLine);
                }
            }

            if (result == String.Empty)
            {
                return String.Empty;
            }
            else
            {
                return result;
            }
        }

        // �T�u�v���V�[�W���F�o�[�R�[�h���v�����g����i�{�o�[�W�����́A�a�n�w�h�c���̃e�L�X�g�t�@�C���𐶐�����j
        private void printBarcode(string dir, string id, string m_model_long, DataGridView dgv1, ref DataGridView dgv2, ref TextBox txt, string shipKind)
        {
            TfPrint tf = new TfPrint();
            tf.createBoxidFiles(dir, id, m_model_long, dgv1, ref dgv2, ref txt, shipKind);
        }

        // �ꎞ�e�[�u���̑I�����ꂽ�������R�[�h���A�ꊇ����������
        private void btnDeleteSelection_Click(object sender, EventArgs e)
        {
            DataGridView dgv = new DataGridView();

            if (tabControl1.SelectedTab == tabControl1.TabPages["tabInline"])
                dgv = dgvInline;

            // �Z���̑I��͈͂��Q��ȏ�̏ꍇ�́A���b�Z�[�W�̕\���݂̂Ńv���V�[�W���𔲂���
            if (dgv.Columns.GetColumnCount(DataGridViewElementStates.Selected) >= 2)
            {
                MessageBox.Show("Please select range with only one columns.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                return;
            }

            DialogResult result = MessageBox.Show("Do you really want to delete the selected rows?",
                "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                foreach (DataGridViewCell cell in dgv.SelectedCells)
                {
                    int i = cell.RowIndex;
                    dtOverall.Rows[i].Delete();
                }
                dtOverall.AcceptChanges();
                updateDataGridViews(dtOverall, ref dgvInline);

                txtProductSerial.Focus();
                txtProductSerial.SelectAll();
                txtProductSerial.Enabled = true;
            }
        }

        // �P���x��������̃V���A������ύX����i�Ǘ��������[�U�[�̂݁j
        private void btnChangeLimit_Click(object sender, EventArgs e)
        {
            // �t�H�[���S�i�P���x��������V���A�����ύX�j���A�f���Q�[�g�C�x���g��t�����ĊJ��
            bool bl = TfGeneral.checkOpenFormExists("frmCapacity");
            if (bl)
            {
                MessageBox.Show("Please close or complete another form.", "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            }
            else
            {
                frmCapacity f4 = new frmCapacity();
                //�q�C�x���g���L���b�`���āA�f�[�^�O���b�h���X�V����
                f4.RefreshEvent += delegate (object sndr, EventArgs excp)
                {
                    int l = f4.getLimit();
                    if (l != 0)
                    {
                        limit2 = f4.getLimit();
                        txtLimit.Text = limit2.ToString();
                        limit1 = limit2;
                    }
                    updateDataGridViews(dtOverall, ref dgvInline);
                    this.Focus();
                };

                f4.updateControls(limit2.ToString());
                f4.Show();
            }
        }

        // �o�^�ς݂̃{�b�N�X�h�c�ցA���W���[����ǉ��i�Ǘ����[�U�[�̂݁j
        private void btnAddSerial_Click(object sender, EventArgs e)
        {
            // �ǉ����[�h�łȂ��ꍇ�́A�ǉ����[�h�̕\���֐؂�ւ���
            if (!formAddMode)
            {
                formAddMode = true;
                btnAddSerial.Text = "Register";
                btnRegisterBoxId.Enabled = false;
                btnExport.Enabled = false;
                btnCancelBoxid.Enabled = false;
                btnDeleteSerial.Enabled = false;
                txtProductSerial.Enabled = true;
                if (dtOverall.Rows.Count >= 0)
                {
                    formReturnMode = (dtOverall.Rows[0]["return"].ToString() == "R" ? true : false);
                }
            }
            // ���ɒǉ����[�h�̏ꍇ�́A�c�a�ւ̓o�^���s��
            else
            {
                // �c�d�k�d�s�d �r�p�k���𔭍s���A�f�[�^�x�[�X����폜����
                string boxId = txtBoxId.Text;
                string sql = "delete from product_serial_517eb where boxid = '" + boxId + "'";
                System.Diagnostics.Debug.Print(sql);
                TfSQL tf = new TfSQL();
                bool res1 = tf.sqlExecuteNonQuery(sql, false);

                // DataTble�Ƀ{�b�N�X�h�c��ǉ����A�{�ԃe�[�u���ֈꊇ�o�^
                DataTable dt = dtOverall.Copy();
                dt.Columns.Add("boxid", Type.GetType("System.String"));
                for (int i = 0; i < dt.Rows.Count; i++)
                    dt.Rows[i]["boxid"] = boxId;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string buff = string.Empty;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        buff += dt.Rows[i][j].ToString() + "  ";
                        System.Diagnostics.Debug.Print(buff);
                    }
                }

                bool res2 = tf.sqlMultipleInsertOverall(dt);

                if (!res1 || !res2)
                {
                    MessageBox.Show("Error happened in the register process.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                }
                else
                {
                    MessageBox.Show("Register completed.", "Notice",
                        MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                }

                // �ǉ����[�h���I�����A�{�����[�h�̕\���֐؂�ւ���
                formAddMode = false;
                btnAddSerial.Text = "Add Product";
                btnRegisterBoxId.Enabled = true;
                btnExport.Enabled = true;
                btnCancelBoxid.Enabled = true;
                btnDeleteSerial.Enabled = true; 
                txtProductSerial.Enabled = false;
                txtProductSerial.Text = string.Empty;
            }
        }

        // �o�^�ς݂̃{�b�N�X�h�c�́A���W���[�����폜�i�Ǘ����[�U�[�̂݁j
        private void btnDeleteSerial_Click(object sender, EventArgs e)
        {
            // �Z���̑I��͈͂��Q��ȏ�̏ꍇ�́A���b�Z�[�W�̕\���݂̂Ńv���V�[�W���𔲂���
            if (dgvInline.Columns.GetColumnCount(DataGridViewElementStates.Selected) >= 2)
            {
                MessageBox.Show("Please select range with only one columns.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                return;
            }

            DialogResult result = MessageBox.Show("Do you really want to delete the selected rows?",
                "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                // �c�d�k�d�s�d �r�p�k���𔭍s���A�f�[�^�x�[�X����폜����
                string boxId = txtBoxId.Text;
                string whereSer = string.Empty;
                foreach (DataGridViewCell cell in dgvInline.SelectedCells)
                {
                    whereSer += "'" + cell.Value.ToString() + "', ";
                }
                string sql = "delete from product_serial_517eb where boxid = '" + boxId + "' and  serialno in (" + VBS.Left(whereSer, whereSer.Length - 2) + ")";
                System.Diagnostics.Debug.Print(sql);
                TfSQL tf = new TfSQL();
                int res = tf.sqlExecuteNonQueryInt(sql, false);

                if (res >= 1)
                {
                    // �f�[�^�O���b�h�r���[����폜����
                    foreach (DataGridViewCell cell in dgvInline.SelectedCells)
                    {
                        int i = cell.RowIndex;
                        dtOverall.Rows[i].Delete();
                    }
                    dtOverall.AcceptChanges();
                    updateDataGridViews(dtOverall, ref dgvInline);
                    MessageBox.Show(res.ToString() + " module(s) deleted.", "Notice",
                        MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                }
                else
                {
                    MessageBox.Show("Delete failed.", "Notice",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                }
            }
        }
        
        // �o�^�ς݂̃{�b�N�X�h�c����ъY�����W���[���̍폜�i�Ǘ����[�U�[�̂݁j
        private void btnCancelBoxid_Click(object sender, EventArgs e)
        {
            // �{���ɍ폜���Ă悢���A�Q�d�Ŋm�F����B
            DialogResult result1 = MessageBox.Show("Do you really delete this box id's all the serial data?",
                "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result1 == DialogResult.Yes)
            {
                DialogResult result2 = MessageBox.Show("Are you really sure? Please select NO if you are not sure.",
                    "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result2 == DialogResult.Yes)
                {
                    string boxid = txtBoxId.Text;
                    TfSQL tf = new TfSQL();
                    int res = tf.sqlDeleteBoxid517EB(boxid);

                    dtOverall.Clear();
                    // �f�[�^�O���b�g�r���[�̍X�V
                    updateDataGridViews(dtOverall, ref dgvInline);

                    //�e�t�H�[��frmBox�̃f�[�^�O���b�g�r���[���X�V���邽�߁A�f���Q�[�g�C�x���g�𔭐�������
                    this.RefreshEvent(this, new EventArgs());
                    this.Focus();

                    if (res != -1)
                    {
                        MessageBox.Show("Boxid " + boxid + " and its " + res + " products were deleted.", "Process Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancelBoxid.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("An Error has happened in the process and no data has been deleted.", "Process Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                    btnAddSerial.Enabled = false;
                    btnExport.Enabled = false;
                }
            }
        }

        // �L�����Z�����ɁA�f�[�^�e�[�u���̃��R�[�h�̕ێ����ł��Ȃ��|�A�x������
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // frmCapacity �i�a�n�w������V���A�����j����Ă��Ȃ��ꍇ�́A��ɕ���悤�ʒm����
            string formName = "frmCapacity";
            bool bl = false;
            foreach (Form buff in Application.OpenForms)
            {
                if (buff.Name == formName) { bl = true; }
            }
            if (bl)
            {
                MessageBox.Show("You need to close another form before canceling.", "Notice",
                  MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                return;
            }

            // �f�[�^�e�[�u���̃��R�[�h�������Ȃ��ꍇ�A�܂��͕ҏW���[�h�̏ꍇ�́A���̂܂ܕ���                        
            if (dtOverall.Rows.Count == 0 || !formEditMode)
            {
                Application.OpenForms["frmBox"].Focus();
                Close();
                return;
            }

            // �f�[�^�e�[�u���̃��R�[�h����������ꍇ�A�ꎞ�I�ɕێ�����Ă��郌�R�[�h�����ł���|�A�x������
            DialogResult result = MessageBox.Show("The current serial data has not been saved." + System.Environment.NewLine +
                "Do you rally cancel?", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                dtOverall.Clear();
                updateDataGridViews(dtOverall, ref dgvInline);
                MessageBox.Show("The temporary serial numbers are deleted.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                Application.OpenForms["frmBox"].Focus();
                Close();
            }
            else
            {
                return;
            }
        }

        // ����{�^����V���[�g�J�b�g�ł̏I���������Ȃ�
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x112;
            const long SC_CLOSE = 0xF060L;
            if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE) { return; }
            base.WndProc(ref m);
        }

        //MP3�t�@�C���i����͌x�����j���Đ�����
        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern int mciSendString(String command,
           StringBuilder buffer, int bufferSize, IntPtr hwndCallback);

        private string aliasName = "MediaFile";

        private void soundAlarm()
        {
            string currentDir = System.Environment.CurrentDirectory;
            string fileName = currentDir + @"\warning.mp3";
            string cmd;

            if (sound)
            {
                cmd = "stop " + aliasName;
                mciSendString(cmd, null, 0, IntPtr.Zero);
                cmd = "close " + aliasName;
                mciSendString(cmd, null, 0, IntPtr.Zero);
                sound = false;
            }

            cmd = "open \"" + fileName + "\" type mpegvideo alias " + aliasName;
            if (mciSendString(cmd, null, 0, IntPtr.Zero) != 0) return;
            cmd = "play " + aliasName;
            mciSendString(cmd, null, 0, IntPtr.Zero);
            sound = true;
        }

        // �f�[�^���G�N�Z���փG�N�X�|�[�g
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (txtBoxId.Text == "")
            {
                MessageBox.Show("Please input the carton number!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCarton.Focus();
                return;
            }
            DataTable dt1 = new DataTable();
            dt1 = (DataTable)dgvInline.DataSource;
            ExcelClass xl = new ExcelClass();
            xl.Export(dgvInline, txtBoxId.Text, "Box " + txtBoxId.Text);
        }

        private void txtCarton_TextChanged(object sender, EventArgs e)
        {
            if (lblFrmName.Text != "VIEW")
            {
                string[] box = cmbModel.Text.Split('_');
                txtBoxId.Text = box[1] + "-" + DateTime.Today.ToString("yyMMdd") + "-" + txtCarton.Text;
            }
        }

        private void txtCompSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtProductSerial.Focus();
                txtProductSerial.SelectAll();
            }
        }

        private void CmbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (VBS.Mid(cmbModel.Text, 6, 4) == "517E")
            {
                limit1 = 60;
                txtOkCount.Text = okCount.ToString() + "/" + limit1.ToString();
                txtProductSerial.Enabled = true;
                txtProductSerial.Focus();
            }
            else if (VBS.Mid(cmbModel.Text, 6, 4) == "517F")
            {
                limit1 = 65;
                txtOkCount.Text = okCount.ToString() + "/" + limit1.ToString();
                txtProductSerial.Enabled = true;
                txtProductSerial.Focus();
            }
            else
            {
                limit1 = 80;
                txtOkCount.Text = okCount.ToString() + "/" + limit1.ToString();
                txtProductSerial.Enabled = true;
                txtProductSerial.Focus();
            }
        }

        private void txtCarton_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
                {
                    e.Handled = true;
                    MessageBox.Show("Please input only number!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        // �T�u�v���V�[�W���F�f�[�^�e�[�u���̒��g���`�F�b�N����A�{�A�v���P�[�V�����ɑ΂��āA���ڂ͊֌W�Ȃ�
        private void printDataTable(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    System.Diagnostics.Debug.Print(dt.Rows[i][j].ToString());
                }
            }
        }

        // �T�u�v���V�[�W���F�f�[�^�r���[�̒��g���`�F�b�N����A�{�A�v���P�[�V�����ɑ΂��āA���ڂ͊֌W�Ȃ�
        private void printDataView(DataView dv)
        {
            foreach (DataRowView drv in dv)
            {
                System.Diagnostics.Debug.Print(drv["lot"].ToString() + " " +
                    drv["tjudge"].ToString() + " " + drv["inspectdate"].ToString());
            }
        }
    }
}