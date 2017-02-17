using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using General_Desktop_Application.Enumerables;

namespace General_Desktop_Application.Classes
{
    class Tools
    {
        public static string GetMessageBox(int iErrorMessage)
        {
            switch (iErrorMessage)
            {
                default: return "Unknown application error, you try restart it.";
            }
        }
    }
}