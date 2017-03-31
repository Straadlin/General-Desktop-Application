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

namespace General_Desktop_Application.Presentation
{
    public partial class Form_002 : Form
    {
        // Objects
        Form_001 objForm_001;
        Form_003 objForm_003;
        Form_004 objForm_004;

        // Properties
        public Form_001 ObjForm_001 { get { return objForm_001; } }

        public Form_002(Form_001 objForm_001)
        {
            InitializeComponent();

            this.objForm_001 = objForm_001;

            KeyPreview = true;
        }

        private void Form_002_Load(object sender, EventArgs e)
        {
            Text += " - " + Preferences.TitleSoftware;
        }

        private void Form_002_FormClosed(object sender, FormClosedEventArgs e)
        {
            objForm_001.Close();
        }

        private void Form_002_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit of application?", Preferences.TitleSoftware, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                e.Cancel = true;
        }

        private void Form_002_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
            else if (e.KeyCode == Keys.F1)
                MessageBox.Show(Preferences.InfoMessagesF1, Preferences.TitleSoftware);
            else if (e.KeyCode == Keys.Enter)
            {
                if (txtUser.Focused && !string.IsNullOrEmpty(txtUser.Text))
                    txtPassword.Focus();
                else if (txtPassword.Focused && !string.IsNullOrEmpty(txtPassword.Text))
                    btnLogIn.Focus();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            if (!string.IsNullOrEmpty(txtUser.Text) && !string.IsNullOrEmpty(txtPassword.Text))
            {
                var vUser = BUser.FindAndValidateByUserNameOrEmailOrCellphone(txtUser.Text, txtPassword.Text);

                if (vUser != null)
                {
                    var vSession = BSession.CreateSession(vUser);

                    if (vSession != null)
                    {
                        Hide();
                        objForm_004 = new Form_004(this, vUser, vSession);
                        objForm_004.Show();
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("There was an unknown error, try to exit this application and then entry again.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("Didn't find any user with these data.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUser.Focus();
                }
            }
            else
            {
                Cursor = Cursors.Default;
                MessageBox.Show("The user's data aren't incorrect.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUser.Focus();
            }

            Cursor = Cursors.Default;
        }

        private void lblSettings_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void lblSettings_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void lblSettings_Click(object sender, EventArgs e)
        {
            objForm_003 = new Form_003(this);
            objForm_003.Show();
            Hide();
        }
    }
}