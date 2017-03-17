using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using General_Desktop_Application.EF;

namespace General_Desktop_Application.Presentation
{
    public partial class Form_004 : Form
    {
        // Objects
        Form_002 objForm_002;
        user objUser;
        session objSession;

        public Form_004(Form_002 objForm_002, user objUser, session objSession)
        {
            this.objForm_002 = objForm_002;
            this.objUser = objUser;
            this.objSession = objSession;

            InitializeComponent();
        }
    }
}