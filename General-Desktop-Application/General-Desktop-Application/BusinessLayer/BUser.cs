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
    class BUser
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

        public static bool DisableToEdit(Guid objGuid, session objSession)
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    user objUser = objContext.users.Where(u => u.user_uuid__uniqueidentifier == objGuid).FirstOrDefault();

                    if (objUser != null)
                    {
                        objUser.sess_uuid_used__uniqueidentifier = objSession.sess_uuid__uniqueidentifier;
                        objContext.SaveChanges();
                    }
                }
            }
            catch { }

            return false;
        }

        public static bool EnableToEdit(Guid objGuid)
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    user objUser = objContext.users.Where(u => u.user_uuid__uniqueidentifier == objGuid).FirstOrDefault();

                    if (objUser != null)
                    {
                        objUser.sess_uuid_used__uniqueidentifier = null;
                        objContext.SaveChanges();

                        return true;
                    }
                }
            }
            catch { }

            return false;
        }

        public static user Add(
            string stUsername,
            string stEmail,
            string stCellphone,
            string stPassword,
            string stFirstname,
            string stLastname,
            byte byRoleAccess,
            string stExtradata,
            byte[] byaPicture,
            DateTime objDate,
            string stCity,
            string stState,
            session objSessionCreator)
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    Guid objGuid;
                    do
                    {
                        objGuid = Guid.NewGuid();
                    } while (objContext.sessions.Where(s => s.sess_uuid__uniqueidentifier == objGuid).Count() > 0);

                    //objContext.proc_user_insert(objGuid,stUsername,stEmail,stCellphone,stPassword,)

                    //user objUser = new user()
                    //{
                    //    user_uuid__uniqueidentifier = objGuid,
                    //    user_username__varchar = stUsername,
                    //    user_email__varchar = stEmail,
                    //    user_cellphone__varchar = stCellphone,
                    //    user_password__varbinary,
                    //    user_firstname__varbinary,
                    //    user_lastname__varbinary,
                    //    user_roleaccess__tinyint,
                    //    user_extradata__varchar,
                    //    reso_uuid_picture__uniqueidentifier,
                    //    date_uuid_birthdate__uniqueidentifier,
                    //    city_uuid__uniqueidentifier,
                    //    sess_uuid_used__uniqueidentifier = null,
                    //    sess_uuid_created__uniqueidentifier,
                    //    user_uuid_root__uniqueidentifier = null,
                    //    sess_uuid_deleted__uniqueidentifier = null
                    //};
                }
            }
            catch { }

            return null;
        }
    }
}