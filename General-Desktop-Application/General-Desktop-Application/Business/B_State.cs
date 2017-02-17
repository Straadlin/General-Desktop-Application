using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using General_Desktop_Application.Models;
using General_Desktop_Application.Data;

namespace General_Desktop_Application.Business
{
    static class B_State
    {
        public static bool AddState(M_State objM_State, string stConnectionString)
        {
            if (objM_State != null)
                return D_State.Insert(objM_State, stConnectionString);
            else
                return false;
        }
    }
}