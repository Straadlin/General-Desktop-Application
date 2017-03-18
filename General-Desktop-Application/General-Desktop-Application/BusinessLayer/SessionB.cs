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

                    session objSession = new session()
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

                    return objContext.sessions.Where(s => s.sess_uuid__uniqueidentifier == objSession.sess_uuid__uniqueidentifier).FirstOrDefault();
                }
            }
            catch { }

            return null;
        }
    }
}