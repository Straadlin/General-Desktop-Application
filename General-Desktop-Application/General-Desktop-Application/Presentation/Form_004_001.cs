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

            var vUsersNewModel = UserB.GetAllUsersDecrypted().OrderBy(u => u.user_username__varchar).ThenBy(u => u.user_firstname__varchar).ThenBy(u => u.user_lastname__varchar);

            foreach (var vItem in vUsersNewModel)
            {
                lsbUsers.Items.Add((!string.IsNullOrEmpty(vItem.user_username__varchar) ? vItem.user_username__varchar : (!string.IsNullOrEmpty(vItem.user_email__varchar) ? vItem.user_email__varchar : vItem.user_cellphone__varchar)) + " - " + vItem.user_firstname__varchar + " - " + vItem.user_lastname__varchar);
            }

            lblQuantity.Text = "[Quantity: " + lsbUsers.Items.Count + "]";
        }

        private void RefreshAccessRoles()
        {
            cboRoleAccess.Items.Clear();

            cboRoleAccess.Items.Add("1 - Administrator");
        }

        private void RefreshStates()
        {
            cboState.Items.Clear();

            foreach (var vItem in StateB.GetMexicosStates())
            {
                cboState.Items.Add(vItem.stat_name__varchar);
            }
        }

        private void RefreshCities()
        {
            cboCity.Items.Clear();

            if (cboState.SelectedIndex > 0)
            {
                var vState = StateB.FindByName(cboState.SelectedItem.ToString());
                foreach (var vItem in CityB.GetCities(vState))
                    cboState.Items.Add(vItem.city_name__varchar);
            }
        }

        private void txtPassword_MouseHover(object sender, EventArgs e)
        {
            if (txtPassword.Enabled)
                txtPassword.PasswordChar = '\0';
        }

        private void txtPassword_MouseLeave(object sender, EventArgs e)
        {
                txtPassword.PasswordChar = '•';
        }

        private void lsbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var vUser = UserB.FindByUserNameOrEmailOrCellphone(!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[0]) ? lsbUsers.SelectedItem.ToString().Split(' ')[0] : (!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[1]) ? lsbUsers.SelectedItem.ToString().Split(' ')[1] : lsbUsers.SelectedItem.ToString().Split(' ')[2]));

            if (vUser != null)
            {
                RefreshAccessRoles();
                RefreshStates();
                RefreshCities();

                var vUserNewModel=UserB.FindByUUIDDecrypted(vUser.user_uuid__uniqueidentifier);

                txtUserName.Text = vUser.user_username__varchar;
                txtEmail.Text = vUser.user_email__varchar;
                txtCellphone.Text = vUser.user_cellphone__varchar;
                txtFirstName.Text = vUserNewModel.user_firstname__varchar;
                txtLastName.Text = vUserNewModel.user_lastname__varchar;
                dtpBirthdate.Value = vUser.date_uuid_birthdate__uniqueidentifier != null ? vUser.date.date_value__date : new DateTime(2000, 1, 1);
                if(vUser.city_uuid__uniqueidentifier != null)
                {
                    cboCity.Text = vUser.city.city_name__varchar;

                    cboState.Text = vUser.city.state.stat_name__varchar;
                }

                //picture
            }
            else
            {
                txtUserName.Text = "";
                txtEmail.Text = "";
                txtCellphone.Text = "";
                txtPassword.Text = "";
                RefreshAccessRoles();
                txtFirstName.Text = "";
                txtLastName.Text = "";
                dtpBirthdate.Value = new DateTime(2000, 1, 1);
                RefreshStates();
                RefreshCities();
                //pcbPicture.clea

                RefreshMainList();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}