﻿using System;
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
            TfSQL SQL2 = new TfSQL("pqmdb");
            string cmd = @"INSERT INTO tbl_model_dbplace(model, dbplace) 
                           VALUES('" + txtModel.Text + "','" + txtPlace.Text + "')";
            SQL2.sqlExecuteNonQuery(cmd, true);
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
