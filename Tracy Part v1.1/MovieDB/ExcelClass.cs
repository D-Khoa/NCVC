﻿using System;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Tracy
{
    public class ExcelClass
    {

        public void ExportToExcel(DataTable dt)
        {
            try
            {
                if (dt == null || dt.Columns.Count == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                Excel.Application excelApp = new Excel.Application();
                excelApp.Workbooks.Add();
                Excel.Worksheet ws = excelApp.ActiveSheet;

                // column headings
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ws.Cells[1, (i + 1)] = dt.Columns[i].ColumnName;
                }

                // rows
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ws.Cells[(i + 2), (j + 1)] = dt.Rows[i][j];
                    }
                }
                excelApp.Visible = true;
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }


        public void ExportBatchDetailToExcel(string batchNo, string modelNo, string modelName, DateTime batchDate, DataTable dtParts)
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Workbooks.Add();
                Excel.Worksheet ws = excelApp.ActiveSheet;

                string[] titleAry = { "Tracebility Data Sheet", "General info:", "Parts info:" };
                string[] headerAry1 = { "Batch Date", "Batch No", "Model No", "Model Name" };
                //string[] headerAry2 = { "Leader Id", "Leader Name", "Input Time", "Output Time", "Remark" };

                string[] valueArray1 = { batchNo, modelNo, modelName };
               // string[] valueArray2 = { leader, leaderName, remark };
                DateTime[] dateArray = { batchDate };

                // ランドスケープへ、印刷設定を変更
                var ps = ws.PageSetup;
                ps.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

                // 出力ターゲット行変数
                int r = 0;

                // タイトル
                Excel.Range rng = ws.Cells[r = r + 1, 1] as Excel.Range;
                //rng.Value = headerAry1[0];
                //rng.Font.Bold = true;

                //Excel.Range rng1 = ws.Cells[r = r + 4, 1] as Excel.Range;
                //rng1.Value = headerAry1[1];
                //rng1.Font.Bold = true;

                //Excel.Range rng2 = ws.Cells[r = r + 5, 1] as Excel.Range;
                //rng1.Value = headerAry1[2];
                //rng1.Font.Bold = true;

                //Excel.Range rng3 = ws.Cells[r = r + 6, 1] as Excel.Range;
                //rng1.Value = headerAry1[3];
                //rng1.Font.Bold = true;

                //// ヘッダー情報見出し
                rng = ws.Cells[r = r + 2, 1] as Excel.Range;
                rng.Value = titleAry[1];
                rng.Font.Bold = true;

                // 複数列編集用変数
                int s = r;
                int t = r;
                int u = r;

                // 罫線上部アドレス用変数
                int y = r + 1;

                // 一般情報ヘッダー左側
                for (int i = 0; i < headerAry1.Length; i++)
                {
                    ws.Cells[++r, 1] = headerAry1[i];
                }

                // 一般情報ヘッダー右側
                //for (int i = 0; i < headerAry2.Length; i++)
                //{
                //    ws.Cells[++s, 3] = headerAry2[i];
                //}

                //一般情報ＶＡＬＵＥ左側
                for (int i = 0; i < valueArray1.Length; i++)
                {
                    if (t == 3)
                    {
                        ws.Cells[++t, 2] = dateArray[0];
                        
                    }

                    ws.Cells[++t, 2] = valueArray1[i];
                }

                // 一般情報ＶＡＬＵＥ右側
                //for (int i = 0; i < valueArray2.Length; i++)
                //{
                //    if (u == 8)
                //    {
                //        ws.Cells[++u, 4] = dateArray[1];
                //    }

                //    if (u == 9)
                //    {
                //        ws.Cells[++u, 4] = dateArray[2];
                //    }

                //    ws.Cells[++u, 4] = valueArray2[i];
                //}

                // 罫線および色の設定
                setBorder(ws.get_Range("A" + y.ToString(), "B" + r.ToString()));
                ws.get_Range("A" + y.ToString(), "A" + r.ToString()).Interior.Color = Excel.XlRgbColor.rgbLightBlue ;
               // ws.get_Range("C" + y.ToString(), "C" + r.ToString()).Interior.Color = Excel.XlRgbColor.rgbLightBlue;


                //オペレーター見出し
                //rng = ws.Cells[r = r + 2, 1] as Excel.Range;
                //rng.Value = titleAry[2];
                //rng.Font.Bold = true;

                //// column headings
                //DataTable dt = dtOperator;

                //// 罫線上部アドレス用変数
                //y = ++r;

                //for (int i = 0; i < dt.Columns.Count ; i++)
                //{
                //    ws.Cells[r, i + 1] = dt.Columns[i].ColumnName;
                //    ws.Cells[r, i + 4] = dt.Columns[i].ColumnName;
                //}

                //// rows
                //int d = dt.Rows.Count % 2 == 1 ? dt.Rows.Count / 2 + 1 : dt.Rows.Count / 2;
                //int e = dt.Rows.Count - d;
                //int v = r;   

                //for (int i = 0; i < d; i++)
                //{
                //    r++;
                //    for (int j = 0; j < dt.Columns.Count; j++)
                //    {
                //        ws.Cells[r, j + 1] = dt.Rows[i][j];
                //    }
                //}

                //for (int i = d; i < d + e; i++)
                //{
                //    v++;
                //    for (int j = 0; j < dt.Columns.Count; j++)
                //    {
                //        ws.Cells[v, j + 4] = dt.Rows[i][j];
                //    }
                //}

                // 罫線および色の設定
                //setBorder(ws.get_Range("A" + y.ToString(), "F" + r.ToString()));
                //ws.get_Range("A" + y.ToString(), "F" + y.ToString()).Interior.Color = Excel.XlRgbColor.rgbLightBlue;


                //部品見出し
                //Excel.Range rng = ws.Cells[r = r + 2, 1] as Excel.Range;
                rng = ws.Cells[r = r + 2, 1] as Excel.Range;
                rng.Value = titleAry[2];
                rng.Font.Bold = true;

                // column headings
                DataTable dt = dtParts;

                // 罫線上部アドレス用変数
                y = ++r;

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    ws.Cells[r, i + 1] = dt.Columns[i].ColumnName;
                }

                // rows
                int d = dt.Rows.Count;

                for (int i = 0; i < d; i++)
                {
                    r++;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ws.Cells[r, j + 1] = dt.Rows[i][j];
                    }
                }

                // 罫線および色の設定
                setBorder(ws.get_Range("A" + y.ToString(), "F" + (r - 1).ToString()));
                ws.get_Range("A" + y.ToString(), "F" + y.ToString()).Interior.Color = Excel.XlRgbColor.rgbLightBlue;


                // 副資材見出し
                //rng = ws.Cells[r = r + 1, 1] as Excel.Range;
                //rng.Value =  titleAry[4];
                //rng.Font.Bold = true;

                //// column headings
                //dt = dtSubMaterial;

                //// 罫線上部アドレス用変数
                //y = ++r;

                //for (int i = 0; i < dt.Columns.Count; i++)
                //{
                //    ws.Cells[r, i + 1] = dt.Columns[i].ColumnName;
                //}

                //// rows
                //d = dt.Rows.Count;

                //for (int i = 0; i < d; i++)
                //{
                //    r++;
                //    for (int j = 0; j < dt.Columns.Count; j++)
                //    {
                //        ws.Cells[r, j + 1] = dt.Rows[i][j];
                //    }
                //}

                //// 罫線および色の設定
                //setBorder(ws.get_Range("A" + y.ToString(), "E" + (r - 1).ToString()));
                //ws.get_Range("A" + y.ToString(), "E" + y.ToString()).Interior.Color = Excel.XlRgbColor.rgbLightBlue;

                // 列幅設定
                for (int i = 1; i <= 6; i++)
                {
                    ws.Columns[i].ColumnWidth = 20;
                }

                // エクセルアプリケーションの表示
                excelApp.Visible = true;

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                System.Diagnostics.Debug.Print(ex.Message);
            }
        }

        private void setBorder(Excel.Range range)
        {
            range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlContinuous;
            range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;
            range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous;
            range.Borders.get_Item(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous;
            range.Borders.get_Item(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlContinuous;
            range.Borders.get_Item(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlContinuous;
        }

        public void ExportBatchToCsvForPrint(string filePath, string fixedValue, string subAssyNo, string subAssyName, DateTime outputTime, string output, string batchNo, string line)
        {
            try
            {
                StreamWriter sw = new StreamWriter(filePath, false);
                string[] buff = { fixedValue, subAssyNo, subAssyName, outputTime.ToString("yyyy/MM/dd"), output, batchNo, line };

                for (int i = 0; i < buff.Length; i++)
                {
                    sw.Write(buff[i]);
                    if (i < buff.Length - 1)
                    {
                        sw.Write(",");
                    }
                }

                sw.Close();
                MessageBox.Show("Barcode Label printed.","Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
