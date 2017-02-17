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
    static class D_State
    {
        public static List<M_State> Select(string stQuery)
        {
            List<M_State> objList = new List<M_State>();

            try
            {
                using (SqlConnection obSqlConnection = new SqlConnection(Data.GetConnectionString()))
                {
                    SqlCommand objSqlCommand = new SqlCommand(stQuery, obSqlConnection);
                    obSqlConnection.Open();

                    using (DbDataReader objDbDataReader = objSqlCommand.ExecuteReader())
                    {
                        while (objDbDataReader.Read())
                        {
                            objList.Add(new M_State(
                                (string)objDbDataReader["stat_uuid"],
                                (string)objDbDataReader["stat_value"]
                            ));
                        }
                    }
                }
            }
            catch { }

            return objList;
        }

        public static List<M_State> Select(string stQuery, string stConnectionString)
        {
            List<M_State> objList = new List<M_State>();

            try
            {
                using (SqlConnection obSqlConnection = new SqlConnection(stConnectionString))
                {
                    SqlCommand objSqlCommand = new SqlCommand(stQuery, obSqlConnection);
                    obSqlConnection.Open();

                    using (DbDataReader objDbDataReader = objSqlCommand.ExecuteReader())
                    {
                        while (objDbDataReader.Read())
                        {
                            objList.Add(new M_State(
                                (string)objDbDataReader["stat_uuid"],
                                (string)objDbDataReader["stat_value"],
                            stConnectionString));
                        }
                    }
                }
            }
            catch { }

            return objList;
        }

        public static bool Insert(M_State objM_State)
        {
            return Data.Execute("INSERT INTO build_general.state(stat_uuid, stat_value) VALUES('" + objM_State.Uuid + "', '" + objM_State.Value + "');");
        }

        public static bool Insert(M_State objM_State, string stConnectionString)
        {
            return Data.Execute("INSERT INTO build_general.state(stat_uuid, stat_value) VALUES('" + objM_State.Uuid + "', '" + objM_State.Value + "');", stConnectionString);
        }

        public static bool Update(M_State objM_State)
        {
            return Data.Execute("UPDATE build_general.state SET stat_value = '" + objM_State.Value + "' WHERE(stat_uuid = '" + objM_State.Uuid + "');");
        }

        public static bool Delete(M_State objM_State)
        {
            return Data.Execute("DELETE FROM build_general.state WHERE(stat_uuid = '" + objM_State.Uuid + "');");
        }
    }
}