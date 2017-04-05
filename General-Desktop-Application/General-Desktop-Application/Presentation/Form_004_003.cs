using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Reporting.WinForms;

using General_Desktop_Application.BusinessLayer;
using General_Desktop_Application.Classes;
using General_Desktop_Application.EF;
using General_Desktop_Application.Reports;

namespace General_Desktop_Application.Presentation
{
    public partial class Form_004_003 : Form
    {
        // Objects

        // Attributes

        // Properties
        public Form_004 ObjForm_004
        { set; get; }

        public Form_004_003(Form_004 objForm_004)
        {
            InitializeComponent();

            ObjForm_004 = objForm_004;

            KeyPreview = true;
        }

        private void Form_004_003_Load(object sender, EventArgs e)
        {
            ObjForm_004.ActivateOrDeactivateComponents("Reports");

            RefreshMainValues();

            ObjForm_004.Cursor = Cursors.Default;
        }

        private void Form_004_003_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit of this section?", Preferences.TitleSoftware, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                e.Cancel = true;
        }

        private void Form_004_003_FormClosed(object sender, FormClosedEventArgs e)
        {
            ObjForm_004.ActivateOrDeactivateComponents(null);
        }

        private void Form_004_003_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
                Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cboState_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshMainValues();
        }

        private bool RefreshMainValues()
        {

            if (cboType.SelectedIndex > -1)
            {
                try
                {
                    var vPrincipalCompany = BPrincipalCompany.Get();

                    if (cboType.SelectedIndex == 0)
                    {
                        var vUsers = BUser.GetAllUsers();

                        List<Report_Users_001> objListReport = new List<Report_Users_001>();

                        if (vUsers != null)
                        {
                            for (int inCont = 0; inCont < vUsers.Count; inCont++)
                            {
                                string stCreatedBy, stModifiedBy;
                                stCreatedBy = stModifiedBy = "";

                                var vUserBranches = BUser.GetAllBranches(vUsers[inCont].user_uuid__uniqueidentifier);

                                if (vUserBranches != null && vUserBranches.Count > 1)
                                {
                                    var vUser = BUser.FindByUUID(vUserBranches[0].user_uuid__uniqueidentifier.Value);

                                    stCreatedBy = Tools.Decrypt(vUser.user_firstname__nvarchar) + " " + Tools.Decrypt(vUser.user_lastname__nvarchar);

                                    vUser = BUser.FindByUUID(vUserBranches[vUserBranches.Count-1].user_uuid__uniqueidentifier.Value);

                                    stModifiedBy = Tools.Decrypt(vUser.user_firstname__nvarchar) + " " + Tools.Decrypt(vUser.user_lastname__nvarchar);
                                }
                                else
                                {
                                    stCreatedBy = Tools.Decrypt(vUsers[inCont].user_firstname__nvarchar) + " " + Tools.Decrypt(vUsers[inCont].user_lastname__nvarchar);
                                }

                                objListReport.Add(new Report_Users_001()
                                {
                                    Number = Convert.ToString(inCont + 1),
                                    Username = vUsers[inCont].user_username__nvarchar,
                                    Email = vUsers[inCont].user_email__nvarchar,
                                    Cellphone = vUsers[inCont].user_cellphone__nvarchar,
                                    FirstName = Tools.Decrypt(vUsers[inCont].user_firstname__nvarchar),
                                    LastName = Tools.Decrypt(vUsers[inCont].user_lastname__nvarchar),
                                    RoleAccess = vUsers[inCont].user_roleaccess__tinyint == 1 ? "Administrator" : "<>",
                                    CreatedBy = stCreatedBy,
                                    LastModificationBy = stModifiedBy
                                });
                            }
                        }
                        ///////////////////////////////////////////////////////////////////////////////

                        rpvReport.LocalReport.ReportEmbeddedResource = "General_Desktop_Application.Reports.Report_Users_001.rdlc";

                        // We clean DataSource of report.
                        rpvReport.LocalReport.DataSources.Clear();

                        //
                        // We establish the parameters that we'll send them to report.
                        //
                        string stPUser = Tools.Decrypt(ObjForm_004.ObjUser.user_firstname__nvarchar) + " " + Tools.Decrypt(ObjForm_004.ObjUser.user_lastname__nvarchar);
                        string stPCompany = vPrincipalCompany.prco_name__nvarchar;
                        string stPAddress = vPrincipalCompany.prco_address__nvarchar != null ? vPrincipalCompany.prco_address__nvarchar : "";
                        ReportParameter[] objReportParameter = {
                            new ReportParameter("pReportName", "User Report"),
                            new ReportParameter("pLogo", vPrincipalCompany.reso_uuid_logo__uniqueidentifier != null ? Convert.ToBase64String(BResource.FindByUUID(vPrincipalCompany.reso_uuid_logo__uniqueidentifier.Value).reso_value__varbinary) : null),
                            new ReportParameter("pDate", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()),
                            new ReportParameter("pCompany", stPCompany.Length>20 ? stPCompany.Substring(0,20) : stPCompany),
                            new ReportParameter("pRFC", vPrincipalCompany.prco_rfc__nvarchar != null ? vPrincipalCompany.prco_rfc__nvarchar :null),
                            new ReportParameter("pUser", stPUser.Length > 20 ? stPUser.Substring(0, 20) : stPUser),
                            new ReportParameter("pAddress", stPAddress.Length > 20 ? stPAddress.Substring(0, 20):stPAddress)
                        };

                        // We send the list with parameters.
                        rpvReport.LocalReport.SetParameters(objReportParameter);

                        //
                        // We establish the list as report's Datasource.
                        //
                        rpvReport.LocalReport.DataSources.Add(new ReportDataSource("dsReport_User_001", objListReport));

                        // We adjust the report's visualization mode.

                        rpvReport.SetDisplayMode(DisplayMode.PrintLayout);

                        rpvReport.ZoomMode = ZoomMode.Percent;
                        rpvReport.ZoomPercent = 100;

                        //
                        // We do a refresh to reportViewer.
                        //
                        rpvReport.RefreshReport();
                    }

                }
                catch
                {
                    MessageBox.Show(Preferences.GlobalErrorOperation, Preferences.TitleSoftware, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return false;
                }
            }

            return true;
        }
    }
}