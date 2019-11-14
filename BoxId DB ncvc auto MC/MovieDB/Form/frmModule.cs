using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using KvComHelper;
using Keyence.AutoID;
using System.Collections.Generic;

namespace BoxIdDb
{
    public partial class frmModule : Form
    {
        #region variables

        // delegate function to trigger ivent in the prarent form "frmBox"
        public delegate void RefreshEventHandler(object sender, EventArgs e);
        public event RefreshEventHandler RefreshEvent;

        // address of setting file and the barcode text output folder
        string appconfig = System.Environment.CurrentDirectory + "\\info.ini";
        string directory = @"C:\Users\takusuke.fujii\Desktop\Auto Print\\";

        // variables for controlling the form's mode and basic functions
        DataTable dtInline;
        DataTable dtOqc;
        bool formEditMode;
        bool formReturnMode;
        bool sound;
        bool errorInCheks;
        string user;
        string m_eeee, m_line;
        string m_model;
        string m_model_long;
        

        // variables for controlling the tray capacity
        double trayLimitHw1Large;
        double trayLimitHw1Small;
        double trayLimitHw2Large;
        double trayLimitHw2Small;
        double trayLimitHw3Small;
        double trayLimitHw3Large;
        double trayLimit = 9999;
        double packLimitHw1Large;
        double packLimitHw1Small;
        double packLimitHw2Large;
        double packLimitHw2Small;
        double packLimitHw3Small;
        double packLimitHw3Large;
        double packLimitJudge = 9999;
        double packLimitChange = 0;
        int okCount;

        // variables for tester data get sql commands
        string fltHw1Line = "lot not like '%CA%' and lot not like '%REL%'";
        string fltHw1Oqc = "lot like '%CA%' and lot not like '%REL%'";
        string fltHw2Line = "lot like '%DEVELOPMENT4' or lot like '%TAP-CAL'";
        string fltHw2Oqc = "lot like '%DEVELOPMENT6' or lot like '%TAP-CAL-OQC' or lot like '%TAP-OQC'";
        string fltHw3Line = "lot like '%DEVELOPMENT4'";
        string fltHw3Oqc = "lot like '%DEVELOPMENT5'";
        string testerTableThisMonth;
        string testerTableLastMonth;

        // variables for KV-COM PLC communication
        frmEditDevice kvForm;
        
        const string deviceStartScan = "R15100";
        const string deviceJudgeOK = "R15001";
        const string deviceJudgeNG = "R15002";
        const string deviceValueON = "1";
        const string deviceValueOFF = "0";


        // variables for controling operation
        const string operationPackStanby = "Pack Standby";
        const string operationTrayStandby = "Tray Standby";
        const string operationTrayReadying = "Tray Reading";
        const string operationModuleRestart = "Module Restart";

        private readonly Dictionary<string, ReaderType> readerTable = new Dictionary<string, ReaderType>
        {
            {"SR-D100", ReaderType.SR_D100},
            {"SR-750", ReaderType.SR_750},
            {"SR-700", ReaderType.SR_700},
            {"SR-1000", ReaderType.SR_1000},
            {"SR-2000", ReaderType.SR_2000}
        };

        #endregion


        #region form initialization

        // constructor
        public frmModule()
        {
            InitializeComponent();
            btnLiON.Enabled = false;
            btnLiOff.Enabled = false;
            kvForm = new frmEditDevice();
            kvForm.Show();
            kvForm.Visible = false;
            KvSetStartScan();

            comboBoxReader.Items.Clear();
            foreach (var reader in readerTable)
            {
                comboBoxReader.Items.Add(reader.Key);
            }
            comboBoxReader.SelectedIndex = readerTable.Count - 1;

            //
            // Register handler for reading data received event
            //
            this.barcodeReaderControl1.OnDataReceived += barcodeReaderControl1_OnDataReceived;
            //
            // Set the interface to connect the reader
            //
            this.barcodeReaderControl1.Comm.Interface = Interface.Ethernet;
            //
            // Set the IP address of the reader to connect
            //
            this.barcodeReaderControl1.IpAddress = "192.168.100.100";
            //
            // Set the TCP port number of the reader to connect
            //
            // Comannd port number must be different from Data port number
            //
            this.barcodeReaderControl1.Ether.CommandPort = 9003;
            this.barcodeReaderControl1.Ether.DataPort = 9004;
        }

