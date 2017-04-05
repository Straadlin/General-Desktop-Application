using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

using General_Desktop_Application.BusinessLayer;
using General_Desktop_Application.Classes;
using General_Desktop_Application.EF;
using General_Desktop_Application.Properties;

namespace General_Desktop_Application.Presentation
{
    public partial class Form_004 : Form
    {
        // Objects
        Form_002 objForm_002;
        Form_004_001 objForm_004_001;
        Form_004_002 objForm_004_002;
        Form_004_003 objForm_004_003;
        Form_004_004 objForm_004_004;

        // Attributes
        bool boForceToClose = false;

        // Properties
        public user ObjUser { set; get; }
        public session ObjSession { set; get; }
        public ToolStripStatusLabel ObjToolStripStatusLabelCurrentSection { get { return tsslCurrentSection; } }

        public Form_004(Form_002 objForm_002, user objUser, session objSession)
        {
            this.objForm_002 = objForm_002;
            ObjUser = objUser;
            ObjSession = objSession;

            InitializeComponent();

            KeyPreview = true;
        }

        private void Form_004_Load(object sender, EventArgs e)
        {
            BlockUserSectionsByRol();

            UpdateIP();

            Text += " - " + Preferences.TitleSoftware;

            tsslUser.Text = (!string.IsNullOrEmpty(ObjUser.user_username__nvarchar) ? ObjUser.user_username__nvarchar : (!string.IsNullOrEmpty(ObjUser.user_email__nvarchar) ? ObjUser.user_email__nvarchar : ObjUser.user_cellphone__nvarchar)) + " - " + Tools.Decrypt(ObjUser.user_firstname__nvarchar) + " " + Tools.Decrypt(ObjUser.user_lastname__nvarchar);

            Business.FreeAllRegistersAssociatedWithThisUser(ObjUser);

            Program.ExtractReporViewerFiles();

            if (Settings.Default["Local"].ToString() == "True")
            {
                timCreateBackup.Enabled = true;
                timDeleteBackup.Enabled = true;
            }
        }

        private void Form_004_FormClosed(object sender, FormClosedEventArgs e)
        {
            timClock.Enabled = TimSession.Enabled = false;

            Business.FreeAllRegistersAssociatedWithThisUser(ObjUser);
            BSession.UpdateLastTimeSession(ObjSession);

            //CloseWindows();

            objForm_002.ObjForm_001.Close();
        }

        private void Form_004_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!boForceToClose && MessageBox.Show("Do you want to exit of application?", Preferences.TitleSoftware, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                e.Cancel = true;
        }

        private void closeSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void timClock_Tick(object sender, EventArgs e)
        {
            timClock.Interval = 40000;

            DateTime objDateTime = await Business.GetServersDateAndTimeAsync();
            tsslDate.Text = "Server's datetime: " + objDateTime.ToLongDateString() + " / " + objDateTime.ToShortTimeString();
            TimeSpan objTimeSpan = DateTime.Now - objDateTime;

            if (objTimeSpan.TotalMinutes > 3)
            {
                boForceToClose = true;

                timClock.Enabled = TimSession.Enabled = false;

                MessageBox.Show("The difference of time between server and this device isn't correct, you need to check the clocs in both devices.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Error);

                Close();
            }
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            objForm_004_001 = new Form_004_001(this);
            objForm_004_001.MdiParent = this;
            objForm_004_001.Show();
        }

        private void TimSession_Tick(object sender, EventArgs e)
        {
            UpdateDateSession();
        }

        public void ActivateOrDeactivateComponents(string stCurrentSection)
        {
            if (!string.IsNullOrEmpty(stCurrentSection))
            {
                mnsMenu.Enabled = false;
                tsslCurrentSection.Visible = true;
                tsslCurrentSection.Text = stCurrentSection;
            }
            else
            {
                mnsMenu.Enabled = true;
                tsslCurrentSection.Visible = false;
                tsslCurrentSection.Text = "";
            }
        }

        public async void UpdateIP()
        {
            string stIP = await Network.GetExternalIPAsync();

            if (!string.IsNullOrEmpty(stIP))
            {
                BSession.ChangeIpSession(ObjSession, Convert.ToByte(stIP.Split('.')[0]), Convert.ToByte(stIP.Split('.')[1]), Convert.ToByte(stIP.Split('.')[2]), Convert.ToByte(stIP.Split('.')[3]));
            }
        }

        private async void UpdateDateSession()
        {
            await BSession.UpdateLastDateSessionAsync(ObjSession);
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            objForm_004_002 = new Form_004_002(this);
            objForm_004_002.MdiParent = this;
            objForm_004_002.Show();
        }

        private void aboutDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            objForm_004_004 = new Form_004_004(this);
            objForm_004_004.MdiParent = this;
            objForm_004_004.Show();
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            objForm_004_003 = new Form_004_003(this);
            objForm_004_003.MdiParent = this;
            objForm_004_003.Show();
        }

