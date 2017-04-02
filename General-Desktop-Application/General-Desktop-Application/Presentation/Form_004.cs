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
using General_Desktop_Application.EF;

namespace General_Desktop_Application.Presentation
{
    public partial class Form_004 : Form
    {
        // Objects
        Form_002 objForm_002;
        Form_004_001 objForm_004_001;
        Form_004_002 objForm_004_002;
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

                MessageBox.Show("The difference of time between server and this device is incorrect, you need to check the clocs in both devices.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                //stpStatusBar.Enabled = false;
                tsslCurrentSection.Visible = true;
                tsslCurrentSection.Text = stCurrentSection;
            }
            else
            {
                mnsMenu.Enabled = true;
                //stpStatusBar.Enabled = true;
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