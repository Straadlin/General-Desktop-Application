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
    public partial class Form_001 : Form
    {
        // Objects
        Form_002 oForm_002;

        public Form_001()
        {
            InitializeComponent();

            Text = lblTitle.Text = Preferences.TitleSoftware;
            lblVersion.Text = "Versión " + Preferences.CurrentVersion;
        }

        private void Form_001_Load(object sender, EventArgs e)
        {

        }

        private void timTimer_Tick(object sender, EventArgs e)
        {
            timTimer.Enabled = false;

            oForm_002 = new Form_002(this);
            oForm_002.Show();
            Hide();
        }
    }
}