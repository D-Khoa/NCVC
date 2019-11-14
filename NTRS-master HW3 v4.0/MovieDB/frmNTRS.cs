using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace NTRS
{
    public partial class frmNTRS : Form
    {
        // �v�����g�p�e�L�X�g�t�@�C���̕ۑ��p�t�H���_���A��{�ݒ�t�@�C���Őݒ肷��
        //string appconfig = System.Environment.CurrentDirectory + @"\info.ini";
        string appconfig = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\NTRS Setting\info.ini";
        string outPath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\NTRS Log\";

        //���̑��A�񃍁[�J���ϐ�
        int okCount;
        int ngCount;
        int targetProcessCount;
        bool sound;
        string model, line, ecode;
        string subAssyName;
        string targetProcess;
        string targetProcessCrust;
        string targetProcessCombined;
        string okImageFile;
        string ngImageFile;
        string standByImageFile;
        string ntrsSwitch;
        string bracketCrustLinkSwitch;
        string headTableThisMonth = string.Empty;
        string headTableLastMonth = string.Empty;

        // <summary>
        // application name that is given from caller application for displaying itself with version on login screen
        /// </summary>
        private string applicationName;
        // �R���X�g���N�^
        public frmNTRS(string applicationname)
        {
            InitializeComponent();

            applicationName = applicationname;
        }

        // ���[�h���̏���
        private void frmModule_Load(object sender, EventArgs e)
        {
            okImageFile = readIni("MODULE-DATA MATCHING", "OK IMAGE FILE", appconfig);
            ngImageFile = readIni("MODULE-DATA MATCHING", "NG IMAGE FILE", appconfig);
            standByImageFile = readIni("MODULE-DATA MATCHING", "STAND-BY IMAGE FILE", appconfig);
            string standByImagePath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\NTRS Setting\images\" + standByImageFile;
            pnlResult.BackgroundImageLayout = ImageLayout.Zoom;
            pnlResult.BackgroundImage = System.Drawing.Image.FromFile(standByImagePath);

            model = readIni("MODULE-DATA MATCHING", "MODEL", appconfig);
            line = readIni("MODULE-DATA MATCHING", "LINE", appconfig);
            ecode = readIni("MODULE-DATA MATCHING", "E-CODE", appconfig);
            subAssyName = readIni("MODULE-DATA MATCHING", "SUB ASSY NAME", appconfig);
            targetProcess = readIni("MODULE-DATA MATCHING", "TARGET PROCESS", appconfig);
            ntrsSwitch = readIni("MODULE-DATA MATCHING", "NTRS INLINE MATCHING", appconfig);
            bracketCrustLinkSwitch = readIni("MODULE-DATA MATCHING", "BRACKET-CRUST LINK", appconfig);
            targetProcessCrust = readIni("MODULE-DATA MATCHING", "CRUST TARGET PROCESS", appconfig);

            headTableThisMonth = model.ToLower() + DateTime.Today.ToString("yyyyMM");
            headTableLastMonth = model.ToLower() + ((VBS.Right(DateTime.Today.ToString("yyyyMM"), 2) != "01") ?
                (long.Parse(DateTime.Today.ToString("yyyyMM")) - 1).ToString() : (long.Parse(DateTime.Today.ToString("yyyy")) - 1).ToString() + "12");

            txtSubAssy.Text = model + " - " + subAssyName + " - LINE " + line;
            targetProcessCount = targetProcess.Where(c => c == ',').Count() + 1 + 
                (targetProcessCrust == string.Empty ? 0 : targetProcessCrust.Where(c => c == ',').Count() + 1);
            targetProcessCombined = targetProcess + (targetProcessCrust == string.Empty ? string.Empty : "," + targetProcessCrust);

            // �J�E���^�[�̕\���i�f�t�H���g�̓[���j
            txtOkCount.Text = okCount.ToString();
            txtNgCount.Text = ngCount.ToString();

            // ���O�p�t�H���_�̍쐬�i�t�H���_�����݂��Ȃ��ꍇ�j
            if (!Directory.Exists(outPath)) Directory.CreateDirectory(outPath);

            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {

                Version deploy = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;

                StringBuilder version = new StringBuilder();
                version.Append("VERSION: ");
                version.Append(applicationName + "_");
                version.Append(deploy.Major);
                version.Append("_");
                //version.Append(deploy.Minor);
                //version.Append("_");
                //version.Append(deploy.Build);
               // version.Append("_");
                version.Append(deploy.Revision);

                Version_lbl.Text = version.ToString();
            }
        }

        // �V���A�����X�L�������ꂽ���̏���            
        private void txtModuleId_KeyDown(object sender, KeyEventArgs e)
       {
            // �G���^�[�L�[�̏ꍇ�A�e�L�X�g�{�b�N�X�̌������P�V���܂��͂Q�S���̏ꍇ�̂݁A�������s��
            if (e.KeyCode != Keys.Enter) return;
            if (txtModuleId.ReadOnly) return;
            if (txtModuleId.Text.Length != 17 && txtModuleId.Text.Length != 24) return;
            if (subAssyName == "MOTOR ASSY")
            {
                txtModuleId.MaxLength = 24;
                if (txtModuleId.Text.Length != 24)
                {
                    MessageBox.Show("Module must be 24 digits", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string scanTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            TfSQL tf = new TfSQL();
            DataTable dt = new DataTable();
            string log = string.Empty;
            string module = txtModuleId.Text;
            string mdlShort = VBS.Left(module, 17); // �P�V�P�^�ɐݒ��߂�
            string sql1;
            string serCrush = tf.sqlExecuteScalarStringPqm("select lot from " + headTableThisMonth + "tbi where serno = '" + mdlShort + "' and lot != 'null' union all select lot from " + headTableLastMonth + "tbi where serno = '" + mdlShort + "' and lot != 'null'");

            // �w�X�X�W�^�w�X�X�X�t�@�C�i���`�r�r�x�́A�S�Ă̂`�n�h�H���̃f�[�^���ƍ�����
            if (bracketCrustLinkSwitch == "ON")
            {
                sql1 = "select process, judge, inspectdate from " +
                    "(select process, judge, max(inspectdate) as inspectdate, row_number() OVER (PARTITION BY process ORDER BY max(inspectdate) desc) as flag " +
                    "from ((select process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTableThisMonth + 
                        " where (process in (" + targetProcess + ") and serno = '" + mdlShort + "') " +
                            "or (process in (" + targetProcessCrust + ") and (" + 
                                "serno in ('" + serCrush + "')))))" +
                 " d group by judge, process order by judge desc, process) a where flag = 1" +
                 " union all " +
                 "select process, judge, inspectdate from " +
                    "(select process, judge, max(inspectdate) as inspectdate, row_number() OVER (PARTITION BY process ORDER BY max(inspectdate) desc) as flag " +
                    "from ((select process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTableLastMonth +
                        " where (process in (" + targetProcess + ") and serno = '" + mdlShort + "') " +
                            "or (process in (" + targetProcessCrust + ") and (" +
                                "serno in ('" + serCrush + "')))))" +
                 " d group by judge, process order by judge desc, process) a where flag = 1";
            }
            else
            {
                sql1 = "select process, judge, inspectdate from " +
                    "(select process, judge, max(inspectdate) as inspectdate, row_number() OVER (PARTITION BY process ORDER BY max(inspectdate) desc) as flag " +
                    "from (" +
                    "(select process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTableThisMonth + " where process in (" + targetProcess + ") and serno = '" + mdlShort + "') union all " +
                    "(select process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTableLastMonth + " where process in (" + targetProcess + ") and serno = '" + mdlShort + "')" +
                    ") d group by judge, process order by judge desc, process) a where flag = 1";
            }
            System.Diagnostics.Debug.Print(sql1);
            tf.sqlDataAdapterFillDatatableFromPqmDb(sql1, ref dt);

            // ���[�^�[�t�@�C�i���`�r�r�x�ɂ��ẮA�h�m�k�h�m�d�e�X�^�[�̏������킹�ă}�b�`���O����
            if (ntrsSwitch == "ON")
            {
                string sql2 = "select process, judge, inspectdate from (" +
                    "select process, judge, max(inspectdate) as inspectdate, row_number() OVER (PARTITION BY process ORDER BY max(inspectdate) desc) as flag " +
                    "from (" +
                    "(select 'BOJAY' as process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTableThisMonth + " where serno = '" + mdlShort + "' and factory = 'SLEF') union all " +
                    "(select 'BOJAY' as process, case when tjudge = '0' then 'PASS' else 'FAIL' end as judge, inspectdate from " + headTableLastMonth + " where serno = '" + mdlShort + "' and factory = 'SLEF')" +
                    ") d group by judge, process order by judge desc, process) a where flag = 1";
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
            var passResults = allResults.Where(r => r.judge == "PASS").Select(r => new ProcessList() { process = r.process}).OrderBy(r => r.process).ToList();
            foreach (var p in passResults) System.Diagnostics.Debug.Print(p.process);
            
            // �Q�D�P�Ɋ܂܂�Ȃ��t�F�C���̃v���Z�X�����擾
            var failResults = allResults.Where(r => r.judge == "FAIL").Select(r => new ProcessList() { process = r.process}).OrderBy(r => r.process).ToList();
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
            if (passResults.Count == targetProcessCount && VBS.Mid(module, 12, 4) == ecode)
            {
                string okImagePass = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\NTRS Setting\images\" + okImageFile;
                string judge;
                judge = "PASS";
                pnlResult.BackgroundImageLayout = ImageLayout.Zoom;
                pnlResult.BackgroundImage = System.Drawing.Image.FromFile(okImagePass);
                grMain.BackColor = Color.Aqua;
                txtProduct.Text = txtModuleId.Text;
                importDatabase(judge);

                // �n�j�J�E���g�̉��Z
                okCount += 1;
                txtOkCount.Text = okCount.ToString();

                // ���̃��W���[���̃X�L�����ɂ��Ȃ��A�X�L�����p�e�L�X�g�{�b�N�X�̃e�L�X�g��I�����A�㏑���\�ɂ���
                txtModuleId.SelectAll();
            }
            else
            {
                string ngImagePath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\NTRS Setting\images\" + ngImageFile;
                pnlResult.BackgroundImageLayout = ImageLayout.Zoom;
                pnlResult.BackgroundImage = System.Drawing.Image.FromFile(ngImagePath);
                grMain.BackColor = Color.LightPink;

                // �m�f�J�E���g�̉��Z
                ngCount += 1;
                txtNgCount.Text = ngCount.ToString();

                // ���̃��W���[���̃X�L�������X�g�b�v����߂��A�X�L�����p�e�L�X�g�{�b�N�X�𖳌��ɂ���
                txtModuleId.ReadOnly = true;
                txtModuleId.BackColor = Color.Red; 

                if (VBS.Mid(module, 12, 4) != ecode)
                {
                    MessageBox.Show("Ecode is wrong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                string outFile = outPath + DateTime.Today.ToString("yyyyMMdd") + ".txt";
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public int Check()
        {
            TfSQL tf = new TfSQL();
            string strTemp = "Select count(*) from t_ntrs_op_id where process=N'" + subAssyName + "'";
            int count = int.Parse(tf.sqlExecuteScalarStringSerial(strTemp).ToString());
            if (count.Equals(0))
            {
                return 1;
            }
            else
            {
                string strTemp1 = "Select count(*) from t_ntrs_op_id where process = N'" + subAssyName + "' and serial_no = N'" + txtProduct.Text + "'";
                int count1 = int.Parse(tf.sqlExecuteScalarStringSerial(strTemp1).ToString());
                if (count1.Equals(0))
                {
                    return 1;
                }
            }
            return 0;
        }

        // Import database
        private void importDatabase(string tjudge)
        {
            if (txtProduct.Text != String.Empty)
            {
                string d = DateTime.Now.ToString("yyyy/MM/dd HH:00:00");
                string dd = DateTime.Now.ToString();
                TfSQL tf = new TfSQL();
                string ser;
                //string sql = tf.sqlExecuteScalarStringSerial("SELECT serial_no from t_ntrs where serial_no = '" + txtProduct.Text + "'");
                //string sql1 = tf.sqlExecuteScalarStringSerial("SELECT process from t_ntrs where serial_no = '" + txtProduct.Text + "'");
                string[] subAssy = subAssyName.Split(' ');
                int a = Check();
                if (a == 1)//sql != txtProduct.Text || sql1 != subAssyName)
                {
                    if (ckbOpId.Checked == true)
                    {
                        ser = tf.sqlExecuteScalarStringSerial("INSERT INTO t_ntrs_op_id(model, line, process, app1_op_id, app2_op_id, serial_no, regist_date, tjudge, regist_date_real) " +
                        "VALUES('" + model + "', '" + line + "', '" + subAssyName + "', '" + txtOpId1.Text + "', '" + txtOpId2.Text + "', '" + txtProduct.Text + "', '" + d + "', '" + tjudge + "', '" + dd + "')");
                    }
                    else
                    {
                        ser = tf.sqlExecuteScalarStringSerial("INSERT INTO t_ntrs_op_id(model, line, process, app1_op_id, app2_op_id, serial_no, regist_date, tjudge, regist_date_real) " +
                        "VALUES('" + model + "', '" + line + "', '" + subAssyName + "', '" + subAssy[0] + "_1', '" + subAssy[0] + "_2', '" + txtProduct.Text + "', '" + d + "', '" + tjudge + "', '" + dd + "')");
                    }
                }
            }
        }
        // Check Duplicate barcode
        private void checkDuplicate()
        {
            if (txtProduct.Text != String.Empty)// && txtProduct.Text.Length != 24)
            {
                DateTime d = DateTime.Now;
                //DateTime dd = DateTime.Now.GetDateTimeFormats("yyyy/mm/dd");
                TfSQL tf = new TfSQL();
                string ser = tf.sqlExecuteScalarStringSerial("SELECT barcode_num FROM serial_data_t WHERE barcode_num = '" + txtProduct.Text + "'");
                if (ser != txtProduct.Text)
                {
                    lblResult.Text = "Barcode is OK"; lblResult.ForeColor = Color.Green;

                    string okImagePath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\NTRS Setting\images\" + okImageFile;
                    pnlResult.BackgroundImageLayout = ImageLayout.Zoom;
                    pnlResult.BackgroundImage = System.Drawing.Image.FromFile(okImagePath);
                    grMain.BackColor = Color.Aqua;

                    string date = DateTime.Today.AddDays(1).ToString("yyyy/MM/dd");
                    string date1 = DateTime.Today.ToString("yyyy/MM/dd");
                    string count = tf.sqlExecuteScalarStringSerial("select count(barcode_num) from serial_data_t where regist_date > '" + date1 + "' and regist_date <= '" + date + "'");
                    lblCount.Text = "TOTAL: " + count;
                }
                else
                {
                    lblResult.Text = "Duplicate Barcode";
                    lblResult.ForeColor = Color.Red;

                    string ngImagePath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\NTRS Setting\images\" + ngImageFile;
                    pnlResult.BackgroundImageLayout = ImageLayout.Zoom;
                    pnlResult.BackgroundImage = System.Drawing.Image.FromFile(ngImagePath);
                    txtModuleId.BackColor = Color.Red;
                    txtModuleId.ReadOnly = true;
                    grMain.BackColor = Color.LightPink;
                    soundAlarm();
                }
            }
        }

        // ���Z�b�g�{�^���������̏����F �p�l���ƃe�X�g���ʃe�L�X�g�{�b�N�X���N���A���A�X�L�����p�e�L�X�g�{�b�N�X��L���ɂ���
        private void btnReset_Click(object sender, EventArgs e)
        {
            string standByImagePath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\NTRS Setting\images\" + standByImageFile;
            pnlResult.BackgroundImageLayout = ImageLayout.Zoom;
            pnlResult.BackgroundImage = System.Drawing.Image.FromFile(standByImagePath);
            grMain.BackColor = SystemColors.Control;

            lblResult.ResetText();
            txtResultDetail.Text = string.Empty;
            txtModuleId.ResetText();
            txtModuleId.Focus();
            txtProduct.ResetText();
            txtModuleId.ReadOnly = false;
            txtModuleId.BackColor = Color.White;
            txtProduct.BackColor = Color.White; 
        }

        // �����̃��O�t�@�C���̃��R�[�h���𐔂���
        private void btnTodaysCount_Click(object sender, EventArgs e)
        {

        }

        // �J�E���^�[���N���A����
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

        // Windows API ���C���|�[�g
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filepath);

        // �ݒ�e�L�X�g�t�@�C���̓ǂݍ���
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

        //MP3�t�@�C�����Đ�����
        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern int mciSendString(String command,
           StringBuilder buffer, int bufferSize, IntPtr hwndCallback);

        private string aliasName = "MediaFile";

        private void soundAlarm()
        {
            string fileName = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\NTRS Setting\warning.mp3";
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

        private void txtOpId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            txtOpId2.Focus();
        }

        private void ckbOpId_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbOpId.Checked == true)
            {
                txtOpId1.Enabled = true;
                txtOpId2.Enabled = true;
                txtModuleId.Enabled = false;
            }
            else
            {
                txtOpId1.Enabled = false;
                txtOpId2.Enabled = false;
                txtModuleId.Enabled = true;
            }
        }

        private void txtOpId2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            txtModuleId.Enabled = true;
            txtModuleId.Focus();
        }

        private void txtOpId1_Click(object sender, EventArgs e)
        {
            txtOpId1.SelectAll();
        }

        private void txtOpId2_Click(object sender, EventArgs e)
        {
            txtOpId2.SelectAll();
        }

        private void txtModuleId_TextChanged(object sender, EventArgs e)
        {
            txtProduct.Text = txtModuleId.Text;
        }
    }
}