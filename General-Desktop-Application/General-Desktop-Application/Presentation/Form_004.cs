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
        session objSession;

        // Attributes

        // Properties
        public user ObjUser { get { return objUser; } }
        public session ObjSession { get { return objSession; } }
        public ToolStripStatusLabel ObjToolStripStatusLabelCurrentSection { get { return tsslCurrentSection; } }

        public Form_004(Form_002 objForm_002, user objUser, session objSession)
        {
            this.objForm_002 = objForm_002;
            this.objUser = objUser;
            this.objSession = objSession;

            InitializeComponent();

            KeyPreview = true;
        }

        private void Form_004_Load(object sender, EventArgs e)
        {
            UpdateIP();

            Text += " - " + Preferences.TitleSoftware;

            tsslUser.Text = (!string.IsNullOrEmpty(objUser.user_username__nvarchar) ? objUser.user_username__nvarchar : (!string.IsNullOrEmpty(objUser.user_email__nvarchar) ? objUser.user_email__nvarchar : objUser.user_cellphone__nvarchar)) + " - " + objUser.user_firstname__nvarchar + " " + objUser.user_lastname__nvarchar;
        }

        private void Form_004_FormClosed(object sender, FormClosedEventArgs e)
        {
            SessionB.UpdateLastTimeSession(objSession);

            //CloseWindows();

            objForm_002.ObjForm_001.Close();
        }

        private void Form_004_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit of application?", Preferences.TitleSoftware, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                e.Cancel = true;
        }

        private void closeSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timClock_Tick(object sender, EventArgs e)
        {
            DateTime objDateTime = DateB.GetServersDateAndTime();
            tsslDate.Text = "Server's date and time: " + objDateTime.ToLongDateString() + " / " + objDateTime.ToLongTimeString();
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