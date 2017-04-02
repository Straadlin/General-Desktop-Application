using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;

using General_Desktop_Application.Properties;
using General_Desktop_Application.BusinessLayer;

namespace General_Desktop_Application.Classes
{
    class Preferences
    {
        public static string TitleSoftware { get { return "Invalid Company"; } }
        public static string CurrentVersion { get { return "1.001.170303"; } }
        public static string ProductCode { get { return "Ct1x9w3c"; } }
        public static string DesignedDeveloped { get { return "Straad"; } }
        public static bool EnglishLanguage { get { return true; } }
        public static string PathBackups { get { return "C:\\straad_data\\backups"; } }
        public static string PathTemporalBackupFile { get { return PathBackups + "\\" + DatabaseName + "_temp.bak"; } }
        public static string PathScriptInitializerFile { get { return @"Initializer.sql"; } }
        public static string Connectionstring
        {
            get
            {
                //return Settings.Default["Local"].ToString() == "True" ?
                //    "metadata=res://*/EF.Model.csdl|res://*/EF.Model.ssdl|res://*/EF.Model.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=" + Settings.Default["Instance"].ToString() + ";initial catalog=" + DatabaseName + ";integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" :
                //    "metadata=res://*/EF.Model.csdl|res://*/EF.Model.ssdl|res://*/EF.Model.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=" + Settings.Default["IP"].ToString() + ";initial catalog=" + DatabaseName + ";user id=" + UserDatabase + ";password=" + PasswordDatabaseUser + ";MultipleActiveResultSets=True;App=EntityFramework&quot;";

                if (Settings.Default["Local"].ToString() == "True")
                {
                    // Initialize the connection string builder for the
                    // underlying provider.
                    SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();

                    // Specify the provider name, server and database.
                    // Set the properties for the data source.
                    sqlBuilder.DataSource = Settings.Default["Instance"].ToString();// serverName
                    sqlBuilder.InitialCatalog = DatabaseName;// databaseName
                    sqlBuilder.IntegratedSecurity = true;

                    // Build the SqlConnection connection string.
                    string providerString = sqlBuilder.ToString();

                    // Initialize the EntityConnectionStringBuilder.
                    EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();

                    //Set the provider name.
                    entityBuilder.Provider = "System.Data.SqlClient";// providerName

                    // Set the provider-specific connection string.
                    entityBuilder.ProviderConnectionString = providerString;

                    // Set the Metadata location.
                    entityBuilder.Metadata = @"res://*/EF.Model.csdl|res://*/EF.Model.ssdl|res://*/EF.Model.msl";
                    return entityBuilder.ToString();
                    //Console.WriteLine(entityBuilder.ToString());

                    //using (EntityConnection conn =
                    //    new EntityConnection(entityBuilder.ToString()))
                    //{
                    //    conn.Open();
                    //    Console.WriteLine("Just testing the connection.");
                    //    conn.Close();
                    //}
                }
                else
                {
                    // Initialize the connection string builder for the
                    // underlying provider.
                    SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();

                    // Specify the provider name, server and database.
                    // Set the properties for the data source.
                    sqlBuilder.DataSource = Settings.Default["IP"].ToString() + "," + Settings.Default["Port"].ToString();// serverName
                    sqlBuilder.InitialCatalog = DatabaseName;// databaseName
                    sqlBuilder.NetworkLibrary = "DBMSSOCN";
                    sqlBuilder.UserID = UserDatabase;
                    sqlBuilder.Password = PasswordDatabaseUser;

                    // Build the SqlConnection connection string.
                    string providerString = sqlBuilder.ToString();

                    // Initialize the EntityConnectionStringBuilder.
                    EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();

                    //Set the provider name.
                    entityBuilder.Provider = "System.Data.SqlClient";// providerName

                    // Set the provider-specific connection string.
                    entityBuilder.ProviderConnectionString = providerString;

                    // Set the Metadata location.
                    entityBuilder.Metadata = @"res://*/EF.Model.csdl|res://*/EF.Model.ssdl|res://*/EF.Model.msl";
                    return entityBuilder.ToString();
                    //Console.WriteLine(entityBuilder.ToString());

                    //using (EntityConnection conn =
                    //    new EntityConnection(entityBuilder.ToString()))
                    //{
                    //    conn.Open();
                    //    Console.WriteLine("Just testing the connection.");
                    //    conn.Close();
                    //}
                }
            }
        }
        //
        //------------------------------------------------------------------------------------------
        public static string DatabaseName { get { return "straad_generaldesktopapplication_" + ProductCode; } }
        public static string UserDatabase { get { return "internal_" + DatabaseName; } }
        public static string PasswordDatabaseUser { get { return "6[A9$Sd#u-5CHJUsm_5Rxts-CtVa7SmE6"; } }
        public static string PasswordInstanceSAPWD { get { return "a123A45"; } }
        public static bool ArchitectDatabase64 { get { return true; } }
        //------------------------------------------------------------------------------------------
        public static string DefaultSystemUser { get { return "admin"; } }
        public static string DefaultSystemPassword { get { return "admin"; } }
        public static string DefaultFirstNameUserSystem { get { return "Administrator"; } }
        public static string DefaultLastNameUserSystem { get { return "Administrator"; } }
        public static string EncryptedDataAndHashPassword { get { return "'d-f5]8T.x6_[s3'"; } }
        //------------------------------------------------------------------------------------------
        public static string InfoMessagesF1 { get { return "Version: " + CurrentVersion + "\r\nPC: " + ProductCode; } }
        //------------------------------------------------------------------------------------------
        public static string GlobalSuccessOperation { get { return "The operation was done successfully."; } }
        public static string GlobalErrorOperation { get { return "There was a problem when it tryed to do this operation. Try restarting the appplication."; } }
        public static string GlobalTextToComparePasswords { get { return "Th1sTexCompl3t3lyIrrXD"; } }
    }
}