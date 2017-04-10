using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using General_Desktop_Application.Classes;
using General_Desktop_Application.DataLayer;
using General_Desktop_Application.EF;

namespace General_Desktop_Application.BusinessLayer
{
    class BCountry
    {
        public static country FindByCode(string stCode)
        {
            try
            {
                using (ModelContext objContext = new ModelContext())
                {
                    return objContext.countries.Where(c => c.coun_code__nvarchar == stCode).FirstOrDefault();
                }
            }
            catch { }

            return null;
        }
    }
}