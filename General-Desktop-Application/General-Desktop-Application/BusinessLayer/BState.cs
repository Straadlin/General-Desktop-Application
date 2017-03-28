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
    class BState
    {
        public static state FindByName(string stName)
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    return objContext.states.Where(s=> s.stat_name__varchar == stName).FirstOrDefault();
                }
            }
            catch { }

            return null;
        }

        public static List<state> GetMexicosStates()
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    return (from country in objContext.countries
                            join state in objContext.states on country.coun_uuid__uniqueidentifier equals state.coun_uuid__uniqueidentifier
                            where (country.coun_code__varchar == "MX")
                            select state).ToList();
                }
            }
            catch { }

            return null;
        }
    }
}