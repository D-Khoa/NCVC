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
        public TestFrm()
        {
            InitializeComponent();
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
    }
}
