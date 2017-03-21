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
    class SessionB
    {
        public static session CreateSession(user objUser)
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

                    date objDate = DateB.FindOrAddDate(DateTime.Now);

                    session objSession;

                    objSession = new session()
                    {
                        sess_uuid__uniqueidentifier = Guid.NewGuid(),
                        sess_starttime__time = TimeSpan.Parse(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second),
                        sess_lastactivity__time = TimeSpan.Parse(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second),
                        sess_ipbatch01__tinyint = null,
                        sess_ipbatch02__tinyint = null,
                        sess_ipbatch03__tinyint = null,
                        sess_ipbatch04__tinyint = null,
                        sess_extradata__nvarchar = null,
                        date_uuid__uniqueidentifier = objDate.date_uuid__uniqueidentifier,
                        user_uuid_created__uniqueidentifier = objUser.user_uuid__uniqueidentifier
                    };

                    objContext.sessions.Add(objSession);

                    objContext.SaveChanges();

                    return objContext.sessions.Where(s => s.sess_uuid__uniqueidentifier == objSession.sess_uuid__uniqueidentifier).FirstOrDefault();
                }
            }
            catch { }

            return null;
        }

        public static session FindSeassion(Guid objGuid)
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    return objContext.sessions.Where(s => s.sess_uuid__uniqueidentifier == objGuid).FirstOrDefault();
                }
            }
            catch { }

            return null;
        }

        public static bool ChangeIpSession(session objSessionExternall, byte byIpBatch01, byte byIpBatch02, byte byIpBatch03, byte byIpBatch04)
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    session objSession = objContext.sessions.Where(s => s.sess_uuid__uniqueidentifier == objSessionExternall.sess_uuid__uniqueidentifier).FirstOrDefault();
                    DateTime objDateTime = DateB.GetServersDateAndTime();
                    objSession.sess_lastactivity__time = new TimeSpan(objDateTime.Hour, objDateTime.Minute, objDateTime.Second);
                    objSession.sess_ipbatch01__tinyint = byIpBatch01;
                    objSession.sess_ipbatch02__tinyint = byIpBatch02;
                    objSession.sess_ipbatch03__tinyint = byIpBatch03;
                    objSession.sess_ipbatch04__tinyint = byIpBatch04;
                    objContext.SaveChanges();

                    return true;
                }
            }
            catch { }

            return false;
        }

        public static bool UpdateLastTimeSession(session objSessionExtternall)
        {
            try
            {
                using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                {
                    session objSession = objContext.sessions.Where(s => s.sess_uuid__uniqueidentifier == objSessionExtternall.sess_uuid__uniqueidentifier).FirstOrDefault();
                    DateTime objDateTime = DateB.GetServersDateAndTime();
                    objSession.sess_lastactivity__time = new TimeSpan(objDateTime.Hour, objDateTime.Minute, objDateTime.Second);
                    objContext.SaveChanges();

                    return true;
                }
            }
            catch { }

            return false;
        }

        public static Task<bool> UpdateLastDateSessionAsync(session objSessionExtternall)
        {
            return Task.Run(() =>
            {
                try
                {
                    using (straad_generaldesktopapplication_pcpcpcpc_001Entities objContext = new straad_generaldesktopapplication_pcpcpcpc_001Entities())
                    {
                        session objSession = objContext.sessions.Where(s => s.sess_uuid__uniqueidentifier == objSessionExtternall.sess_uuid__uniqueidentifier).FirstOrDefault();
                        DateTime objDateTime = DateB.GetServersDateAndTime();
                        objSession.sess_lastactivity__time = new TimeSpan(objDateTime.Hour, objDateTime.Minute, objDateTime.Second);
                        objContext.SaveChanges();

                        return true;
                    }
                }
                catch { }

                return false;
            });
        }
    }
}