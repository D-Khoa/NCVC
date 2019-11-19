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
    public partial class TestFrm : Form
    {
        DataGridViewCheckBoxColumn checkcol;
        DataGridViewCheckBoxCell checkcell;
        public TestFrm()
        {
            InitializeComponent();
            addCheckbox();
        }

        private void addCheckbox()
        {
            checkcol = new DataGridViewCheckBoxColumn();
            {
                checkcol.HeaderText = "Check";
                checkcol.Name = "chck";
                checkcol.TrueValue = true;
                checkcol.FalseValue = false;
                checkcol.FlatStyle = FlatStyle.Standard;
                checkcol.CellTemplate = new DataGridViewCheckBoxCell();
                //checkcol.CellTemplate.ValueType = typeof(bool);
            }
            dataGridView1.Rows.Add("1", "12", "123", "145");
            dataGridView1.Columns.Add(checkcol);
            ////checkcell = new DataGridViewCheckBoxCell();
            //foreach(DataGridViewRow r in dataGridView1.Rows)
            //{
            //    checkcell = (DataGridViewCheckBoxCell)r.Cells["chk"];
            //}
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            DotNetBarcode barCode = new DotNetBarcode(DotNetBarcode.Types.Jan13);
            Single x1, x2, y1, y2;
            string barCodeNo = textBox1.Text;
            x1 = 0;
            y1 = 0;
            x2 = panel1.Size.Width;
            y2 = panel1.Size.Height;
            if (!string.IsNullOrEmpty(barCodeNo))
                barCode.WriteBar(barCodeNo, x1, y1, x2, y2, e.Graphics);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            panel1.Refresh();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["chck"].Value != null && (bool)dataGridView1.CurrentRow.Cells["chck"].Value)
            {
                dataGridView1.CurrentRow.Cells["chck"].Value = null;
            }
            else if (dataGridView1.CurrentRow.Cells["chck"].Value == null)
            {
                dataGridView1.CurrentRow.Cells["chck"].Value = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           foreach(DataGridViewRow r in dataGridView1.Rows)
            {
                if(r.Cells["chck"].Value != null && (bool)r.Cells["chck"].Value)
                {
                    r.Cells[0].Value = "111111";
                }
                else
                    r.Cells[0].Value = "2222";
            }

            DateTime mont = new DateTime();
            mont = dateTimePicker1.Value.AddMonths(int.Parse(textBox1.Text));
            label1.Text = mont.ToString("yyyyMM");
        }
    }
}
