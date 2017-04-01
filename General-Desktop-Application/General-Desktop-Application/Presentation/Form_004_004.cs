using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using General_Desktop_Application.Classes;

namespace General_Desktop_Application.Presentation
{
    public partial class Form_004_004 : Form
    {
        // Objects

        // Attributes

        // Properties
        public Form_004 ObjForm_004
        { set; get; }

        public Form_004_004(Form_004 objForm_004)
        {
            InitializeComponent();

            ObjForm_004 = objForm_004;

            KeyPreview = true;
        }

        private void Form_004_004_Load(object sender, EventArgs e)
        {
            lblCompany.Text = "Company Name:";
            lblProduct.Text = "Product Name: " + Preferences.TitleSoftware;
            lblVersion.Text = "Version: " + Preferences.CurrentVersion;
            lblPC.Text = "PC: "+Preferences.ProductCode;
            lblDesigned.Text = "Designed and Developed by: " + Preferences.DesignedDeveloped;
            txtInfo.Text = Preferences.EnglishLanguage ? 
                "It authorizes to the previously mentioned company to use of this application, further the installation in different work stations that it needs, respecting ever the same conditions to guaranty and technical assistance." :
                "Se autoriza a la empresa antes mencionada en el uso de esta aplicación, así como la instalación de este mismo producto en los diferentes equipos que ella desee, respetando siempre las mismas condiciones para validez de garantía y asistencia técnica.";

            ObjForm_004.ActivateOrDeactivateComponents("About...");

            ObjForm_004.Cursor = Cursors.Default;

        }

        private void Form_004_004_FormClosed(object sender, FormClosedEventArgs e)
        {
            ObjForm_004.ActivateOrDeactivateComponents(null);
        }

        private void Form_004_004_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
                Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}