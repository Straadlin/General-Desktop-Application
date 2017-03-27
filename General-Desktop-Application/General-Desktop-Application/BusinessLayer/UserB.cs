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
        public static int GetCountAllUsers()
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

        public static proc_user_select_Result FindAndValidateByUserNameOrEmailOrCellphone(string stUserNameEmailCellphone, string stPassword)
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    var vGuid = objContext.proc_systemserver_verifyloginuser(stUserNameEmailCellphone, stPassword).FirstOrDefault().Value;


                    if (vGuid != null)
                    {
                        return objContext.proc_user_select(vGuid).FirstOrDefault();
                    }
                }
            }
            catch { }

            return null;
        }

        public static user FindByUserNameOrEmailOrCellphone(string stUserNameEmailCellphone)
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    return objContext.users.Where(u => u.user_username__varchar == stUserNameEmailCellphone || u.user_email__varchar == stUserNameEmailCellphone || u.user_cellphone__varchar == stUserNameEmailCellphone && u.user_uuid_root__uniqueidentifier == null && u.sess_uuid_deleted__uniqueidentifier == null).FirstOrDefault();
                }
            }
            catch { }

            return null;
        }

        public static user FindByUUID(Guid objUUID)
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    return objContext.users.Where(u => u.user_uuid__uniqueidentifier == objUUID).FirstOrDefault();
                }
            }
            catch { }

            return null;
        }

        public static proc_user_select_Result FindByUUIDDecrypted(Guid objUUID)
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    return objContext.proc_user_select(objUUID).FirstOrDefault();
                }
            }
            catch { }

            return null;
        }

        public static List<user> GetAllUsers()
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    return objContext.users.Where(u => u.user_uuid_root__uniqueidentifier == null && u.sess_uuid_deleted__uniqueidentifier == null).ToList();
                }
            }
            catch { }

            return null;
        }

        public static List<proc_user_selectAll_Result> GetAllUsersDecrypted()
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    return objContext.proc_user_selectAll().ToList();
                }
            }
            catch { }

            return null;
        }
    }
}