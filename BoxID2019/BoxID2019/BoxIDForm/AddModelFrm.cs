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
    public partial class AddModelFrm : CommonFrm
    {
        public AddModelFrm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            TfSQL SQL = new TfSQL("boxidcardb");
            string cmd = @"INSERT INTO tbl_model_box_limit(model, box_limit)
                           VALUES('" + txtModel.Text + "','" + txtLimit.Text + "')";
            SQL.sqlExecuteNonQuery(cmd, true);
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPlace_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please enter numbers only!", "Warring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
