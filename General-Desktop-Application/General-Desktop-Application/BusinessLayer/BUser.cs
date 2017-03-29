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

        public static sbyte FindByUserNameOrEmailOrCellphone(string stUserName, string stEmail, string stCellphone)
        {
            try
            {
                if (!string.IsNullOrEmpty(stUserName) || !string.IsNullOrEmpty(stEmail) || !string.IsNullOrEmpty(stCellphone))
                {
                    using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                    {
                        if (!string.IsNullOrEmpty(stUserName) && objContext.users.Where(u => u.user_username__varchar == stUserName && u.user_uuid_root__uniqueidentifier == null && u.sess_uuid_deleted__uniqueidentifier == null).FirstOrDefault() != null)
                            return 1;
                        if (!string.IsNullOrEmpty(stEmail) && objContext.users.Where(u => u.user_email__varchar == stEmail && u.user_uuid_root__uniqueidentifier == null && u.sess_uuid_deleted__uniqueidentifier == null).FirstOrDefault() != null)
                            return 2;
                        if (!string.IsNullOrEmpty(stCellphone) && objContext.users.Where(u => u.user_cellphone__varchar == stCellphone && u.user_uuid_root__uniqueidentifier == null && u.sess_uuid_deleted__uniqueidentifier == null).FirstOrDefault() != null)
                            return 3;
                    }
                }
                else
                    return -1;
            }
            catch { }

            return 0;
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
            string stNamePicture,
            byte[] byaPicture,
            DateTime objDateBirthday,
            string stState,
            string stCity,
            session objSessionCreator)
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    // Add Date
                    Guid? objGuidDate = null;
                    if (!(objDateBirthday.Year == 2000 && objDateBirthday.Month == 1 && objDateBirthday.Day == 1))
                        objGuidDate = BDate.FindOrAddDate(objDateBirthday).date_uuid__uniqueidentifier;

                    // Add City
                    Guid? objGuidCity = null;
                    if (!string.IsNullOrEmpty(stCity))
                        objGuidCity = BCity.FindByName(stCity, BState.FindByName(stState, BCountry.FindByCode("MX"))).city_uuid__uniqueidentifier;

                    // Add Resource
                    Guid? objGuidResource = null;
                    if (byaPicture != null && !string.IsNullOrEmpty(stNamePicture))
                        objGuidResource = BResource.Add(stNamePicture, 1, null, byaPicture, null).reso_uuid__uniqueidentifier;
                    
                    // Add User
                    Guid objGuidUser = Guid.Empty;
                    do
                    {
                        objGuidUser = Guid.NewGuid();
                    } while (FindByUUID(objGuidUser) != null);

                    objContext.proc_user_insert(
                        objGuidUser,
                        stUsername,
                        stEmail,
                        stCellphone,
                        stPassword,
                        stFirstname,
                        stLastname,
                        byRoleAccess,
                        stExtradata,
                        objGuidResource,
                        objGuidDate,
                        objGuidCity,
                        null,
                        objSessionCreator.sess_uuid__uniqueidentifier,
                        null,
                        null
                        );

                    objContext.SaveChanges();

                    return objContext.users.Where(u => u.user_uuid__uniqueidentifier == objGuidUser).FirstOrDefault();
                }
            }
            catch { }

            return null;
        }
    }
}