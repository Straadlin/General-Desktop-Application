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
        user objUserSelectedPrincipalItem;

        // Attributes
        byte byAction;
        string stPathPicture;

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
            byAction = 0;
            stPathPicture = null;
            objUserSelectedPrincipalItem = null;

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

            var vUsers = BUser.GetAllUsers();
            //var vUsersNewModel = BUser.GetAllUsersDecrypted().OrderBy(u => u.user_username__varchar).ThenBy(u => u.user_firstname__varchar).ThenBy(u => u.user_lastname__varchar);

            foreach (var vItem in vUsers)
                lsbUsers.Items.Add((!string.IsNullOrEmpty(vItem.user_username__varchar) ? vItem.user_username__varchar : (!string.IsNullOrEmpty(vItem.user_email__varchar) ? vItem.user_email__varchar : vItem.user_cellphone__varchar)) + " - " + Tools.Decrypt(vItem.user_firstname__varchar) + " " + Tools.Decrypt(vItem.user_lastname__varchar));

            lblQuantity.Text = "[Quantity: " + lsbUsers.Items.Count + "]";
        }

        private void RefreshRolesAccess()
        {
            cboRoleAccess.Items.Add("1 - Administrator");
        }

        private void RefreshStates()
        {
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

        private void lsbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            objUserSelectedPrincipalItem = null;

            if (byAction == 0 && lsbUsers.SelectedIndex > -1)
            {
                user vUser = BUser.FindByUserNameOrEmailOrCellphone(!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[0]) ? lsbUsers.SelectedItem.ToString().Split(' ')[0] : (!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[1]) ? lsbUsers.SelectedItem.ToString().Split(' ')[1] : lsbUsers.SelectedItem.ToString().Split(' ')[2]));

                if (vUser != null)
                {
                    objUserSelectedPrincipalItem = vUser;

                    btnEdit.Enabled = btnDelete.Enabled = true;

                    cboRoleAccess.Items.Clear();
                    cboState.Items.Clear();
                    cboCity.Items.Clear();

                    RefreshRolesAccess();
                    RefreshStates();

                    txtUserName.Text = vUser.user_username__varchar;
                    txtEmail.Text = vUser.user_email__varchar;
                    txtCellphone.Text = vUser.user_cellphone__varchar;

                    foreach (var vItem in cboRoleAccess.Items)
                        if (vItem.ToString()[0] == vUser.user_roleaccess__tinyint.ToString()[0])
                            cboRoleAccess.SelectedItem = vItem;

                    txtFirstName.Text = Tools.Decrypt(vUser.user_firstname__varchar);
                    txtLastName.Text = Tools.Decrypt(vUser.user_lastname__varchar);
                    dtpBirthdate.Value = vUser.date_uuid_birthdate__uniqueidentifier != null ? BDate.FindByUUID(vUser.date_uuid_birthdate__uniqueidentifier.Value).date_value__date : DateTime.Now;

                    if (vUser.city_uuid__uniqueidentifier != null)
                    {
                        var vCity = BCity.FindByUUID(vUser.city_uuid__uniqueidentifier.Value);

                        cboState.Text = BState.FindByUUID(vCity.stat_uuid__uniqueidentifier).stat_name__varchar;

                        cboCity.Text = vCity.city_name__varchar;

                        RefreshCities();
                    }

                    if (vUser.reso_uuid_picture__uniqueidentifier != null)
                        pcbPicture.Image = Tools.ConvertirByteAImagen(BResource.FindByUUID(vUser.reso_uuid_picture__uniqueidentifier.Value).reso_value__varbinary);
                    else
                        pcbPicture.Image = null;

                    btnEdit.Enabled = btnDelete.Enabled = true;
                }
                else
                {
                    txtUserName.Text = "";
                    txtEmail.Text = "";
                    txtCellphone.Text = "";
                    txtPassword.Text = "";
                    txtFirstName.Text = "";
                    txtLastName.Text = "";
                    dtpBirthdate.Value = DateTime.Now;
                    //dtpBirthdate.Value = new DateTime(1900, 1, 1);

                    cboRoleAccess.Items.Clear();
                    cboState.Items.Clear();
                    cboCity.Items.Clear();

                    RefreshRolesAccess();
                    RefreshStates();

                    pcbPicture.Image = null;

                    RefreshMainList();

                    if (lsbUsers.Items.Count > -1)
                        lsbUsers.SelectedIndex = 0;

                    btnEdit.Enabled = btnDelete.Enabled = false;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            gpbA.Enabled = false;

            txtUserName.Enabled = true;
            txtEmail.Enabled = true;
            txtCellphone.Enabled = true;
            txtPassword.Enabled = true;
            txtRePassword.Enabled = true;
            txtFirstName.Enabled = true;
            txtLastName.Enabled = true;
            dtpBirthdate.Enabled = true;
            cboRoleAccess.Enabled = true;
            cboState.Enabled = true;
            cboCity.Enabled = true;
            pcbPicture.Enabled = true;

            txtUserName.Text = "";
            txtEmail.Text = "";
            txtCellphone.Text = "";
            txtPassword.Text = "";
            txtRePassword.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            dtpBirthdate.Value = DateTime.Now;
            //dtpBirthdate.Value = new DateTime(2000, 01, 01);

            cboRoleAccess.Items.Clear();
            cboState.Items.Clear();
            cboCity.Items.Clear();

            RefreshRolesAccess();
            RefreshStates();

            pcbPicture.Image = null;

            btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = false;
            btnAccept.Visible = btnCancel.Visible = true;

            txtUserName.Focus();

            byAction = 1;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lsbUsers.SelectedIndex > -1)
            {
                var vUser = BUser.FindByUserNameOrEmailOrCellphone(!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[0]) ? lsbUsers.SelectedItem.ToString().Split(' ')[0] : (!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[1]) ? lsbUsers.SelectedItem.ToString().Split(' ')[1] : lsbUsers.SelectedItem.ToString().Split(' ')[2]));

                if (vUser != null)
                {
                    if (vUser.sess_uuid_used__uniqueidentifier == null)
                    {
                        BUser.DisableToEdit(vUser.user_uuid__uniqueidentifier, ObjForm_004.ObjSession);

                        gpbA.Enabled = false;

                        txtUserName.Enabled = true;
                        txtEmail.Enabled = true;
                        txtCellphone.Enabled = true;
                        txtPassword.Enabled = true;
                        txtRePassword.Enabled = true;
                        txtFirstName.Enabled = true;
                        txtLastName.Enabled = true;
                        dtpBirthdate.Enabled = true;
                        cboRoleAccess.Enabled = true;
                        cboState.Enabled = true;
                        cboCity.Enabled = true;
                        pcbPicture.Enabled = true;

                        txtPassword.Text = Preferences.GlobalTextToComparePasswords;
                        txtRePassword.Text = Preferences.GlobalTextToComparePasswords;

                        btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = false;
                        btnAccept.Visible = btnCancel.Visible = true;

                        txtUserName.Focus();

                        byAction = 2;
                    }
                    else
                    {
                        MessageBox.Show("It isn't possible delete this user because in other session some user is editing it.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("The user doesn't exist.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("You must select some user in the list.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lsbUsers.SelectedIndex > -1)
            {
                var vUser = BUser.FindByUserNameOrEmailOrCellphone(!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[0]) ? lsbUsers.SelectedItem.ToString().Split(' ')[0] : (!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[1]) ? lsbUsers.SelectedItem.ToString().Split(' ')[1] : lsbUsers.SelectedItem.ToString().Split(' ')[2]));

                if (vUser != null)
                {
                    if (vUser.sess_uuid_used__uniqueidentifier == null)
                    {
                        if (vUser.user_uuid__uniqueidentifier != ObjForm_004.ObjUser.user_uuid__uniqueidentifier)
                        {
                            BUser.DisableToEdit(vUser.user_uuid__uniqueidentifier, ObjForm_004.ObjSession);

                            gpbA.Enabled = false;

                            btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = false;
                            btnAccept.Visible = btnCancel.Visible = true;

                            btnCancel.Focus();

                            byAction = 3;
                        }
                        else
                        {
                            MessageBox.Show("It isn't possible delete this user because you're using right now.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("It isn't possible delete this user because in other session some user is editing it.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("The user doesn't exist.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("You must select some user in the list.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            switch (byAction)
            {
                case 1:
                    {
                        if (string.IsNullOrEmpty(txtUserName.Text)||RegularExpressions.CheckUsername(txtUserName.Text))
                        {
                            if (string.IsNullOrEmpty(txtEmail.Text) || RegularExpressions.CheckEmail(txtEmail.Text))
                            {
                                if (string.IsNullOrEmpty(txtCellphone.Text) || RegularExpressions.CheckNumber(txtCellphone.Text))
                                {
                                    switch (BUser.FindByUserNameOrEmailOrCellphone(txtUserName.Text, txtEmail.Text, txtCellphone.Text))
                                    {
                                        case 0:
                                            {
                                                if (RegularExpressions.CheckPassword(txtPassword.Text)&&txtPassword.Text == txtRePassword.Text)
                                                {
                                                    if (cboRoleAccess.SelectedIndex > -1)
                                                    {
                                                        if (RegularExpressions.CheckFirstNameOrLastName(txtFirstName.Text))
                                                        {
                                                            if (RegularExpressions.CheckFirstNameOrLastName(txtLastName.Text))
                                                            {
                                                                if (cboState.SelectedIndex == -1 || cboCity.SelectedIndex > -1)
                                                                {
                                                                    byte[] logo = null;
                                                                    if (pcbPicture.Image != null)
                                                                        logo = Tools.ConvertirImagenAByte(pcbPicture.Image);

                                                                    if (BUser.Add(
                                                                        !string.IsNullOrEmpty(txtUserName.Text) ? txtUserName.Text : null,
                                                                        !string.IsNullOrEmpty(txtEmail.Text) ? txtEmail.Text : null,
                                                                        !string.IsNullOrEmpty(txtCellphone.Text) ? txtCellphone.Text : null,
                                                                        txtPassword.Text != Preferences.GlobalTextToComparePasswords ? Tools.Encrypt(txtPassword.Text) : null,
                                                                        txtFirstName.Text,
                                                                        txtLastName.Text,
                                                                        Convert.ToByte(cboRoleAccess.SelectedIndex + 1),
                                                                        null,
                                                                        stPathPicture,
                                                                        logo,
                                                                        dtpBirthdate.Value,
                                                                        cboState.SelectedIndex > -1 ? cboState.Text : null,
                                                                        cboCity.SelectedIndex > -1 ? cboCity.Text : null,
                                                                        ObjForm_004.ObjSession
                                                                        ) != null)
                                                                    {
                                                                        gpbA.Enabled = true;

                                                                        txtUserName.Enabled = false;
                                                                        txtEmail.Enabled = false;
                                                                        txtCellphone.Enabled = false;
                                                                        txtPassword.Enabled = false;
                                                                        txtRePassword.Enabled = false;
                                                                        txtFirstName.Enabled = false;
                                                                        txtLastName.Enabled = false;
                                                                        dtpBirthdate.Enabled = false;
                                                                        cboRoleAccess.Enabled = false;
                                                                        cboState.Enabled = false;
                                                                        cboCity.Enabled = false;
                                                                        pcbPicture.Enabled = false;

                                                                        txtUserName.Text = "";
                                                                        txtEmail.Text = "";
                                                                        txtCellphone.Text = "";
                                                                        txtPassword.Text = "";
                                                                        txtRePassword.Text = "";
                                                                        txtFirstName.Text = "";
                                                                        txtLastName.Text = "";
                                                                        dtpBirthdate.Value = DateTime.Now;
                                                                        //dtpBirthdate.Value = new DateTime(2000, 01, 01);

                                                                        cboRoleAccess.Items.Clear();
                                                                        cboState.Items.Clear();
                                                                        cboCity.Items.Clear();

                                                                        RefreshRolesAccess();
                                                                        RefreshStates();
                                                                        pcbPicture.Image = null;

                                                                        btnEdit.Enabled = btnDelete.Enabled = false;

                                                                        btnAccept.Visible = btnCancel.Visible = false;
                                                                        btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = true;

                                                                        lsbUsers.Items.Clear();
                                                                        RefreshMainList();

                                                                        byAction = 0;

                                                                        MessageBox.Show(Preferences.GlobalSuccessOperation, Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                    }
                                                                    else
                                                                    {
                                                                        MessageBox.Show(Preferences.GlobalErrorOperation, Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    MessageBox.Show("The city must be selected if you selected some city.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                                    cboCity.Focus();
                                                                }
                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("The last name must be filled.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                                txtLastName.Focus();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("The first name must be filled.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                            txtFirstName.Focus();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("You must select some rol by this user.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                        cboRoleAccess.Focus();
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("The password fields are incorrect, them must be the same and include at least 4 characters, between numbers and letters.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                    txtPassword.Focus();
                                                }
                                                break;
                                            }
                                        case -1:
                                            {
                                                MessageBox.Show("You must fill some of the following fields: Username, Email or Cellphone, at least one of them must be filled.\r\nThe username must include at least 4 characters, between numbers and letters.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                txtUserName.Focus();
                                                break;
                                            }
                                        case 1:
                                            {
                                                MessageBox.Show("The username already exists.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                txtUserName.Focus();
                                                break;
                                            }
                                        case 2:
                                            {
                                                MessageBox.Show("The email already exists.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                txtEmail.Focus();
                                                break;
                                            }
                                        case 3:
                                            {
                                                MessageBox.Show("The cellphone already exists.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                txtCellphone.Focus();
                                                break;
                                            }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("The cellphone isn't correct.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    txtCellphone.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("The email isn't correct.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txtEmail.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The username isn't correct.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtUserName.Focus();
                        }
                        break;
                    }
                case 2:
                    {
                        if (string.IsNullOrEmpty(txtUserName.Text) || RegularExpressions.CheckUsername(txtUserName.Text))
                        {
                            if (string.IsNullOrEmpty(txtEmail.Text) || RegularExpressions.CheckEmail(txtEmail.Text))
                            {
                                if (string.IsNullOrEmpty(txtCellphone.Text) || RegularExpressions.CheckNumber(txtCellphone.Text))
                                {
                                    switch (BUser.FindByUserNameOrEmailOrCellphoneWithExcludedUser(txtUserName.Text, txtEmail.Text, txtCellphone.Text, objUserSelectedPrincipalItem))
                                    {
                                        case 0:
                                            {
                                                if (txtPassword.Text == Preferences.GlobalTextToComparePasswords || RegularExpressions.CheckPassword(txtPassword.Text) && txtPassword.Text == txtRePassword.Text)
                                                {
                                                    if (cboRoleAccess.SelectedIndex > -1)
                                                    {
                                                        if (RegularExpressions.CheckFirstNameOrLastName(txtFirstName.Text))
                                                        {
                                                            if (RegularExpressions.CheckFirstNameOrLastName(txtLastName.Text))
                                                            {
                                                                if (cboState.SelectedIndex == -1 || cboCity.SelectedIndex > -1)
                                                                {
                                                                    //objUserSelectedPrincipalItem = BUser.FindByUUID(objUserSelectedPrincipalItem.user_uuid__uniqueidentifier);// Its is neccesary to work the trigger

                                                                    byte[] logo = null;
                                                                    if (pcbPicture.Image != null)
                                                                        logo = Tools.ConvertirImagenAByte(pcbPicture.Image);

                                                                    if (BUser.Edit(
                                                                        objUserSelectedPrincipalItem,
                                                                        !string.IsNullOrEmpty(txtUserName.Text) ? txtUserName.Text : null,
                                                                        !string.IsNullOrEmpty(txtEmail.Text) ? txtEmail.Text : null,
                                                                        !string.IsNullOrEmpty(txtCellphone.Text) ? txtCellphone.Text : null,
                                                                        txtPassword.Text != Preferences.GlobalTextToComparePasswords ? Tools.Encrypt(txtPassword.Text) : objUserSelectedPrincipalItem.user_password__varchar,
                                                                        txtFirstName.Text,
                                                                        txtLastName.Text,
                                                                        Convert.ToByte(cboRoleAccess.SelectedIndex + 1),
                                                                        null,
                                                                        stPathPicture,
                                                                        logo,
                                                                        dtpBirthdate.Value,
                                                                        cboState.SelectedIndex > -1 ? cboState.Text : null,
                                                                        cboCity.SelectedIndex > -1 ? cboCity.Text : null,
                                                                        ObjForm_004.ObjSession
                                                                        ))
                                                                    {
                                                                        gpbA.Enabled = true;

                                                                        txtUserName.Enabled = false;
                                                                        txtEmail.Enabled = false;
                                                                        txtCellphone.Enabled = false;
                                                                        txtPassword.Enabled = false;
                                                                        txtRePassword.Enabled = false;
                                                                        txtFirstName.Enabled = false;
                                                                        txtLastName.Enabled = false;
                                                                        dtpBirthdate.Enabled = false;
                                                                        cboRoleAccess.Enabled = false;
                                                                        cboState.Enabled = false;
                                                                        cboCity.Enabled = false;
                                                                        pcbPicture.Enabled = false;

                                                                        txtUserName.Text = "";
                                                                        txtEmail.Text = "";
                                                                        txtCellphone.Text = "";
                                                                        txtPassword.Text = "";
                                                                        txtRePassword.Text = "";
                                                                        txtFirstName.Text = "";
                                                                        txtLastName.Text = "";
                                                                        dtpBirthdate.Value = DateTime.Now;

                                                                        cboRoleAccess.Items.Clear();
                                                                        cboState.Items.Clear();
                                                                        cboCity.Items.Clear();

                                                                        RefreshRolesAccess();
                                                                        RefreshStates();
                                                                        pcbPicture.Image = null;

                                                                        btnEdit.Enabled = btnDelete.Enabled = false;

                                                                        btnAccept.Visible = btnCancel.Visible = false;
                                                                        btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = true;

                                                                        byAction = 0;

                                                                        MessageBox.Show(Preferences.GlobalSuccessOperation, Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                    }
                                                                    else
                                                                    {
                                                                        MessageBox.Show(Preferences.GlobalErrorOperation, Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    MessageBox.Show("The city must be selected if you selected some city.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                                    cboCity.Focus();
                                                                }
                                                            }
                                                            else
                                                            {
                                                                MessageBox.Show("The last name must be filled.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                                txtLastName.Focus();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("The first name must be filled.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                            txtFirstName.Focus();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("You must select some rol by this user.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                        cboRoleAccess.Focus();
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("The password fields are incorrect, them must be the same and include at least 4 characters, between numbers and letters.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                    txtPassword.Focus();
                                                }
                                                break;
                                            }
                                        case -1:
                                            {
                                                MessageBox.Show("You must fill some of the following fields: Username, Email or Cellphone, at least one of them must be filled.\r\nThe username must include at least 4 characters, between numbers and letters.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                txtUserName.Focus();
                                                break;
                                            }
                                        case 1:
                                            {
                                                MessageBox.Show("The username already exists.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                txtUserName.Focus();
                                                break;
                                            }
                                        case 2:
                                            {
                                                MessageBox.Show("The email already exists.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                txtEmail.Focus();
                                                break;
                                            }
                                        case 3:
                                            {
                                                MessageBox.Show("The cellphone already exists.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                txtCellphone.Focus();
                                                break;
                                            }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("The cellphone isn't correct.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    txtCellphone.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("The email isn't correct.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txtEmail.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("The username isn't correct.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtUserName.Focus();
                        }
                        break;
                    }
                case 3:
                    {
                        var vUser = BUser.FindByUserNameOrEmailOrCellphone(!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[0]) ? lsbUsers.SelectedItem.ToString().Split(' ')[0] : (!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[1]) ? lsbUsers.SelectedItem.ToString().Split(' ')[1] : lsbUsers.SelectedItem.ToString().Split(' ')[2]));

                        if(vUser!=null)
                        {
                            if(BUser.Remove(vUser, ObjForm_004.ObjSession))
                            {
                                gpbA.Enabled = true;

                                txtUserName.Text = "";
                                txtEmail.Text = "";
                                txtCellphone.Text = "";
                                txtPassword.Text = "";
                                txtRePassword.Text = "";
                                txtFirstName.Text = "";
                                txtLastName.Text = "";
                                dtpBirthdate.Value = DateTime.Now;
                                //dtpBirthdate.Value = new DateTime(2000, 01, 01);

                                cboRoleAccess.Items.Clear();
                                cboState.Items.Clear();
                                cboCity.Items.Clear();

                                RefreshRolesAccess();
                                RefreshStates();
                                pcbPicture.Image = null;

                                btnEdit.Enabled = btnDelete.Enabled = false;

                                btnAccept.Visible = btnCancel.Visible = false;
                                btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = true;

                                lsbUsers.Items.Clear();
                                RefreshMainList();

                                byAction = 0;

                                MessageBox.Show(Preferences.GlobalSuccessOperation, Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(Preferences.GlobalErrorOperation, Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        else
                        {
                            MessageBox.Show("The user doesn't exist.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        break;
                    }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            switch (byAction)
            {
                case 1:
                    {
                        gpbA.Enabled = true;

                        txtUserName.Enabled = false;
                        txtEmail.Enabled = false;
                        txtCellphone.Enabled = false;
                        txtPassword.Enabled = false;
                        txtRePassword.Enabled = false;
                        txtFirstName.Enabled = false;
                        txtLastName.Enabled = false;
                        dtpBirthdate.Enabled = false;
                        cboRoleAccess.Enabled = false;
                        cboState.Enabled = false;
                        cboCity.Enabled = false;
                        pcbPicture.Enabled = false;

                        txtUserName.Text = "";
                        txtEmail.Text = "";
                        txtCellphone.Text = "";
                        txtPassword.Text = "";
                        txtRePassword.Text = "";
                        txtFirstName.Text = "";
                        txtLastName.Text = "";
                        dtpBirthdate.Value= DateTime.Now;
                        //dtpBirthdate.Value = new DateTime(2000, 01, 01);

                        cboRoleAccess.Items.Clear();
                        cboState.Items.Clear();
                        cboCity.Items.Clear();

                        RefreshRolesAccess();
                        RefreshStates();
                        pcbPicture.Image = null;

                        btnEdit.Enabled = btnDelete.Enabled = false;

                        btnAccept.Visible = btnCancel.Visible = false;
                        btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = true;

                        byAction = 0;
                        break;
                    }
                case 2:
                    {
                        var vUser = BUser.FindByUserNameOrEmailOrCellphone(!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[0]) ? lsbUsers.SelectedItem.ToString().Split(' ')[0] : (!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[1]) ? lsbUsers.SelectedItem.ToString().Split(' ')[1] : lsbUsers.SelectedItem.ToString().Split(' ')[2]));

                        if (vUser != null)
                        {
                            BUser.EnableToEdit(vUser.user_uuid__uniqueidentifier);

                            gpbA.Enabled = true;

                            txtUserName.Enabled = false;
                            txtEmail.Enabled = false;
                            txtCellphone.Enabled = false;
                            txtPassword.Enabled = false;
                            txtRePassword.Enabled = false;
                            txtFirstName.Enabled = false;
                            txtLastName.Enabled = false;
                            dtpBirthdate.Enabled = false;
                            cboRoleAccess.Enabled = false;
                            cboState.Enabled = false;
                            cboCity.Enabled = false;
                            pcbPicture.Enabled = false;

                            txtUserName.Text = "";
                            txtEmail.Text = "";
                            txtCellphone.Text = "";
                            txtPassword.Text = "";
                            txtRePassword.Text = "";
                            txtFirstName.Text = "";
                            txtLastName.Text = "";
                            dtpBirthdate.Value= DateTime.Now;
                            //dtpBirthdate.Value = new DateTime(2000, 01, 01);

                            cboRoleAccess.Items.Clear();
                            cboState.Items.Clear();
                            cboCity.Items.Clear();

                            RefreshRolesAccess();
                            RefreshStates();
                            pcbPicture.Image = null;

                            btnEdit.Enabled = btnDelete.Enabled = false;

                            btnAccept.Visible = btnCancel.Visible = false;
                            btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = true;

                            byAction = 0;
                        }
                        break;
                    }
                case 3:
                    {
                        var vUser = BUser.FindByUserNameOrEmailOrCellphone(!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[0]) ? lsbUsers.SelectedItem.ToString().Split(' ')[0] : (!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[1]) ? lsbUsers.SelectedItem.ToString().Split(' ')[1] : lsbUsers.SelectedItem.ToString().Split(' ')[2]));

                        if (vUser != null)
                        {
                            BUser.EnableToEdit(vUser.user_uuid__uniqueidentifier);

                            gpbA.Enabled = true;

                            txtUserName.Text = "";
                            txtEmail.Text = "";
                            txtCellphone.Text = "";
                            txtPassword.Text = "";
                            txtRePassword.Text = "";
                            txtFirstName.Text = "";
                            txtLastName.Text = "";
                            dtpBirthdate.Value= DateTime.Now;
                            //dtpBirthdate.Value = new DateTime(2000, 01, 01);

                            cboRoleAccess.Items.Clear();
                            cboState.Items.Clear();
                            cboCity.Items.Clear();

                            RefreshRolesAccess();
                            RefreshStates();
                            pcbPicture.Image = null;

                            btnEdit.Enabled = btnDelete.Enabled = false;

                            btnAccept.Visible = btnCancel.Visible = false;
                            btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = true;

                            byAction = 0;
                        }
                        break;
                    }
            }
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
    }
}