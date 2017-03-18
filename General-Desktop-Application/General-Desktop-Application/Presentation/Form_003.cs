using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using General_Desktop_Application.BusinessLayer;
using General_Desktop_Application.Classes;
using General_Desktop_Application.Properties;
using System.Diagnostics;
using System.IO;


namespace General_Desktop_Application.Presentation
{
    public partial class Form_003 : Form
    {
        // Objects
        Form_002 objForm_002;

        // Attributes

        // Builder
        public Form_003(Form_002 oForm_002)
        {
            InitializeComponent();

            this.objForm_002 = oForm_002;

            KeyPreview = true;
        }

        private void Form_003_Load(object sender, EventArgs e)
        {
            Text += " - " + Preferences.TitleSoftware;
            lblInstallationMode.Text = "Installation mode " + (Preferences.ArchitectDatabase64 ? "(64 Bits - MSSQL 2016)" : "(32 Bits - MSSQL 2014)") + ":";

            UpdateInstances();

            if (Convert.ToBoolean(Settings.Default["Local"]))
            {
                rbtLocal.Checked = true;
                rbtRemote.Checked = false;
            }
            else
            {
                rbtLocal.Checked = false;
                rbtRemote.Checked = true;
            }

            mskIP.Text = Settings.Default["IP"].ToString();
            nupdPort.Value = Convert.ToDecimal(string.IsNullOrEmpty(Settings.Default["Port"].ToString()) ? 1433 : Convert.ToDecimal(Settings.Default["Port"]));
            cboInstanceConnection.Text = Settings.Default["Instance"].ToString();
        }

        private void Form_003_FormClosed(object sender, FormClosedEventArgs e)
        {
            Tools.DeleteTemporalFiles();

            objForm_002.Show();
        }

        private void Form_003_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Form_003_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
            else if (e.KeyCode == Keys.F1)
                MessageBox.Show("Versión: " + Preferences.CurrentVersion, Preferences.TitleSoftware);
            else if (e.KeyCode == Keys.Enter)
            {
                if (mskIP.Focused)
                    nupdPort.Focus();
                else if (nupdPort.Focused)
                    btnConnect.Focus();
            }
        }

        private void rbtLocal_CheckedChanged(object sender, EventArgs e)
        {
            AdjustRadioButtons();
        }