        // load event
        private void frmModule_Load(object sender, EventArgs e)
        {
            user = txtUser.Text;

            // get barcode print file output folder address and tray capacity for each address from setting file
            directory = readIni("TARGET DIRECTORY", "DIR", appconfig);
            fltHw1Line = readIni("TESTER FILTER", "HW1INLINE", appconfig);
            fltHw1Oqc = readIni("TESTER FILTER", "HW1OQC", appconfig);
            fltHw2Line = readIni("TESTER FILTER", "HW2INLINE", appconfig);
            fltHw2Oqc = readIni("TESTER FILTER", "HW2OQC", appconfig);
            fltHw3Line = readIni("TESTER FILTER", "HW3INLINE", appconfig);
            fltHw3Oqc = readIni("TESTER FILTER", "HW3OQC", appconfig);
            //int.TryParse(readIni("TRAY CAPACITY", "HW1 LARGE", appconfig), out trayLimitHw1Large);
            //int.TryParse(readIni("TRAY CAPACITY", "HW1 SMALL", appconfig), out trayLimitHw1Small);
            //int.TryParse(readIni("TRAY CAPACITY", "HW2 LARGE", appconfig), out trayLimitHw2Large);
            //int.TryParse(readIni("TRAY CAPACITY", "HW2 SMALL", appconfig), out trayLimitHw2Small);
            //int.TryParse(readIni("TRAY CAPACITY", "HW3 LARGE", appconfig), out trayLimitHw3Large);
            //int.TryParse(readIni("TRAY CAPACITY", "HW3 SMALL", appconfig), out trayLimitHw3Small);
            //int.TryParse(readIni("PACK CAPACITY", "HW1 LARGE", appconfig), out packLimitHw1Large);
            //int.TryParse(readIni("PACK CAPACITY", "HW1 SMALL", appconfig), out packLimitHw1Small);
            //int.TryParse(readIni("PACK CAPACITY", "HW2 LARGE", appconfig), out packLimitHw2Large);
            //int.TryParse(readIni("PACK CAPACITY", "HW2 SMALL", appconfig), out packLimitHw2Small);
            //int.TryParse(readIni("PACK CAPACITY", "HW3 LARGE", appconfig), out packLimitHw3Large);
            //int.TryParse(readIni("PACK CAPACITY", "HW3 SMALL", appconfig), out packLimitHw3Small);

            // Admin function dfg
            if (user == "User_9") { btnDelBox.Enabled = true; txtOkCount.Enabled = true; }
            // instantiate and define datatables 
            dtInline = new DataTable();
            dtOqc = new DataTable();
            defineDtOverall(ref dtInline);
            definedtOqc(ref dtOqc);
            if (!formEditMode) readDatatable(ref dtInline, txtBoxId.Text);

            // update grid views
            updateDataGridViews(dtInline, dtOqc, ref dgvInline, ref dgvOqc);

            // show operation step
            txtOperation.Text = operationPackStanby;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //
                // Connect to the Reader
                //
                this.barcodeReaderControl1.Connect();
                this.textBox1.Text = "Connected successfully";
                if (this.barcodeReaderControl1.Comm.Interface == Interface.USB)
                {
                    //
                    // Send "SKCLOSE" in order to occupy the data port connection
                    //
                    this.barcodeReaderControl1.SKCLOSE();
                }
                else
                {
                    //
                    // Make sure that command response character string is specified.
                    //
                    string val = this.barcodeReaderControl1.RP("610");
                    switch (this.barcodeReaderControl1.ReaderType)
                    {
                        case ReaderType.SR_2000:
                        case ReaderType.SR_1000:
                        case ReaderType.SR_700:
                            if (!val.Equals("1"))
                            {
                                this.textBox1.Text = "Set Baseic command response string to Detailed response.";
                            }
                            break;
                        case ReaderType.SR_D100:
                        case ReaderType.SR_750:
                            if (!val.Equals("0"))
                            {
                                this.textBox1.Text = "Disable the setting of Specify response character.";
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.textBox1.Text = ex.Message;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            try
            {
                //
                // Send LON command
                //
                this.barcodeReaderControl1.LON();
            }
            catch (CommandException cex)
            {
                //
                // ExtErrCode shows the number of command error
                //
                this.textBox1.Text = "Command err," + cex.ExtErrCode;
            }
            catch (Exception ex)
            {
                this.textBox1.Text = ex.Message;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            try
            {
                //
                // Send LOFF command
                //
                this.barcodeReaderControl1.LOFF();
            }
            catch (Exception ex)
            {
                this.textBox1.Text = ex.Message;
            }
        }

        private void barcodeReaderControl1_OnDataReceived(object sender, OnDataReceivedEventArgs e)
        {
            //
            // Delegate display processing of the received data to the textBox
            //
            this.txtBarcode.Invoke(new updateTextBoxDelegate(updateTextBox), e.data);
        }

        //
        // Delegated function of the textBox
        //
        private delegate void updateTextBoxDelegate(byte[] data);
        private void updateTextBox(byte[] data)
        {
            //
            // Display the received data to the textBox 
            //
            this.txtBarcode.Text = Encoding.GetEncoding("Shift_JIS").GetString(data);

            //
            // Image file saving process
            //
            bool saveImage;
            if (this.txtBarcode.Text.StartsWith("ERROR"))
            {
                saveImage = radioButtonImageErrSave.Checked;

            }
            else
            {
                saveImage = radioButtonImageOkSave.Checked;
            }
            if (saveImage)
            {
                try
                {
                    //
                    // Get file path of saved file
                    //
                    string srcFile = this.barcodeReaderControl1.LSIMG();
                    string dstFile = srcFile.Split('\\')[2];
                    //
                    // Get image file
                    //
                    this.barcodeReaderControl1.GetFile(srcFile, dstFile);
                    MessageBox.Show(dstFile, "Image file");
                }
                catch (Exception ex)
                {
                    this.textBox1.Text = ex.Message;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            try
            {
                //
                // Start processing of LiveView
                //
                this.barcodeReaderControl1.StartLiveView();
            }
            catch (Exception ex)
            {
                this.textBox1.Text = ex.Message;
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton5.Checked)
            {
                this.barcodeReaderControl1.StopLiveView();
                this.barcodeReaderControl1.Comm.Interface = Interface.USB;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton4.Checked)
            {
                this.barcodeReaderControl1.StopLiveView();
                this.barcodeReaderControl1.Comm.Interface = Interface.Ethernet;
            }
        }

        private void comboBoxReader_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.barcodeReaderControl1.StopLiveView();

            ReaderType readerType = readerTable[comboBoxReader.Text];

            //
            // Set the type of reader to connect
            //
            this.barcodeReaderControl1.ReaderType = readerType;

            groupBox3.Enabled = ((readerType != ReaderType.SR_D100) && (readerType != ReaderType.SR_750));
        }

        // set variable values from the parent form frmBox (this function is called by frmBox, after constructor but before load event)
        public void updateControls(string boxId, DateTime printDate, string serialNo, string shaft, string overlay, string user, bool editMode, bool returnMode)
        {
            txtBoxId.Text = boxId;
            dtpPrintDate.Value = printDate;
           // txtProductSerial.Text = deviceScannedProduct;
            cmbShaft.Text = shaft;
            cmbOverlay.Text = overlay;
            txtUser.Text = user;

            cmbShaft.Enabled = editMode;
            cmbOverlay.Enabled = editMode;
            btnPrint.Enabled = !editMode;
            btnPrintSmall.Enabled = !editMode;
            btnRegisterBoxId.Enabled = !editMode;
            btnRegisterSmall.Enabled = !editMode;
            formEditMode = editMode;
            formReturnMode = returnMode;
            this.Text = editMode ? "Product Serial - Edit Mode" : "Product Serial - Browse Mode";
        }

        #endregion


        #region main operation and user interface procedures

        // query tester records
        private void GetTesterRecordsFromDbServer(string serial)
        {
            txtProductSerial.Text = serial;
            string serialShort = VBS.Left(serial, 17);
            if (string.IsNullOrEmpty(serial)) return;

            // get the model, mathing database tables, and sql commands
            string filterKey = setDataBaseTableAndGetTesterFilterKey(serialShort);

            // get current month and last month tester data from database 
            string sql1 = "(SELECT serno, lot, tjudge, inspectdate" +
                " FROM " + testerTableThisMonth +
                " WHERE serno = '" + serialShort + "'" +
                " ORDER BY tjudge ASC,inspectdate ASC)" +
                " UNION ALL" +
                " (SELECT serno, lot, tjudge, inspectdate" +
                " FROM " + testerTableLastMonth +
                " WHERE serno = '" + serialShort + "'" +
                " ORDER BY tjudge ASC,inspectdate ASC)";
            System.Diagnostics.Debug.Print(System.Environment.NewLine + sql1);
            DataTable dt1 = new DataTable();
            TfSQL tf = new TfSQL();
            tf.sqlDataAdapterFillDatatableFromTesterDb(sql1, ref dt1);

            // for the case of Hayward2 model, get the "sticktion" ng records
            bool pulseNG = false;
            if (filterKey == "HW2")
            {
                string sql2 = "select serno, lot, inspectdate, inspect, judge from " + testerTableThisMonth + "data " +
                    "where inspect in ('McPM_Min','MiPM_Min','MiP34MMi','McP76MMi') and judge = '1' and serno = '" + serialShort + "' " +
                    "union all " +
                    "select serno, lot, inspectdate, inspect, judge from " + testerTableLastMonth + "data " +
                    "where inspect in ('McPM_Min','MiPM_Min','MiP34MMi','McP76MMi') and judge = '1' and serno = '" + serialShort + "'";
                 DataTable dt2 = new DataTable();
                tf.sqlDataAdapterFillDatatableFromTesterDb(sql2, ref dt2);
                pulseNG = dt2.Rows.Count >= 1 ? true : false;
            }

            // check duplicate for each scan of product serial
            bool duplicate = false;
            if (!formReturnMode)
            {
                string sql3 = "select serialno from product_serial_rt where serialno like '" + serialShort + "%'";
                duplicate = !string.IsNullOrEmpty(tf.sqlExecuteScalarString(sql3));
            }
            
            // separate the data into INLINE and OQC, then judge them
            string filterLine = string.Empty;
            string filterOqc = string.Empty;
            if (filterKey == "HW1") { filterLine = fltHw1Line; filterOqc = fltHw1Oqc; }
            else if (filterKey == "HW2") { filterLine = fltHw2Line; filterOqc = fltHw2Oqc; }
            else if (filterKey == "HW3") { filterLine = fltHw3Line; filterOqc = fltHw3Oqc; }
            else { filterLine = fltHw1Line; filterOqc = fltHw1Oqc; } //エラー対策

            // get INLINE data
            DataView dv1 = new DataView(dt1);
            dv1.RowFilter = filterLine;
            dv1.Sort = "tjudge, inspectdate";
            System.Diagnostics.Debug.Print(System.Environment.NewLine + "In-Line:");
            DataTable dt3 = dv1.ToTable();

            // get OQC data
            DataView dv2 = new DataView(dt1);
            dv2.RowFilter = filterOqc;
            dv2.Sort = "tjudge, inspectdate";
            System.Diagnostics.Debug.Print(System.Environment.NewLine + "OQC:");
            DataTable dt4 = dv2.ToTable();

            // get model from the product serial
            string model = getTwoCharacterModelByProductSerial(serial);

            // INLINE: add record into data source data table
            //dtInline = new DataTable();
            //defineDtOverall(ref dtInline);
            DataRow dr = dtInline.NewRow();
            dr["serialno"] = serial;
            dr["model"] = model;
            dr["datecd"] = VBS.Mid(serial, 4, 4).Length < 4 ? "Error" : VBS.Mid(serial, 4, 4);
            dr["line"] = VBS.Mid(serial, 8, 1).Length < 1 ? "Eror" : VBS.Mid(serial, 8, 1);
            dr["lot"] = VBS.Mid(serial, 4, 5).Length < 5 ? "Error" : VBS.Mid(serial, 4, 5);
            dr["eeee"] = VBS.Mid(serial, 12, 4).Length < 4 ? "Error" : VBS.Mid(serial, 12, 4);
            dr["return"] = formReturnMode ? "R" : "N";

            if (dt3.Rows.Count != 0)
            {
                string linepass = String.Empty;
                string buff = dt3.Rows[0]["tjudge"].ToString();
                if (buff == "0") linepass = "PASS";
                else if (buff == "1") linepass = "FAIL";
                else linepass = "ERROR";

                // show sticktion NG or duplicate               
                if (pulseNG) linepass = "PLS NG";
                if (duplicate) linepass = "DUPLICATE";

                dr["stationid"] = dt3.Rows[0]["lot"].ToString();
                dr["judge"] = linepass;
                dr["testtime"] = (DateTime)dt3.Rows[0]["inspectdate"];
            }

            dtInline.Rows.Add(dr);

            // OQC: add record into data source data table
            //dtOqc = new DataTable();
            //defineDtOverall(ref dtOqc);
            DataRow dr_si = dtOqc.NewRow();
            dr_si["serialno"] = serial;
            dr_si["model"] = model;
            dr_si["datecd"] = VBS.Mid(serial, 4, 4).Length < 4 ? "Error" : VBS.Mid(serial, 4, 4);
            dr_si["line"] = VBS.Mid(serial, 8, 1).Length < 1 ? "Error" : VBS.Mid(serial, 8, 1);
            dr_si["lot"] = VBS.Mid(serial, 4, 5).Length < 5 ? "Error" : VBS.Mid(serial, 4, 5);
            dr_si["eeee"] = VBS.Mid(serial, 12, 4).Length < 4 ? "Error" : VBS.Mid(serial, 12, 4);
            dr_si["return"] = formReturnMode ? "R" : "N";

            if (dt4.Rows.Count != 0)
            {
                string linepass = String.Empty;
                string buff = dt4.Rows[0]["tjudge"].ToString();
                if (buff == "0") linepass = "PASS";
                else if (buff == "1") linepass = "FAIL";
                else linepass = "ERROR";

                // show sticktion NG or duplicate               
                if (pulseNG) linepass = "PLS NG";
                if (duplicate) linepass = "DUPLICATE";

                dr_si["stationid"] = dt4.Rows[0]["lot"].ToString();
                dr_si["judge"] = linepass;
                dr_si["testtime"] = (DateTime)dt4.Rows[0]["inspectdate"];
            }

            dtOqc.Rows.Add(dr_si);

            // judge pack capacity from the product serial
            if (dtInline.Rows.Count == 1)
            {
                string mdl = dtInline.Rows[0]["model"].ToString();
                packLimitJudge = getPackCapacityByModel(mdl);
                trayLimit = getTrayCapacityByModel(mdl);
            }

            // update data grid view and check errors
            updateDataGridViews(dtInline, dtOqc, ref dgvInline, ref dgvOqc);
        }

        // issue new box id, register product serial records, and print barcode label
        private void btnRegisterBoxId_Click(object sender, EventArgs e)
        {
            if (cmbShaft.Text == String.Empty | cmbOverlay.Text == String.Empty)
            {
                MessageBox.Show("Please select shaft or overlay.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                return;
            }

            btnRegisterBoxId.Enabled = false;
            btnCancel.Enabled = false;
            string boxId = txtBoxId.Text;

            // check if product serial already exists in database (chck duplicate serial)
            TfSQL tf = new TfSQL();
            Tuple<int, string> duplicateRowSerial = tf.sqlCheckDuplicateProductSerial(dtInline);
            if (duplicateRowSerial.Item1 != -1)
            {
                MessageBox.Show("The following serials had already been registered in database." + Environment.NewLine 
                    + "Row " + duplicateRowSerial.Item1.ToString() + ", " + duplicateRowSerial.Item2 + Environment.NewLine 
                    + "Please replace the module and scan all the tray again.", 
                    "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                btnCancel.Enabled = true;
                return;
            }

            // issue new box id
            m_model = dtInline.Rows[0]["model"].ToString();
            string boxIdNew = tf.sqlGetNewBoxId(m_model, cmbShaft.Text, cmbOverlay.Text, txtUser.Text);

            // add the new box id into product serial datatable
            DataTable dt = dtInline.Copy();
            dt.Columns.Add("boxid", Type.GetType("System.String"));
            for (int i = 0; i < dt.Rows.Count; i++)
                dt.Rows[i]["boxid"] = boxIdNew;

            // register data tabel data to database
            bool res1 = tf.sqlMultipleInsertOverall(dt);

            if (res1)
            {
                // print barcode label
                m_model = dtInline.Rows[0]["model"].ToString();
                m_model_long = getMainModelLong(m_model);
                string shipKind = dtInline.Rows[0]["return"].ToString();
                printBarcode(directory, boxIdNew, m_model_long, cmbShaft.Text, cmbOverlay.Text, dgvDateCode, ref dgvDateCode2, ref txtBoxIdPrint, shipKind);

                // delete data table's records
                dtInline.Clear();
                dtOqc.Clear();
                dt = null;

                txtBoxId.Text = boxIdNew;
                dtpPrintDate.Value = DateTime.ParseExact(VBS.Mid(boxIdNew, 3, 6), "yyMMdd", CultureInfo.InvariantCulture);

                // update parent frmBox data grid view by delegate function
                this.RefreshEvent(this, new EventArgs());
                this.Focus();
                MessageBox.Show("The box id " + boxIdNew + " and " + Environment.NewLine +
                    "its product serials were registered.", "Process Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBoxId.Text = String.Empty;
                txtProductSerial.Text = String.Empty;
                updateDataGridViews(dtInline, dtOqc, ref dgvInline, ref dgvOqc);
                btnRegisterBoxId.Enabled = false;
                btnCancel.Enabled = true;
            }
            else
            {
                // in case multiple insert of produt serial records failed, delete the new box id record in database 
                string sql = "delete from box_id_rt WHERE boxid= '" + boxIdNew + "'";
                int res = tf.sqlExecuteNonQueryInt(sql, false);
                MessageBox.Show("Box id and product serials were not registered.", "Process Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnRegisterBoxId.Enabled = true;
                btnCancel.Enabled = true;
            }
        }
        private void btnRegisterSmall_Click(object sender, EventArgs e)
        {
            if (cmbShaft.Text == String.Empty | cmbOverlay.Text == String.Empty)
            {
                MessageBox.Show("Please select shaft or overlay.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                return;
            }

            btnRegisterBoxId.Enabled = false;
            btnRegisterSmall.Enabled = false;
            btnCancel.Enabled = false;
            string boxId = txtBoxId.Text;

            // check if product serial already exists in database (chck duplicate serial)
            TfSQL tf = new TfSQL();
            Tuple<int, string> duplicateRowSerial = tf.sqlCheckDuplicateProductSerial(dtInline);
            if (duplicateRowSerial.Item1 != -1)
            {
                MessageBox.Show("The following serials had already been registered in database." + Environment.NewLine
                    + "Row " + duplicateRowSerial.Item1.ToString() + ", " + duplicateRowSerial.Item2 + Environment.NewLine
                    + "Please replace the module and scan all the tray again.",
                    "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                btnCancel.Enabled = true;
                return;
            }

            // issue new box id
            m_model = dtInline.Rows[0]["model"].ToString();
            string boxIdNew = tf.sqlGetNewBoxId(m_model, cmbShaft.Text, cmbOverlay.Text, txtUser.Text);

            // add the new box id into product serial datatable
            DataTable dt = dtInline.Copy();
            dt.Columns.Add("boxid", Type.GetType("System.String"));
            for (int i = 0; i < dt.Rows.Count; i++)
                dt.Rows[i]["boxid"] = boxIdNew;

            // register data tabel data to database
            bool res1 = tf.sqlMultipleInsertOverall(dt);

            if (res1)
            {
                // print barcode label
                m_model = dtInline.Rows[0]["model"].ToString();
                m_model_long = getMainModelLong(m_model);
                string shipKind = dtInline.Rows[0]["return"].ToString();
                printBarcodeSmall(directory, boxIdNew, m_model_long, cmbShaft.Text, cmbOverlay.Text, dgvDateCode, ref dgvDateCode2, ref txtBoxIdPrint, shipKind);

                // delete data table's records
                dtInline.Clear();
                dtOqc.Clear();
                dt = null;

                txtBoxId.Text = boxIdNew;
                dtpPrintDate.Value = DateTime.ParseExact(VBS.Mid(boxIdNew, 3, 6), "yyMMdd", CultureInfo.InvariantCulture);

                // update parent frmBox data grid view by delegate function
                this.RefreshEvent(this, new EventArgs());
                this.Focus();
                MessageBox.Show("The box id " + boxIdNew + " and " + Environment.NewLine +
                    "its product serials were registered.", "Process Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBoxId.Text = String.Empty;
                txtProductSerial.Text = String.Empty;
                updateDataGridViews(dtInline, dtOqc, ref dgvInline, ref dgvOqc);
                btnRegisterBoxId.Enabled = false;
                btnRegisterSmall.Enabled = false;
                btnCancel.Enabled = true;
            }
            else
            {
                // in case multiple insert of produt serial records failed, delete the new box id record in database 
                string sql = "delete from box_id_rt WHERE boxid= '" + boxIdNew + "'";
                int res = tf.sqlExecuteNonQueryInt(sql, false);
                MessageBox.Show("Box id and product serials were not registered.", "Process Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnRegisterBoxId.Enabled = true;
                btnRegisterSmall.Enabled = true;
                btnCancel.Enabled = true;
            }
        }
        // updata datagrid views
        private void updateDataGridViews(DataTable dt1, DataTable dt2, ref DataGridView dgv1, ref DataGridView dgv2)
        {
            // set data source of data grid as datatable
            updateDataGridViewsSub(dt1, dt2, ref dgv1, ref dgv2);

            // reset error flags
            errorInCheks = false;

            // check NG tester data or missing tester data
            colorViewForFailAndBlank(ref dgv1);
           // colorViewForFailAndBlank(ref dgv2);

            // check duplicate
            colorViewForDuplicateSerial(ref dgv1);
            colorViewForDuplicateSerial(ref dgv2);

            // check mixed config
            colorMixedEcode(dt1, ref dgv1);
            colorMixedConfig(dt1, ref dgv1);

            // add row number in row header column
            for (int i = 0; i < dgv1.Rows.Count; i++) { dgv1.Rows[i].HeaderCell.Value = (i + 1).ToString(); }
            for (int j = 0; j < dgv2.Rows.Count; j++) { dgv2.Rows[j].HeaderCell.Value = (j + 1).ToString(); }

            // adjust row header column width
            dgv1.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            dgv2.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            // show bottom row in data grid views
            if (dgv1.Rows.Count >= 1) { dgv1.FirstDisplayedScrollingRowIndex = dgv1.Rows.Count - 1; }
            if (dgv2.Rows.Count >= 1) { dgv2.FirstDisplayedScrollingRowIndex = dgv2.Rows.Count - 1; }

            // show count
            okCount = getOkCount(dt1);
            txtOkCount.Text = okCount.ToString() + "/" + packLimitJudge.ToString();

            

            // enable pack register button when okCount and pack capacity matche
            if (okCount == packLimitJudge && dgv1.Rows.Count == packLimitJudge)
            {
                btnRegisterBoxId.Enabled = true;
                btnRegisterSmall.Enabled = true;
                btnPrint.Visible = false;
                btnPrintSmall.Visible = false;
            }
            else { btnRegisterBoxId.Enabled = false; btnRegisterSmall.Enabled = false; }
        }

        #endregion


        #region sub part of operation procedures

        // get product records in database and keep it in tata table
        private void readDatatable(ref DataTable dt, string boxId)
        {
            string sql = "select serialno, model, datecd, line, lot, eeee, stationid, judge, testtime, return " +
                "FROM product_serial_rt WHERE boxid='" + boxId + "'";
            TfSQL tf = new TfSQL();
            tf.sqlDataAdapterFillDatatable(sql, ref dt);
        }

        // get database talbe name (customer model description) and teser id filter key that matches the product serial
        private string setDataBaseTableAndGetTesterFilterKey(string serno)
        {
            string ecd = VBS.Mid(serno, 12, 4);
            TfSQL tf = new TfSQL();
            string tablekey = tf.sqlExecuteScalarString("select tablekey from mdl_ecd_char where eeee = '" + ecd + "'");
            string filterkey = tf.sqlExecuteScalarString("select model_cd from mdl_ecd_char where eeee = '" + ecd + "'");

            //if (VBS.Mid(serno, 14, 2) == "FR" || VBS.Mid(serno, 14, 2) == "FQ")
            //{ tablekey = "ld4g"; filterkey = "HW1"; }
            //else if (VBS.Mid(serno, 14, 2) == "FM" || VBS.Mid(serno, 14, 2) == "FL")
            //{ tablekey = "ld4f"; filterkey = "HW1"; }
            //else if (VBS.Mid(serno, 14, 2) == "XH" || VBS.Mid(serno, 14, 2) == "23" || VBS.Mid(serno, 14, 2) == "H1" || VBS.Mid(serno, 14, 2) == "X8" || VBS.Mid(serno, 14, 2) == "7Q")
            //{ tablekey = "x584"; filterkey = "HW2"; }
            //else if (VBS.Mid(serno, 14, 2) == "XD" || VBS.Mid(serno, 14, 2) == "1Y" || VBS.Mid(serno, 14, 2) == "H0" || VBS.Mid(serno, 14, 2) == "X7" || VBS.Mid(serno, 14, 2) == "7P")
            //{ tablekey = "x583"; filterkey = "HW2"; }
            //else if (VBS.Mid(serno, 14, 2) == "RM")
            //{ tablekey = "x999"; filterkey = "HW3"; }
            //else if (VBS.Mid(serno, 14, 2) == "RL")
            //{ tablekey = "x998"; filterkey = "HW3"; }
            //else
            //{ tablekey = "ld4f"; filterkey = "HW1"; }// sql exception prevention

            testerTableThisMonth = tablekey + DateTime.Today.ToString("yyyyMM");
            testerTableLastMonth = tablekey + ((VBS.Right(DateTime.Today.ToString("yyyyMM"), 2) != "01") ?
                (long.Parse(DateTime.Today.ToString("yyyyMM")) - 1).ToString() : (long.Parse(DateTime.Today.ToString("yyyy")) - 1).ToString() + "12");

            return filterkey;
        }

        // get short model description from the product serial
        private string getTwoCharacterModelByProductSerial(string serial)
        {
            string ecd = VBS.Mid(serial, 12, 4);
            TfSQL tf = new TfSQL();
            return tf.sqlExecuteScalarString("select box_id_char from mdl_ecd_char where eeee = '" + ecd + "'");

            ///if (VBS.Mid(serial, 14, 2) == "FR") return "LM";
            ///else if (VBS.Mid(serial, 14, 2) == "FQ") return "LN";
            ///else if (VBS.Mid(serial, 14, 2) == "FM") return "SM";
            ///else if (VBS.Mid(serial, 14, 2) == "FL") return "SN";
            ///else if (VBS.Mid(serial, 14, 2) == "RM") return "L3";
            ///else if (VBS.Mid(serial, 14, 2) == "RL") return "S3";
            ///else if (VBS.Mid(serial, 14, 2) == "XH" || VBS.Mid(serial, 14, 2) == "23" || VBS.Mid(serial, 14, 2) == "H1") return "LT";
            ///else if (VBS.Mid(serial, 14, 2) == "XD" || VBS.Mid(serial, 14, 2) == "1Y" || VBS.Mid(serial, 14, 2) == "H0") return "ST";
            ///else if (VBS.Mid(serial, 14, 2) == "X8" || VBS.Mid(serial, 14, 2) == "7Q") return "LF";
            ///else if (VBS.Mid(serial, 14, 2) == "X7" || VBS.Mid(serial, 14, 2) == "7P") return "SF";
            ///else return "Error";
        }

        // judge tray capacity from short model description
        private double getTrayCapacityByModel(string model)
        {
            TfSQL tf = new TfSQL();
            return tf.sqlExecuteScalarDouble("select tray_limit from mdl_ecd_char where box_id_char = '" + model + "'");

            ///if (model == "LM" || model == "LN") return trayLimitHw1Large;
            ///else if (model == "SM" || model == "SN") return trayLimitHw1Small;
            ///else if (model == "LT" || model == "LF") return trayLimitHw2Large;
            ///else if (model == "ST" || model == "SF") return trayLimitHw2Small;
            ///else if (model == "L3") return trayLimitHw3Large;
            ///else if (model == "S3") return trayLimitHw3Small;
            ///else return 9999;
        }

        // judge pack capacity from short model description
        private double getPackCapacityByModel(string model)
        {
            TfSQL tf = new TfSQL();
            double limit = tf.sqlExecuteScalarDouble("select pack_limit from mdl_ecd_char where box_id_char = '" + model + "'");
            
            ///if (model == "LM" || model == "LN") limit = packLimitHw1Large;
            ///else if (model == "SM" || model == "SN") limit = packLimitHw1Small;
            ///else if (model == "LT" || model == "LF") limit = packLimitHw2Large;
            ///else if (model == "ST" || model == "SF") limit = packLimitHw2Small;
            ///else if (model == "L3") limit = packLimitHw3Large;
            ///else if (model == "S3") limit = packLimitHw3Small;
            ///else limit = 9999;

            // reflect newly pack capacity set by user
            if (packLimitChange != 0) limit = packLimitChange;

            return limit;
        }

        // get long model description from short model description for label printing purpose
        private string getMainModelLong(string m_short)
        {
            TfSQL tf = new TfSQL();
            string m_long = tf.sqlExecuteScalarString("select model_name from mdl_ecd_char where box_id_char = '" + m_short + "'");

            ///if (m_short == "LM") m_long = "LD4G-003L";
            ///else if (m_short == "LN") m_long = "LD4G-001";
            ///else if (m_short == "SM") m_long = "LD4F-003L";
            ///else if (m_short == "SN") m_long = "LD4F-001";
            ///else if (m_short == "LT") m_long = "LD4J-001";
            ///else if (m_short == "ST") m_long = "LD4H-001";
            ///else if (m_short == "L3") m_long = "X999";
            ///else if (m_short == "S3") m_long = "X998";
            ///else if (m_short == "LF") m_long = "LD4J-040";
            ///else if (m_short == "SF") m_long = "LD4H-043";
            ///else m_long = "Error";

            return m_long;
        }

        // print box id barcode label and lot summary label
        private void printBarcode(string dir, string id, string m_model_long, string shaft, string overlay, DataGridView dgv1, ref DataGridView dgv2, ref TextBox txt, string shipKind)
        {
            TfPrint tf = new TfPrint();
            tf.createBoxidFiles(dir, id, m_model_long, shaft, overlay, dgv1, ref dgv2, ref txt, shipKind);
        }
        private void printBarcodeSmall(string dir, string id, string m_model_long, string shaft, string overlay, DataGridView dgv1, ref DataGridView dgv2, ref TextBox txt, string shipKind)
        {
            TfPrintSmall tf = new TfPrintSmall();
            tf.createBoxidFiles(dir, id, m_model_long, shaft, overlay, dgv1, ref dgv2, ref txt, shipKind);
        }
        #endregion


        #region sub part of user interface procedures

        // define data table to process InLine data and call sql for browse mode
        private void defineDtOverall(ref DataTable dt)
        {
            dt.Columns.Add("serialno", Type.GetType("System.String"));
            dt.Columns.Add("model", Type.GetType("System.String"));
            dt.Columns.Add("datecd", Type.GetType("System.String"));
            dt.Columns.Add("line", Type.GetType("System.String"));
            dt.Columns.Add("lot", Type.GetType("System.String"));
            dt.Columns.Add("eeee", Type.GetType("System.String"));
            dt.Columns.Add("stationid", Type.GetType("System.String"));
            dt.Columns.Add("judge", Type.GetType("System.String"));
            dt.Columns.Add("testtime", Type.GetType("System.DateTime"));
            dt.Columns.Add("return", Type.GetType("System.String"));
        }

        // define data table to process OQC data
        private void definedtOqc(ref DataTable dt)
        {
            dt.Columns.Add("serialno", Type.GetType("System.String"));
            dt.Columns.Add("model", Type.GetType("System.String"));
            dt.Columns.Add("datecd", Type.GetType("System.String"));
            dt.Columns.Add("line", Type.GetType("System.String"));
            dt.Columns.Add("lot", Type.GetType("System.String"));
            dt.Columns.Add("eeee", Type.GetType("System.String"));
            dt.Columns.Add("stationid", Type.GetType("System.String"));
            dt.Columns.Add("judge", Type.GetType("System.String"));
            dt.Columns.Add("testtime", Type.GetType("System.DateTime"));
            dt.Columns.Add("return", Type.GetType("System.String"));
        }

        // make line and lot summary
        private void updateDataGridViewsSub(DataTable dt1, DataTable dt2, ref DataGridView dgv1, ref DataGridView dgv2)
        {
            dgv1.DataSource = dt1;
            dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dgv2.DataSource = dt2;
            dgv2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            string[] criteriaLine = getLineArray(dt1);
            makeDatatableSummary(dt1, ref dgvLine, criteriaLine, "line");

            string[] criteriaDateCode = getLotArray(dt1);
            makeDatatableSummary(dt1, ref dgvDateCode, criteriaDateCode, "lot");
        }

        // make summary data table and set it as the data source of data grid view
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

        // get array of line
        private string[] getLineArray(DataTable dt0)
        {
            DataTable dt1 = dt0.Copy();
            DataView dv = dt1.DefaultView;
            dv.Sort = "line";
            DataTable dt2 = dv.ToTable(true, "line");
            string[] array = new string[dt2.Rows.Count + 1];
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                array[i] = dt2.Rows[i]["line"].ToString();
            }
            array[dt2.Rows.Count] = "Total";
            return array;
        }

        // get array of lot
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

        // count the number of pass modules
        private int getOkCount(DataTable dt)
        {
            if (dt.Rows.Count <= 0) return 0;
            DataTable distinct = dt.DefaultView.ToTable(true, new string[] { "serialno", "judge" });
            DataRow[] dr = distinct.Select("judge = 'PASS'");
            int dist = dr.Length;
            return dist;
        }
        
        // color data grid view for tester result fail and blank
        private void colorViewForFailAndBlank(ref DataGridView dgv)
        {
            int row = dgv.Rows.Count;
            for (int i = 0; i < row; ++i)
            {
                if (dgv["judge", i].Value.ToString() == "FAIL" || dgv["judge", i].Value.ToString() == "PLS NG"
                    || dgv["judge", i].Value.ToString() == "DUPLICATE" || dgv["judge", i].Value.ToString() == String.Empty)
                {
                    dgv["stationid", i].Style.BackColor = Color.Red;
                    dgv["judge", i].Style.BackColor = Color.Red;
                    dgv["testtime", i].Style.BackColor = Color.Red;

                    if (dgv.Name == "dgvOqc") tabControl1.SelectedIndex = 1;
                    else tabControl1.SelectedIndex = 0;

                    soundAlarm();
                    errorInCheks = true;
                }
                else
                {
                    dgv["stationid", i].Style.BackColor = Color.FromKnownColor(KnownColor.Window);
                    dgv["judge", i].Style.BackColor = Color.FromKnownColor(KnownColor.Window);
                    dgv["testtime", i].Style.BackColor = Color.FromKnownColor(KnownColor.Window);

                    tabControl1.SelectedIndex = 0;
                }
            }
        }

        // color data grid view for duplicate product serial
        private void colorViewForDuplicateSerial(ref DataGridView dgv)
        {
            DataTable dt = ((DataTable)dgv.DataSource).Copy();
            if (dt.Rows.Count <= 0) return;

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                string serial = dgv["serialno", i].Value.ToString();
                DataRow[] dr = dt.Select("serialno = '" + serial + "'");
                if (dr.Length >= 2 || dgv["serialno", i].Value.ToString().Length > 26)
                {
                    if (dgv.Name == "dgvOqc") tabControl1.SelectedIndex = 1;
                    else tabControl1.SelectedIndex = 0;

                    dgv["serialno", i].Style.BackColor = Color.Red;
                    soundAlarm();
                    errorInCheks = true;
                }
                else
                {
                    dgv["serialno", i].Style.BackColor = Color.FromKnownColor(KnownColor.Window);
                    tabControl1.SelectedIndex = 0;
                }
            }
        }

        // color data grid view for more than 1 config
        private void colorMixedConfig(DataTable dt, ref DataGridView dgv)
        {
            if (dt.Rows.Count <= 0) return;

            DataTable distinct = dt.DefaultView.ToTable(true, new string[] { "model" });

            if (distinct.Rows.Count == 1)
                m_model = distinct.Rows[0]["model"].ToString();

            if (distinct.Rows.Count >= 2)
            {
                string A = distinct.Rows[0]["model"].ToString();
                string B = distinct.Rows[1]["model"].ToString();
                int a = distinct.Select("model = '" + A + "'").Length;
                int b = distinct.Select("model = '" + B + "'").Length;

                // regard the model with more records as the main model for this pack
                m_model = a > b ? A : B;

                // get the model with less records and mark them as mixed model
                string C = a < b ? A : B;
                int c = -1;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["model"].ToString() == C) { c = i; }
                }

                if (c != -1)
                {
                    dgv["model", c].Style.BackColor = Color.Red;
                    soundAlarm();
                    errorInCheks = true;
                }
                else
                {
                    dgv.Columns["model"].DefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Window);
                }
            }
        }

        // color data grid view for mixed E-code
        private void colorMixedEcode(DataTable dt, ref DataGridView dgv)
        {
            DataTable distinct1 = dt.DefaultView.ToTable(true, new string[] { "eeee" });

            if (distinct1.Rows.Count == 1)
                m_eeee = distinct1.Rows[0]["eeee"].ToString();

            if (distinct1.Rows.Count >= 2)
            {
                string A1 = distinct1.Rows[0]["eeee"].ToString();
                string B1 = distinct1.Rows[1]["eeee"].ToString();
                int a1 = distinct1.Select("eeee = '" + A1 + "'").Length;
                int b1 = distinct1.Select("eeee = '" + B1 + "'").Length;

                m_eeee = a1 > b1 ? A1 : B1;

                string C1 = a1 < b1 ? A1 : B1;
                int c1 = -1;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["eeee"].ToString() == C1) { c1 = i; }
                }

                if (c1 != -1)
                {
                    dgv["eeee", c1].Style.BackColor = Color.Red;
                    soundAlarm();
                    errorInCheks = true;
                }
                else
                {
                    dgv.Columns["eeee"].DefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Window);
                }
            }
        }

        // color grid view for different line
        private void colorMixedLine(DataTable dt, ref DataGridView dgv)
        {
            DataTable distinct1 = dt.DefaultView.ToTable(true, new string[] { "line" });

            if (distinct1.Rows.Count == 1)
                m_line = distinct1.Rows[0]["line"].ToString();

            if (distinct1.Rows.Count >= 2)
            {
                string A1 = distinct1.Rows[0]["line"].ToString();
                string B1 = distinct1.Rows[1]["line"].ToString();
                int a1 = distinct1.Select("line = '" + A1 + "'").Length;
                int b1 = distinct1.Select("line = '" + B1 + "'").Length;

                m_line = a1 > b1 ? A1 : B1;

                string C1 = a1 < b1 ? A1 : B1;
                int c1 = -1;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["line"].ToString() == C1) { c1 = i; }
                }

                if (c1 != -1)
                {
                    dgv["line", c1].Style.BackColor = Color.Red;
                    soundAlarm();
                    errorInCheks = true;
                }
                else
                {
                    dgv.Columns["line"].DefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Window);
                }
            }
        }

        // delete selected product serial recors on form screen 
        private void deleteCurrentRowInDataGridView()
        {
            DataGridView dgv = new DataGridView();

            if (tabControl1.SelectedTab == tabControl1.TabPages["tabInline"])
                dgv = dgvInline;
            else if (tabControl1.SelectedTab == tabControl1.TabPages["tabOqc"])
                dgv = dgvOqc;

            int i = dgv.SelectedCells[0].RowIndex;
            dtInline.Rows[i].Delete();
            dtOqc.Rows[i].Delete();
            dtInline.AcceptChanges();
            dtOqc.AcceptChanges();
            updateDataGridViews(dtInline, dtOqc, ref dgvInline, ref dgvOqc);
        }

        #endregion


        #region supplementary button and other from events

        // re-print box id barcode label in view mode
        private void btnPrint_Click(object sender, EventArgs e)
        {
            string boxId = txtBoxId.Text;
            m_model = dtInline.Rows[0]["model"].ToString();
            m_model_long = getMainModelLong(m_model);
            string shaft = cmbShaft.Text;
            string overlay = cmbOverlay.Text;
            string shipKind = dtInline.Rows[0]["return"].ToString();
            printBarcode(directory, boxId, m_model_long, shaft, overlay, dgvDateCode, ref dgvDateCode2, ref txtBoxIdPrint, shipKind);
        }
        private void btnPrintSmall_Click(object sender, EventArgs e)
        {
            string boxId = txtBoxId.Text;
            m_model = dtInline.Rows[0]["model"].ToString();
            m_model_long = getMainModelLong(m_model);
            string shaft = cmbShaft.Text;
            string overlay = cmbOverlay.Text;
            string shipKind = dtInline.Rows[0]["return"].ToString();
            printBarcodeSmall(directory, boxId, m_model_long, shaft, overlay, dgvDateCode, ref dgvDateCode21, ref txtBoxIdPrint1, shipKind);
        }
        // change pack capacity, allowed only to admin user
        private void txtOkCount_DoubleClick(object sender, EventArgs e)
        {
            // フォーム４（１ラベルあたりシリアル数変更）を、デレゲートイベントを付加して開く
            bool bl = TfGeneral.checkOpenFormExists("frmCapacity");
            if (bl)
            {
                MessageBox.Show("Please close or complete another form.", "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            }
            else
            {
                frmCapacity f4 = new frmCapacity();
                //子イベントをキャッチして、データグリッドを更新する
                f4.RefreshEvent += delegate (object sndr, EventArgs excp)
                {
                    int l = f4.getLimit();
                    if (l != 0)
                    {
                        packLimitChange = f4.getLimit();
                        packLimitJudge = packLimitChange;
                    }
                    updateDataGridViews(dtInline, dtOqc, ref dgvInline, ref dgvOqc);
                    this.Focus();
                };

                f4.updateControls(packLimitChange.ToString());
                f4.Show();
            }
        }
        
        // before closing form, warn the user that records on the screen has not been registered and will disappear
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // if forfrmCapacity has not been closed, let the user do so
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

            // if there are no data on the data grid view, close kv form and this form
            if (dtInline.Rows.Count == 0 || !formEditMode)
            {
                Application.OpenForms["frmBox"].Focus();
                kvForm.Close();
                this.Close();
                return;
            }

            // if there are some data on the data grid view, warn the user before closing kv form and this form
            DialogResult result = MessageBox.Show("The current serial data has not been saved." + System.Environment.NewLine +
                "Do you really cancel?", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                dtInline.Clear();
                dtOqc.Clear();
                updateDataGridViews(dtInline, dtOqc, ref dgvInline, ref dgvOqc);
                MessageBox.Show("The temporary serial numbers are deleted.", "Notice",
                    MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                Application.OpenForms["frmBox"].Focus();
                kvForm.Close();
                this.Close();
            }
            else
            {
                return;
            }
        }

        // set combo box value list when user move the cursor into the combo box
        private void cmbShaft_Enter(object sender, EventArgs e)
        {
            if (m_model == null) return;

            string sql = "select distinct shaft from mdl_sht_ovl where model='" + m_model + "'";
            
            TfSQL tf = new TfSQL();
            tf.getComboBoxDataViaCsv(sql, ref cmbShaft);
        }

        // set combo box value list when user move the cursor into the combo box
        private void cmbOverlay_Enter(object sender, EventArgs e)
        {
            if (m_model == null) return;

            string sql = "select distinct over_lay from mdl_sht_ovl where model='" + m_model + "'";
            
            TfSQL tf = new TfSQL();
            tf.getComboBoxDataViaCsv(sql, ref cmbOverlay);
        }

        // show master edit form when enter key is pressed on combo box 
        private void cmbShaft_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || user != "User_9") return;
            if (TfGeneral.checkOpenFormExists("frmShaftOverlay")) return;

            frmShaftOverlay f7 = new frmShaftOverlay();
            f7.Show();
        }

        // show master edit form when enter key is pressed on combo box 
        private void cmbOverlay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || user != "User_9") return;
            if (TfGeneral.checkOpenFormExists("frmShaftOverlay")) return;

            frmShaftOverlay f7 = new frmShaftOverlay();
            f7.Show();
        }

