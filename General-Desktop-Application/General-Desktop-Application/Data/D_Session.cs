using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Common;
using System.Data.SqlClient;

using General_Desktop_Application.Models;

namespace General_Desktop_Application.Data
{
    static class D_Session
    {
        //public static List<M_Session> Select(string stQuery)
        //{
        //    List<M_Session> objList = new List<M_Session>();

        //    try
        //    {
        //        using (SqlConnection obSqlConnection = new SqlConnection(Data.GetConnectionString()))
        //        {
        //            SqlCommand objSqlCommand = new SqlCommand(stQuery, obSqlConnection);
        //            obSqlConnection.Open();

        //            using (DbDataReader objDbDataReader = objSqlCommand.ExecuteReader())
        //            {
        //                while (objDbDataReader.Read())
        //                {
        //                    objList.Add(new M_Session(
        //                        (string)objDbDataReader["sess_uuid"],
        //                        (string)objDbDataReader["sess_value"]
        //                    ));
        //                }
        //            }
        //        }
        //    }
        //    catch { }

        //    return objList;
        //}

        //public static List<M_Session> Select(string stQuery, string stConnectionString)
        //{
        //    List<M_Session> objList = new List<M_Session>();

        //    try
        //    {
        //        using (SqlConnection obSqlConnection = new SqlConnection(stConnectionString))
        //        {
        //            SqlCommand objSqlCommand = new SqlCommand(stQuery, obSqlConnection);
        //            obSqlConnection.Open();

        //            using (DbDataReader objDbDataReader = objSqlCommand.ExecuteReader())
        //            {
        //                while (objDbDataReader.Read())
        //                {
        //                    objList.Add(new M_Session(
        //                        (string)objDbDataReader["sess_uuid"],
        //                        (string)objDbDataReader["sess_value"],
        //                    stConnectionString));
        //                }
        //            }
        //        }
        //    }
        //    catch { }

        //    return objList;
        //}

        //public static bool Insert(M_Session objM_Session)
        //{
        //    return Data.Execute("INSERT INTO build_general.session(sess_uuid, sess_value) VALUES('" + objM_Session.Uuid + "', '" + objM_Session.Value + "');");
        //}

        public static bool Insert(M_Session objM_Session, string stConnectionString)
        {
            //return Data.Execute("INSERT INTO build_general.session(sess_uuid, sess_value) VALUES('" + objM_Session.Uuid + "', '" + objM_Session.Value + "');", stConnectionString);
            return false;
        }

        //public static bool Update(M_Session objM_Session)
        //{
        //    return Data.Execute("UPDATE build_general.session SET sess_value = '" + objM_Session.Value + "' WHERE(sess_uuid = '" + objM_Session.Uuid + "');");
        //}

        public static bool Delete(M_Session objM_Session)
        {
            return Data.Execute("DELETE FROM build_general.session WHERE(sess_uuid = '" + objM_Session.Uuid + "');");
        }
    }
}