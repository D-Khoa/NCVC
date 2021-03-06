using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Linq;
using System.Drawing;
using System.IO;

namespace JigQuick
{
    public partial class frmTipTail : Form
    {
        // コンフィグファイルと、出力テキストファイルは、デスクトップの指定のフォルダに保存する
        string appconfig = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\JigQuickDesk\JigQuickApp\info.ini";
        string outPath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\JigQuickDesk\ConverterTarget\";

        //その他、非ローカル変数
        DataTable dtPartsLot;
        DataTable dtTbi;
        int rowPartsLot;
        int partsLotCount;
        long cumCount;
        bool sound;
        bool duplicate;
        bool wrongScanOrder;
        string autoRegister = "ON";
        string parentTextBoxFunc = "OFF";
        string c1c2prMatching = "ON";
        string processMode;
        string[] description;
        string[] partsLotBreakdown;
        string[] jigposition;
        string[] c1c2prList;

        /// <summary>
        // application name that is given from caller application for displaying itself with version on login screen
        /// </summary>
        private string applicationName;

        // コンストラクタ
        public frmTipTail(string applicationname)
        {
            applicationName = applicationname;

            InitializeComponent();
        }

        // ロード時の処理
        private void frmInut_Load(object sender, EventArgs e)
        {
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
                version.Append(deploy.Build);
                version.Append("_");
                version.Append(deploy.Revision);

                Version_lbl.Text = version.ToString();
            }

            // 部品ロット保持用データテーブルの行数設定を取得
            rowPartsLot = int.Parse(readIni("OTHERS", "PARTS LOT COUNT", appconfig));
            description = readIni("OTHERS", "DESCRIPTION", appconfig).Split(',');
            partsLotBreakdown = readIni("OTHERS", "PARTS LOT BREAKDOWN", appconfig).Split(',');
            jigposition = readIni("OTHERS", "JIG POSITION", appconfig).Split(',');
            processMode = readIni("LABEL DESCRIPTION", "PROCESS TEXT BOX", appconfig);
            c1c2prMatching = readIni("APPLICATION BEHAVIOR", "CH1, CH2, PR MATCHING", appconfig);
            c1c2prList = readIni("OTHERS", "CH1, CH2, PR LIST", appconfig).Split(','); 
            // 自動登録モードのＯＮ・ＯＦＦ取得、登録ボタンの設定
            //autoRegister = readIni("APPLICATION BEHAVIOR", "AUTOMATIC REGISTER", appconfig);

            // 各種処理用のテーブルを生成
            dtPartsLot = new DataTable();
            dtTbi = new DataTable();

            // テーブルの定義
            defineTables(ref dtPartsLot, ref dtTbi);

            // ラベルの設定
            setLabels();

            // 累積カウント（同日のテキストファイル内のレコード数）を取得
            string outFile = outPath + DateTime.Today.ToString("yyyyMMdd") + ".txt";
            if (System.IO.File.Exists(outFile))
            {
                using (StreamReader r = new StreamReader(outFile))
                {
                    int i = 0;
                    while (r.ReadLine() != null) { i++; }

                    cumCount = (i -2) / rowPartsLot;
                    if (processMode == "Docking (D-5-2)") cumCount = cumCount / 2;
                }
            }

            // カウントテキストボックスの中身の表示
            txtCount.Text = cumCount.ToString();
        }

        // サブプロシージャ：ＤＴの定義
        private void defineTables(ref DataTable dt1, ref DataTable dt2)
        {
            // 部品ロットグリッドビューを準備する
            dt1.Columns.Add("part_code", Type.GetType("System.String"));
            dt1.Columns.Add("part_name", Type.GetType("System.String"));
            dt1.Columns.Add("vendor", Type.GetType("System.String"));
            dt1.Columns.Add("invoice", Type.GetType("System.String"));
            dt1.Columns.Add("shipdate", Type.GetType("System.String"));
            dt1.Columns.Add("qty", Type.GetType("System.String"));

            for (int i = 0; i < rowPartsLot; i++) dt1.Rows.Add(dt1.NewRow());
            dgvPartsLot.DataSource = dt1;

            // 行ヘッダーに行番号を追加し、行ヘッダ幅を自動調節する
            for (int i = 0; i < rowPartsLot; i++) dgvPartsLot.Rows[i].HeaderCell.Value = partsLotBreakdown[i];
            dgvPartsLot.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            // 登録処理用ＴＢＩデータテーブルを準備する
            dt2.Columns.Add("S/N", Type.GetType("System.String"));
            dt2.Columns.Add("Lot", Type.GetType("System.String"));
            dt2.Columns.Add("ModelName", Type.GetType("System.String"));
            dt2.Columns.Add("Date", Type.GetType("System.String"));
            dt2.Columns.Add("Time", Type.GetType("System.String"));
            dt2.Columns.Add("LotInfo", Type.GetType("System.String"));
        }

