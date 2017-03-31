using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Mail;
using System.Text.RegularExpressions;

namespace General_Desktop_Application.Classes
{
    public static class RegularExpressions
    {
        public static bool CheckUsername(string stValue)
        {
            return Regex.IsMatch(stValue, @"^([0-9A-Za-z]+){4,100}$", RegexOptions.CultureInvariant);
            //return Regex.IsMatch(stValue, @"^([A-Za-z]+[0-9A-Za-z]*){4,100}$", RegexOptions.CultureInvariant);
        }

        public static bool CheckEmail(string stValue)
        {
            try
            {
                MailAddress m = new MailAddress(stValue);

                return true;
            }
            catch /*(FormatException)*/ { }

            return false;
        }

        public static bool CheckIP(string stValue)
        {
            return Regex.IsMatch(stValue, @"^([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3})$", RegexOptions.CultureInvariant);
            //return Regex.IsMatch(stValue, @"^([0-9]?[0-9]?[0-9]\.[0-9]?[0-9]?[0-9]\.[0-9]?[0-9]?[0-9]\.[0-9]?[0-9]?[0-9])$", RegexOptions.CultureInvariant);
        }

        public static bool CheckFirstNameOrLastName(string stValue)
        {
            return Regex.IsMatch(stValue, @"^([A-Za-z\s,á,é,í,ó,ú,ñ,Á,É,Í,Ó,Ú,Ñ]+){1,30}$", RegexOptions.CultureInvariant);
        }

        public static bool CheckUrl(string stValue)
        {
            return false;
        }

        public static bool CheckMoney(string stValue)
        {
            return Regex.IsMatch(stValue, @"^(\-|\+)?(\s)?(\$)?(\s)?[0-9]+(\,[0-9]+)*(.[0-9]+)?$", RegexOptions.CultureInvariant);
        }

        public static bool CheckDecimal(string stValue)
        {
            return Regex.IsMatch(stValue, @"^(\-)?[0-9]+(.[0-9]+)?$", RegexOptions.CultureInvariant);
        }

        public static bool CheckNumber(string stValue)
        {
            return Regex.IsMatch(stValue, @"^[0-9]{10}$", RegexOptions.CultureInvariant);
        }

        public static bool CheckRFC(string stValue)
        {
            return Regex.IsMatch(stValue, "^[A-Z,Ñ,&]{3,4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?$", RegexOptions.CultureInvariant);
        }

        public static bool CheckPassword(string stValue)
        {
            return Regex.IsMatch(stValue, @"^([0-9A-Za-z]+){4,100}$", RegexOptions.CultureInvariant);
        }
    }
}