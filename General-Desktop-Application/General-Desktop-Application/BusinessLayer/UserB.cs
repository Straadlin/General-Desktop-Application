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
    class UserB
    {
        public static int SelectCountAllUsers()
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    return objContext.users.Count();
                }
            }
            catch
            {
                return -1;
            }
        }

        public static user FindByUserNameOrEmailOrCellphone(string stUserNameEmailCellphone, string stPassword)
        {
            user objUser = null;

            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    objUser = objContext.users.Where(u => (u.user_username__nvarchar == stUserNameEmailCellphone || u.user_email__nvarchar == stUserNameEmailCellphone || u.user_cellphone__nvarchar == stUserNameEmailCellphone) && u.user_password__nvarchar == stPassword).FirstOrDefault();
                }
            }
            catch { }

            return objUser;
        }
    }
}
