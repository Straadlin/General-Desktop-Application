using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using General_Desktop_Application.Properties;
using General_Desktop_Application.Classes;

namespace General_Desktop_Application.Presentation
{
    public partial class Form_002 : Form
    {
        // Objects
        Form_001 oForm_001;
        Form_003 oForm_003;
        Form_004 oForm_004;

        public Form_002(Form_001 oForm_001)
        {
            InitializeComponent();

            this.oForm_001 = oForm_001;

            KeyPreview = true;
        }

        private void Form_002_Load(object sender, EventArgs e)
        {
            Text += " - " + PreferencesStraad.TitleSoftware;
        }

        private void Form_002_FormClosed(object sender, FormClosedEventArgs e)
        {
            oForm_001.Close();
        }

        private void Form_002_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit the application?", PreferencesStraad.TitleSoftware, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                e.Cancel = true;
        }

        private void Form_002_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
            else if (e.KeyCode == Keys.F1)
                MessageBox.Show("Versión: " + PreferencesStraad.CurrentVersion, PreferencesStraad.TitleSoftware);
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
            if (Convert.ToBoolean(Settings.Default["Local"]))
            {
                // Local mode
            }
            else
            {
                // Remote mode
            }
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
            oForm_003 = new Form_003(this);
            oForm_003.Show();
            Hide();
        }
    }
}