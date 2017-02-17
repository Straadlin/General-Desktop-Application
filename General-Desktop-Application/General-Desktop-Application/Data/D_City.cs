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
    static class D_City
    {
        public static List<M_City> Select(string stQuery)
        {
            List<M_City> objList = new List<M_City>();

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
                            objList.Add(new M_City(
                                (string)objDbDataReader["city_uuid"],
                                (string)objDbDataReader["city_value"],
                                (string)objDbDataReader["stat_uuid"]
                            ));
                        }
                    }
                }
            }
            catch { }

            return objList;
        }

        public static List<M_City> Select(string stQuery, string stConnectionString)
        {
            List<M_City> objList = new List<M_City>();

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
                            objList.Add(new M_City(
                                (string)objDbDataReader["city_uuid"],
                                (string)objDbDataReader["city_value"],
                                (string)objDbDataReader["stat_uuid"],
                            stConnectionString));
                        }
                    }
                }
            }
            catch { }

            return objList;
        }

        public static bool Insert(M_City objM_City)
        {
            return Data.Execute("INSERT INTO build_general.city(city_uuid, city_value, stat_uuid) VALUES('" + objM_City.Uuid + "', '" + objM_City.Value + "', '" + objM_City.UuidState + "');");
        }

        public static bool Insert(M_City objM_City, string stConnectionString)
        {
            return Data.Execute("INSERT INTO build_general.city(city_uuid, city_value, stat_uuid) VALUES('" + objM_City.Uuid + "', '" + objM_City.Value + "', '" + objM_City.UuidState + "');", stConnectionString);
        }

        public static bool Update(M_City objM_City)
        {
            return Data.Execute("UPDATE build_general.city SET city_value = '" + objM_City.Value + "', stat_uuid = '" + objM_City.UuidState + "' WHERE(city_uuid = '" + objM_City.Uuid + "');");
        }

        public static bool Delete(M_City objM_City)
        {
            return Data.Execute("DELETE FROM build_general.city WHERE(city_uuid = '" + objM_City.Uuid + "');");
        }
    }
}