        #endregion


        #region procedures with supportive functions

        // read setting file
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filepath);
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

        // disable right-upper close button and alt F4 close short cut to control close procedure
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x112;
            const long SC_CLOSE = 0xF060L;
            if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE) { return; }
            base.WndProc(ref m);
        }

        // play MP3 file to sound alarm
        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern int mciSendString(String command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);
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

        // export data to excel file
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            dt1 = (DataTable)dgvInline.DataSource;
            dt2 = (DataTable)dgvOqc.DataSource;
            ExcelClass xl = new ExcelClass();
            xl.ExportToExcel2(dt1, dt2);
            //xl.ExportToCsv(dt, System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\ipqcdb.csv");
        }

        #endregion


        #region kv-com plc related procedures
       
        // restart of scanning after ng module is removed
        private void btnModuelRestart_Click(object sender, EventArgs e)
        {
            errorInCheks = false;
            deleteCurrentRowInDataGridView();
            //KvSetJudgeOK();
            //trayModuleReadOperationLoop();
        }

        // continuously send command to PLC to scan product serial, get judge from data base, and return the judge to PLC
        //private void trayModuleReadOperationLoop()
        //{
        //    string deviceEndValueOld = string.Empty;
        //    string deviceEndValueNew = string.Empty;
        //    string deviceReadValue = string.Empty;
        //    //deviceEndValueOld = KvGetEndValue();

        //    txtOperation.Text = operationTrayReadying;
        //    //button1_Click(null, null);

        //    // repeat up to the number of tray capacity
        //    while (txtOperation.Text == operationTrayReadying)  //day la vong lap vo han ne, lap toi khi nao co con Ng hoac du so luong 100pcs
        //        // phai thoat ra khoi vong nay thi o nho ben ngoai moi thay doi dc
        //    {
        //        // exit loop when ok count reach pack capacity for user to register box id 
        //        if (okCount == packLimitJudge)
        //        {
        //            txtOperation.Text = operationPackStanby;
        //            return;
        //        }

        //        // exit loop when ok count reach tray capacity for user to replace tray 
        //        if (okCount != 0 && okCount / packLimitJudge == 1)
        //        {
        //            txtOperation.Text = operationTrayStandby;
        //            return;
        //        }

        //        // sql query to db server for tester records.
        //        // if error happens, "errorInCheks" is set true in the "updateDataGridViews" procedure included in "GetTesterRecordsFromDbServer". 
        //        //GetTesterRecordsFromDbServer(deviceScannedProduct);

        //        // wait until the machine finishes reading new product id

        //        //do

        //        //{
        //        //    Thread.Sleep(500);

        //        //    deviceEndValueNew = KvGetEndValue();
        //        //    //Thread.Sleep(700);

        //        //} while (deviceEndValueNew == "" || deviceEndValueNew == deviceEndValueOld);

        //        //deviceEndValueOld = deviceEndValueNew;

        //        //Thread.Sleep(500);
        //        //deviceScannedProduct = KvGetProdcutId();
        //        //serno = deviceScannedProduct;


        //        // sql query to db server for tester records.
        //        // if error happens, "errorInCheks" is set true in the "updateDataGridViews" procedure included in "GetTesterRecordsFromDbServer". 

        //        //Thread.Sleep(300);
        //        if (txtProductSerial.Text != "")
        //        {
        //            GetTesterRecordsFromDbServer(txtProductSerial.Text);
        //        }
        //        else return;

        //        // exit loop when judge is ng
        //        if (errorInCheks)
        //        {
        //            KvSetJudgeNG();
        //            txtOperation.Text = operationModuleRestart;
        //            return;
        //        }
        //        else
        //        {
        //            KvSetJudgeOK();
        //        }
        //       // button1_Click(null, null);
        //    }
        //}

        //// write device check ON
        //private void KvGetReadCyc()
        //{
        //    kvForm.ClearDiviceAndValue();
        //    kvForm.Device = deviceRead;
        //    kvForm.ReadDivices();
        //}
        // write device to start scan ON
        private void KvSetStartScan()
        {
            kvForm.ClearDiviceAndValue();
            kvForm.Device = deviceStartScan;
            kvForm.Value = deviceValueON;
            kvForm.WriteDivices();
        }

        //// write device start scan OFF
        //private void KvStartScanOFF()
        //{
        //    kvForm.ClearDiviceAndValue();
        //    kvForm.Device = deviceStartScan;
        //    kvForm.Value = deviceValueOFF;
        //    kvForm.WriteDivices();
        //}

        // write device to notice judge ok
        private void KvSetJudgeOK()
        {
            kvForm.ClearDiviceAndValue();
            kvForm.Device = deviceJudgeOK;
            kvForm.Value = deviceValueON;
            kvForm.WriteDivices();
            KvSetOK();
           // KvSetStartScan();
        }

        // write device to notice judge ng
        private void KvSetJudgeNG()
        {
            kvForm.ClearDiviceAndValue();
            kvForm.Device = deviceJudgeNG;
            kvForm.Value = deviceValueON;
            kvForm.WriteDivices();
            KvSetNG();
           // KvSetStartScan();
        }

        // write device to set judge NG
        private void KvSetNG()
        {
            kvForm.ClearDiviceAndValue();
            kvForm.Device = deviceJudgeNG;
            kvForm.Value = deviceValueOFF;
            kvForm.WriteDivices();
        }

        // write device to set judge OK
        private void KvSetOK()
        {
            kvForm.ClearDiviceAndValue();
            kvForm.Device = deviceJudgeOK;
            kvForm.Value = deviceValueOFF;
            kvForm.WriteDivices();
        }

        // read device to get product id
        //private string KvGetProdcutId()
        //{
        //    string productId = string.Empty;

        //    foreach (string device in deviceGetProductId)
        //    {
        //        kvForm.ClearDiviceAndValue();
        //        kvForm.Device = device;
        //        kvForm.ReadDivices();
        //        productId += ConvertHexDecimalToAsciiPair(kvForm.Value);
        //    }
        //   // KvStartScanOFF();
        //    return productId;
        //}

        // convert hex to string
        private static string ConvertHexDecimalToAsciiPair(string hexString)
        {
            try
            {
                long hexLong;
                long.TryParse(hexString, out hexLong);
                return ((char)(hexLong / 256)).ToString() + ((char)(hexLong % 256)).ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        // read device to get reading state
        //private string KvGetEndValue()
        //{
        //    string endValue = string.Empty;
        //    foreach (string device in deviceProductIdEndAddress)
        //    {
        //        kvForm.ClearDiviceAndValue();
        //        kvForm.Device = device;
        //        kvForm.ReadDivices();
        //        endValue += kvForm.Value;
        //    }
        //    //kvForm.ClearDiviceAndValue();
        //    //kvForm.Device = deviceProductIdEndAddress;
        //    //kvForm.ReadDivices();
        //    return endValue;
        //}

        #endregion

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            if (txtBarcode.Text != String.Empty && txtBarcode.Text != "ERROR\r" && txtBarcode.Text != txtProductSerial.Text || txtBarcode.Text != "ERROR\r")
            {
                txtProductSerial.Text = txtBarcode.Text;
                //txtBarcode.ResetText();

                GetTesterRecordsFromDbServer(txtProductSerial.Text);

                if (errorInCheks)
                {
                    KvSetJudgeNG();
                    txtOperation.Text = operationModuleRestart;
                    txtBarcode.ResetText();
                    return;
                }
                else
                {
                    KvSetJudgeOK();
                }
            }
        }
        private void btnDelBox_Click(object sender, EventArgs e)
        {
            TfSQL tf = new TfSQL();
            DialogResult result = MessageBox.Show("Do you really want to delete this BoxID ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                tf.sqlDeleteBoxid(txtBoxId.Text);
                MessageBox.Show("Deleted BoxID" + txtBoxId.Text, "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}