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

        // Properties
        public Form_004 ObjForm_004
        { set; get; }

        public Form_004_001(Form_004 objForm_004)
        {
            InitializeComponent();

            ObjForm_004 = objForm_004;

            KeyPreview = true;
        }

        private void Form_004_001_Load(object sender, EventArgs e)
        {
            byAction = 0;
            stPathPicture = null;
            objUserSelectedPrincipalItem = null;

            ObjForm_004.ActivateOrDeactivateComponents("Users");

            RefreshMainList();

            ObjForm_004.Cursor = Cursors.Default;
        }

        private void Form_004_001_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit of this section?", Preferences.TitleSoftware, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                e.Cancel = true;
        }

        private void Form_004_001_FormClosed(object sender, FormClosedEventArgs e)
        {
            ObjForm_004.ActivateOrDeactivateComponents(null);
        }

        private void Form_004_001_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
                Close();
            else if (e.KeyChar == 13)
            {
                if (txtUserName.Focused)
                {
                    if (txtUserName.Text.Length > 0)
                        txtEmail.Focus();
                }
                else if (txtEmail.Focused)
                {
                    if (txtEmail.Text.Length > 0 || txtUserName.Text.Length > 0)
                        txtCellphone.Focus();
                }
                else if (txtCellphone.Focused)
                {
                    if (txtCellphone.Text.Length > 0 || txtUserName.Text.Length > 0 || txtEmail.Text.Length > 0)
                        txtPassword.Focus();
                }
                else if (txtPassword.Focused)
                {
                    if (txtPassword.Text.Length > 0)
                        cboRoleAccess.Focus();
                }
                else if (cboRoleAccess.Focused)
                {
                    if (cboRoleAccess.SelectedIndex > -1)
                        txtFirstName.Focus();
                }
                else if (txtFirstName.Focused)
                {
                    if (txtFirstName.Text.Length > 0)
                        txtLastName.Focus();
                }
                else if (txtLastName.Focused)
                {
                    if (txtLastName.Text.Length > 0)
                        dtpBirthdate.Focus();
                }
                else if (dtpBirthdate.Focused)
                {
                    cboState.Focus();
                }
                else if (cboState.Focused)
                {
                    //if (cboState.SelectedIndex > -1)
                    cboCity.Focus();
                }
                else if (cboCity.Focused)
                {
                    if (cboState.SelectedIndex < 1 || cboCity.SelectedIndex > 0)
                        btnAccept.Focus();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lsbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            objUserSelectedPrincipalItem = null;

            if (byAction == 0 && lsbUsers.SelectedIndex > -1)
            {
                var vUser = BUser.FindByUserNameOrEmailOrCellphone(!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[0]) ? lsbUsers.SelectedItem.ToString().Split(' ')[0] : (!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[1]) ? lsbUsers.SelectedItem.ToString().Split(' ')[1] : lsbUsers.SelectedItem.ToString().Split(' ')[2]));

                if (vUser != null)
                {
                    objUserSelectedPrincipalItem = vUser;

                    btnEdit.Enabled = btnDelete.Enabled = true;

                    cboRoleAccess.Items.Clear();
                    cboState.Items.Clear();
                    cboCity.Items.Clear();

                    RefreshRolesAccess();
                    RefreshStates();

                    txtUserName.Text = vUser.user_username__nvarchar;
                    txtEmail.Text = vUser.user_email__nvarchar;
                    txtCellphone.Text = vUser.user_cellphone__nvarchar;

                    txtPassword.Text = vUser.user_password__nvarchar;

                    foreach (var vItem in cboRoleAccess.Items)
                        if (vItem.ToString()[0] == vUser.user_roleaccess__tinyint.ToString()[0])
                            cboRoleAccess.SelectedItem = vItem;

                    txtFirstName.Text = Tools.Decrypt(vUser.user_firstname__nvarchar);
                    txtLastName.Text = Tools.Decrypt(vUser.user_lastname__nvarchar);
                    dtpBirthdate.Value = vUser.date_uuid_birthdate__uniqueidentifier != null ? BDate.FindByUUID(vUser.date_uuid_birthdate__uniqueidentifier.Value).date_value__date : DateTime.Now;

                    if (vUser.city_uuid__uniqueidentifier != null)
                    {
                        var vCity = BCity.FindByUUID(vUser.city_uuid__uniqueidentifier.Value);

                        cboState.Text = BState.FindByUUID(vCity.stat_uuid__uniqueidentifier).stat_name__nvarchar;

                        cboCity.Text = vCity.city_name__nvarchar;

                        RefreshCities();
                    }

                    pcbPicture.Image = vUser.reso_uuid_picture__uniqueidentifier != null ? Tools.ConvertirByteAImagen(BResource.FindByUUID(vUser.reso_uuid_picture__uniqueidentifier.Value).reso_value__varbinary) : null;

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            gpbA.Enabled = false;

            txtUserName.Enabled = true;
            txtEmail.Enabled = true;
            txtCellphone.Enabled = true;
            txtPassword.Enabled = true;
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
            txtFirstName.Text = "";
            txtLastName.Text = "";
            dtpBirthdate.Value = DateTime.Now;

            cboRoleAccess.Items.Clear();
            cboState.Items.Clear();
            cboCity.Items.Clear();

            RefreshRolesAccess();
            RefreshStates();

            pcbPicture.Image = null;

            btnAccept.Visible = btnCancel.Visible = true;
            btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = false;

            btnClose.Enabled = false;

            txtUserName.Focus();

            byAction = 1;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lsbUsers.SelectedIndex > -1)
            {
                //var vUser = BUser.FindByUserNameOrEmailOrCellphone(!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[0]) ? lsbUsers.SelectedItem.ToString().Split(' ')[0] : (!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[1]) ? lsbUsers.SelectedItem.ToString().Split(' ')[1] : lsbUsers.SelectedItem.ToString().Split(' ')[2]));

                var vUser = BUser.FindByUUID(objUserSelectedPrincipalItem.user_uuid__uniqueidentifier);

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
                        txtFirstName.Enabled = true;
                        txtLastName.Enabled = true;
                        dtpBirthdate.Enabled = true;
                        cboRoleAccess.Enabled = true;
                        cboState.Enabled = true;
                        cboCity.Enabled = true;
                        pcbPicture.Enabled = true;

                        btnAccept.Visible = btnCancel.Visible = true;
                        btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = false;

                        btnClose.Enabled = false;

                        txtUserName.Focus();

                        byAction = 2;
                    }
                    else
                    {
                        MessageBox.Show("It isn't possible edit this user because in other session some user is editing it.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("The user doesn't exist.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    ClearAllComponents();
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
                //var vUser = BUser.FindByUserNameOrEmailOrCellphone(!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[0]) ? lsbUsers.SelectedItem.ToString().Split(' ')[0] : (!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[1]) ? lsbUsers.SelectedItem.ToString().Split(' ')[1] : lsbUsers.SelectedItem.ToString().Split(' ')[2]));

                var vUser = BUser.FindByUUID(objUserSelectedPrincipalItem.user_uuid__uniqueidentifier);

                if (vUser != null)
                {
                    if (vUser.sess_uuid_used__uniqueidentifier == null)
                    {
                        if (vUser.user_uuid__uniqueidentifier != ObjForm_004.ObjUser.user_uuid__uniqueidentifier)
                        {
                            BUser.DisableToEdit(vUser.user_uuid__uniqueidentifier, ObjForm_004.ObjSession);

                            gpbA.Enabled = false;

                            btnAccept.Visible = btnCancel.Visible = true;
                            btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = false;

                            btnClose.Enabled = false;

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

                    ClearAllComponents();
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
                        if (string.IsNullOrEmpty(txtUserName.Text) || RegularExpressions.CheckIsUsernameAndLength(txtUserName.Text, 4, 100))
                        {
                            if (string.IsNullOrEmpty(txtEmail.Text) || RegularExpressions.CheckIskEmail(txtEmail.Text))
                            {
                                if (string.IsNullOrEmpty(txtCellphone.Text) || RegularExpressions.CheckIsNumeric(txtCellphone.Text, 10, 10))
                                {
                                    switch (BUser.FindByUserNameOrEmailOrCellphone(txtUserName.Text, txtEmail.Text, txtCellphone.Text))
                                    {
                                        case 0:
                                            {
                                                if (RegularExpressions.CheckIsPasswordAndLength(txtPassword.Text, 4, 15))
                                                {
                                                    if (cboRoleAccess.SelectedIndex > -1)
                                                    {
                                                        if (RegularExpressions.CheckIsFirstNameOrLastNameAndLength(txtFirstName.Text, 1, 100))
                                                        {
                                                            if (RegularExpressions.CheckIsFirstNameOrLastNameAndLength(txtLastName.Text, 1, 100))
                                                            {
                                                                if (cboState.SelectedIndex < 1 || cboCity.SelectedIndex > -1)
                                                                {
                                                                    byte[] logo = null;
                                                                    if (pcbPicture.Image != null)
                                                                        logo = Tools.ConvertirImagenAByte(pcbPicture.Image);

                                                                    if (BUser.Add(
                                                                        !string.IsNullOrEmpty(txtUserName.Text) ? txtUserName.Text : null,
                                                                        !string.IsNullOrEmpty(txtEmail.Text) ? txtEmail.Text : null,
                                                                        !string.IsNullOrEmpty(txtCellphone.Text) ? txtCellphone.Text : null,
                                                                        txtPassword.Text,
                                                                        txtFirstName.Text,
                                                                        txtLastName.Text,
                                                                        Convert.ToByte(cboRoleAccess.SelectedIndex + 1),
                                                                        null,
                                                                        stPathPicture,
                                                                        logo,
                                                                        dtpBirthdate.Value,
                                                                        cboState.SelectedIndex > 0 ? cboState.Text : null,
                                                                        cboCity.SelectedIndex > -1 ? cboCity.Text : null,
                                                                        ObjForm_004.ObjSession
                                                                        ) != null)
                                                                    {
                                                                        gpbA.Enabled = true;

                                                                        txtUserName.Enabled = false;
                                                                        txtEmail.Enabled = false;
                                                                        txtCellphone.Enabled = false;
                                                                        txtPassword.Enabled = false;
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
                                                                        txtFirstName.Text = "";
                                                                        txtLastName.Text = "";
                                                                        dtpBirthdate.Value = DateTime.Now;

                                                                        cboRoleAccess.Items.Clear();
                                                                        cboState.Items.Clear();
                                                                        cboCity.Items.Clear();

                                                                        RefreshRolesAccess();
                                                                        RefreshStates();
                                                                        pcbPicture.Image = null;

                                                                        btnAccept.Visible = btnCancel.Visible = false;
                                                                        btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = true;

                                                                        btnEdit.Enabled = btnDelete.Enabled = false;

                                                                        btnClose.Enabled = true;

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
                                                    MessageBox.Show("The password field isn't correct, it must include at least 4 characters, between numbers and letters.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                        if (string.IsNullOrEmpty(txtUserName.Text) || RegularExpressions.CheckIsUsernameAndLength(txtUserName.Text, 4, 100))
                        {
                            if (string.IsNullOrEmpty(txtEmail.Text) || RegularExpressions.CheckIskEmail(txtEmail.Text))
                            {
                                if (string.IsNullOrEmpty(txtCellphone.Text) || RegularExpressions.CheckIsNumeric(txtCellphone.Text, 10, 10))
                                {
                                    switch (BUser.FindByUserNameOrEmailOrCellphoneWithExcludedUser(txtUserName.Text, txtEmail.Text, txtCellphone.Text, objUserSelectedPrincipalItem))
                                    {
                                        case 0:
                                            {
                                                if (txtPassword.Text==objUserSelectedPrincipalItem.user_password__nvarchar||RegularExpressions.CheckIsPasswordAndLength(txtPassword.Text, 4, 15))
                                                {
                                                    if (cboRoleAccess.SelectedIndex > -1)
                                                    {
                                                        if (RegularExpressions.CheckIsFirstNameOrLastNameAndLength(txtFirstName.Text, 1, 100))
                                                        {
                                                            if (RegularExpressions.CheckIsFirstNameOrLastNameAndLength(txtLastName.Text, 1, 100))
                                                            {
                                                                if (cboState.SelectedIndex < 1 || cboCity.SelectedIndex > -1)
                                                                {
                                                                    byte[] logo = null;
                                                                    if (pcbPicture.Image != null)
                                                                        logo = Tools.ConvertirImagenAByte(pcbPicture.Image);

                                                                    if (BUser.Edit(
                                                                        objUserSelectedPrincipalItem,
                                                                        !string.IsNullOrEmpty(txtUserName.Text) ? txtUserName.Text : null,
                                                                        !string.IsNullOrEmpty(txtEmail.Text) ? txtEmail.Text : null,
                                                                        !string.IsNullOrEmpty(txtCellphone.Text) ? txtCellphone.Text : null,
                                                                        txtPassword.Text,
                                                                        txtFirstName.Text,
                                                                        txtLastName.Text,
                                                                        Convert.ToByte(cboRoleAccess.SelectedIndex + 1),
                                                                        null,
                                                                        stPathPicture,
                                                                        logo,
                                                                        dtpBirthdate.Value,
                                                                        cboState.SelectedIndex > 0 ? cboState.Text : null,
                                                                        cboCity.SelectedIndex > -1 ? cboCity.Text : null,
                                                                        ObjForm_004.ObjSession
                                                                        ))
                                                                    {
                                                                        gpbA.Enabled = true;

                                                                        txtUserName.Enabled = false;
                                                                        txtEmail.Enabled = false;
                                                                        txtCellphone.Enabled = false;
                                                                        txtPassword.Enabled = false;
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
                                                                        txtFirstName.Text = "";
                                                                        txtLastName.Text = "";
                                                                        dtpBirthdate.Value = DateTime.Now;

                                                                        cboRoleAccess.Items.Clear();
                                                                        cboState.Items.Clear();
                                                                        cboCity.Items.Clear();

                                                                        RefreshRolesAccess();
                                                                        RefreshStates();
                                                                        pcbPicture.Image = null;

                                                                        btnAccept.Visible = btnCancel.Visible = false;
                                                                        btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = true;

                                                                        btnEdit.Enabled = btnDelete.Enabled = false;

                                                                        btnClose.Enabled = true;

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
                                                    MessageBox.Show("The password field isn't correct, it must include at least 4 characters, between numbers and letters.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                        //var vUser = BUser.FindByUserNameOrEmailOrCellphone(!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[0]) ? lsbUsers.SelectedItem.ToString().Split(' ')[0] : (!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[1]) ? lsbUsers.SelectedItem.ToString().Split(' ')[1] : lsbUsers.SelectedItem.ToString().Split(' ')[2]));

                        var vUser = BUser.FindByUUID(objUserSelectedPrincipalItem.user_uuid__uniqueidentifier);

                        if (vUser != null)
                        {
                            if (BUser.Remove(vUser, ObjForm_004.ObjSession))
                            {
                                gpbA.Enabled = true;

                                txtUserName.Text = "";
                                txtEmail.Text = "";
                                txtCellphone.Text = "";
                                txtPassword.Text = "";
                                txtFirstName.Text = "";
                                txtLastName.Text = "";
                                dtpBirthdate.Value = DateTime.Now;

                                cboRoleAccess.Items.Clear();
                                cboState.Items.Clear();
                                cboCity.Items.Clear();

                                RefreshRolesAccess();
                                RefreshStates();
                                pcbPicture.Image = null;

                                btnAccept.Visible = btnCancel.Visible = false;
                                btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = true;

                                btnEdit.Enabled = btnDelete.Enabled = false;

                                btnClose.Enabled = true;

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

                            ClearAllComponents();
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
                        txtFirstName.Text = "";
                        txtLastName.Text = "";
                        dtpBirthdate.Value = DateTime.Now;

                        cboRoleAccess.Items.Clear();
                        cboState.Items.Clear();
                        cboCity.Items.Clear();

                        RefreshRolesAccess();
                        RefreshStates();
                        pcbPicture.Image = null;

                        btnAccept.Visible = btnCancel.Visible = false;
                        btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = true;

                        btnEdit.Enabled = btnDelete.Enabled = false;

                        btnClose.Enabled = true;

                        byAction = 0;
                        break;
                    }
                case 2:
                    {
                        //var vUser = BUser.FindByUserNameOrEmailOrCellphone(!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[0]) ? lsbUsers.SelectedItem.ToString().Split(' ')[0] : (!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[1]) ? lsbUsers.SelectedItem.ToString().Split(' ')[1] : lsbUsers.SelectedItem.ToString().Split(' ')[2]));

                        var vUser = BUser.FindByUUID(objUserSelectedPrincipalItem.user_uuid__uniqueidentifier);

                        if (vUser != null)
                        {
                            BUser.EnableToEdit(vUser.user_uuid__uniqueidentifier);

                            gpbA.Enabled = true;

                            txtUserName.Enabled = false;
                            txtEmail.Enabled = false;
                            txtCellphone.Enabled = false;
                            txtPassword.Enabled = false;
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
                            txtFirstName.Text = "";
                            txtLastName.Text = "";
                            dtpBirthdate.Value = DateTime.Now;

                            cboRoleAccess.Items.Clear();
                            cboState.Items.Clear();
                            cboCity.Items.Clear();

                            RefreshRolesAccess();
                            RefreshStates();
                            pcbPicture.Image = null;

                            btnAccept.Visible = btnCancel.Visible = false;
                            btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = true;

                            btnEdit.Enabled = btnDelete.Enabled = false;

                            btnClose.Enabled = true;

                            byAction = 0;
                        }
                        break;
                    }
                case 3:
                    {
                        //var vUser = BUser.FindByUserNameOrEmailOrCellphone(!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[0]) ? lsbUsers.SelectedItem.ToString().Split(' ')[0] : (!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[1]) ? lsbUsers.SelectedItem.ToString().Split(' ')[1] : lsbUsers.SelectedItem.ToString().Split(' ')[2]));

                        var vUser = BUser.FindByUUID(objUserSelectedPrincipalItem.user_uuid__uniqueidentifier);

                        if (vUser != null)
                        {
                            BUser.EnableToEdit(vUser.user_uuid__uniqueidentifier);

                            gpbA.Enabled = true;

                            txtUserName.Text = "";
                            txtEmail.Text = "";
                            txtCellphone.Text = "";
                            txtPassword.Text = "";
                            txtFirstName.Text = "";
                            txtLastName.Text = "";
                            dtpBirthdate.Value = DateTime.Now;

                            cboRoleAccess.Items.Clear();
                            cboState.Items.Clear();
                            cboCity.Items.Clear();

                            RefreshRolesAccess();
                            RefreshStates();
                            pcbPicture.Image = null;

                            btnAccept.Visible = btnCancel.Visible = false;
                            btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = true;

                            btnEdit.Enabled = btnDelete.Enabled = false;

                            btnClose.Enabled = true;

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

        private void RefreshMainList()
        {
            lsbUsers.Items.Clear();

            var vUsers = BUser.GetAllUsers();

            foreach (var vItem in vUsers)
                lsbUsers.Items.Add((!string.IsNullOrEmpty(vItem.user_username__nvarchar) ? vItem.user_username__nvarchar : (!string.IsNullOrEmpty(vItem.user_email__nvarchar) ? vItem.user_email__nvarchar : vItem.user_cellphone__nvarchar)) + " - " + Tools.Decrypt(vItem.user_firstname__nvarchar) + " " + Tools.Decrypt(vItem.user_lastname__nvarchar));

            lblQuantity.Text = "[Quantity: " + lsbUsers.Items.Count + "]";
        }

        private void ClearAllComponents()
        {
            lsbUsers.Items.Clear();

            RefreshMainList();

            txtUserName.Text = "";
            txtEmail.Text = "";
            txtCellphone.Text = "";
            txtPassword.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            dtpBirthdate.Value = DateTime.Now;

            cboRoleAccess.Items.Clear();
            cboState.Items.Clear();
            cboCity.Items.Clear();

            RefreshRolesAccess();
            RefreshStates();
            pcbPicture.Image = null;
        }

        private void RefreshRolesAccess()
        {
            cboRoleAccess.Items.Add("1 - Administrator");
        }

        private void RefreshStates()
        {
            cboState.Items.Add("");
            foreach (var vItem in BState.GetMexicosStates().OrderBy(s => s.stat_name__nvarchar))
                cboState.Items.Add(vItem.stat_name__nvarchar);
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
                        foreach (var vItem in vCities.OrderBy(c => c.city_name__nvarchar))
                            cboCity.Items.Add(vItem.city_name__nvarchar);
                }
            }
        }

        private void txtPassword_MouseHover(object sender, EventArgs e)
        {
            if (byAction == 1 || byAction == 2 )
                txtPassword.PasswordChar = '\0';
        }

        private void txtPassword_MouseLeave(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar != '•')
                txtPassword.PasswordChar = '•';
        }
    }
}