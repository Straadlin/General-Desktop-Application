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
    class BResource
    {
        public static resource FindByUUID(Guid objUUID)
        {
            try
            {
                using (ModelContext objContext = new ModelContext())
                {
                    return objContext.resources.Where(r => r.reso_uuid__uniqueidentifier == objUUID).FirstOrDefault();
                }
            }
            catch { }

            return null;
        }

        public static resource FindByName(string stNameFile)
        {
            try
            {
                using (ModelContext objContext = new ModelContext())
                {
                    return objContext.resources.Where(r => r.reso_name__nvarchar == stNameFile).FirstOrDefault();
                }
            }
            catch { }

            return null;
        }

        public static resource Add(string stName, byte byExtension, string stDescription, byte[] byaValue, string stExternalUrlName)
        {
            try
            {
                using (ModelContext objContext = new ModelContext())
                {
                    Guid objGuid;
                    do
                    {
                        objGuid = Guid.NewGuid();
                    } while (FindByUUID(objGuid) != null);

                    resource objResource;

                    objResource = new resource()
                    {
                        reso_uuid__uniqueidentifier = objGuid,
                        reso_name__nvarchar = stName,
                        reso_extension__tinyint = byExtension,
                        reso_description__nvarchar = stDescription,
                        reso_value__varbinary = byaValue,
                        reso_externalurlorname__nvarchar = stExternalUrlName,
                    };

                    objContext.resources.Add(objResource);

                    objContext.SaveChanges();

                    return objContext.resources.Where(r => r.reso_uuid__uniqueidentifier == objResource.reso_uuid__uniqueidentifier).FirstOrDefault();
                }
            }
            catch { }

            return null;
        }
    }
}