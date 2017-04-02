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
    public static class BPrincipalCompany
    {
        public static principalcompany Get()
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    return objContext.principalcompanies.Where(p => (p.prco_uuid_root__uniqueidentifier == null)).FirstOrDefault();
                }
            }
            catch { }

            return null;
        }

        public static principalcompany FindByUUID(Guid objUUID)
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    return objContext.principalcompanies.Where(p => p.prco_uuid__uniqueidentifier == objUUID).FirstOrDefault();
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
                    principalcompany objPrincipalCompany = objContext.principalcompanies.Where(p => p.prco_uuid__uniqueidentifier == objGuid).FirstOrDefault();

                    if (objPrincipalCompany != null)
                    {
                        objPrincipalCompany.sess_uuid_used__uniqueidentifier = objSession.sess_uuid__uniqueidentifier;
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
                    principalcompany objPrincipalCompany = objContext.principalcompanies.Where(p => p.prco_uuid__uniqueidentifier == objGuid).FirstOrDefault();

                    if (objPrincipalCompany != null)
                    {
                        objPrincipalCompany.sess_uuid_used__uniqueidentifier = null;
                        objContext.SaveChanges();

                        return true;
                    }
                }
            }
            catch { }

            return false;
        }

        public static bool Edit(
            principalcompany objPrincipalCompany,
            string stRfc,
            string stName,
            string stAddress,
            string stPhone,
            string stEmail,
            string stFacebookUrl,
            bool boDevelopmentMode,
            int? inTimeBetweenBackups,
            string stNamePicture,
            byte[] byaPicture,
            string stState,
            string stCity,
            session objSession)
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    var vPreferencePrincipal = objContext.principalcompanies.Where(p => p.prco_uuid__uniqueidentifier == objPrincipalCompany.prco_uuid__uniqueidentifier).FirstOrDefault();

                    var vLastBranch = objContext.proc_principalcompany_findAllBranches(objPrincipalCompany.prco_uuid__uniqueidentifier).LastOrDefault();

                    // Add Preference
                    Guid objGuidPreferenceAux = Guid.NewGuid();
                    do
                    {
                        objGuidPreferenceAux = Guid.NewGuid();
                    } while (FindByUUID(objGuidPreferenceAux) != null);

                    principalcompany objPrincipalCompanyAux = new principalcompany()
                    {
                        prco_uuid__uniqueidentifier = objGuidPreferenceAux,
                        prco_rfc__nvarchar = vPreferencePrincipal.prco_rfc__nvarchar,
                        prco_name__nvarchar = vPreferencePrincipal.prco_name__nvarchar,
                        prco_address__nvarchar = vPreferencePrincipal.prco_address__nvarchar,
                        prco_phone__nvarchar = vPreferencePrincipal.prco_phone__nvarchar,
                        prco_email__nvarchar = vPreferencePrincipal.prco_email__nvarchar,
                        prco_facebook__nvarchar = vPreferencePrincipal.prco_facebook__nvarchar,
                        prco_developmentmode__bit = vPreferencePrincipal.prco_developmentmode__bit,
                        prco_timebetweenbackups__int = vPreferencePrincipal.prco_timebetweenbackups__int,
                        reso_uuid_logo__uniqueidentifier = vPreferencePrincipal.reso_uuid_logo__uniqueidentifier,
                        city_uuid__uniqueidentifier = vPreferencePrincipal.city_uuid__uniqueidentifier,
                        sess_uuid_used__uniqueidentifier = null,
                        sess_uuid_created__uniqueidentifier = vPreferencePrincipal.sess_uuid_created__uniqueidentifier,
                        prco_uuid_root__uniqueidentifier = vLastBranch.prco_uuid__uniqueidentifier
                    };
                    objContext.principalcompanies.Add(objPrincipalCompanyAux);

                    // Add City
                    Guid? objGuidCity = null;
                    if (!string.IsNullOrEmpty(stCity))
                        objGuidCity = BCity.FindByName(stCity, BState.FindByName(stState, BCountry.FindByCode("MX"))).city_uuid__uniqueidentifier;

                    // Add Resource
                    Guid? objGuidResource = null;
                    if (byaPicture != null && !string.IsNullOrEmpty(stNamePicture))
                        objGuidResource = BResource.Add(stNamePicture, 1, null, byaPicture, null).reso_uuid__uniqueidentifier;

                    vPreferencePrincipal.prco_rfc__nvarchar = stRfc;
                    vPreferencePrincipal.prco_name__nvarchar = stName;
                    vPreferencePrincipal.prco_address__nvarchar = stAddress;
                    vPreferencePrincipal.prco_phone__nvarchar = stPhone;
                    vPreferencePrincipal.prco_email__nvarchar = stEmail;
                    vPreferencePrincipal.prco_facebook__nvarchar = stFacebookUrl;
                    vPreferencePrincipal.prco_developmentmode__bit = boDevelopmentMode;
                    vPreferencePrincipal.prco_timebetweenbackups__int = inTimeBetweenBackups > 0 ? inTimeBetweenBackups: null;
                    vPreferencePrincipal.reso_uuid_logo__uniqueidentifier = objGuidResource;
                    vPreferencePrincipal.city_uuid__uniqueidentifier = objGuidCity;
                    vPreferencePrincipal.sess_uuid_used__uniqueidentifier = null;
                    vPreferencePrincipal.sess_uuid_created__uniqueidentifier = objSession.sess_uuid__uniqueidentifier;
                    vPreferencePrincipal.prco_uuid_root__uniqueidentifier = null;

                    objContext.SaveChanges();

                    return true;
                }
            }
            catch { }

            return false;
        }
    }
}