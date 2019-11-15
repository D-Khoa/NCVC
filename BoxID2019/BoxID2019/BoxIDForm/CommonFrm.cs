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
    public partial class CommonFrm : Form
    {
        public string Title_Name
        {
            get { return lbTitle.Text; }
            set { lbTitle.Text = value; }
        }

        public static string UserName { get; set; }
        public static string Model { get; set; }
        public CommonFrm()
        {
            InitializeComponent();
        }
    }
}
