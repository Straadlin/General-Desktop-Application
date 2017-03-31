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
    public static class BUser
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

        public static user FindAndValidateByUserNameOrEmailOrCellphone(string stUserNameEmailCellphone, string stPassword)
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    stPassword = Tools.GetDefaulHash(stPassword);

                    return objContext.users.Where(u => (u.user_username__varchar == stUserNameEmailCellphone || u.user_email__varchar == stUserNameEmailCellphone || u.user_cellphone__varchar == stUserNameEmailCellphone) && u.user_password__varchar == stPassword && u.user_uuid_root__uniqueidentifier == null && u.sess_uuid_deleted__uniqueidentifier == null).FirstOrDefault();
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

        public static sbyte FindByUserNameOrEmailOrCellphoneWithExcludedUser(string stUserName, string stEmail, string stCellphone, user objUserSelected)
        {
            try
            {
                if (!string.IsNullOrEmpty(stUserName) || !string.IsNullOrEmpty(stEmail) || !string.IsNullOrEmpty(stCellphone))
                {
                    using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                    {
                        if (!string.IsNullOrEmpty(stUserName) && objContext.users.Where(u => u.user_username__varchar == stUserName && u.user_uuid__uniqueidentifier != objUserSelected.user_uuid__uniqueidentifier && u.user_uuid_root__uniqueidentifier == null && u.sess_uuid_deleted__uniqueidentifier == null).FirstOrDefault() != null)
                            return 1;
                        if (!string.IsNullOrEmpty(stEmail) && objContext.users.Where(u => u.user_email__varchar == stEmail && u.user_uuid__uniqueidentifier != objUserSelected.user_uuid__uniqueidentifier && u.user_uuid_root__uniqueidentifier == null && u.sess_uuid_deleted__uniqueidentifier == null).FirstOrDefault() != null)
                            return 2;
                        if (!string.IsNullOrEmpty(stCellphone) && objContext.users.Where(u => u.user_cellphone__varchar == stCellphone && u.user_uuid__uniqueidentifier != objUserSelected.user_uuid__uniqueidentifier && u.user_uuid_root__uniqueidentifier == null && u.sess_uuid_deleted__uniqueidentifier == null).FirstOrDefault() != null)
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
                    return objContext.users.Where(u => (u.user_username__varchar == stUserNameEmailCellphone || u.user_email__varchar == stUserNameEmailCellphone || u.user_cellphone__varchar == stUserNameEmailCellphone) && u.user_uuid_root__uniqueidentifier == null && u.sess_uuid_deleted__uniqueidentifier == null).FirstOrDefault();
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
                        objUser.sess_uuid_used__uniqueidentifier = null;// This line is very important to work well the trigger
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
                    if (objDateBirthday.Year != DateTime.Now.Year)
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
                    Guid objGuidUser = Guid.NewGuid();
                    do
                    {
                        objGuidUser = Guid.NewGuid();
                    } while (FindByUUID(objGuidUser) != null);

                    objContext.users.Add(new user()
                    {
                        user_uuid__uniqueidentifier = objGuidUser,
                        user_username__varchar = stUsername,
                        user_email__varchar = stEmail,
                        user_cellphone__varchar = stCellphone,
                        user_password__varchar = Tools.GetDefaulHash(stPassword),
                        user_firstname__varchar = Tools.Encrypt(stFirstname),
                        user_lastname__varchar = Tools.Encrypt(stLastname),
                        user_roleaccess__tinyint = byRoleAccess,
                        user_extradata__varchar = stExtradata,
                        reso_uuid_picture__uniqueidentifier = objGuidResource,
                        date_uuid_birthdate__uniqueidentifier = objGuidDate,
                        city_uuid__uniqueidentifier = objGuidCity,
                        sess_uuid_used__uniqueidentifier = null,
                        sess_uuid_created__uniqueidentifier = objSessionCreator.sess_uuid__uniqueidentifier,
                        user_uuid_root__uniqueidentifier = null,
                        sess_uuid_deleted__uniqueidentifier = null
                    });

                    objContext.SaveChanges();

                    return objContext.users.Where(u => u.user_uuid__uniqueidentifier == objGuidUser).FirstOrDefault();
                }
            }
            catch { }

            return null;
        }

        public static bool Edit(
            user objUser,
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
            session objSession)
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    var vUserPrincipal = objContext.users.Where(u => u.user_uuid__uniqueidentifier == objUser.user_uuid__uniqueidentifier).FirstOrDefault();

                    var vLastBranch = objContext.proc_user_findAllBranches(objUser.user_uuid__uniqueidentifier).LastOrDefault();

                    // Add User
                    Guid objGuidUserAux = Guid.NewGuid();
                    do
                    {
                        objGuidUserAux = Guid.NewGuid();
                    } while (FindByUUID(objGuidUserAux) != null);

                    user objUserAux = new user()
                    {
                        user_uuid__uniqueidentifier = objGuidUserAux,
                        user_username__varchar = vUserPrincipal.user_username__varchar,
                        user_email__varchar = vUserPrincipal.user_email__varchar,
                        user_cellphone__varchar = vUserPrincipal.user_cellphone__varchar,
                        user_password__varchar = vUserPrincipal.user_password__varchar,
                        user_firstname__varchar = vUserPrincipal.user_firstname__varchar,
                        user_lastname__varchar = vUserPrincipal.user_lastname__varchar,
                        user_roleaccess__tinyint = vUserPrincipal.user_roleaccess__tinyint,
                        user_extradata__varchar = vUserPrincipal.user_extradata__varchar,
                        reso_uuid_picture__uniqueidentifier = vUserPrincipal.reso_uuid_picture__uniqueidentifier,
                        date_uuid_birthdate__uniqueidentifier = vUserPrincipal.date_uuid_birthdate__uniqueidentifier,
                        city_uuid__uniqueidentifier = vUserPrincipal.city_uuid__uniqueidentifier,
                        sess_uuid_used__uniqueidentifier = null,
                        sess_uuid_created__uniqueidentifier = objSession.sess_uuid__uniqueidentifier,
                        user_uuid_root__uniqueidentifier = vLastBranch.user_uuid__uniqueidentifier,
                        sess_uuid_deleted__uniqueidentifier = null
                    };
                    objContext.users.Add(objUserAux);

                    // Add Date
                    Guid? objGuidDate = null;
                    if (objDateBirthday.Year != DateTime.Now.Year)
                        objGuidDate = BDate.FindOrAddDate(objDateBirthday).date_uuid__uniqueidentifier;

                    // Add City
                    Guid? objGuidCity = null;
                    if (!string.IsNullOrEmpty(stCity))
                        objGuidCity = BCity.FindByName(stCity, BState.FindByName(stState, BCountry.FindByCode("MX"))).city_uuid__uniqueidentifier;

                    // Add Resource
                    Guid? objGuidResource = null;
                    if (byaPicture != null && !string.IsNullOrEmpty(stNamePicture))
                        objGuidResource = BResource.Add(stNamePicture, 1, null, byaPicture, null).reso_uuid__uniqueidentifier;

                    vUserPrincipal.user_username__varchar = stUsername;
                    vUserPrincipal.user_email__varchar = stEmail;
                    vUserPrincipal.user_cellphone__varchar = stCellphone;
                    vUserPrincipal.user_password__varchar = Tools.GetDefaulHash(stPassword);
                    vUserPrincipal.user_firstname__varchar = Tools.Encrypt(stFirstname);
                    vUserPrincipal.user_lastname__varchar = Tools.Encrypt(stLastname);
                    vUserPrincipal.user_roleaccess__tinyint = byRoleAccess;
                    vUserPrincipal.user_extradata__varchar = stExtradata;
                    vUserPrincipal.reso_uuid_picture__uniqueidentifier = objGuidResource;
                    vUserPrincipal.date_uuid_birthdate__uniqueidentifier = objGuidDate;
                    vUserPrincipal.city_uuid__uniqueidentifier = objGuidCity;
                    vUserPrincipal.sess_uuid_used__uniqueidentifier = null;
                    vUserPrincipal.sess_uuid_created__uniqueidentifier = objSession.sess_uuid__uniqueidentifier;
                    vUserPrincipal.user_uuid_root__uniqueidentifier = null;
                    vUserPrincipal.sess_uuid_deleted__uniqueidentifier = null;

                    objContext.SaveChanges();

                    return true;
                }
            }
            catch { }

            return false;
        }

        public static bool Remove(user objUser, session objSession)
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    var vUser = objContext.users.Where(u => u.user_uuid__uniqueidentifier == objUser.user_uuid__uniqueidentifier && u.sess_uuid_used__uniqueidentifier == objSession.sess_uuid__uniqueidentifier).FirstOrDefault();

                    if (vUser != null)
                    {
                        vUser.sess_uuid_used__uniqueidentifier = null;
                        vUser.sess_uuid_deleted__uniqueidentifier = objSession.sess_uuid__uniqueidentifier;

                        objContext.SaveChanges();

                        return true;
                    }
                }
            }
            catch { }

            return false;
        }
    }
}