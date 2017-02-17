using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using General_Desktop_Application.Models;
using General_Desktop_Application.Data;

namespace General_Desktop_Application.Business
{
    static class B_User
    {
        public static bool AddUser(M_User objM_User, string stConnectionString)
        {
            if (objM_User != null)
                return D_User.Insert(objM_User, stConnectionString);
            else
                return false;
        }
    }
}