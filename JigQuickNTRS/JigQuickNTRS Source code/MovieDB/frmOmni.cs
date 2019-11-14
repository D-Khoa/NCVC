using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Data.Linq;
using System.Globalization;
using System.Security.Permissions;
using System.Diagnostics;


namespace JigQuick
{
    public partial class frmOmni : Form
    {
        #region Variables
        // �R���t�B�O�t�@�C���ƁA�o�̓e�L�X�g�t�@�C���́A�f�X�N�g�b�v�̎w��̃t�H���_�ɕۑ�����
        string appconfig = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\JigQuickDesk\JigQuickApp\info.ini";
        string outPath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\JigQuickDesk\ConverterTarget\";
        string outPath2 = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\JigQuickDesk\NtrsLog\";

        //�i�h�f�p�t�h�b�j�p�A�񃍁[�J���ϐ�
        DataTable dtPartsLot;
        DataTable dtTbi;
        int rowPartsLot;
        int partsLotCount;
        long cumCount;
        bool sound;
        bool duplicate;
        string autoRegister = "ON";
        string parentTextBoxFunc = "OFF";
        string[] description;
        string[] partsLotBreakdown;

        //�m�s�q�r�p�A�񃍁[�J���ϐ�
        int okCount;
        int ngCount;
        int targetProcessCount, targetProcessCount4257;
        string model;
        string modelsub;
        string subAssyName;
        string targetProcess, targetProcess4257;
        string targetProcessCrust, targetProcessCrust4257;
        string targetProcessCombined, targetProcessCombined4257;
        string headTableThisMonth = string.Empty;
        string headTableLastMonth = string.Empty;
        string headTablesubThisMonth = string.Empty;
        string headTablesubLastMonth = string.Empty;
        string okImageFile;
        string ngImageFile;
        string standByImageFile;
        string ntrsSwitch;
        string bracketCrustLinkSwitch;
        #endregion

        // �R���X�g���N�^
        public frmOmni()
        {
            InitializeComponent();
        }

        // ���[�h���̏���
        private void frmInut_Load(object sender, EventArgs e)
        {
            // �i�h�f�p�t�h�b�j
            dtPartsLot = new DataTable();
            // ���i���b�g�ێ��p�f�[�^�e�[�u���̍s���ݒ���擾
            rowPartsLot = int.Parse(readIni("OTHERS", "PARTS LOT COUNT", appconfig));
            description = readIni("OTHERS", "DESCRIPTION", appconfig).Split(',');
            partsLotBreakdown = readIni("OTHERS", "PARTS LOT BREAKDOWN", appconfig).Split(',');

            // �����o�^���[�h�̂n�m�E�n�e�e�擾�A�o�^�{�^���̐ݒ�
            //autoRegister = readIni("APPLICATION BEHAVIOR", "AUTOMATIC REGISTER", appconfig);

            // �e�폈���p�̃e�[�u���𐶐�
            dtPartsLot = new DataTable();
            dtTbi = new DataTable();

            // �e�[�u���̒�`
            defineTables(ref dtPartsLot, ref dtTbi);

            // ���x���̐ݒ�
            setLabels();

            // �ݐσJ�E���g�i�����̃e�L�X�g�t�@�C�����̃��R�[�h���j���擾
            string outFile = outPath + DateTime.Today.ToString("yyyyMMdd") + ".txt";
            if (System.IO.File.Exists(outFile))
            {
                using (StreamReader r = new StreamReader(outFile))
                {
                    int i = 0;
                    while (r.ReadLine() != null) { i++; }
                    cumCount = (i -2) / rowPartsLot;
                }
            }

            // �J�E���g�e�L�X�g�{�b�N�X�̒��g�̕\��
            txtCount.Text = cumCount.ToString();

            // --------------------------------------------------------------------------------------
            // �ȉ��A�m�s�q�r�̏����ݒ�
            okImageFile = readIni("MODULE-DATA MATCHING", "OK IMAGE FILE", appconfig);
            ngImageFile = readIni("MODULE-DATA MATCHING", "NG IMAGE FILE", appconfig);
            standByImageFile = readIni("MODULE-DATA MATCHING", "STAND-BY IMAGE FILE", appconfig);
            string standByImagePath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\JigQuickDesk\JigQuickApp\images\" + standByImageFile;
            pnlResult.BackgroundImageLayout = ImageLayout.Zoom;
            pnlResult.BackgroundImage = System.Drawing.Image.FromFile(standByImagePath);
            pnl4257.BackgroundImageLayout = ImageLayout.Zoom;
            pnl4257.BackgroundImage = System.Drawing.Image.FromFile(standByImagePath);

            model = readIni("MODULE-DATA MATCHING", "MODEL", appconfig);
            modelsub = readIni("MODULE-DATA MATCHING", "MODELSUB", appconfig);
            subAssyName = readIni("MODULE-DATA MATCHING", "SUB ASSY NAME", appconfig);
            targetProcess = readIni("MODULE-DATA MATCHING", "TARGET PROCESS", appconfig);
            targetProcess4257 = readIni("MODULE-DATA MATCHING", "TARGET PROCESS NEW", appconfig);
            ntrsSwitch = readIni("MODULE-DATA MATCHING", "NTRS INLINE MATCHING", appconfig);
            bracketCrustLinkSwitch = readIni("MODULE-DATA MATCHING", "BRACKET-CRUST LINK", appconfig);
            targetProcessCrust = readIni("MODULE-DATA MATCHING", "CRUST TARGET PROCESS", appconfig);
            targetProcessCrust4257 = readIni("MODULE-DATA MATCHING", "CRUST TARGET PROCESS NEW", appconfig);

            headTableThisMonth = model.ToLower() + DateTime.Today.ToString("yyyyMM");
            headTablesubThisMonth = modelsub.ToLower() + DateTime.Today.ToString("yyyyMM");
            headTableLastMonth = model.ToLower() + ((VBS.Right(DateTime.Today.ToString("yyyyMM"), 2) != "01") ?
                (long.Parse(DateTime.Today.ToString("yyyyMM")) - 1).ToString() : (long.Parse(DateTime.Today.ToString("yyyy")) - 1).ToString() + "12");
            headTablesubLastMonth = modelsub.ToLower() + ((VBS.Right(DateTime.Today.ToString("yyyyMM"), 2) != "01") ?
               (long.Parse(DateTime.Today.ToString("yyyyMM")) - 1).ToString() : (long.Parse(DateTime.Today.ToString("yyyy")) - 1).ToString() + "12");

            txtSubAssy.Text = model + "  " + subAssyName;
            targetProcessCount = targetProcess.Where(c => c == ',').Count() + 1 +
                (targetProcessCrust == string.Empty ? 0 : targetProcessCrust.Where(c => c == ',').Count() + 1);
            targetProcessCombined = targetProcess + (targetProcessCrust == string.Empty ? string.Empty : "," + targetProcessCrust);

            targetProcessCount4257 = targetProcess4257.Where(c => c == ',').Count() + 1 +
                (targetProcessCrust4257 == string.Empty ? 0 : targetProcessCrust4257.Where(c => c == ',').Count() + 1);
            targetProcessCombined4257 = targetProcess4257 + (targetProcessCrust4257 == string.Empty ? string.Empty : "," + targetProcessCrust4257);

            // �J�E���^�[�̕\���i�f�t�H���g�̓[���j
            txtOkCount.Text = okCount.ToString();
            txtNgCount.Text = ngCount.ToString();

            // ���O�p�t�H���_�̍쐬�i�t�H���_�����݂��Ȃ��ꍇ�j
            if (!Directory.Exists(outPath2)) Directory.CreateDirectory(outPath2);
        }

