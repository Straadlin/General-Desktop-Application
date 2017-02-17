using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Common;
using System.Data.SqlClient;

using General_Desktop_Application.Classes;
using General_Desktop_Application.Properties;

namespace General_Desktop_Application.Data
{
    static class Data
    {
        public static string GetConnectionString()
        {
            SqlConnectionStringBuilder objSqlConnectionStringBuilder = new SqlConnectionStringBuilder();

            if (Convert.ToBoolean(Settings.Default["Local"]))
            {
                objSqlConnectionStringBuilder.DataSource = Settings.Default["Instance"].ToString();
                objSqlConnectionStringBuilder.InitialCatalog = PreferencesStraad.DatabaseName;
                objSqlConnectionStringBuilder.IntegratedSecurity = true;
            }
            else
            {
                objSqlConnectionStringBuilder.DataSource = Settings.Default["IP"] + "," + Settings.Default["Port"];
                objSqlConnectionStringBuilder.NetworkLibrary = "DBMSSOCN";
                objSqlConnectionStringBuilder.InitialCatalog = PreferencesStraad.DatabaseName;
                objSqlConnectionStringBuilder.UserID = PreferencesStraad.UserDatabase;
                objSqlConnectionStringBuilder.Password = PreferencesStraad.PasswordDatabaseUser;
            }

            return objSqlConnectionStringBuilder.ConnectionString;
        }

        public static string GetConnectionString(string stInstance)
        {
            SqlConnectionStringBuilder objSqlConnectionStringBuilder = new SqlConnectionStringBuilder();

            objSqlConnectionStringBuilder.DataSource = stInstance;
            objSqlConnectionStringBuilder.InitialCatalog = PreferencesStraad.DatabaseName;
            objSqlConnectionStringBuilder.IntegratedSecurity = true;

            return objSqlConnectionStringBuilder.ConnectionString;
        }

        public static string GetConnectionString(string byIPBatch001, string stIPBatch002, string stIPBatch003, string stIPBatch004,string stPort)
        {
            SqlConnectionStringBuilder obSqlConnectionStringBuilder = new SqlConnectionStringBuilder();

            obSqlConnectionStringBuilder.DataSource = byIPBatch001 + "." + stIPBatch002 + "." + stIPBatch003 + "." + stIPBatch004  + "," +stPort;
            obSqlConnectionStringBuilder.NetworkLibrary = "DBMSSOCN";
            obSqlConnectionStringBuilder.InitialCatalog = PreferencesStraad.DatabaseName;
            obSqlConnectionStringBuilder.UserID = PreferencesStraad.UserDatabase;
            obSqlConnectionStringBuilder.Password = PreferencesStraad.PasswordDatabaseUser;

            return obSqlConnectionStringBuilder.ConnectionString;
        }

        public static bool Execute(string stQuery)
        {
            try
            {
                using (SqlConnection objSqlConnection = new SqlConnection(GetConnectionString()))
                {
                    SqlCommand obSqlCommand = new SqlCommand(stQuery, objSqlConnection);
                    objSqlConnection.Open();

                    obSqlCommand.ExecuteNonQuery();
                }

                return true;
            }
            catch { return false; }
        }

        public static bool Execute(string stQuery, string stConnectionString)
        {
            try
            {
                using (SqlConnection objSqlConnection = new SqlConnection(stConnectionString))
                {
                    SqlCommand obSqlCommand = new SqlCommand(stQuery, objSqlConnection);
                    objSqlConnection.Open();

                    obSqlCommand.ExecuteNonQuery();
                }

                return true;
            }
            catch { return false; }
        }

        public static bool Execute(string stQuery, SqlConnection obSqlConnection)
        {
            try
            {
                SqlCommand objSqlCommand = new SqlCommand(stQuery, obSqlConnection);
                if (obSqlConnection.State == System.Data.ConnectionState.Closed)
                    obSqlConnection.Open();

                objSqlCommand.ExecuteNonQuery();

                return true;
            }
            catch { return false; }
        }

        public static List<int> SelectListInt(string stQuery, string stNameField)
        {
            List<int> objList = new List<int>();

            try
            {
                using (SqlConnection objSqlConnection = new SqlConnection(Data.GetConnectionString()))
                {
                    SqlCommand obSqlCommand = new SqlCommand(stQuery, objSqlConnection);
                    objSqlConnection.Open();

                    using (DbDataReader lectorDatos = obSqlCommand.ExecuteReader())
                    {
                        while (lectorDatos.Read())
                        {
                            objList.Add((int)lectorDatos[stNameField]);
                        }
                    }
                }
            }
            catch { }

            return objList;
        }

        public static List<int> SelectListInt(string stQuery, string stNameField, string stConnectionString)
        {
            List<int> objList = new List<int>();

            try
            {
                using (SqlConnection objSqlConnection = new SqlConnection(stConnectionString))
                {
                    SqlCommand obSqlCommand = new SqlCommand(stQuery, objSqlConnection);
                    objSqlConnection.Open();

                    using (DbDataReader lectorDatos = obSqlCommand.ExecuteReader())
                    {
                        while (lectorDatos.Read())
                        {
                            objList.Add((int)lectorDatos[stNameField]);
                        }
                    }
                }
            }
            catch { }

            return objList;
        }

        public static List<string> SelectListString(string stQuery, string stNameField)
        {
            List<string> objList = new List<string>();

            try
            {
                using (SqlConnection objSqlConnection = new SqlConnection(Data.GetConnectionString()))
                {
                    SqlCommand obSqlCommand = new SqlCommand(stQuery, objSqlConnection);
                    objSqlConnection.Open();

                    using (DbDataReader lectorDatos = obSqlCommand.ExecuteReader())
                    {
                        while (lectorDatos.Read())
                        {
                            objList.Add((string)lectorDatos[stNameField]);
                        }
                    }
                }
            }
            catch { }

            return objList;
        }
    }
}