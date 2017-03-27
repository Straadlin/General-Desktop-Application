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
    class CityB
    {
        public static List<city> GetAll()
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    return objContext.cities.ToList();
                }
            }
            catch
            {
                return null;
            }
        }

        public static List<city> GetCities(state objState)
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    return objContext.cities.Where(c => c.stat_uuid__uniqueidentifier == objState.stat_uuid__uniqueidentifier).ToList();
                }
            }
            catch { }

            return null;
        }
    }
}