        private void rbtRemote_CheckedChanged(object sender, EventArgs e)
        {
            AdjustRadioButtons();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            objForm_002.Show();
            Close();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            if (rbtLocal.Checked)
            {
                if (cboInstanceConnection.SelectedIndex > -1)
                {
                    Settings.Default["Local"] = rbtLocal.Checked ? "True" : "False";
                    Settings.Default["Instance"] = cboInstanceConnection.Text;
                    Settings.Default.Save();

                    if (UserB.SelectCountAllUsers() > 0)
                    {
                        MessageBox.Show("The connection test was done successfully.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        objForm_002.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("The connection could not be established. We ask that:\r\n• Verify that the SQL Server engine is turned on\r\n• Verify that the data structure is correctly installed.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        cboInstanceConnection.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("There isn't any instance selected.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                if (mskIP.Text.Split('.')[0].Length > 0 && mskIP.Text.Split('.')[1].Length > 0 && mskIP.Text.Split('.')[2].Length > 0 && mskIP.Text.Split('.')[3].Length > 0)
                {
                    Settings.Default["Local"] = rbtLocal.Checked ? "True" : "False";
                    Settings.Default["IP"] = mskIP.Text.Split('.')[0].Trim().PadLeft(3, '0') + "." + mskIP.Text.Split('.')[1].Trim().PadLeft(3, '0') + "." + mskIP.Text.Split('.')[2].Trim().PadLeft(3, '0') + "." + mskIP.Text.Split('.')[3].Trim().PadLeft(3, '0');
                    Settings.Default["Port"] = nupdPort.Value.ToString();
                    Settings.Default.Save();


                    if (UserB.SelectCountAllUsers() > 0)
                    {
                        MessageBox.Show("The connection test was done successfully.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        objForm_002.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("The connection could not be established. We ask that:\r\n• Verify that the SQL Server engine on the server is turned on\r\n• Verify that the data structure is correctly installed on the server.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        mskIP.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("The selected ip is not correct.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    mskIP.Focus();
                }
            }

            Cursor = Cursors.Default;
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            string stPath = Preferences.ArchitectDatabase64 ? "dependencies\\SQL_Server_2016_Express_64_Bit_ENU.exe" : "dependencies\\SQL_Server_2014_Express_32_Bit_ENU.exe";

            if (!File.Exists(stPath))
            {
                stPath = null;

                if (MessageBox.Show("MSSQL installer could not be found. Would you like to choose another file path?", Preferences.TitleSoftware, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    OpenFileDialog objFileDialog = new OpenFileDialog();
                    objFileDialog.InitialDirectory = "C:\\";
                    objFileDialog.Filter = "Execute file(*.exe)|*exe";
                    objFileDialog.ShowDialog();
                    objFileDialog.Multiselect = false;
                    stPath = objFileDialog.FileName;
                }
            }

            if (!string.IsNullOrEmpty(stPath) && MessageBox.Show("Do you want to start the installation of Microsoft SQL Server? this may take awhile.", Preferences.TitleSoftware, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Process oProcess = new Process();

                if (rbtAutomatic.Checked)
                    oProcess = SQLServer.zmct(stPath, true);
                else if (rbtManual.Checked)
                    oProcess = SQLServer.zmct(stPath, false);
                else
                    oProcess.StartInfo = new ProcessStartInfo(stPath);

                try
                {
                    btnInstall.Enabled = false;
                    oProcess.Start();
                    oProcess.WaitForExit();

                    cboInstanceConnection.Items.Clear();
                    cboInstanceMaintenance.Items.Clear();

                    UpdateInstances();
                }
                catch
                {
                    MessageBox.Show("There was a problem with the MSSQL wizard.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            btnInstall.Enabled = true;
        }

        private void cboInstanceMaintenance_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            if (Business.SelectCountDatabases(cboInstanceMaintenance.SelectedItem.ToString()) == 1)
            {
                btnCreate.Enabled = false;
                btnBackUp.Enabled = true;
                btnRestore.Enabled = false;
                btnDelete.Enabled = true;
            }
            else
            {
                btnCreate.Enabled = true;
                btnBackUp.Enabled = false;
                btnRestore.Enabled = true;
                btnDelete.Enabled = false;
            }

            Cursor = Cursors.Default;
        }

        private void btnBackUp_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to back up the database of the application?", Preferences.TitleSoftware, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (!Directory.Exists(Preferences.PathBackups))
                    Directory.CreateDirectory(Preferences.PathBackups);
                else
                    if (File.Exists(Preferences.PathTemporalBackupFile))
                    File.Delete(Preferences.PathTemporalBackupFile);

                SaveFileDialog obSaveFileDialog = new SaveFileDialog();
                obSaveFileDialog.Filter = "BAK |*.bak";
                obSaveFileDialog.InitialDirectory = Preferences.PathBackups;
                obSaveFileDialog.FileName = GetFileName();

                if (DialogResult.OK == obSaveFileDialog.ShowDialog(this))
                {
                    Cursor = Cursors.WaitCursor;

                    try
                    {
                        string stConnectionString = "Server = " + cboInstanceMaintenance.SelectedItem.ToString() + "; database = master; integrated security = yes";

                        Business.Execute("USE master; BACKUP DATABASE " + Preferences.DatabaseName + " TO DISK = N'" + Preferences.PathTemporalBackupFile + "' WITH NOFORMAT, NOINIT, NAME = N'test-Completa Base de datos Copia de seguridad', SKIP, NOREWIND, NOUNLOAD, STATS = 10;", stConnectionString);

                        File.Copy(Preferences.PathTemporalBackupFile, obSaveFileDialog.FileName);
                        File.Delete(Preferences.PathTemporalBackupFile);

                        Cursor = Cursors.Default;

                        MessageBox.Show("The strcuture of system has been successfully backed.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception obException)
                    {
                        Cursor = Cursors.Default;

                        MessageBox.Show("Failed to support the structure of the system, verify the user's credentials or try to save the copy on:\r\n..\\Microsoft SQL Server\\MSSQL12.MSSQLSERVER\\MSSQL\\Backup\\.\r\n\\Microsoft SQL Server\\MSSQL13.MSSQLSERVER\\MSSQL\\Backup\\." + "\r\n\n " + obException.Message, Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to create the database structure the application on this instance?", Preferences.TitleSoftware, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;

                if (!File.Exists(Preferences.PathTemporalBackupFile))
                {
                    Program.ExtractScript();

                    string stValue;

                    using (FileStream objFileStream = new FileStream(Preferences.PathScriptInitializerFile, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader objStreamReader = new StreamReader(objFileStream, Encoding.UTF8))
                        {
                            stValue = objStreamReader.ReadToEnd();
                            stValue = stValue.Replace("_pspsps_", Preferences.PasswordDatabaseUser);
                            stValue = stValue.Replace("_internal_", Preferences.UserDatabase);
                            stValue = stValue.Replace("_dbname_", Preferences.DatabaseName);

                            objStreamReader.Close();
                        }
                        objFileStream.Close();
                    }

                    File.Delete(Preferences.PathScriptInitializerFile);

                    using (FileStream objFileStream = new FileStream(Preferences.PathScriptInitializerFile, FileMode.CreateNew, FileAccess.Write))
                    {
                        using (StreamWriter objStreamWriter = new StreamWriter(objFileStream, Encoding.UTF8))
                        {
                            objStreamWriter.Write(stValue);

                            objStreamWriter.Close();
                        }

                        objFileStream.Close();
                    }

                    File.SetAttributes(Preferences.PathScriptInitializerFile, FileAttributes.Hidden);
                }

                try
                {
                    ProcessStartInfo objProcessStartInfo = new ProcessStartInfo("sqlcmd", "-S " + cboInstanceMaintenance.SelectedItem.ToString() + " -i " + Preferences.PathScriptInitializerFile);

                    objProcessStartInfo.UseShellExecute = false;
                    objProcessStartInfo.CreateNoWindow = true;
                    objProcessStartInfo.RedirectStandardOutput = true;

                    Process objProcess = new Process();
                    objProcess.StartInfo = objProcessStartInfo;
                    objProcess.Start();

                    btnBackUp.Enabled = btnDelete.Enabled = true;
                    btnCreate.Enabled = btnRestore.Enabled = false;

                    MessageBox.Show("The structure of the system has been successfully created.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    btnCreate.Enabled = btnRestore.Enabled = false;

                    MessageBox.Show("Error creating the system structure.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to restore the database on this instance?", Preferences.TitleSoftware, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (!Directory.Exists(Preferences.PathBackups))
                    Directory.CreateDirectory(Preferences.PathBackups);
                else
                    if (File.Exists(Preferences.PathTemporalBackupFile))
                    File.Delete(Preferences.PathTemporalBackupFile);

                OpenFileDialog obOpenFileDialog = new OpenFileDialog();
                obOpenFileDialog.Filter = "BAK |*.bak";
                obOpenFileDialog.InitialDirectory = Preferences.PathBackups;
                obOpenFileDialog.FileName = "";

                if (DialogResult.OK == obOpenFileDialog.ShowDialog(this))
                {
                    Cursor = Cursors.WaitCursor;

                    string stPrePath;
                    if (Preferences.ArchitectDatabase64)
                    {
                        stPrePath = "C:\\Program Files\\Microsoft SQL Server";
                    }
                    else
                    {
                        if (Directory.Exists("C:\\Program Files (x86)\\Microsoft SQL Server"))
                            stPrePath = "C:\\Program Files (x86)\\Microsoft SQL Server";
                        else
                            stPrePath = "C:\\Program Files\\Microsoft SQL Server";
                    }

                    File.Copy(obOpenFileDialog.FileName, Preferences.PathTemporalBackupFile);

                    string stConnectionString = "Server = " + cboInstanceMaintenance.SelectedItem.ToString() + "; database = master; integrated security = yes";

                    string stErrorCode = "";
                    if (stErrorCode.Length == 0 && !Business.Execute("USE master; RESTORE DATABASE " + Preferences.DatabaseName + " FROM  DISK = N'" + Preferences.PathTemporalBackupFile + "' WITH  FILE = 1,  MOVE N'" + Preferences.DatabaseName + "' TO N'" + stPrePath + (Preferences.ArchitectDatabase64 ? "\\MSSQL13." : "\\MSSQL12.") + cboInstanceMaintenance.SelectedItem.ToString().Split('\\')[1] + "\\MSSQL\\DATA\\" + Preferences.DatabaseName + ".mdf',  MOVE N'" + Preferences.DatabaseName + "_log' TO N'" + stPrePath + (Preferences.ArchitectDatabase64 ? "\\MSSQL13." : "\\MSSQL12.") + cboInstanceMaintenance.SelectedItem.ToString().Split('\\')[1] + "\\MSSQL\\DATA\\" + Preferences.DatabaseName + ".ldf',  NOUNLOAD,  STATS = 5;", stConnectionString))
                        stErrorCode = "A";
                    if (stErrorCode.Length == 0 && !Business.Execute("USE [" + Preferences.DatabaseName + "]; DROP USER [" + Preferences.UserDatabase + "];", stConnectionString))
                        stErrorCode = "B";
                    if (stErrorCode.Length == 0 && !Business.Execute("USE " + Preferences.DatabaseName + "; CREATE LOGIN " + Preferences.UserDatabase + " WITH PASSWORD = N'" + Preferences.PasswordDatabaseUser + "', DEFAULT_DATABASE = [" + Preferences.DatabaseName + "], DEFAULT_LANGUAGE = [Spanish], CHECK_EXPIRATION = OFF, CHECK_POLICY = OFF; CREATE USER " + Preferences.UserDatabase + " FOR LOGIN [" + Preferences.UserDatabase + "]; ALTER ROLE [db_datareader] ADD MEMBER [" + Preferences.UserDatabase + "]; ALTER ROLE [db_datawriter] ADD MEMBER [" + Preferences.UserDatabase + "];", stConnectionString))
                        stErrorCode = "C";

                    if (stErrorCode.Length == 0)
                    {

                        File.Delete(Preferences.PathTemporalBackupFile);

                        btnCreate.Enabled = false;
                        btnBackUp.Enabled = true;
                        btnRestore.Enabled = false;
                        btnDelete.Enabled = true;

                        MessageBox.Show("The structure of the system has been successfully restored.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to restore the system structure  (" + stErrorCode + "), check user credentials or try to open the copy from\r\n\nC:\\.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    Cursor = Cursors.Default;
                }
            }

            //if (MessageBox.Show("Do you want to restore the database on this instance?", Preferences.TitleSoftware, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            //{
            //    if (!Directory.Exists(Preferences.PathBackups))
            //        Directory.CreateDirectory(Preferences.PathBackups);
            //    else
            //        if (File.Exists(Preferences.PathTemporalBackupFile))
            //        File.Delete(Preferences.PathTemporalBackupFile);

            //    OpenFileDialog obOpenFileDialog = new OpenFileDialog();
            //    obOpenFileDialog.Filter = "BAK |*.bak";
            //    obOpenFileDialog.InitialDirectory = Preferences.PathBackups;
            //    obOpenFileDialog.FileName = "";

            //    if (DialogResult.OK == obOpenFileDialog.ShowDialog(this))
            //    {
            //        Cursor = Cursors.WaitCursor;

            //        string stPrePath;
            //        if (Preferences.ArchitectDatabase64)
            //        {
            //            stPrePath = "C:\\Program Files\\Microsoft SQL Server";
            //        }
            //        else
            //        {
            //            if (Directory.Exists("C:\\Program Files (x86)\\Microsoft SQL Server"))
            //                stPrePath = "C:\\Program Files (x86)\\Microsoft SQL Server";
            //            else
            //                stPrePath = "C:\\Program Files\\Microsoft SQL Server";
            //        }

            //        File.Copy(obOpenFileDialog.FileName, Preferences.PathTemporalBackupFile);

            //        string stConnectionString = "Server = " + cboInstanceMaintenance.SelectedItem.ToString() + "; database = master; integrated security = yes";

            //        string stErrorCode = "";
            //        if (stErrorCode.Length == 0 && !Business.Business.Execute("USE master; RESTORE DATABASE " + Preferences.DatabaseName + " FROM  DISK = N'" + Preferences.PathTemporalBackupFile + "' WITH  FILE = 1,  MOVE N'" + Preferences.DatabaseName + "' TO N'" + stPrePath + "\\MSSQL12." + cboInstanceMaintenance.SelectedItem.ToString().Split('\\')[1] + "\\MSSQL\\DATA\\" + Preferences.DatabaseName + ".mdf',  MOVE N'" + Preferences.DatabaseName + "_log' TO N'" + stPrePath + "\\MSSQL12." + cboInstanceMaintenance.SelectedItem.ToString().Split('\\')[1] + "\\MSSQL\\DATA\\" + Preferences.DatabaseName + ".ldf',  NOUNLOAD,  STATS = 5;", stConnectionString))
            //            stErrorCode = "A";
            //        if (stErrorCode.Length == 0 && !Business.Business.Execute("USE [" + Preferences.DatabaseName + "]; DROP USER [" + Preferences.UserDatabase + "];", stConnectionString))
            //            stErrorCode = "B";
            //        if (stErrorCode.Length == 0 && !Business.Business.Execute("USE " + Preferences.DatabaseName + "; CREATE LOGIN " + Preferences.UserDatabase + " WITH PASSWORD = N'" + Preferences.PasswordDatabaseUser + "', DEFAULT_DATABASE = [" + Preferences.DatabaseName + "], DEFAULT_LANGUAGE = [Spanish], CHECK_EXPIRATION = OFF, CHECK_POLICY = OFF; CREATE USER " + Preferences.UserDatabase + " FOR LOGIN [" + Preferences.UserDatabase + "]; ALTER ROLE [db_datareader] ADD MEMBER [" + Preferences.UserDatabase + "]; ALTER ROLE [db_datawriter] ADD MEMBER [" + Preferences.UserDatabase + "];", stConnectionString))
            //            stErrorCode = "C";

            //        if (stErrorCode.Length == 0)
            //        {

            //            File.Delete(Preferences.PathTemporalBackupFile);

            //            btnCreate.Enabled = false;
            //            btnBackUp.Enabled = true;
            //            btnRestore.Enabled = false;
            //            btnDelete.Enabled = true;

            //            MessageBox.Show("The structure of the system has been successfully restored.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        else
            //        {
            //            MessageBox.Show("Failed to restore the system structure  (" + stErrorCode + "), check user credentials or try to open the copy from\r\n\nC:\\.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }

            //        Cursor = Cursors.Default;
            //    }
            //}
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete the database of the application?", Preferences.TitleSoftware, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;

                string stConnectionString = "Server = " + cboInstanceMaintenance.SelectedItem.ToString() + "; database = master; integrated security = yes";

                if (Business.Execute("USE master; SET NOCOUNT ON DECLARE @DBName varchar(50) DECLARE @spidstr varchar(8000) DECLARE @ConnKilled smallint SET @ConnKilled = 0 SET @spidstr = '' SET @DBName = '" + Preferences.DatabaseName + "' IF db_id(@DBName) < 4 BEGIN RETURN END SELECT @spidstr = COALESCE(@spidstr,',' ) + 'kill '+ CONVERT(varchar, spid) + '; ' FROM master..sysprocesses WHERE dbid = db_id(@DBName) IF LEN(@spidstr) > 0 BEGIN EXEC(@spidstr) SELECT @ConnKilled = COUNT(1) FROM master..sysprocesses WHERE dbid = db_id(@DBName) END; USE master; DROP LOGIN " + Preferences.UserDatabase + "; DROP DATABASE " + Preferences.DatabaseName + ";", stConnectionString))
                {
                    btnCreate.Enabled = true;
                    btnBackUp.Enabled = false;
                    btnRestore.Enabled = true;
                    btnDelete.Enabled = false;

                    Cursor = Cursors.Default;

                    MessageBox.Show("The structure of system has been deleted succesfull.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Cursor = Cursors.Default;

                    MessageBox.Show("Failed to destroy the structure of system.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateInstances()
        {
            string[] staInstances = SQLServer.fjol();

            if (staInstances != null)
            {
                foreach (string s in staInstances)
                {
                    string[] aux = s.Split('\\');
                    if (aux.Length == 2 && aux[aux.Length - 1].Substring(0, 6) == "STRAAD" && !aux[aux.Length - 1].Contains('_'))
                    {
                        cboInstanceConnection.Items.Add(s);
                        cboInstanceMaintenance.Items.Add(s);
                    }
                }

                if (cboInstanceConnection.Items.Count > 0)
                    cboInstanceConnection.SelectedIndex = cboInstanceMaintenance.SelectedIndex = cboInstanceConnection.Items.Count - 1;
            }
        }

        private void AdjustRadioButtons()// PROBLEM: It exceutes every time when us set click in radio buttons
        {
            if (rbtLocal.Checked)
            {
                lblInstance.Visible = cboInstanceConnection.Visible = true;
                lblIP.Visible = mskIP.Visible = lblPort.Visible = nupdPort.Visible = false;
                pnlInstallation.Enabled = pnlMaintenance.Enabled = true;
                cboInstanceConnection.Focus();
            }
            else
            {
                lblInstance.Visible = cboInstanceConnection.Visible = false;
                lblIP.Visible = mskIP.Visible = lblPort.Visible = nupdPort.Visible = true;
                pnlInstallation.Enabled = pnlMaintenance.Enabled = false;
                mskIP.Focus();
            }
        }

        private string GetFileName()
        {
            int inDay = DateTime.Now.Day;
            int inMonth = DateTime.Now.Month;
            int inHour = DateTime.Now.Hour;
            int inMinute = DateTime.Now.Minute;
            int inSecond = DateTime.Now.Second;

            return Preferences.DatabaseName + "_" + DateTime.Now.Year.ToString().Substring(2, 2) + "" + (inMonth < 10 ? "0" + inMonth : inMonth.ToString()) + "" + (inDay < 10 ? "0" + inDay : inDay.ToString()) + "" + (inHour < 10 ? "0" + inHour : inHour.ToString()) + "" + (inMinute < 10 ? "0" + inMinute : inMinute.ToString()) + "" + (inSecond < 10 ? "0" + inSecond : inSecond.ToString());
        }
    }
}