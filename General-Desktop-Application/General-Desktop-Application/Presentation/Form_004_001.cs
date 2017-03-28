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
        byte byAction;

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

            var vUsersNewModel = BUser.GetAllUsersDecrypted().OrderBy(u => u.user_username__varchar).ThenBy(u => u.user_firstname__varchar).ThenBy(u => u.user_lastname__varchar);

            foreach (var vItem in vUsersNewModel)
            {
                lsbUsers.Items.Add((!string.IsNullOrEmpty(vItem.user_username__varchar) ? vItem.user_username__varchar : (!string.IsNullOrEmpty(vItem.user_email__varchar) ? vItem.user_email__varchar : vItem.user_cellphone__varchar)) + " - " + vItem.user_firstname__varchar + " - " + vItem.user_lastname__varchar);
            }

            lblQuantity.Text = "[Quantity: " + lsbUsers.Items.Count + "]";
        }

        private void RefreshRolesAccess()
        {
            cboRoleAccess.Items.Add("1 - Administrator");
        }

        private void RefreshStates()
        {
            foreach (var vItem in BState.GetMexicosStates().OrderBy(s=>s.stat_name__varchar))
                cboState.Items.Add(vItem.stat_name__varchar);
        }

        private void RefreshCities()
        {
            if (cboState.SelectedIndex > 0)
            {
                var vState = BState.FindByName(cboState.SelectedItem.ToString());

                if (vState != null)
                {
                    var vCities = BCity.GetCities(vState);

                    if (vCities != null)
                        foreach (var vItem in vCities.OrderBy(c => c.city_name__varchar))
                            cboCity.Items.Add(vItem.city_name__varchar);
                }

                //foreach (var vItem in BCity.GetCities(vState))
                //    cboState.Items.Add(vItem.city_name__varchar);
            }
        }

        private void lsbUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (byAction == 0 && lsbUsers.SelectedIndex > -1)
            {
                var vUser = BUser.FindByUserNameOrEmailOrCellphone(!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[0]) ? lsbUsers.SelectedItem.ToString().Split(' ')[0] : (!string.IsNullOrEmpty(lsbUsers.SelectedItem.ToString().Split(' ')[1]) ? lsbUsers.SelectedItem.ToString().Split(' ')[1] : lsbUsers.SelectedItem.ToString().Split(' ')[2]));

                if (vUser != null)
                {
                    btnEdit.Enabled = btnDelete.Enabled = true;

                    cboRoleAccess.Items.Clear();
                    cboState.Items.Clear();
                    cboCity.Items.Clear();

                    RefreshRolesAccess();
                    RefreshStates();

                    var vUserNewModel = BUser.FindByUUIDDecrypted(vUser.user_uuid__uniqueidentifier);

                    txtUserName.Text = vUser.user_username__varchar;
                    txtEmail.Text = vUser.user_email__varchar;
                    txtCellphone.Text = vUser.user_cellphone__varchar;
                    txtFirstName.Text = vUserNewModel.user_firstname__varchar;
                    txtLastName.Text = vUserNewModel.user_lastname__varchar;
                    dtpBirthdate.Value = vUser.date_uuid_birthdate__uniqueidentifier != null ? vUser.date.date_value__date : new DateTime(2000, 1, 1);

                    if (vUser.city_uuid__uniqueidentifier != null)
                    {
                        cboState.Text = vUser.city.state.stat_name__varchar;

                        RefreshCities();

                        cboCity.Text = vUser.city.city_name__varchar;
                    }

                    if (vUser.reso_uuid_picture__uniqueidentifier != null)
                        pcbPicture.Image = Tools.ConvertirByteAImagen(vUser.resource.reso_value__varbinary);

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
                    dtpBirthdate.Value = new DateTime(2000, 1, 1);

                    cboRoleAccess.Items.Clear();
                    cboState.Items.Clear();
                    cboCity.Items.Clear();

                    RefreshRolesAccess();
                    RefreshStates();

                    pcbPicture.Image = null;

                    RefreshMainList();

                    if (lsbUsers.Items.Count > 0)
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
            dtpBirthdate.Value = new DateTime(2000, 01, 01);

            cboRoleAccess.Items.Clear();
            cboState.Items.Clear();
            cboCity.Items.Clear();

            RefreshRolesAccess();
            RefreshStates();

            pcbPicture.Image = null;

            btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = false;
            btnAccept.Visible = btnCancel.Visible = true;

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

                        btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = false;
                        btnAccept.Visible = btnCancel.Visible = true;

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

                            btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = false;
                            btnAccept.Visible = btnCancel.Visible = true;

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

                        byte[] logo = Tools.ConvertirImagenAByte(pcbPicture.Image);

                        btnAccept.Visible = btnCancel.Visible = false;
                        btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = true;

                        byAction = 0;
                        break;
                    }
                case 2:
                    {
                        btnAccept.Visible = btnCancel.Visible = false;
                        btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = true;

                        byAction = 0;
                        break;
                    }
                case 3:
                    {
                        btnAccept.Visible = btnCancel.Visible = false;
                        btnAdd.Visible = btnEdit.Visible = btnDelete.Visible = true;

                        byAction = 0;
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
                        dtpBirthdate.Value = new DateTime(2000, 01, 01);

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
                            dtpBirthdate.Value = new DateTime(2000, 01, 01);

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

                            txtUserName.Text = "";
                            txtEmail.Text = "";
                            txtCellphone.Text = "";
                            txtPassword.Text = "";
                            txtRePassword.Text = "";
                            txtFirstName.Text = "";
                            txtLastName.Text = "";
                            dtpBirthdate.Value = new DateTime(2000, 01, 01);

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
                OpenFileDialog objOpenFileDialog = new OpenFileDialog();
                objOpenFileDialog.Filter = "Picture files |*.jpg";

                if (objOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    String direccion = objOpenFileDialog.FileName;
                    pcbPicture.ImageLocation = direccion;
                }
                else
                {
                    pcbPicture.Image = null;
                    MessageBox.Show("The picture hasn't been uploaded.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void cboState_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCity.Items.Clear();

            RefreshCities();

            //var vState = BState.FindByName(cboState.Text.Split(' ')[0]);

            //if (vState != null)
            //{
            //    var vCities = BCity.GetCities(vState).OrderBy(c => c.city_name__varchar);

            //    if (vCities != null)
            //    {
            //        foreach (var vItem in vCities)
            //        {
            //            cboCity.Items.Add(vItem.city_name__varchar);
            //        }
            //    }
            //}
        }
    }
}