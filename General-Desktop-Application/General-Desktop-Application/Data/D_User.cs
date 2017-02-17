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
    static class D_User
    {
        public static List<M_User> Select(string stQuery)
        {
            List<M_User> objList = new List<M_User>();

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
                            objList.Add(new M_User(
                                objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("user_uuid")) ? null : (string)objDbDataReader["user_uuid"],// N
                                objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("stEmail")) ? null : (string)objDbDataReader["stEmail"],// N
                                (string)objDbDataReader["user_value"],
                                (string)objDbDataReader["user_password"],
                                (string)objDbDataReader["user_fullname"],
                                (bool)objDbDataReader["user_gender"],
                                objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("user_landline")) ? null : (string)objDbDataReader["user_landline"],// N
                                objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("user_cellphonenumber")) ? null : (string)objDbDataReader["user_cellphonenumber"],// N
                                objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("user_complement")) ? null : (string)objDbDataReader["user_complement"],// N
                                (byte)objDbDataReader["user_role"],
                                (bool)objDbDataReader["user_enabled"],
                                objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("user_maximumhoursperiod")) ? (byte)0 : (byte)objDbDataReader["user_maximumhoursperiod"],// N
                                objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("user_minimumhoursperiod")) ? (byte)0 : (byte)objDbDataReader["user_minimumhoursperiod"],// N
                                (string)objDbDataReader["city_uuid"],
                                objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("file_uuid_avatar")) ? null : (string)objDbDataReader["file_uuid_avatar"],// N
                                objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("user_uuid_root")) ? null : (string)objDbDataReader["user_uuid_root"],// N
                                objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("sess_uuid_used")) ? null : (string)objDbDataReader["sess_uuid_used"],// N
                                (string)objDbDataReader["sess_uuid_created"],
                                objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("sess_uuid_deleted")) ? null : (string)objDbDataReader["sess_uuid_deleted"]// N
                            ));
                        }
                    }
                }
            }
            catch { }

            return objList;
        }
        public static List<M_User> Select(string stQuery, string stConnectionString)
        {
            List<M_User> objList = new List<M_User>();

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
                            objList.Add(new M_User(
                                objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("user_uuid")) ? null : (string)objDbDataReader["user_uuid"],// N
                                (string)objDbDataReader["user_value"],
                                objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("stEmail")) ? null : (string)objDbDataReader["stEmail"],// N
                                (string)objDbDataReader["user_password"],
                                (string)objDbDataReader["user_fullname"],
                                (bool)objDbDataReader["user_gender"],
                                objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("user_landline")) ? null : (string)objDbDataReader["user_landline"],// N
                                objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("user_cellphonenumber")) ? null : (string)objDbDataReader["user_cellphonenumber"],// N
                                objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("user_complement")) ? null : (string)objDbDataReader["user_complement"],// N
                                (byte)objDbDataReader["user_role"],
                                (bool)objDbDataReader["user_enabled"],
                                objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("user_maximumhoursperiod")) ? (byte)0 : (byte)objDbDataReader["user_maximumhoursperiod"],// N
                                objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("user_minimumhoursperiod")) ? (byte)0 : (byte)objDbDataReader["user_minimumhoursperiod"],// N
                                (string)objDbDataReader["city_uuid"],
                                objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("file_uuid_avatar")) ? null : (string)objDbDataReader["file_uuid_avatar"],// N
                                objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("user_uuid_root")) ? null : (string)objDbDataReader["user_uuid_root"],// N
                                objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("sess_uuid_used")) ? null : (string)objDbDataReader["sess_uuid_used"],// N
                                (string)objDbDataReader["sess_uuid_created"],
                                objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("sess_uuid_deleted")) ? null : (string)objDbDataReader["sess_uuid_deleted"]// N
                            ));
                        }
                    }
                }
            }
            catch { }

            return objList;
        }

        //public static List<M_User> Select(string stQuery, string stConnectionString)
        //{
        //    List<M_User> objList = new List<M_User>();

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
        //                    objList.Add(new M_User(
        //                        objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("user_uuid")) ? null : (string)objDbDataReader["user_uuid"],// N
        //                        (string)objDbDataReader["user_value"],
        //                        objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("stEmail")) ? null : (string)objDbDataReader["stEmail"],// N
        //                        (string)objDbDataReader["user_password"],
        //                        (string)objDbDataReader["user_fullname"],
        //                        (bool)objDbDataReader["user_gender"],
        //                        objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("user_landline")) ? null : (string)objDbDataReader["user_landline"],// N
        //                        objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("user_cellphonenumber")) ? null : (string)objDbDataReader["user_cellphonenumber"],// N
        //                        objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("user_complement")) ? null : (string)objDbDataReader["user_complement"],// N
        //                        (byte)objDbDataReader["user_role"],
        //                        (bool)objDbDataReader["user_enabled"],
        //                        objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("user_maximumhoursperiod")) ? (byte)0 : (byte)objDbDataReader["user_maximumhoursperiod"],// N
        //                        objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("user_minimumhoursperiod")) ? (byte)0 : (byte)objDbDataReader["user_minimumhoursperiod"],// N
        //                        (string)objDbDataReader["city_uuid"],
        //                        objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("file_uuid_avatar")) ? null : (string)objDbDataReader["file_uuid_avatar"],// N
        //                        objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("user_uuid_root")) ? null : (string)objDbDataReader["user_uuid_root"],// N
        //                        objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("sess_uuid_used")) ? null : (string)objDbDataReader["sess_uuid_used"],// N
        //                        (string)objDbDataReader["sess_uuid_created"],
        //                        objDbDataReader.IsDBNull(objDbDataReader.GetOrdinal("sess_uuid_deleted")) ? null : (string)objDbDataReader["sess_uuid_deleted"],// N
        //                    stConnectionString));
        //                    //objList.Add(new M_User(
        //                    //    (string)objDbDataReader["user_uuid"],
        //                    //    (string)objDbDataReader["stat_value"],
        //                    //stConnectionString));
        //                }
        //            }
        //        }
        //    }
        //    catch { }

        //    return objList;
        //}

        public static bool Insert(M_User objM_User)
        {
            // FALTA CONVERT THE PASSWORD IN SOME ENCRYPT STRING
            return Data.Execute("INSERT INTO build_general.user(user_uuid, user_value, user_email, user_password, user_fullname, user_gender, user_landline, user_cellphonenumber, user_complement, user_role, user_enabled, user_maximiumhoursperiod, user_minimiumhoursperiod, city_uuid, file_uuid_avatar, user_uuid_root, sess_uuid_used, sess_uuid_created, sess_uuid_deleted) VALUES('" + objM_User.Uuid + "', " + (string.IsNullOrEmpty(objM_User.Value) ? "null" : "'" + objM_User.Value + "'") + ", " + (string.IsNullOrEmpty(objM_User.Email) ? "null" : "'" + objM_User.Email + "'") + ", '" + objM_User.Password + "','" + objM_User.FullName + "', " + objM_User.Gender + ", " + (string.IsNullOrEmpty(objM_User.LandLine) ? "null" : "'" + objM_User.LandLine + "'") + ", " + (string.IsNullOrEmpty(objM_User.CellphoneNumber) ? "null" : "'" + objM_User.CellphoneNumber + "'") + ", " + (string.IsNullOrEmpty(objM_User.Complement) ? "null" : "'" + objM_User.Complement + "'") + ", " + objM_User.Role + ", " + objM_User.Enabled + ", " + (objM_User.MaximumHoursPeriod == null ? "null" : objM_User.MaximumHoursPeriod.ToString()) + ", " + (objM_User.MinimumHoursPeriod == null ? "null" : objM_User.MinimumHoursPeriod.ToString()) + ", '" + objM_User.UuidCity + "', " + (string.IsNullOrEmpty(objM_User.UuidFileAvatar) ? "null" : "'" + objM_User.UuidFileAvatar + "'") + ", " + (string.IsNullOrEmpty(objM_User.UuidUserRoot) ? "null" : "'" + objM_User.UuidUserRoot + "'") + ", " + (string.IsNullOrEmpty(objM_User.UuidSessionUsed) ? "null" : "'" + objM_User.UuidSessionUsed + "'") + ", " + objM_User.UuidSessionCreated + ", " + (string.IsNullOrEmpty(objM_User.UuidSessionDeleted) ? "null" : "'" + objM_User.UuidSessionDeleted + "'") + ");");
        }

        public static bool Insert(M_User objM_User, string stConnectionString)
        {
            // FALTA CONVERT THE PASSWORD IN SOME ENCRYPT STRING
            return Data.Execute("INSERT INTO build_general.user(user_uuid, user_value, user_email, user_password, user_fullname, user_gender, user_landline, user_cellphonenumber, user_complement, user_role, user_enabled, user_maximiumhoursperiod, user_minimiumhoursperiod, city_uuid, file_uuid_avatar, user_uuid_root, sess_uuid_used, sess_uuid_created, sess_uuid_deleted) VALUES('" + objM_User.Uuid + "', " + (string.IsNullOrEmpty(objM_User.Value) ? "null" : "'" + objM_User.Value + "'") + ", " + (string.IsNullOrEmpty(objM_User.Email) ? "null" : "'" + objM_User.Email + "'") + ", '" + objM_User.Password + "','" + objM_User.FullName + "', " + objM_User.Gender + ", " + (string.IsNullOrEmpty(objM_User.LandLine) ? "null" : "'" + objM_User.LandLine + "'") + ", " + (string.IsNullOrEmpty(objM_User.CellphoneNumber) ? "null" : "'" + objM_User.CellphoneNumber + "'") + ", " + (string.IsNullOrEmpty(objM_User.Complement) ? "null" : "'" + objM_User.Complement + "'") + ", " + objM_User.Role + ", " + objM_User.Enabled + ", " + (objM_User.MaximumHoursPeriod == null ? "null" : objM_User.MaximumHoursPeriod.ToString()) + ", " + (objM_User.MinimumHoursPeriod == null ? "null" : objM_User.MinimumHoursPeriod.ToString()) + ", '" + objM_User.UuidCity + "', " + (string.IsNullOrEmpty(objM_User.UuidFileAvatar) ? "null" : "'" + objM_User.UuidFileAvatar + "'") + ", " + (string.IsNullOrEmpty(objM_User.UuidUserRoot) ? "null" : "'" + objM_User.UuidUserRoot + "'") + ", " + (string.IsNullOrEmpty(objM_User.UuidSessionUsed) ? "null" : "'" + objM_User.UuidSessionUsed + "'") + ", " + objM_User.UuidSessionCreated + ", " + (string.IsNullOrEmpty(objM_User.UuidSessionDeleted) ? "null" : "'" + objM_User.UuidSessionDeleted + "'") + ");", stConnectionString);
        }

        public static bool Update(M_User objM_User)
        {
            //FALTA TERMINARLO
            return Data.Execute("UPDATE build_general.user SET stat_value = '" + objM_User.Value + "' WHERE(user_uuid = '" + objM_User.Uuid + "');");
        }

        public static bool Delete(M_User objM_User)
        {
            return Data.Execute("DELETE FROM build_general.user WHERE(user_uuid = '" + objM_User.Uuid + "');");
        }
    }
}