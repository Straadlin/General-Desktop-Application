using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using General_Desktop_Application.Models;
using General_Desktop_Application.Data;

namespace General_Desktop_Application.Business
{
    static class B_Session
    {
        public static bool AddSession(M_Session objM_Session, string stConnectionString)
        {
            if (objM_Session != null)
                return D_Session.Insert(objM_Session, stConnectionString);
            else
                return false;
        }
    }
}