﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

using General_Desktop_Application.Classes;
using General_Desktop_Application.DataLayer;
using General_Desktop_Application.EF;
//using General_Desktop_Application.Models;

namespace General_Desktop_Application.BusinessLayer
{
    class Business
    {
        public static bool Execute(string stQuery)
        {
            return Data.Execute(stQuery);
        }

        public static bool Execute(string stQuery, string stConnectionString)
        {
            return Data.Execute(stQuery, stConnectionString);
        }

        //public static bool Execute(string stQuery, System.Data.SqlClient.SqlConnection obSqlConnection)
        //{
        //    return Data.Data.Execute(stQuery, obSqlConnection);
        //}

        //public static int SelectCountAllUsers(string stInstance)
        //{
        //    List<int> obListInt = Data.Data.SelectListInt("SELECT COUNT(*) AS value FROM build_level001.[user]", "value", Data.Data.GetConnectionString(stInstance));
        //    return obListInt.Count > 0 ? obListInt[0] : 0;
        //}

        //public static int SelectCountAllUsers(string stIPBatch01, string stIPBatch02, string stIPBatch03, string stIPBatch04, string stPort)
        //{
        //    List<int> obList = Data.Data.SelectListInt("SELECT COUNT(*) AS value FROM build_level001.[user]", "value", Data.Data.GetConnectionString(stIPBatch01, stIPBatch02, stIPBatch03, stIPBatch04, stPort));

        //    if (obList != null && obList.Count > 0 && obList[0] > 0)
        //        return obList[0];

        //    return 0;
        //}

        public static int SelectCountDatabases(string stInstance)
        {
            List<int> obList = Data.SelectListInt("SELECT COUNT(*) AS value FROM sysdatabases WHERE NAME ='" + Preferences.DatabaseName + "'", "value", Data.GetConnectionString(stInstance));

            if (obList != null && obList.Count > 0 && obList[0] > 0)
                return obList[0];

            return 0;
        }

        public static DateTime GetServersDateAndTime()
        {
            try
            {
                using (ModelContext objContext = new ModelContext())
                {
                    SqlParameter objSqlParameter = new SqlParameter("@dateTime", "");

                    var vDate = objContext.Database.SqlQuery<string>("EXEC build_level000.proc_systemserver_getdatetime @dateTime", objSqlParameter).FirstOrDefault();

                    return Convert.ToDateTime(vDate);
                }
            }
            catch { }

            return new DateTime(2000, 1, 1).Date;
        }

        public static Task<DateTime> GetServersDateAndTimeAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    using (ModelContext objContext = new ModelContext())
                    {
                        SqlParameter objSqlParameter = new SqlParameter("@dateTime", "");

                        var vDate = objContext.Database.SqlQuery<string>("EXEC build_level000.proc_systemserver_getDatetime @dateTime", objSqlParameter).FirstOrDefault();

                        return Convert.ToDateTime(vDate);
                    }
                }
                catch { }

                return new DateTime(2000, 1, 1).Date;
            });
        }

        public static bool FreeAllRegistersAssociatedWithThisUser(user objUser)
        {
            try
            {
                using (ModelContext objContext = new ModelContext())
                {
                    // This is identcal like the next block but in Linq
                    //SELECT u.*FROM[build_level002].[session] AS s
                    //   INNER JOIN[build_level001].[user] AS u ON s.sess_uuid__uniqueidentifier = u.sess_uuid_used__uniqueidentifier
                    //   WHERE s.user_uuid_created__uniqueidentifier = '9C749C38-DD8F-4BAC-898B-896ACAE44A23'

                    bool boBand = false;

                    var query01 =
                        (from sessionQ in objContext.sessions
                         join userQ in objContext.users on sessionQ.sess_uuid__uniqueidentifier equals userQ.sess_uuid_used__uniqueidentifier
                         where sessionQ.user_uuid_created__uniqueidentifier == objUser.user_uuid__uniqueidentifier
                         select userQ)
                        .ToList();

                    if (query01.Count > 0)
                    {
                        foreach (var vItem in query01)
                            vItem.sess_uuid_used__uniqueidentifier = null;

                        boBand = true;
                    }

                    var query02 =
                        (from sessionQ in objContext.sessions
                         join principalcompanyQ in objContext.principalcompanies on sessionQ.sess_uuid__uniqueidentifier equals principalcompanyQ.sess_uuid_used__uniqueidentifier
                         where sessionQ.user_uuid_created__uniqueidentifier == objUser.user_uuid__uniqueidentifier
                         select principalcompanyQ)
                        .ToList();

                    if (query02.Count > 0)
                    {
                        foreach (var vItem in query02)
                            vItem.sess_uuid_used__uniqueidentifier = null;

                        boBand = true;
                    }

                    if (boBand)
                        objContext.SaveChanges();

                    //objContext.users
                    //    .Where(u => u.sess_uuid_used__uniqueidentifier == objUser.user_uuid__uniqueidentifier)
                    //    .ToList()
                    //    .ForEach(x => x.sess_uuid_used__uniqueidentifier = null);                
                    //objContext.SaveChanges();

                    return true;
                }
            }
            catch { }

            return false;
        }
    }
}