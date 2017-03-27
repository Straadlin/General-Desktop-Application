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
        user objUser;
        proc_user_select_Result objUserNewModel;
        session objSession;

        // Attributes
        bool boForceToClose = false;

        // Properties
        public user ObjUser { get { return objUser; } }
        public session ObjSession { get { return objSession; } }
        public ToolStripStatusLabel ObjToolStripStatusLabelCurrentSection { get { return tsslCurrentSection; } }

        public Form_004(Form_002 objForm_002, user objUser, proc_user_select_Result objUserNewModel, session objSession)
        {
            this.objForm_002 = objForm_002;
            this.objUser = objUser;
            this.objSession = objSession;
            this.objUserNewModel = objUserNewModel;

            InitializeComponent();

            KeyPreview = true;
        }

        private void Form_004_Load(object sender, EventArgs e)
        {
            UpdateIP();

            Text += " - " + Preferences.TitleSoftware;

            tsslUser.Text = (!string.IsNullOrEmpty(objUser.user_username__varchar) ? objUser.user_username__varchar : (!string.IsNullOrEmpty(objUser.user_email__varchar) ? objUser.user_email__varchar : objUser.user_cellphone__varchar)) + " - " + objUserNewModel.user_firstname__varchar + " " + objUserNewModel.user_lastname__varchar;
        }

        private void Form_004_FormClosed(object sender, FormClosedEventArgs e)
        {
            timClock.Enabled = TimSession.Enabled = false;

            SessionB.UpdateLastTimeSession(objSession);

            //CloseWindows();

            objForm_002.ObjForm_001.Close();
        }

        private void Form_004_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!boForceToClose&&MessageBox.Show("Do you want to exit of application?", Preferences.TitleSoftware, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                e.Cancel = true;
        }

        private void closeSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void timClock_Tick(object sender, EventArgs e)
        {
            timClock.Interval = 40000;

            DateTime objDateTime = await DateB.GetServersDateAndTimeAsync();
            tsslDate.Text = "Server's date and time: " + objDateTime.ToLongDateString() + " / " + objDateTime.ToShortTimeString();
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

        public void ActivateComponents(bool activate, string stCurrentSection)
        {
            if (activate)
            {
                mnsMenu.Enabled = true;
                stpStatusBar.Enabled = true;
                if (stCurrentSection != null)
                    tsslCurrentSection.Text = stCurrentSection;
            }
            else
            {
                mnsMenu.Enabled = false;
                stpStatusBar.Enabled = false;
                tsslCurrentSection.Text = "";
            }
        }

        public async void UpdateIP()
        {
            string stIP = await Network.GetExternalIPAsync();

            if (!string.IsNullOrEmpty(stIP))
            {
                SessionB.ChangeIpSession(ObjSession, Convert.ToByte(stIP.Split('.')[0]), Convert.ToByte(stIP.Split('.')[1]), Convert.ToByte(stIP.Split('.')[2]), Convert.ToByte(stIP.Split('.')[3]));
            }
        }

        private async void UpdateDateSession()
        {
            await SessionB.UpdateLastDateSessionAsync(objSession);
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