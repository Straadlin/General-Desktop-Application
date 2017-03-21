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
    class DateB
    {
        public static date FindOrAddDate(DateTime objDateTime)
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    date objDate = objContext.dates.Where(d => d.date_value__date == objDateTime.Date).FirstOrDefault();

                    if (objDate == null)
                    {
                        Guid objGuid;
                        do
                        {
                            objGuid = Guid.NewGuid();
                        } while (objContext.dates.Where(d => d.date_uuid__uniqueidentifier == objGuid).Count() > 0);

                        objContext.dates.Add(new date() { date_uuid__uniqueidentifier = objGuid, date_value__date = objDateTime });
                        objContext.SaveChanges();

                        return objContext.dates.Where(d => d.date_uuid__uniqueidentifier == objGuid).FirstOrDefault();
                    }
                    else
                        return objDate;
                }
            }
            catch { }

            return null;
        }

        public static DateTime GetServersDateAndTime()// We need to remake it and too this must be using a function
        {
            return DateTime.Now;
        }
    }
}