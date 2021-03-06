﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity.Core.Objects;

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
                using (ModelContext objContext = new ModelContext())
                {
                    return objContext.users.Count();
                }
            }
            catch
            {
                return -1;
            }
        }

        public static proc_user_findAllBranches_Result GetLastBranch(Guid objUUID)
        {
            try
            {
                using (ModelContext objContext = new ModelContext())
                {
                    return objContext.proc_user_findAllBranches(objUUID).ToList()[1];
                }
            }
            catch
            {
                return null;
            }
        }

        public static List<proc_user_findAllBranches_Result> GetAllBranches(Guid objUUID)
        {
            try
            {
                using (ModelContext objContext = new ModelContext())
                {
                    return objContext.proc_user_findAllBranches(objUUID).ToList();
                }
            }
            catch
            {
                return null;
            }
        }

        public static user FindAndValidateByUserNameOrEmailOrCellphone(string stUserNameEmailCellphone, string stPassword)
        {
            try
            {
                using (ModelContext objContext = new ModelContext())
                {
                    stPassword = Tools.GetDefaulHash(stPassword);

                    return objContext.users.Where(u => (u.user_username__nvarchar == stUserNameEmailCellphone || u.user_email__nvarchar == stUserNameEmailCellphone || u.user_cellphone__nvarchar == stUserNameEmailCellphone) && u.user_password__nvarchar == stPassword && u.user_uuid_root__uniqueidentifier == null && u.sess_uuid_deleted__uniqueidentifier == null).FirstOrDefault();
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
                    using (ModelContext objContext = new ModelContext())
                    {
                        if (!string.IsNullOrEmpty(stUserName) && objContext.users.Where(u => u.user_username__nvarchar == stUserName && u.user_uuid_root__uniqueidentifier == null && u.sess_uuid_deleted__uniqueidentifier == null).FirstOrDefault() != null)
                            return 1;
                        if (!string.IsNullOrEmpty(stEmail) && objContext.users.Where(u => u.user_email__nvarchar == stEmail && u.user_uuid_root__uniqueidentifier == null && u.sess_uuid_deleted__uniqueidentifier == null).FirstOrDefault() != null)
                            return 2;
                        if (!string.IsNullOrEmpty(stCellphone) && objContext.users.Where(u => u.user_cellphone__nvarchar == stCellphone && u.user_uuid_root__uniqueidentifier == null && u.sess_uuid_deleted__uniqueidentifier == null).FirstOrDefault() != null)
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
                    using (ModelContext objContext = new ModelContext())
                    {
                        if (!string.IsNullOrEmpty(stUserName) && objContext.users.Where(u => u.user_username__nvarchar == stUserName && u.user_uuid__uniqueidentifier != objUserSelected.user_uuid__uniqueidentifier && u.user_uuid_root__uniqueidentifier == null && u.sess_uuid_deleted__uniqueidentifier == null).FirstOrDefault() != null)
                            return 1;
                        if (!string.IsNullOrEmpty(stEmail) && objContext.users.Where(u => u.user_email__nvarchar == stEmail && u.user_uuid__uniqueidentifier != objUserSelected.user_uuid__uniqueidentifier && u.user_uuid_root__uniqueidentifier == null && u.sess_uuid_deleted__uniqueidentifier == null).FirstOrDefault() != null)
                            return 2;
                        if (!string.IsNullOrEmpty(stCellphone) && objContext.users.Where(u => u.user_cellphone__nvarchar == stCellphone && u.user_uuid__uniqueidentifier != objUserSelected.user_uuid__uniqueidentifier && u.user_uuid_root__uniqueidentifier == null && u.sess_uuid_deleted__uniqueidentifier == null).FirstOrDefault() != null)
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
                using (ModelContext objContext = new ModelContext())
                {
                    return objContext.users.Where(u => (u.user_username__nvarchar == stUserNameEmailCellphone || u.user_email__nvarchar == stUserNameEmailCellphone || u.user_cellphone__nvarchar == stUserNameEmailCellphone) && u.user_uuid_root__uniqueidentifier == null && u.sess_uuid_deleted__uniqueidentifier == null).FirstOrDefault();
                }
            }
            catch { }

            return null;
        }

        public static user FindByUUID(Guid objUUID)
        {
            try
            {
                using (ModelContext objContext = new ModelContext())
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
                using (ModelContext objContext = new ModelContext())
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
                using (ModelContext objContext = new ModelContext())
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
                using (ModelContext objContext = new ModelContext())
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
                using (ModelContext objContext = new ModelContext())
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
                        user_username__nvarchar = stUsername,
                        user_email__nvarchar = stEmail,
                        user_cellphone__nvarchar = stCellphone,
                        user_password__nvarchar = Tools.GetDefaulHash(stPassword),
                        user_firstname__nvarchar = Tools.Encrypt(stFirstname),
                        user_lastname__nvarchar = Tools.Encrypt(stLastname),
                        user_roleaccess__tinyint = byRoleAccess,
                        user_extradata__nvarchar = stExtradata,
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
                using (ModelContext objContext = new ModelContext())
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
                        user_username__nvarchar = vUserPrincipal.user_username__nvarchar,
                        user_email__nvarchar = vUserPrincipal.user_email__nvarchar,
                        user_cellphone__nvarchar = vUserPrincipal.user_cellphone__nvarchar,
                        user_password__nvarchar = vUserPrincipal.user_password__nvarchar,
                        user_firstname__nvarchar = vUserPrincipal.user_firstname__nvarchar,
                        user_lastname__nvarchar = vUserPrincipal.user_lastname__nvarchar,
                        user_roleaccess__tinyint = vUserPrincipal.user_roleaccess__tinyint,
                        user_extradata__nvarchar = vUserPrincipal.user_extradata__nvarchar,
                        reso_uuid_picture__uniqueidentifier = vUserPrincipal.reso_uuid_picture__uniqueidentifier,
                        date_uuid_birthdate__uniqueidentifier = vUserPrincipal.date_uuid_birthdate__uniqueidentifier,
                        city_uuid__uniqueidentifier = vUserPrincipal.city_uuid__uniqueidentifier,
                        sess_uuid_used__uniqueidentifier = null,
                        sess_uuid_created__uniqueidentifier = vUserPrincipal.sess_uuid_created__uniqueidentifier,
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

                    vUserPrincipal.user_username__nvarchar = stUsername;
                    vUserPrincipal.user_email__nvarchar = stEmail;
                    vUserPrincipal.user_cellphone__nvarchar = stCellphone;
                    vUserPrincipal.user_password__nvarchar = objUserAux.user_password__nvarchar != stPassword ? Tools.GetDefaulHash(stPassword) : objUserAux.user_password__nvarchar;
                    vUserPrincipal.user_firstname__nvarchar = Tools.Encrypt(stFirstname);
                    vUserPrincipal.user_lastname__nvarchar = Tools.Encrypt(stLastname);
                    vUserPrincipal.user_roleaccess__tinyint = byRoleAccess;
                    vUserPrincipal.user_extradata__nvarchar = stExtradata;
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
                using (ModelContext objContext = new ModelContext())
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