        // �T�u�v���V�[�W���F�c�s�̒�`
        private void defineTables(ref DataTable dt1, ref DataTable dt2)
        {
            // ���i���b�g�O���b�h�r���[����������
            dt1.Columns.Add("part_code", Type.GetType("System.String"));
            dt1.Columns.Add("part_name", Type.GetType("System.String"));
            dt1.Columns.Add("vendor", Type.GetType("System.String"));
            dt1.Columns.Add("invoice", Type.GetType("System.String"));
            dt1.Columns.Add("shipdate", Type.GetType("System.String"));
            dt1.Columns.Add("qty", Type.GetType("System.String"));

            for (int i = 0; i < rowPartsLot; i++) dt1.Rows.Add(dt1.NewRow());
            dgvPartsLot.DataSource = dt1;

            // �s�w�b�_�[�ɍs�ԍ���ǉ����A�s�w�b�_�����������߂���
            for (int i = 0; i < rowPartsLot; i++) dgvPartsLot.Rows[i].HeaderCell.Value = partsLotBreakdown[i];
            dgvPartsLot.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            // �o�^�����p�s�a�h�f�[�^�e�[�u������������
            dt2.Columns.Add("S/N", Type.GetType("System.String"));
            dt2.Columns.Add("Lot", Type.GetType("System.String"));
            dt2.Columns.Add("ModelName", Type.GetType("System.String"));
            dt2.Columns.Add("Date", Type.GetType("System.String"));
            dt2.Columns.Add("Time", Type.GetType("System.String"));
            dt2.Columns.Add("LotInfo", Type.GetType("System.String"));
        }

        // �T�u�v���V�[�W���F���x���̐ݒ�
        private void setLabels()
        {
            txtModel.Text = readIni("LABEL DESCRIPTION", "MODEL", appconfig);
            txtProcess.Text = readIni("LABEL DESCRIPTION", "PROCESS TEXT BOX", appconfig);
            lblPartsLot.Text = readIni("LABEL DESCRIPTION", "PARTS LOT LABEL", appconfig);
            lblChild.Text = readIni("LABEL DESCRIPTION", "CHILD LABEL", appconfig);
            //lblParent.Text = readIni("LABEL DESCRIPTION", "PARENT LABEL", appconfig);
            parentTextBoxFunc = readIni("APPLICATION BEHAVIOR", "PARENT TEXT BOX", appconfig);
            //txtParent.Enabled = parentTextBoxFunc == "ON" ? true : false;
        }