        // サブプロシージャ：ラベルの設定
        private void setLabels()
        {
            txtModel.Text = readIni("LABEL DESCRIPTION", "MODEL", appconfig);
            txtProcess.Text = readIni("LABEL DESCRIPTION", "PROCESS TEXT BOX", appconfig);
            lblPartsLot.Text = readIni("LABEL DESCRIPTION", "PARTS LOT LABEL", appconfig);
            lblChild.Text = readIni("LABEL DESCRIPTION", "CHILD LABEL", appconfig);
            lblChild2.Text = readIni("LABEL DESCRIPTION", "CHILD LBL 2", appconfig);
            lblParent.Text = readIni("LABEL DESCRIPTION", "PARENT LABEL", appconfig);
            parentTextBoxFunc = readIni("APPLICATION BEHAVIOR", "PARENT TEXT BOX", appconfig);
            txtParent.Enabled = parentTextBoxFunc == "ON" ? true : false;
        }

        // クリアボタン押下時の処理
        private void btnClerChild_Click(object sender, EventArgs e)
        {
            txtChild.Text = string.Empty;
            txtChild2.Text = string.Empty;
            txtParent.Text = string.Empty;
        }

        // 部品ロット情報のスキャン時の処理
        private void txtPartsLot_KeyDown_1(object sender, KeyEventArgs e)
        {
            // バーコード末尾のエンターキー以外は処理しない
            if (e.KeyCode != Keys.Enter) return;

            // 空文字の場合は処理しない
            string scan = txtPartsLot.Text;
            if (scan == string.Empty) return;

            // 部品ロットグリッドビューの現在のセル行と、その次の行を格納する
            int r = dgvPartsLot.CurrentCell.RowIndex;
            int y = r < rowPartsLot - 1 ? r + 1 : rowPartsLot - 1;

            // セミコロンでＱＲ読み取り内容を分割し、グリットビューに表示する
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
                dgvPartsLot.CurrentCell = dgvPartsLot[0 , y];
            }
            // 分割できない文字列の場合は、グリットビューをクリア（空表示にする）
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

