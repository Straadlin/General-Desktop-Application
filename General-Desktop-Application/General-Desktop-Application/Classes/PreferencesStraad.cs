using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General_Desktop_Application.Classes
{
    class PreferencesStraad
    {
        public static string TitleSoftware { get { return "Company Name"; } }
        public static string CurrentVersion { get { return "01.1605201335"; } }
        //------------------------------------------------------------------------------------------
        //public static string FirstNickNameSystemDefault { get { return "cristyn98"; } }
        //public static string FirstUserPasswordSystemDefault { get { return "89nirc"; } }
        //public static string FirstUserNameSystemDefault { get { return "Cristy"; } }
        //------------------------------------------------------------------------------------------
        public static string DatabaseName { get { return "straad_instituteofcourses_v01"; } }
        public static string UserDatabase { get { return "internal_" + DatabaseName; } }
        public static string PasswordDatabaseUser { get { return "6[A9$Sd#u-5C,HJUsm_5Rxts-CtVa,7SmE6"; } }
        public static string PasswordInstanceSAPWD { get { return "a123A45"; } }
        public static bool ArchitectDatabase64 { get { return true; } }
    }
}