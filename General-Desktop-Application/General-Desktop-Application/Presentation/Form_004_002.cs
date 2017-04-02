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
        principalcompany objPrincipalCompany;

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
                {
                    //if (txtRFC.Text.Length > 0)
                    txtCompany.Focus();
                }
                else if (txtCompany.Focused)
                {
                    if (txtCompany.Text.Length > 0)
                        txtAddress.Focus();
                }
                else if (txtAddress.Focused)
                {
                    //if (txtAddress.Text.Length > 0)
                    txtPhone.Focus();
                }
                else if (txtPhone.Focused)
                {
                    //if (txtPhone.Text.Length > 0)
                    txtEmail.Focus();
                }
                else if (txtEmail.Focused)
                {
                    //if (txtEmail.Text.Length > 0)
                    txtFacebook.Focus();
                }
                else if (txtFacebook.Focused)
                {
                    //if (txtFacebook.Text.Length > 0)
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
                        cboMode.Focus();
                }
                else if (cboMode.Focused)
                {
                    if (cboMode.SelectedIndex > -1)
                        nudHoursToBackup.Focus();
                }
                else if (nudHoursToBackup.Focused)
                {
                    nudDaysToDelete.Focus();
                }
                else if (nudDaysToDelete.Focused)
                {
                    btnAccept.Focus();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var vPrincipalCompany = BPrincipalCompany.Get();

            if (vPrincipalCompany != null)
            {
                if (vPrincipalCompany.sess_uuid_used__uniqueidentifier == null)
                {
                    BPrincipalCompany.DisableToEdit(vPrincipalCompany.prco_uuid__uniqueidentifier, ObjForm_004.ObjSession);

                    txtRFC.Enabled = true;
                    txtCompany.Enabled = true;
                    txtAddress.Enabled = true;
                    txtPhone.Enabled = true;
                    txtEmail.Enabled = true;
                    txtFacebook.Enabled = true;
                    cboState.Enabled = true;
                    cboCity.Enabled = true;
                    pcbLogo.Enabled = true;
                    cboMode.Enabled = true;
                    nudHoursToBackup.Enabled = true;
                    nudDaysToDelete.Enabled = true;

                    btnEdit.Visible = false;
                    btnAccept.Visible = btnCancel.Visible = true;

                    btnClose.Enabled = false;

                    txtRFC.Focus();

                    byAction = 2;
                }
                else
                {
                    MessageBox.Show("It isn't possible edit this user because in other session some user is editing it.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRFC.Text) || RegularExpressions.CheckIsRFC(txtRFC.Text))
            {
                if (RegularExpressions.CheckIsNormalText(txtCompany.Text, 1, 100))
                {
                    if (RegularExpressions.CheckIsNormalText(txtAddress.Text,0, 255))
                    {
                        if (RegularExpressions.CheckIsNumber(txtPhone.Text,10, 10))
                        {
                            if (string.IsNullOrEmpty(txtEmail.Text) || RegularExpressions.CheckIskEmail(txtEmail.Text))
                            {
                                if (RegularExpressions.CheckIsUrl(txtFacebook.Text))
                                {
                                    if (cboState.SelectedIndex < 1 || cboCity.SelectedIndex > -1)
                                    {
                                        byte[] logo = null;
                                        if (pcbLogo.Image != null)
                                            logo = Tools.ConvertirImagenAByte(pcbLogo.Image);

                                        if (BPrincipalCompany.Edit(
                                            objPrincipalCompany,
                                            !string.IsNullOrEmpty(txtRFC.Text) ? txtRFC.Text : null,
                                            txtCompany.Text,
                                            !string.IsNullOrEmpty(txtAddress.Text) ? txtAddress.Text : null,
                                            !string.IsNullOrEmpty(txtPhone.Text) ? txtPhone.Text : null,
                                            !string.IsNullOrEmpty(txtEmail.Text) ? txtEmail.Text : null,
                                            !string.IsNullOrEmpty(txtFacebook.Text) ? txtFacebook.Text : null,
                                            cboMode.SelectedIndex == 0 ? true : false,
                                            Convert.ToInt32(nudHoursToBackup.Value),
                                            Convert.ToInt32(nudDaysToDelete.Value),
                                            stPathPicture,
                                            logo,
                                            cboState.SelectedIndex > 0 ? cboState.Text : null,
                                            cboCity.SelectedIndex > -1 ? cboCity.Text : null,
                                            ObjForm_004.ObjSession
                                            ))
                                        {
                                            txtRFC.Enabled = false;
                                            txtCompany.Enabled = false;
                                            txtAddress.Enabled = false;
                                            txtPhone.Enabled = false;
                                            txtEmail.Enabled = false;
                                            txtFacebook.Enabled = false;
                                            cboState.Enabled = false;
                                            cboCity.Enabled = false;
                                            pcbLogo.Enabled = false;
                                            cboMode.Enabled = false;
                                            nudHoursToBackup.Enabled = false;
                                            nudDaysToDelete.Enabled = false;
                                            pcbLogo.Enabled = false;

                                            btnAccept.Visible = btnCancel.Visible = false;
                                            btnEdit.Visible = true;

                                            btnClose.Enabled = true;

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
                                    MessageBox.Show("The facebook isn't correct.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    txtFacebook.Focus();
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
                            MessageBox.Show("The phone isn't correct.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtPhone.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("The address isn't correct.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtAddress.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("The company isn't correct.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCompany.Focus();
                }
            }
            else
            {
                MessageBox.Show("The RFC isn't correct.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRFC.Focus();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            BPrincipalCompany.EnableToEdit(objPrincipalCompany.prco_uuid__uniqueidentifier);

            objPrincipalCompany = BPrincipalCompany.FindByUUID(objPrincipalCompany.prco_uuid__uniqueidentifier);

            txtRFC.Enabled = false;
            txtCompany.Enabled = false;
            txtAddress.Enabled = false;
            txtPhone.Enabled = false;
            txtEmail.Enabled = false;
            txtFacebook.Enabled = false;
            cboState.Enabled = false;
            cboCity.Enabled = false;
            pcbLogo.Enabled = false;
            cboMode.Enabled = false;
            nudHoursToBackup.Enabled = false;
            nudHoursToBackup.Enabled = false;

            txtRFC.Text = objPrincipalCompany.prco_rfc__nvarchar;
            txtCompany.Text = objPrincipalCompany.prco_name__nvarchar;
            txtAddress.Text = objPrincipalCompany.prco_address__nvarchar;
            txtPhone.Text = objPrincipalCompany.prco_phone__nvarchar;
            txtEmail.Text = objPrincipalCompany.prco_email__nvarchar;
            txtFacebook.Text = objPrincipalCompany.prco_facebook__nvarchar;

            cboState.Items.Clear();
            cboCity.Items.Clear();

            RefreshStates();

            if (objPrincipalCompany.city_uuid__uniqueidentifier != null)
            {
                var vCity = BCity.FindByUUID(objPrincipalCompany.city_uuid__uniqueidentifier.Value);

                cboState.Text = BState.FindByUUID(vCity.stat_uuid__uniqueidentifier).stat_name__nvarchar;

                cboCity.Text = vCity.city_name__nvarchar;

                RefreshCities();
            }

            pcbLogo.Image = objPrincipalCompany.reso_uuid_logo__uniqueidentifier != null ? Tools.ConvertirByteAImagen(BResource.FindByUUID(objPrincipalCompany.reso_uuid_logo__uniqueidentifier.Value).reso_value__varbinary) : null;
            cboMode.SelectedIndex = objPrincipalCompany.prco_developmentmode__bit == true ? 0 : 1;
            nudHoursToBackup.Value = objPrincipalCompany.prco_hoursbetweenbackups__int;
            nudDaysToDelete.Value = objPrincipalCompany.prco_daysdeletbackups__int;

            btnAccept.Visible = btnCancel.Visible = false;
            btnEdit.Visible = true;

            btnClose.Enabled = true;

            btnEdit.Focus();

            byAction = 0;
        }

        private void pcbLogo_DoubleClick(object sender, EventArgs e)
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

                        pcbLogo.ImageLocation = objOpenFileDialog.FileName;
                    }
                    else
                    {
                        pcbLogo.Image = null;
                        stPathPicture = null;
                        MessageBox.Show("The picture hasn't been selected.", Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch
                {
                    pcbLogo.Image = null;
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
            objPrincipalCompany = null;

            var vPrincipalCompany = BPrincipalCompany.Get();

            if (vPrincipalCompany != null)
            {
                objPrincipalCompany = vPrincipalCompany;

                RefreshStates();
                RefreshModes();

                txtRFC.Text = vPrincipalCompany.prco_rfc__nvarchar;
                txtCompany.Text = vPrincipalCompany.prco_name__nvarchar;
                txtAddress.Text = vPrincipalCompany.prco_address__nvarchar;
                txtPhone.Text = vPrincipalCompany.prco_phone__nvarchar;
                txtEmail.Text = vPrincipalCompany.prco_email__nvarchar;
                txtFacebook.Text = vPrincipalCompany.prco_facebook__nvarchar;

                if (vPrincipalCompany.city_uuid__uniqueidentifier != null)
                {
                    var vCity = BCity.FindByUUID(vPrincipalCompany.city_uuid__uniqueidentifier.Value);

                    cboState.Text = BState.FindByUUID(vCity.stat_uuid__uniqueidentifier).stat_name__nvarchar;

                    cboCity.Text = vCity.city_name__nvarchar;

                    RefreshCities();
                }

                pcbLogo.Image = vPrincipalCompany.reso_uuid_logo__uniqueidentifier != null ? Tools.ConvertirByteAImagen(BResource.FindByUUID(vPrincipalCompany.reso_uuid_logo__uniqueidentifier.Value).reso_value__varbinary) : null;
                cboMode.SelectedIndex = vPrincipalCompany.prco_developmentmode__bit ? 0 : 1;
                nudHoursToBackup.Value = objPrincipalCompany.prco_hoursbetweenbackups__int;
                nudDaysToDelete.Value = objPrincipalCompany.prco_daysdeletbackups__int;
            }
        }

        private void RefreshModes()
        {
            cboMode.Items.Add("ON");
            cboMode.Items.Add("OFF");
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
    }
}