        private async void timCreateBackup_Tick(object sender, EventArgs e)
        {
            // 1000 = 1 seconds
            // 60000 = 60 seconds / 1 minute
            // 1,800,000 = 30 minutes
            // 3,600,000 = 60 minutes / 1 hour

            timCreateBackup.Enabled = false;

            if (Settings.Default["Local"].ToString() == "True" && BPrincipalCompany.Get().prco_hoursbetweenbackups__int > 0 && ObjUser.user_roleaccess__tinyint == 1)
            {
                tsslOperation.Text = "Checking storage backups...";
                tsslOperation.Visible = true;

                int inHoursToNext = await DoBackup();

                if (inHoursToNext != 0)
                {
                    timCreateBackup.Interval = (3600000 * inHoursToNext) + 60000;
                    timCreateBackup.Enabled = true;
                }
                else
                {
                    MessageBox.Show(Preferences.GlobalErrorOperation, Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                tsslOperation.Text = "...";
                tsslOperation.Visible = false;
            }
        }

        public static Task<int> DoBackup()
        {
            return Task.Run(() =>
            {
                try
                {
                    if (Directory.Exists(Preferences.PathBackups))
                        Directory.CreateDirectory(Preferences.PathBackups);

                    List<string> objList = Directory.GetFiles(Preferences.PathBackups, "*.bak").ToList();

                    if (objList != null)
                    {
                        objList.OrderBy(l => l);

                        for (int inCont = 0; inCont < objList.Count; inCont++)
                            if (objList[inCont].Split('\\').Length != 4 || objList[inCont].Split('\\')[3].Length != 63 || !objList[inCont].Contains(Preferences.DatabaseName) && !RegularExpressions.CheckIsNumeric(objList[inCont].Split('\\')[3].Substring(47, 12), 12, 12))//if (objList[inCont].Split('\\').Length != 4 || objList[inCont].Split('\\')[3].Length != 63 || !objList[inCont].Contains(Preferences.DatabaseName) && !RegularExpressions.CheckIsNumeric(objList.Last().Split('\\')[3].Substring(47, 12), 12, 12))
                                objList.RemoveAt(inCont);
                    }

                    DateTime objDateNow = DateTime.Now;

                    string stQuery = "USE master; BACKUP DATABASE " + Preferences.DatabaseName + " TO DISK = N'" + Preferences.PathBackups + "\\" + Preferences.DatabaseName + "_auto_" + DateTime.Now.Year.ToString().Substring(2, 2) + (objDateNow.Month < 10 ? "0" + objDateNow.Month : objDateNow.Month.ToString()) + (objDateNow.Day < 10 ? "0" + objDateNow.Day : objDateNow.Day.ToString()) + "" + (objDateNow.Hour < 10 ? "0" + objDateNow.Hour : objDateNow.Hour.ToString()) + "" + (objDateNow.Minute < 10 ? "0" + objDateNow.Minute : objDateNow.Minute.ToString()) + "" + (objDateNow.Second < 10 ? "0" + objDateNow.Second : objDateNow.Second.ToString()) + ".bak' WITH NOFORMAT, NOINIT, NAME = N'test-Completa Base de datos Copia de seguridad', SKIP, NOREWIND, NOUNLOAD, STATS = 10;";

                    if (objList == null || objList.Count == 0)
                    {
                        if (Business.Execute(stQuery))
                            return BPrincipalCompany.Get().prco_hoursbetweenbackups__int;
                    }
                    else
                    {
                        int inHoursConfigirated = BPrincipalCompany.Get().prco_hoursbetweenbackups__int;

                        string stDateTime = objList.Last().Split('\\')[3].Substring(47, 12);

                        int inYear = Convert.ToInt32("20" + stDateTime.Substring(0, 2));
                        int inMonth = Convert.ToInt32(stDateTime.Substring(2, 2));
                        int inDay = Convert.ToInt32(stDateTime.Substring(4, 2));
                        int inHour = Convert.ToInt32(stDateTime.Substring(6, 2));
                        int inMinute = Convert.ToInt32(stDateTime.Substring(8, 2));
                        int inSecond = Convert.ToInt32(stDateTime.Substring(10, 2));

                        TimeSpan objTimeSpan = objDateNow - new DateTime(inYear, inMonth, inDay, inHour, inMinute, inSecond);

                        if (objTimeSpan.TotalMinutes > (inHoursConfigirated * 60))
                        {
                            if (Business.Execute(stQuery))
                                return inHoursConfigirated;
                        }
                        else
                            return Convert.ToInt32((inHoursConfigirated * 60) - objTimeSpan.TotalMinutes);
                    }
                }

                catch { }

                return 0;
            });
        }

        private async void timDeleteBackup_Tick(object sender, EventArgs e)
        {
            // 1000 = 1 seconds
            // 60000 = 60 seconds / 1 minute
            // 1,800,000 = 30 minutes
            // 3,600,000 = 60 minutes / 1 hour

            timDeleteBackup.Enabled = false;

            if (Settings.Default["Local"].ToString() == "True" && BPrincipalCompany.Get().prco_daysdeletbackups__int > 0 && ObjUser.user_roleaccess__tinyint == 1)
            {
                tsslOperation.Text = "Deleting older backups...";
                tsslOperation.Visible = true;

                if (await DeleteBackups())
                {
                    timDeleteBackup.Interval = 7200000;
                    timDeleteBackup.Enabled = true;
                }
                else
                {
                    MessageBox.Show("It wasn't possible to delete one or severals backups files.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                tsslOperation.Text = "...";
                tsslOperation.Visible = false;
            }
        }

        public static Task<bool> DeleteBackups()
        {
            return Task.Run(() =>
            {
                if (Directory.Exists(Preferences.PathBackups))
                {
                    List<string> objList = Directory.GetFiles(Preferences.PathBackups, "*.bak").ToList();

                    if (objList != null)
                    {
                        objList.OrderBy(l => l);

                        int inDaysConfigirated = BPrincipalCompany.Get().prco_daysdeletbackups__int;

                        DateTime objDateNow = DateTime.Now;

                        for (int inCont = 0; inCont < objList.Count; inCont++)
                        {
                            if (objList[inCont].Split('\\').Length == 4 || objList[inCont].Split('\\')[3].Length == 63 || objList[inCont].Contains(Preferences.DatabaseName) && RegularExpressions.CheckIsNumeric(objList[inCont].Split('\\')[3].Substring(47, 12), 12, 12))
                            {
                                string stDateTime = objList.Last().Split('\\')[3].Substring(47, 12);

                                int inYear = Convert.ToInt32("20" + stDateTime.Substring(0, 2));
                                int inMonth = Convert.ToInt32(stDateTime.Substring(2, 2));
                                int inDay = Convert.ToInt32(stDateTime.Substring(4, 2));
                                int inHour = Convert.ToInt32(stDateTime.Substring(6, 2));
                                int inMinute = Convert.ToInt32(stDateTime.Substring(8, 2));
                                int inSecond = Convert.ToInt32(stDateTime.Substring(10, 2));

                                TimeSpan objTimeSpan = objDateNow - new DateTime(inYear, inMonth, inDay, inHour, inMinute, inSecond);

                                if (objTimeSpan.TotalHours > (inDaysConfigirated * 24))
                                {
                                    try
                                    {
                                        File.Delete(objList[inCont]);
                                    }
                                    catch { }
                                }
                            }
                        }

                        return true;
                    }
                }
                return false;
            });
        }

        private void BlockUserSectionsByRol()
        {

        }

        //private void CloseWindows()
        //{
        //    Form existe;

        //    if ((existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == "Form_004_001").SingleOrDefault<Form>()) != null)
        //    {
        //        existe.Close();
        //    }
        //}
    }
}