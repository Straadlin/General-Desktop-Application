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
    public partial class Form_004_002 : Form
    {
        // Objects

        // Attributes

        // Properties
        public Form_004 ObjForm_004
        { set; get; }

        public Form_004_002(Form_004 objForm_004)
        {
            InitializeComponent();

            ObjForm_004 = objForm_004;

            KeyPreview = true;
        }

        private void Form_004_002_Load(object sender, EventArgs e)
        {
            ObjForm_004.ActivateOrDeactivateComponents("Preferences");

            RefreshMainValues();

            ObjForm_004.Cursor = Cursors.Default;
        }

        private void Form_004_002_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit of this section?", Preferences.TitleSoftware, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                e.Cancel = true;
        }

        private void Form_004_002_FormClosed(object sender, FormClosedEventArgs e)
        {
            ObjForm_004.ActivateOrDeactivateComponents(null);
        }

        private void Form_004_002_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
                this.Close();
            else
            {
                if (e.KeyChar == 13)
                {
                    if (txtCompany.Focused)
                        if (txtCompany.Text.Length > 0)
                            txtRFC.Focus();
                    if (txtRFC.Focused)
                        if (txtRFC.Text.Length > 0)
                            cboMode.Focus();
                    if (cboMode.Focused)
                        btnSave.Focus();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void RefreshMainValues()
        {

        }
    }
}