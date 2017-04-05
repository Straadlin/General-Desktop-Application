using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using General_Desktop_Application.Classes;
using General_Desktop_Application.Presentation;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

using System.Net;//

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
            try
            {
                Tools.DeleteTemporalFiles();
            }
            catch { }

            Assembly objAssembly = Assembly.GetExecutingAssembly();

            //string[] archivos = new string[2]
            //{
            //    "Dropbox.Api.dll",
            //    "Newtonsoft.Json.dll"
            //};
            //foreach (string item in archivos)
            //    if (!File.Exists(item))
            //        if (!Tools.ExtractToFile(objAssembly, "Resources.Libraries." + item, item))
            //            MessageBox.Show("Error al extraer archivo.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        else
            //            File.SetAttributes(item, FileAttributes.Hidden);
            //    else
            //        File.SetAttributes(item, FileAttributes.Hidden);            

            //Mutex instanceLock = new Mutex(false, "WFA_v01");
            //if (instanceLock.WaitOne(0, false) && Classes.Tools.GetHashMD5(File.ReadAllBytes("Dropbox.Api.dll")) == "8da8bd874df1d40e18cc6ea4665ea46a" && Classes.Tools.GetHashMD5(File.ReadAllBytes("Newtonsoft.Json.dll")) == "95e24268172c8908f5b2c906b90a92a7")
            //{
                //try
                //{
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    Application.Run(new Form_001());
                //}
                //finally
                //{
                //    instanceLock.ReleaseMutex();
                //}
            //}
            //else
            //{
            //    Application.Exit();
            //}

            ////Application.EnableVisualStyles();
            ////Application.SetCompatibleTextRenderingDefault(false);
            //////MessageBox.Show(NetStraad.GetExternalIP());// Only Test
            ////Application.Run(new Form_001());
        }

        static public void ExtractScript()
        {
            Assembly objAssembly = Assembly.GetExecutingAssembly();

            string[] archivos = new string[1]
            {
                    "Initializer.sql"
            };
            foreach (string item in archivos)
                if (!File.Exists(item))
                    if (!Tools.ExtractToFile(objAssembly, "Resources.Scripts." + item, item))
                        MessageBox.Show("Error al extraer archivo.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else
                        File.SetAttributes(item, FileAttributes.Hidden);
                else
                    File.SetAttributes(item, FileAttributes.Hidden);
        }

        static public void ExtractReporViewerFiles()
        {
            Assembly objAssembly = Assembly.GetExecutingAssembly();

            string[] archivos = 
            {
                "Microsoft.ReportViewer.Common.dll",
                "Microsoft.ReportViewer.DataVisualization.DLL",
                "Microsoft.ReportViewer.ProcessingObjectModel.DLL",
                "Microsoft.ReportViewer.WinForms.dll",
                "Microsoft.SqlServer.Types.dll"
            };
            foreach (string item in archivos)
                if (!File.Exists(item))
                    if (!Tools.ExtractToFile(objAssembly, "Resources.Libraries.Report_Viewer_12." + item, item))
                        MessageBox.Show("Error al extraer archivo.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                 //   else
                 //       File.SetAttributes(item, FileAttributes.Hidden);
                //else
                //    File.SetAttributes(item, FileAttributes.Hidden);
        }

        //static public void ExtractCA_Updater_v01()
        //{
        //    Assembly objAssembly = Assembly.GetExecutingAssembly();

        //    string[] archivos = new string[1]
        //    {
        //        "CA_Updater_v01.exe"
        //    };
        //    foreach (string item in archivos)
        //        if (!File.Exists(item))
        //            if (!Tools.ExtractToFile(objAssembly, "Resources.Executables." + item, item))
        //                MessageBox.Show("Error al extraer archivo.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //            else
        //                File.SetAttributes("CA_Updater_v01.exe", FileAttributes.Hidden);
        //}
    }
}