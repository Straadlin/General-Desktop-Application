using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using General_Desktop_Application.Classes;
using General_Desktop_Application.Data;
using General_Desktop_Application.Models;

namespace General_Desktop_Application.Business
{
    class Business
    {
        public static bool Execute(string stQuery)
        {
            return Data.Data.Execute(stQuery);
        }

        public static bool Execute(string stQuery, string stConnectionString)
        {
            return Data.Data.Execute(stQuery, stConnectionString);
        }

        public static bool Execute(string stQuery, System.Data.SqlClient.SqlConnection obSqlConnection)
        {
            return Data.Data.Execute(stQuery, obSqlConnection);
        }

        public static int SelectCountAllUsers(string stInstance)
        {
            List<int> obListInt = Data.Data.SelectListInt("SELECT COUNT(*) AS value FROM build_general.[user]", "value", Data.Data.GetConnectionString(stInstance));
            return obListInt.Count > 0 ? obListInt[0] : 0;
        }

        public static int SelectCountAllUsers(string stIPBatch01, string stIPBatch02, string stIPBatch03, string stIPBatch04, string stPort)
        {
            List<int> obList = Data.Data.SelectListInt("SELECT COUNT(*) AS value FROM build_general.user", "value", Data.Data.GetConnectionString(stIPBatch01, stIPBatch02, stIPBatch03, stIPBatch04, stPort));

            if (obList != null && obList.Count > 0 && obList[0] > 0)
                return obList[0];

            return 0;
        }

        public static int SelectCountDatabases(string stInstance)
        {
            List<int> obList = Data.Data.SelectListInt("SELECT COUNT(*) AS value FROM sysdatabases WHERE NAME ='" + PreferencesStraad.DatabaseName + "'", "value", Data.Data.GetConnectionString(stInstance));

            if (obList != null && obList.Count > 0 && obList[0] > 0)
                return obList[0];

            return 0;
        }
    }
}