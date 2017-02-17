using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using General_Desktop_Application.Models;
using General_Desktop_Application.Data;

namespace General_Desktop_Application.Business
{
    static class B_City
    {
        public static bool AddCity(M_City objM_City, string stConnectionString)
        {
            if (objM_City != null)
                return D_City.Insert(objM_City, stConnectionString);
            else
                return false;
        }
    }
}