        // ���i���b�g��񃊃Z�b�g�{�^���������̏���
        private void btnResetParts_Click(object sender, EventArgs e)
        {
            // �i�h�f�p�t�h�b�j���̃R���g���[���̍X�V
            int r = 4;
            for (int i = 0; i < r; i++)
            {
                dtPartsLot.Rows[i][0] = string.Empty;
                dtPartsLot.Rows[i][1] = string.Empty;
                dtPartsLot.Rows[i][2] = string.Empty;
                dtPartsLot.Rows[i][3] = string.Empty;
                dtPartsLot.Rows[i][4] = string.Empty;
                dtPartsLot.Rows[i][5] = string.Empty;
            }
            
            txtPartsLot.Text = string.Empty;
            txtPartsLot.Enabled = true;
            txtPartsLot.Focus();
            resetViewColor(ref dgvPartsLot);
 
            // �m�s�q�r���̃R���g���[���̍X�V
            btnReset_Click(sender, e);

            // �i�h�f�p�t�h�b�j���̃R���g���[���̍X�V
            txtChild.Text = string.Empty;
            txtChild.Enabled = false;
            txtChild.BackColor = SystemColors.Window;
        }

        // ���i���b�g���̃X�L�������̏���
        private void txtPartsLot_KeyDown_1(object sender, KeyEventArgs e)
        {
            // �o�[�R�[�h�����̃G���^�[�L�[�ȊO�͏������Ȃ�
            if (e.KeyCode != Keys.Enter) return;

            // �󕶎��̏ꍇ�͏������Ȃ�
            string scan = txtPartsLot.Text;
            if (scan == string.Empty) return;

            // ���i���b�g�O���b�h�r���[�̌��݂̃Z���s�ƁA���̎��̍s���i�[����
            int r = dgvPartsLot.CurrentCell.RowIndex;
            int y = r < rowPartsLot - 1 ? r + 1 : rowPartsLot - 1;

            // �Z�~�R�����łp�q�ǂݎ����e�𕪊����A�O���b�g�r���[�ɕ\������
            try
            {
                string[] split = scan.Split(';');
                dtPartsLot.Rows[r][0] = split[0];
                dtPartsLot.Rows[r][1] = split[1];
                dtPartsLot.Rows[r][2] = split[2];
                dtPartsLot.Rows[r][3] = split[3];
                dtPartsLot.Rows[r][4] = split[4];
                dtPartsLot.Rows[r][5] = split[5];

                txtPartsLot.Text = string.Empty;
                dgvPartsLot.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvPartsLot.CurrentCell = dgvPartsLot[0, y];
            }
            // �����ł��Ȃ�������̏ꍇ�́A�O���b�g�r���[���N���A�i��\���ɂ���j
            catch (Exception)
            {
                dtPartsLot.Rows[r][0] = string.Empty;
                dtPartsLot.Rows[r][1] = string.Empty;
                dtPartsLot.Rows[r][2] = string.Empty;
                dtPartsLot.Rows[r][3] = string.Empty;
                dtPartsLot.Rows[r][4] = string.Empty;
                dtPartsLot.Rows[r][5] = string.Empty;

                txtPartsLot.Text = string.Empty;
                dgvPartsLot.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                // MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            partsLotCount = dgvPartsLot.Rows.Cast<DataGridViewRow>().Where(a => a.Cells[0].Value.ToString() != string.Empty)
                .Select(a => a.Cells[0].Value).GroupBy(a => a).Count();
            if (!duplicate && partsLotCount == rowPartsLot)
            {
                txtPartsLot.Enabled = false;
                txtChild.Enabled = true;
            }
            else
            {
                txtPartsLot.Enabled = true;
                txtChild.Enabled = false;
            }
        }
        private void txtChild_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            if (txtChild.Text == string.Empty) return;
            //if (txtChild.Text.Length != 17 && txtChild.Text.Length != 24) return;

            if (txtChild.ReadOnly == true) return;

            if (duplicate || partsLotCount != rowPartsLot) return;

            bool res1;
            bool res = ntrsScanProcess4257(txtChild.Text);
            if (res) { res1 = ntrsScanProcess(txtChild.Text); }
            else return;

            checkAndOutput();
        }

