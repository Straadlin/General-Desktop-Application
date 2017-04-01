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
        byte byAction;
        string stPathPicture;

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
            byAction = 0;
            stPathPicture = null;

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
                Close();
            else if (e.KeyChar == 13)
            {
                if (txtRFC.Focused)
                    if (txtRFC.Text.Length > 0)
                        txtCompany.Focus();
                if (txtCompany.Focused)
                    if (txtCompany.Text.Length > 0)
                        txtAddress.Focus();
                if (txtAddress.Focused)
                    if (txtAddress.Text.Length > 0)
                        txtPhone.Focus();
                if (txtPhone.Focused)
                    if (txtPhone.Text.Length > 0)
                        txtEmail.Focus();
                if (txtEmail.Focused)
                    if (txtEmail.Text.Length > 0)
                        cboState.Focus();
                if (cboState.Focused)
                    if (cboState.SelectedIndex > 0)
                        cboCity.Focus();
                if (cboCity.Focused)
                    if (cboCity.SelectedIndex > 0)
                        cboMode.Focus();
                if (cboMode.Focused)
                    if (cboMode.SelectedIndex > 0)
                        nudLapse.Focus();
                if (nudLapse.Focused)
                    btnAccept.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //var vUser = BUser.FindByUserNameOrEmailOrCellphone(!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[0]) ? lsbUsers.SelectedItem.ToString().Split(' ')[0] : (!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[1]) ? lsbUsers.SelectedItem.ToString().Split(' ')[1] : lsbUsers.SelectedItem.ToString().Split(' ')[2]));

            //if (vUser != null)
            //{
            //    if (vUser.sess_uuid_used__uniqueidentifier == null)
            //    {
            //        BUser.DisableToEdit(vUser.user_uuid__uniqueidentifier, ObjForm_004.ObjSession);

            //        gpbA.Enabled = false;

            //        txtUserName.Enabled = true;
            //        txtEmail.Enabled = true;
            //        txtCellphone.Enabled = true;
            //        txtPassword.Enabled = true;
            //        txtRePassword.Enabled = true;
            //        txtFirstName.Enabled = true;
            //        txtLastName.Enabled = true;
            //        dtpBirthdate.Enabled = true;
            //        cboRoleAccess.Enabled = true;
            //        cboState.Enabled = true;
            //        cboCity.Enabled = true;
            //        pcbPicture.Enabled = true;

            //        txtPassword.Text = Preferences.GlobalTextToComparePasswords;
            //        txtRePassword.Text = Preferences.GlobalTextToComparePasswords;

            //        btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = false;
            //        btnAccept.Visible = btnCancel.Visible = true;

            //        txtUserName.Focus();

            //        byAction = 2;
            //    }
            //    else
            //    {
            //        MessageBox.Show("It isn't possible delete this user because in other session some user is editing it.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("The user doesn't exist.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            //    RefreshAllComponents();
            //}
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void pcbPicture_DoubleClick(object sender, EventArgs e)
        {
            if (byAction > 0)
            {
                try
                {
                    OpenFileDialog objOpenFileDialog = new OpenFileDialog();
                    objOpenFileDialog.Filter = "Picture files |*.jpg";

                    if (objOpenFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        stPathPicture = (objOpenFileDialog.FileName.Split('\\')[objOpenFileDialog.FileName.Split('\\').Length - 1]).Split('.')[0];

                        pcbPicture.ImageLocation = objOpenFileDialog.FileName;
                    }
                    else
                    {
                        pcbPicture.Image = null;
                        stPathPicture = null;
                        MessageBox.Show("The picture hasn't been selected.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch
                {
                    pcbPicture.Image = null;
                    stPathPicture = null;
                    MessageBox.Show("There was an unknown problem at charge the picture.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void cboState_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCity.Items.Clear();

            RefreshCities();
        }

        public void RefreshMainValues()
        {

        }

        private void RefreshMode()
        {
            cboMode.Items.Add("ON");
            cboMode.Items.Add("OFF");
        }

        private void RefreshStates()
        {
            cboState.Items.Add("");
            foreach (var vItem in BState.GetMexicosStates().OrderBy(s => s.stat_name__varchar))
                cboState.Items.Add(vItem.stat_name__varchar);
        }

        private void RefreshCities()
        {
            if (cboState.SelectedIndex > -1)
            {
                var vCountry = BCountry.FindByCode("MX");
                var vState = BState.FindByName(cboState.SelectedItem.ToString(), vCountry);

                if (vState != null)
                {
                    var vCities = BCity.GetCities(vState);

                    if (vCities != null)
                        foreach (var vItem in vCities.OrderBy(c => c.city_name__varchar))
                            cboCity.Items.Add(vItem.city_name__varchar);
                }
            }
        }
    }
}