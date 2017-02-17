using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;//
using System.Net;//
using General_Desktop_Application.Presentation;
using General_Desktop_Application.Classes;

namespace General_Desktop_Application
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
            //MessageBox.Show(NetStraad.GetExternalIP());// Only Test
            Application.Run(new Form_001());
        }
    }
}