        // Check Duplicate barcode
        private void checkDuplicate()
        {
            if (txtProduct.Text != String.Empty)// && txtProduct.Text.Length != 24)
            {
                DateTime d = DateTime.Now;
                //DateTime dd = DateTime.Now.GetDateTimeFormats("yyyy/mm/dd");
                TfSQL tf = new TfSQL();
                string ser = tf.sqlExecuteScalarString("SELECT serial_no FROM t_serno WHERE serial_no = '" + txtProduct.Text + "'");
                if (ser != txtProduct.Text)
                {
                    tf.sqlExecuteScalarString("INSERT INTO t_serno(serial_no, regist_date) VALUES('" + txtProduct.Text + "','" + d + "')");
                    lblResult.Text = "Barcode is OK"; lblResult.ForeColor = Color.Green;

                    string okImagePath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\JigQuickDesk\JigQuickApp\images\" + okImageFile;
                    pnlResult.BackgroundImageLayout = ImageLayout.Zoom;
                    pnlResult.BackgroundImage = System.Drawing.Image.FromFile(okImagePath);

                    string date = DateTime.Today.AddDays(1).ToString("yyyy/MM/dd");
                    string date1 = DateTime.Today.ToString("yyyy/MM/dd");
                    string count = tf.sqlExecuteScalarString("select count(serial_no) from t_serno where regist_date > '" + date1 + "' and regist_date <= '" + date + "'");
                    lblCount.Text = "TOTAL: " + count;
                }
                else
                {
                    lblResult.Text = "Duplicate Barcode";
                    lblResult.ForeColor = Color.Red;

                    string ngImagePath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\JigQuickDesk\JigQuickApp\images\" + ngImageFile;
                    pnlResult.BackgroundImageLayout = ImageLayout.Zoom;
                    pnlResult.BackgroundImage = System.Drawing.Image.FromFile(ngImagePath);
                    txtChild.BackColor = Color.Red;
                    txtChild.ReadOnly = true;
                    soundAlarm();
                }
            }
        }