            resetViewColor(ref dgvPartsLot);
        }

        // ＣＨＩＬＤテキストボックス、スキャン時の処理
        private void txtChild_KeyDown(object sender, KeyEventArgs e)
        {
            // バーコード末尾のエンターキー以外は処理しない
            if (e.KeyCode != Keys.Enter) return;

            // 空文字の場合は処理しない
            if (txtChild.Text == string.Empty) return;

            // 工程によって、テキストボックスのカーソル移動パターンを、ＩＦ文で変更する
            if (processMode == "Case Tip & Tail Holding (D-6)")
            {
                // ＰＡＲＥＮＴが埋まっていない場合は、ＰＡＲＥＮＴスキャン用にカーソルを移動
                if (txtParent.Text == string.Empty)
                {
                    txtParent.Focus();
                }
                // ＣＨＩＬＤ２が埋まっていない場合は、ＣＨＩＬＤ２スキャン用にカーソルを移動
                else if (txtChild2.Text == string.Empty)
                {
                    txtChild2.Focus();
                }
                // 親、子２人、全員テキストがそろった場合のみ、出力
                else
                {
                    checkAndOutput();
                }
            }
            else
            {
                // ＣＨＩＬＤ２が埋まっていない場合は、ＣＨＩＬＤ２スキャン用にカーソルを移動
                if (txtChild2.Text == string.Empty)
                {
                    txtChild2.Focus();
                }
                // ＰＡＲＥＮＴが埋まっていない場合は、ＰＡＲＥＮＴスキャン用にカーソルを移動
                else if (txtParent.Text == string.Empty)
                {
                    txtParent.Focus();
                }
                // 親、子２人、全員テキストがそろった場合のみ、出力
                else
                {
                    checkAndOutput();
                }
            }
        }

        // ＣＨＩＬＤ２テキストボックス、スキャン時の処理
        private void txtChild2_KeyDown(object sender, KeyEventArgs e)
        {
            // バーコード末尾のエンターキー以外は処理しない
            if (e.KeyCode != Keys.Enter) return;

            // 空文字の場合は処理しない
            if (txtChild2.Text == string.Empty) return;

            // ＣＨＩＬＤが埋まっていない場合は、ＣＨＩＬＤスキャン用にカーソルを移動
            if (txtChild.Text == string.Empty)
            {
                txtChild.Focus();
            }
            // ＰＡＲＥＮＴが埋まっていない場合は、ＰＡＲＥＮＴスキャン用にカーソルを移動
            else if (txtParent.Text == string.Empty)
            {
                txtParent.Focus();
            }
            // 親、子２人、全員テキストがそろった場合のみ、出力
            else
            {
                checkAndOutput();
            }
        }

        // ＰＡＲＥＮＴテキストボックス、スキャン時の処理
        private void txtParent_KeyDown(object sender, KeyEventArgs e)
        {
            // バーコード末尾のエンターキー以外は処理しない
            if (e.KeyCode != Keys.Enter) return;

            // 空文字の場合は処理しない
            if (txtParent.Text == string.Empty) return;

            // 工程によって、テキストボックスのカーソル移動パターンを、ＩＦ文で変更する
            if (processMode == "Case Tip & Tail Holding (D-6)")
            {
                // ＣＨＩＬＤ２が埋まっていない場合は、ＣＨＩＬＤ２スキャン用にカーソルを移動
                if (txtChild2.Text == string.Empty)
                {
                    txtChild2.Focus();
                }
                // ＣＨＩＬＤが埋まっていない場合は、ＣＨＩＬＤスキャン用にカーソルを移動
                else if (txtChild.Text == string.Empty)
                {
                    txtChild.Focus();
                }
                // 親、子２人、全員テキストがそろった場合のみ、出力
                else
                {
                    checkAndOutput();
                }
            }
            else
            {
                // ＣＨＩＬＤが埋まっていない場合は、ＣＨＩＬＤスキャン用にカーソルを移動
                if (txtChild.Text == string.Empty)
                {
                    txtChild.Focus();
                }
                // ＣＨＩＬＤ２が埋まっていない場合は、ＣＨＩＬＤ２スキャン用にカーソルを移動
                else if (txtChild2.Text == string.Empty)
                {
                    txtChild2.Focus();
                }
                // 親、子２人、全員テキストがそろった場合のみ、出力
                else
                {
                    checkAndOutput();
                }
            }
         }

        // サブプロシージャ：テキストファイル出力前の確認
        private void checkAndOutput()
        {
            // 部品ロットの重複および空白をマーキングする
            colorViewForDuplicate(ref dgvPartsLot);

            // ＯＫカウント＝ＲＯＷ設定の場合のみ、自動登録を行う
            partsLotCount = dgvPartsLot.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[0].Value.ToString() != string.Empty)
                    .Select(r => r.Cells[0].Value).GroupBy(r => r).Count();

            if (duplicate || partsLotCount != rowPartsLot)
            {
                MessageBox.Show("Part lot info is not enough.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // チャイルド１、チャイルド２、ペアレントのマッチングを行う（当機能の設定、ＯＮの場合）
            if (c1c2prMatching == "ON")
            {
                wrongScanOrder = matchScanOrder(txtChild.Text, txtChild2.Text, txtParent.Text);
                if (!wrongScanOrder)
                {
                    MessageBox.Show("Scan order is wrong." + Environment.NewLine + "Please clear and re-scan all jigs.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // ＴＩＰ＆ＴＡＩＬの場合と、それ以外で、出力内容を場合分け
            if (autoRegister == "ON")
            {
                if (processMode == "Case Tip & Tail Holding (D-6)")
                {
                    outputTbiInfoForTipTail();
                }
                else
                {
                    outputTbiInfoForDocking2();
                }
            }
        }

        // サブプロシージャ：テキストファイル出力（ＴＩＰ＆ＴＡＩＬ用）
        private void outputTbiInfoForTipTail()
        {                           
            string[] sn = { txtChild.Text.Trim(), txtChild2.Text.Trim() };
            string lot = txtParent.Text.Trim() == string.Empty? "null" : txtParent.Text.Trim();
            string model = txtModel.Text.Trim();
            string date = DateTime.Today.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("HH:mm:ss");
            string cumRecords = string.Empty;

            // ＴＩＰ＆ＴＡＩＬの場合、それぞれのジグに対応するそれぞれの複数の部品ロット情報を紐付け、２件のレコードを作成する
            for (int i = 0; i < sn.Length; i++)
            {
                string lotInf = (dgvPartsLot[3, i].Value.ToString().Replace(":", "_") + ":" + jigposition[i] + ":" + description[i] + ":" + dgvPartsLot[0, i].Value.ToString() + ":" +
                    dgvPartsLot[1, i].Value.ToString() + ":" + dgvPartsLot[2, i].Value.ToString().Replace(":", "_") + ":" + dgvPartsLot[4, i].Value.ToString())
                    .Replace(" ", "_").Replace(",", "_").Replace("'", "_").Replace(";", "_").Replace("\"", "_");

                // 製品シリアルの部品コンフィグ情報は、切り捨てる（１７桁のみ保持する）
                if (sn[i].IndexOf("+") >= 1) sn[i] = VBS.Left(sn[i], 17);

                string newRecord = sn[i] + "," + lot + "," + model + "," + date + "," + time + "," + lotInf;
                cumRecords += newRecord + System.Environment.NewLine;
            }

            // 同日日付のファイルが存在する場合は追記し、存在しない場合はファイルを作成しヘッダーを書き込みの上、追記する
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

                // 登録カウントの表示
                cumCount += 1;
                txtCount.Text = cumCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 次のスキャンに備え、
            txtChild.Text = string.Empty;
            txtChild2.Text = string.Empty;
            txtParent.Text = string.Empty;
            txtChild.Focus();
        }

        // サブプロシージャ：テキストファイル出力（ＤＯＣＫＩＮＧ２用）
        private void outputTbiInfoForDocking2()
        {
            string[] sn = { txtParent.Text.Trim(), txtChild2.Text.Trim() };
            string lot = txtChild.Text.Trim() == string.Empty ? "null" : txtChild.Text.Trim();
            string model = txtModel.Text.Trim();
            string date = DateTime.Today.ToString("yyyy/MM/dd");
            string time = DateTime.Now.ToString("HH:mm:ss");
            string cumRecords = string.Empty;

            // ＤＯＣＫＩＮＧ−２場合、ＦＲＡＭＥ ＪＩＧには部品ロット情報付き、ＤＯＣＫＩＮＧ ＵＰＰＥＲには部品ロットなしで、２件のレコードを作成する
            for (int i = 0; i < sn.Length; i++)
            {
                string lotInf = string.Empty;
                if (i == 0)
                {
                    lotInf = ("null" + ":" + jigposition[i] + ":" + description[i] + ":" + "null" + ":" + "null" + ":" + "null" + ":" + "2000/01/01")
                        .Replace(" ", "_").Replace(",", "_").Replace("'", "_").Replace(";", "_").Replace("\"", "_");
                }
                else
                {
                    lotInf = (dgvPartsLot[3, 0].Value.ToString().Replace(":", "_") + ":" + jigposition[i] + ":" + description[i] + ":" + dgvPartsLot[0, 0].Value.ToString() + ":" +
                        dgvPartsLot[1, 0].Value.ToString() + ":" + dgvPartsLot[2, 0].Value.ToString().Replace(":", "_") + ":" + dgvPartsLot[4, 0].Value.ToString())
                        .Replace(" ", "_").Replace(",", "_").Replace("'", "_").Replace(";", "_").Replace("\"", "_");
                }

                // 製品シリアルの部品コンフィグ情報は、切り捨てる（１７桁のみ保持する）
                if (sn[i].IndexOf("+") >= 1) sn[i] = VBS.Left(sn[i], 17);

                string newRecord = sn[i] + "," + lot + "," + model + "," + date + "," + time + "," + lotInf;
                cumRecords += newRecord + System.Environment.NewLine;
            }

            // 同日日付のファイルが存在する場合は追記し、存在しない場合はファイルを作成しヘッダーを書き込みの上、追記する
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

                // 登録カウントの表示
                cumCount += 1;
                txtCount.Text = cumCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 次のスキャンに備え、
            txtChild.Text = string.Empty;
            txtChild2.Text = string.Empty;
            txtParent.Text = string.Empty;
            txtChild.Focus();
        }

        // サブプロシージャ：重複部品ロット・空レコードをマーキングする
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

        // サブプロシージャ：グリットビューの色をリセットする
        private void resetViewColor(ref DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv.Rows[i].DefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Window);
            }
            duplicate = false;
        }

        // サブプロシージャ：チャイルド１、チャイルド２、ペアレントのマッチングを行う
        private bool matchScanOrder(string child1, string child2, string parent)
        {
            //bool ch1Missing = child1.IndexOf(c1c2prList[0]) == -1 ? true : false;
            //bool ch2Missing = child2.IndexOf(c1c2prList[1]) == -1 ? true : false;
            //bool parMissing = parent.IndexOf(c1c2prList[2]) == -1 ? true : false;

            bool ch1Missing = true;
            bool ch2Missing = true;
            bool parMissing = true;
            bool total;

            if (child1.Substring(0, c1c2prList[0].Length) != c1c2prList[0]) ch1Missing = false;
            if (child2.Substring(0, c1c2prList[1].Length) != c1c2prList[1]) ch2Missing = false;
            if (parent.Substring(0, c1c2prList[2].Length) != c1c2prList[2]) parMissing = false;

            if (ch1Missing == false || ch2Missing == false || parMissing == false)
            { total = false; }
            else total = true;
            if (!total) soundAlarm();
            return total;
        }

        //MP3ファイル（今回は警告音）を再生する
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
        // Windows API をインポート
        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern int mciSendString(String command,StringBuilder buffer, int bufferSize, IntPtr hwndCallback);

        // 設定テキストファイルの読み込み
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
        // Windows API をインポート
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filepath);
    }
}