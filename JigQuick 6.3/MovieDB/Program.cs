using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace JigQuick
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Temp tmp = new Temp();
            string formType = tmp.readIni("APPLICATION BEHAVIOR", "FORM TYPE");

            switch (formType)
            {
                case "DOCKING":
                    Application.Run(new frmDocking(Assembly.GetExecutingAssembly().GetName().Name));
                    break;
                case "TIPTAIL":
                    Application.Run(new frmTipTail(Assembly.GetExecutingAssembly().GetName().Name));
                    break;
                case "MULTI":
                    Application.Run(new frmMulti(Assembly.GetExecutingAssembly().GetName().Name));
                    break;
                case "TRIPLE":
                    Application.Run(new frmTriple(Assembly.GetExecutingAssembly().GetName().Name));
                    break;
                case "OMNI":
                    Application.Run(new frmOmni(Assembly.GetExecutingAssembly().GetName().Name));
                    break;
                case "NOJIG99":
                    Application.Run(new frmNoJig99(Assembly.GetExecutingAssembly().GetName().Name));
                    break;
                case "CAR":
                    Application.Run(new frmNoJig99(Assembly.GetExecutingAssembly().GetName().Name));
                    break;
                case "WITHJIG99":
                    Application.Run(new frmWithJig99(Assembly.GetExecutingAssembly().GetName().Name));
                    break;
                case "COILASSY99":
                    Application.Run(new frmCoilAssy99(Assembly.GetExecutingAssembly().GetName().Name));
                    break;
                default:
                    Application.Run(new frmDocking(Assembly.GetExecutingAssembly().GetName().Name));
                    break;
            }
        }
    }
}