        private void checkAndOutput()
        {
            colorViewForDuplicate(ref dgvPartsLot);

            partsLotCount = dgvPartsLot.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[0].Value.ToString() != string.Empty)
                    .Select(r => r.Cells[0].Value).GroupBy(r => r).Count();
            if (duplicate || partsLotCount != rowPartsLot)
            {
                MessageBox.Show("Part lot info is not enough.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (autoRegister == "ON")
            {
                outputTbiInfo();
            }
        }

        private void outputTbiInfo()
        {                           
            string sn = txtChild.Text.Trim();
            //string lot = "null";
            string model = txtModel.Text.Trim();
            string date = DateTime.Today.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("HH:mm:ss");
            string cumRecords = string.Empty;

            if (sn == string.Empty) return;
            if (sn.IndexOf("+") >= 1) sn = VBS.Left(sn, 17);

            for (int i = 0; i < rowPartsLot; i++)
            {
                string lotInf = dgvPartsLot[3, i].Value.ToString().Replace(":", "_") + ":" + string.Empty + ":" + description[i] + ":" + dgvPartsLot[0, i].Value.ToString() + ":" +
                    dgvPartsLot[1, i].Value.ToString() + ":" + dgvPartsLot[2, i].Value.ToString().Replace(":", "_") + ":" + dgvPartsLot[4, i].Value.ToString();
                    lotInf = lotInf.Replace(" ", "_").Replace(",", "_").Replace("'", "_").Replace(";", "_").Replace("\"", "_");
                string newRecord = sn + "," + dgvPartsLot[3, i].Value.ToString().Replace(":", "_") + "," + model + "," + date + "," + time + "," + lotInf;
                cumRecords += newRecord + System.Environment.NewLine;
            }

            try
            {
                string outFile = outPath + DateTime.Today.ToString("yyyyMMdd") + ".txt";
                if (System.IO.File.Exists(outFile))
                {
                    System.IO.File.AppendAllText(outFile, cumRecords, System.Text.Encoding.GetEncoding("UTF-8"));
                }
                else
                {
                    string header = DateTime.Today.ToString("yyyy/MM/dd") + "," + model + Environment.NewLine +
                        "SN,LOT,MODELNAME,DATE,TIME,LOTINFO" + Environment.NewLine;
                    System.IO.File.AppendAllText(outFile, header + cumRecords, System.Text.Encoding.GetEncoding("UTF-8"));
                }

                cumCount += 1;
                txtCount.Text = cumCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtChild.Enabled = true;
            txtChild.Focus();
            txtChild.SelectAll();
        }

        private void colorViewForDuplicate(ref DataGridView dgv)
        {
            DataTable dt = ((DataTable)dgv.DataSource).Copy();
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                string partCode = dgv["part_code", i].Value.ToString();
                DataRow[] dr = dt.Select("part_code = '" + partCode + "'");
                if (partCode == string.Empty || dr.Length >= 2)
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    soundAlarm();
                    duplicate = true;
                }
                else
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Window);
                    duplicate = false;
                }
            }
        }

        private void resetViewColor(ref DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv.Rows[i].DefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Window);
            }
            duplicate = false;
        }

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
        // Windows API
        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern int mciSendString(String command,StringBuilder buffer, int bufferSize, IntPtr hwndCallback);

        private string readIni(string s, string k, string cfs)
        {
            StringBuilder retVal = new StringBuilder(255);
            string section = s;
            string key = k;
            string def = String.Empty;
            int size = 255;
            int strref = GetPrivateProfileString(section, key, def, retVal, size, cfs);
            return retVal.ToString();
        }
        // Windows API
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filepath);

        private string makeSqlWhereClause(string criteria)
        {
            string sql = " where (";
            foreach (string c in criteria.Split(','))
            {
                sql += "process = " + c + " or ";
            }
            sql = VBS.Left(sql, sql.Length - 3) + ") ";
            System.Diagnostics.Debug.Print(sql);
            return sql;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            string standByImagePath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\JigQuickDesk\JigQuickApp\images\" + standByImageFile;
            pnlResult.BackgroundImageLayout = ImageLayout.Zoom;
            pnlResult.BackgroundImage = System.Drawing.Image.FromFile(standByImagePath);
            pnl4257.BackgroundImageLayout = ImageLayout.Zoom;
            pnl4257.BackgroundImage = System.Drawing.Image.FromFile(standByImagePath);

            lblResult.ResetText();
            txtResultDetail.Text = string.Empty;
            txtChild.ResetText();
            txtProduct.ResetText();
            txtChild.ReadOnly = false;
            txtChild.BackColor = Color.White;
            txtProduct.BackColor = Color.White;
            txtChild.Focus();
        }

        private void btnSetZero_Click(object sender, EventArgs e)
        {
            okCount = 0;
            ngCount = 0;
            txtOkCount.Text = okCount.ToString();
            txtNgCount.Text = ngCount.ToString();
        }

        // �e�X�g���ʂ��i�[����N���X
        public class TestResult
        {
            public string process { get; set; }
            public string judge { get; set; }
            public string inspectdate { get; set; }
        }

        // �e�X�g���ʂ̃v���Z�X�R�[�h�݂̂��i�[����N���X
        public class ProcessList
        {
            public string process { get; set; }
        }

        // �m�s�q�r�̏������s���A���茋�ʂ�Ԃ�
        private bool ntrsScanProcess(string id)
        {
            TfSQL tf = new TfSQL();
            DataTable dt = new DataTable();
            string log = string.Empty;
            string module = txtChild.Text;
            string mdlShort = module; // �P�V�P�^�ɐݒ��߂�
            string sql1;

            string scanTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            if (bracketCrustLinkSwitch == "ON")
            {
                sql1 = "select process, judge, inspectdate from " +
                        "(select process, judge, max(inspectdate) as inspectdate, row_number() OVER (PARTITION BY process ORDER BY max(inspectdate) desc) as flag from (" +
                        "(select process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTableThisMonth +
                        " where (process in (" + targetProcess + ") and serno = '" + mdlShort + "') " +
                        "union all select process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTablesubThisMonth +
                        " where (process in (" + targetProcessCrust + ") and " +
                        "serno in (select lot from " + headTableThisMonth + " where serno = '" + mdlShort + "' and lot like 'L27%'))) " +
                       "union all " +
                        "(select process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTableLastMonth +
                        " where (process in (" + targetProcess + ") and serno = '" + mdlShort + "') " +
                        "union all select process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTablesubLastMonth +
                        " where (process in (" + targetProcessCrust + ") and " +
                        "serno in (select lot from " + headTableLastMonth + " where serno = '" + mdlShort + "' and lot like 'L27%'))) " +
                       "union all " +
                        "(select process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTableThisMonth +
                        " where (process in (" + targetProcess + ") and serno = '" + mdlShort + "') " +
                        "union all select process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTablesubLastMonth +
                        " where (process in (" + targetProcessCrust + ") and " +
                        "serno in (select lot from " + headTableThisMonth + " where serno = '" + mdlShort + "' and lot like 'L27%'))) " +
                        ") a group by judge, process order by judge desc, process) b where flag = 1";
            }
            else
            {
                sql1 = "select process, judge, inspectdate from " +
                        "(select process, judge, max(inspectdate) as inspectdate, row_number() OVER (PARTITION BY process ORDER BY max(inspectdate) desc) as flag from (" +
                        "(select process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTableThisMonth + " where process in (" + targetProcess + ") and serno = '" + mdlShort + "') union all " +
                        "(select process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTableLastMonth + " where process in (" + targetProcess + ") and serno = '" + mdlShort + "')" +
                        ") d group by judge, process order by judge desc, process) b where flag = 1";
            }
            System.Diagnostics.Debug.Print(sql1);
            tf.sqlDataAdapterFillDatatableFromPqmDb(sql1, ref dt);

            if (ntrsSwitch == "ON")
            {
                string sql2 = "select process, judge, max(inspectdate) as inspectdate from (" +
                    "(select 'BOJAY' as process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTableThisMonth + " where serno = '" + mdlShort + "' and factory = 'SLEF') union all " +
                    "(select 'BOJAY' as process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTableLastMonth + " where serno = '" + mdlShort + "' and factory = 'SLEF')" +
                    ") d group by judge, process order by judge desc, process";
                System.Diagnostics.Debug.Print(sql2);
                tf.sqlDataAdapterFillDatatableFromTesterDb(sql2, ref dt);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                System.Diagnostics.Debug.Print(dt.Rows[i][0].ToString() + " " + dt.Rows[i][1].ToString() + " " + dt.Rows[i][2].ToString());
            }

            var allResults = dt.AsEnumerable().Select(r => new TestResult()
            { process = r.Field<string>("process"), judge = r.Field<string>("judge"), inspectdate = r.Field<DateTime>("inspectdate").ToString("yyyy/MM/dd HH:mm:ss"), }).ToList();


            // �P�D�p�X�̃v���Z�X�����擾
            var passResults = allResults.Where(r => r.judge == "PASS").Select(r => new ProcessList() { process = r.process }).OrderBy(r => r.process).ToList();
            foreach (var p in passResults) System.Diagnostics.Debug.Print(p.process);

            // �Q�D�P�Ɋ܂܂�Ȃ��t�F�C���̃v���Z�X�����擾
            var failResults = allResults.Where(r => r.judge == "FAIL").Select(r => new ProcessList() { process = r.process }).OrderBy(r => r.process).ToList();
            List<string> process = failResults.Select(r => r.process).Except(passResults.Select(r => r.process)).ToList();
            failResults = failResults.Where(r => process.Contains(r.process)).ToList();
            foreach (var p in failResults) System.Diagnostics.Debug.Print(p.process);

            // �R�D�P�ɂ��Q�ɂ��܂܂�Ȃ��A�e�X�g���ʂȂ��v���Z�X���擾����
            var skipResults = targetProcessCombined.Replace("'", string.Empty).Split(',').ToList().Select(r => new ProcessList() { process = r.ToString() }).OrderBy(r => r.process).ToList();
            process = skipResults.Select(r => r.process).Except(passResults.Select(r => r.process)).ToList().Except(failResults.Select(r => r.process)).ToList();
            skipResults = skipResults.Where(r => process.Contains(r.process)).ToList();
            foreach (var p in skipResults) System.Diagnostics.Debug.Print(p.process);

            // �f�B�X�v���C�p�̃v���Z�X�����X�g�����H����
            string displayPass = string.Empty;
            string displayFail = string.Empty;
            string displayAll = string.Empty;   // ���O�p
            List<TestResult> allLog = new List<TestResult>();
            foreach (var p in passResults)
            {
                displayPass += p.process + " ";
                allLog.Add(new TestResult { process = p.process, judge = "PASS", inspectdate = string.Empty });
            }
            displayPass = displayPass.Trim();
            foreach (var p in failResults)
            {
                displayFail += p.process + " F ";
                allLog.Add(new TestResult { process = p.process, judge = "FAIL", inspectdate = string.Empty });
            }
            foreach (var p in skipResults)
            {
                displayFail += p.process + " S ";
                allLog.Add(new TestResult { process = p.process, judge = "SKIP", inspectdate = string.Empty });
            }
            displayFail = displayFail.Trim();
            allLog = allLog.OrderBy(r => r.process).ToList();
            foreach (var p in allLog)
            {
                displayAll += (p.process + ":" + p.judge + ",");
            }
            displayAll = VBS.Left(displayAll, displayAll.Length - 1);

            // �A�v���P�[�V�����X�N���[���ɁA�e�X�g���ʂ�\������
            bool result = false;
            if (passResults.Count == targetProcessCount && txtChild.BackColor != Color.Red)
            {
                string okImagePass = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\JigQuickDesk\JigQuickApp\images\" + okImageFile;
                pnlResult.BackgroundImageLayout = ImageLayout.Zoom;
                pnlResult.BackgroundImage = System.Drawing.Image.FromFile(okImagePass);
                txtProduct.Text = txtChild.Text;
                checkDuplicate();

                // �n�j�J�E���g�̉��Z
                okCount += 1;
                txtOkCount.Text = okCount.ToString();

                result = true;
                // ���̃��W���[���̃X�L�����ɂ��Ȃ��A�X�L�����p�e�L�X�g�{�b�N�X�̃e�L�X�g��I�����A�㏑���\�ɂ���
                txtChild.SelectAll();
            }
            else
            {
                string ngImagePath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\JigQuickDesk\JigQuickApp\images\" + ngImageFile;
                pnlResult.BackgroundImageLayout = ImageLayout.Zoom;
                pnlResult.BackgroundImage = System.Drawing.Image.FromFile(ngImagePath);

                // �m�f�J�E���g�̉��Z
                ngCount += 1;
                txtNgCount.Text = ngCount.ToString();

                result = true;
                // ���̃��W���[���̃X�L�������X�g�b�v����߂��A�X�L�����p�e�L�X�g�{�b�N�X�𖳌��ɂ���
                txtChild.ReadOnly = true;
                txtChild.BackColor = Color.Red;

                // �A���[���ł̌x��
                soundAlarm();
            }

            // �A�v���P�[�V�����X�N���[���ƃf�X�N�g�b�v�t�H���_�̗����̗p�r�p�ɁA���t�ƃe�X�g���ʏڍו�������쐬
            log = Environment.NewLine + scanTime + "," + module + "," + displayAll;

            // �X�N���[���ւ̕\��
            txtResultDetail.Text = log.Replace(",", ",  ").Replace(Environment.NewLine, string.Empty);

            // ���O�����݁F�������t�̃t�@�C�������݂���ꍇ�͒ǋL���A���݂��Ȃ��ꍇ�̓t�@�C�����쐬�ǋL����iAppendAllText ������Ă����j
            try
            {
                string outFile = outPath2 + DateTime.Today.ToString("yyyyMMdd") + ".txt";
                if (System.IO.File.Exists(outFile))
                {
                    System.IO.File.AppendAllText(outFile, log, System.Text.Encoding.GetEncoding("UTF-8"));
                }
                else
                {
                    string header = DateTime.Today.ToString("yyyy/MM/dd") + " " + model + " " + subAssyName +
                        Environment.NewLine + "SCAN TIME,PRODUCT SERIAL,TEST DETAIL";
                    System.IO.File.AppendAllText(outFile, header + log, System.Text.Encoding.GetEncoding("UTF-8"));
                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return result;
            }
        }
        private bool ntrsScanProcess4257(string id)
        {
            TfSQL tf = new TfSQL();
            DataTable dt4257 = new DataTable();
            string log = string.Empty;
            string module = txtChild.Text;
            string mdlShort = module; // �P�V�P�^�ɐݒ��߂�
            string sql3;

            string scanTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            if (bracketCrustLinkSwitch == "ON")
            {
                sql3 = "select process, judge, inspectdate from " +
                        "(select process, judge, max(inspectdate) as inspectdate, row_number() OVER (PARTITION BY process ORDER BY max(inspectdate) desc) as flag from (" +
                        "(select process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTableThisMonth +
                        " where (process in (" + targetProcess4257 + ") and serno = '" + mdlShort + "') " +
                        "union all select process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTablesubThisMonth +
                        " where (process in (" + targetProcessCrust4257 + ") and " +
                        "serno in (select lot from " + headTableThisMonth + " where serno = '" + mdlShort + "' and lot like 'L27%'))) " +
                       "union all " +
                        "(select process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTableLastMonth +
                        " where (process in (" + targetProcess4257 + ") and serno = '" + mdlShort + "') " +
                        "union all select process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTablesubLastMonth +
                        " where (process in (" + targetProcessCrust4257 + ") and " +
                        "serno in (select lot from " + headTableLastMonth + " where serno = '" + mdlShort + "' and lot like 'L27%'))) " +
                       "union all " +
                        "(select process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTableThisMonth +
                        " where (process in (" + targetProcess4257 + ") and serno = '" + mdlShort + "') " +
                        "union all select process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTablesubLastMonth +
                        " where (process in (" + targetProcessCrust4257 + ") and " +
                        "serno in (select lot from " + headTableThisMonth + " where serno = '" + mdlShort + "' and lot like 'L27%'))) " +
                        "union all select process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTablesubThisMonth +
                        " where (process in (" + targetProcessCrust4257 + ") and " +
                        "serno in (select lot from " + headTableLastMonth + " where serno = '" + mdlShort + "' and lot like 'L27%'))) " +
                        "a group by judge, process order by judge desc, process) b where flag = 1";
            }
            else
            {
                sql3 = "select process, judge, inspectdate from " +
                        "(select process, judge, max(inspectdate) as inspectdate, row_number() OVER (PARTITION BY process ORDER BY max(inspectdate) desc) as flag from (" +
                        "(select process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTableThisMonth + " where process in (" + targetProcess4257 + ") and serno = '" + mdlShort + "') union all " +
                        "(select process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTableLastMonth + " where process in (" + targetProcess4257 + ") and serno = '" + mdlShort + "')" +
                        ") d group by judge, process order by judge desc, process) b where flag = 1";
            }
            System.Diagnostics.Debug.Print(sql3);
            tf.sqlDataAdapterFillDatatableFromPqmDb(sql3, ref dt4257);

            if (ntrsSwitch == "ON")
            {
                string sql2 = "select process, judge, max(inspectdate) as inspectdate from (" +
                    "(select 'BOJAY' as process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTableThisMonth + " where serno = '" + mdlShort + "' and factory = 'SLEF') union all " +
                    "(select 'BOJAY' as process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTableLastMonth + " where serno = '" + mdlShort + "' and factory = 'SLEF')" +
                    ") d group by judge, process order by judge desc, process";
                System.Diagnostics.Debug.Print(sql2);
                tf.sqlDataAdapterFillDatatableFromTesterDb(sql2, ref dt4257);
            }

            for (int i = 0; i < dt4257.Rows.Count; i++)
            {
                System.Diagnostics.Debug.Print(dt4257.Rows[i][0].ToString() + " " + dt4257.Rows[i][1].ToString() + " " + dt4257.Rows[i][2].ToString());
            }

            var allResults = dt4257.AsEnumerable().Select(r => new TestResult()
            { process = r.Field<string>("process"), judge = r.Field<string>("judge"), inspectdate = r.Field<DateTime>("inspectdate").ToString("yyyy/MM/dd HH:mm:ss"), }).ToList();


            // �P�D�p�X�̃v���Z�X�����擾
            var passResults = allResults.Where(r => r.judge == "PASS").Select(r => new ProcessList() { process = r.process }).OrderBy(r => r.process).ToList();
            foreach (var p in passResults) System.Diagnostics.Debug.Print(p.process);

            // �Q�D�P�Ɋ܂܂�Ȃ��t�F�C���̃v���Z�X�����擾
            var failResults = allResults.Where(r => r.judge == "FAIL").Select(r => new ProcessList() { process = r.process }).OrderBy(r => r.process).ToList();
            List<string> process = failResults.Select(r => r.process).Except(passResults.Select(r => r.process)).ToList();
            failResults = failResults.Where(r => process.Contains(r.process)).ToList();
            foreach (var p in failResults) System.Diagnostics.Debug.Print(p.process);

            // �R�D�P�ɂ��Q�ɂ��܂܂�Ȃ��A�e�X�g���ʂȂ��v���Z�X���擾����
            var skipResults = targetProcessCombined4257.Replace("'", string.Empty).Split(',').ToList().Select(r => new ProcessList() { process = r.ToString() }).OrderBy(r => r.process).ToList();
            process = skipResults.Select(r => r.process).Except(passResults.Select(r => r.process)).ToList().Except(failResults.Select(r => r.process)).ToList();
            skipResults = skipResults.Where(r => process.Contains(r.process)).ToList();
            foreach (var p in skipResults) System.Diagnostics.Debug.Print(p.process);

            // �f�B�X�v���C�p�̃v���Z�X�����X�g�����H����
            string displayPass = string.Empty;
            string displayFail = string.Empty;
            string displayAll = string.Empty;   // ���O�p
            List<TestResult> allLog = new List<TestResult>();
            foreach (var p in passResults)
            {
                displayPass += p.process + " ";
                allLog.Add(new TestResult { process = p.process, judge = "PASS", inspectdate = string.Empty });
            }
            displayPass = displayPass.Trim();
            foreach (var p in failResults)
            {
                displayFail += p.process + " F ";
                allLog.Add(new TestResult { process = p.process, judge = "FAIL", inspectdate = string.Empty });
            }
            foreach (var p in skipResults)
            {
                displayFail += p.process + " S ";
                allLog.Add(new TestResult { process = p.process, judge = "SKIP", inspectdate = string.Empty });
            }
            displayFail = displayFail.Trim();
            allLog = allLog.OrderBy(r => r.process).ToList();
            foreach (var p in allLog)
            {
                displayAll += (p.process + ":" + p.judge + ",");
            }
            displayAll = VBS.Left(displayAll, displayAll.Length - 1);

            // �A�v���P�[�V�����X�N���[���ɁA�e�X�g���ʂ�\������
            bool result = false;
            if (skipResults.Count == targetProcessCount4257)
            {
                string skipImagePath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\JigQuickDesk\JigQuickApp\images\SKIP.png";
                pnl4257.BackgroundImageLayout = ImageLayout.Zoom;
                pnl4257.BackgroundImage = System.Drawing.Image.FromFile(skipImagePath);
                txtProduct.Text = txtChild.Text;

                result = true;
            }
            else
            {
                string ngImagePath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\JigQuickDesk\JigQuickApp\images\" + ngImageFile;
                pnl4257.BackgroundImageLayout = ImageLayout.Zoom;
                pnl4257.BackgroundImage = System.Drawing.Image.FromFile(ngImagePath);

                result = false;

                txtChild.BackColor = Color.Red;

                soundAlarm();
            }

            log = Environment.NewLine + scanTime + "," + module + "," + displayAll;

            txtDetail4257.Text = log.Replace(",", ",  ").Replace(Environment.NewLine, string.Empty);

            try
            {
                string outFile = outPath2 + DateTime.Today.ToString("yyyyMMdd") + ".txt";
                if (System.IO.File.Exists(outFile))
                {
                    System.IO.File.AppendAllText(outFile, log, System.Text.Encoding.GetEncoding("UTF-8"));
                }
                else
                {
                    string header = DateTime.Today.ToString("yyyy/MM/dd") + " " + model + " " + subAssyName +
                        Environment.NewLine + "SCAN TIME,PRODUCT SERIAL,TEST DETAIL";
                    System.IO.File.AppendAllText(outFile, header + log, System.Text.Encoding.GetEncoding("UTF-8"));
                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return result;
            }
        }
        public void transProduct(string serial)
        {
            txtChild.Text = serial;
        }

        private void txtChild_TextChanged(object sender, EventArgs e)
        {
            txtProduct.Text = txtChild.Text;
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            TfSQL tf = new TfSQL();
            string date = dtpDate.Value.AddDays(1).ToString("yyyy/MM/dd");
            string date1 = dtpDate.Value.ToString("yyyy/MM/dd");
            string count = tf.sqlExecuteScalarString("select count(serial_no) from t_serno where regist_date > '" + date1 + "' and regist_date <= '" + date + "'");
            lblSearch.Text = "COUNT: " + count;
        }
    }
}