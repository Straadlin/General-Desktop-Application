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
    public partial class Form_004_001 : Form
    {
        // Objects

        // Attributes

        // Propiedades
        public Form_004 ObjForm_004
        { set; get; }

        public Form_004_001(Form_004 objForm_004)
        {
            InitializeComponent();

            ObjForm_004 = objForm_004;

            //this.objForm04.Cursor = Cursors.Default;

            KeyPreview = true;
        }

        private void Form_004_001_Load(object sender, EventArgs e)
        {
            ObjForm_004.ActivateComponents(false, "Users");

            RefreshMainList();

            ObjForm_004.Cursor = Cursors.Default;
        }

        private void Form_004_001_FormClosed(object sender, FormClosedEventArgs e)
        {
            ObjForm_004.ActivateComponents(true, null);
        }

        private void Form_004_001_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit of this section?", Preferences.TitleSoftware, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                e.Cancel = true;
        }

        private void RefreshMainList()
        {
            lsbUsers.Items.Clear();

            var vUsers = UserB.GetAllUsers().OrderBy(u => u.user_username__nvarchar).ThenBy(u => u.user_firstname__nvarchar).ThenBy(u => u.user_lastname__nvarchar);

            foreach (var vItem in vUsers)
            {
                lsbUsers.Items.Add((!string.IsNullOrEmpty(vItem.user_username__nvarchar) ? vItem.user_username__nvarchar : (!string.IsNullOrEmpty(vItem.user_email__nvarchar) ? vItem.user_email__nvarchar : vItem.user_cellphone__nvarchar)) + " - " + vItem.user_firstname__nvarchar + " - " + vItem.user_lastname__nvarchar);
            }

            lblQuantity.Text = "[Quantity: " + lsbUsers.Items.Count+"]";
        }

        private void txtPassword_MouseHover(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = ' ';
        }
    }
}