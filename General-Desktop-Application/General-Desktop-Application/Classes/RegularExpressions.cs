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
        public static bool CheckIsUsernameAndLength(string stValue, int inMinimiumLength, int intMaximiumLength)
        {
            //return Regex.IsMatch(stValue, @"^([A-Za-z]+[0-9A-Za-z]*){4,100}$", RegexOptions.CultureInvariant);
            //return Regex.IsMatch(stValue, @"^([0-9A-Za-z]+){" + inMinimiumLength + "," + intMaximiumLength + "}$", RegexOptions.CultureInvariant);
            return Regex.IsMatch(stValue, @"^([0-9A-Za-z]){" + inMinimiumLength + "," + intMaximiumLength + "}$", RegexOptions.CultureInvariant);
        }

        public static bool CheckIskEmail(string stValue)
        {
            try
            {
                MailAddress m = new MailAddress(stValue);

                return true;
            }
            catch /*(FormatException)*/ { }

            return false;
        }

        public static bool CheckIsIP(string stValue)
        {
            return Regex.IsMatch(stValue, @"^([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3})$", RegexOptions.CultureInvariant);
        }

        public static bool CheckIsFirstNameOrLastNameAndLength(string stValue, int inMinimiumLength, int intMaximiumLength)
        {
            //return Regex.IsMatch(stValue, @"^([A-Za-z\s,á,é,í,ó,ú,ñ,Á,É,Í,Ó,Ú,Ñ]+){" + inMinimiumLength + "," + intMaximiumLength + "}$", RegexOptions.CultureInvariant);
            return Regex.IsMatch(stValue, @"^([A-Za-z\s,á,é,í,ó,ú,ñ,Á,É,Í,Ó,Ú,Ñ]){" + inMinimiumLength + "," + intMaximiumLength + "}$", RegexOptions.CultureInvariant);
        }

        //public static bool CheckIsNormalText(string stValue, int intMaximiumLength)
        //{
        //    return string.IsNullOrEmpty(stValue) || stValue.Length <= intMaximiumLength ? true : false;
        //}

        public static bool CheckIsNormalText(string stValue, int inMinimiumLength, int intMaximiumLength)
        {
            return stValue != null && stValue.Length >= inMinimiumLength && stValue.Length <= intMaximiumLength ? true : false;
        }

        public static bool CheckIsUrl(string stValue)
        {
            return true;
        }

        public static bool CheckIsMoney(string stValue)
        {
            return Regex.IsMatch(stValue, @"^(\-|\+)?(\s)?(\$)?(\s)?[0-9]+(\,[0-9]+)*(.[0-9]+)?$", RegexOptions.CultureInvariant);
        }

        public static bool CheckIsDecimal(string stValue)
        {
            return Regex.IsMatch(stValue, @"^(\-)?[0-9]+(.[0-9]+)?$", RegexOptions.CultureInvariant);
        }

        //public static bool CheckIsNumber(string stValue, int inLength)
        //{
        //    return Regex.IsMatch(stValue, @"^[0-9]{" + inLength + "}$", RegexOptions.CultureInvariant);
        //}

        public static bool CheckIsNumeric(string stValue, int inMinimiumLength, int intMaximiumLength)
        {
            return Regex.IsMatch(stValue, @"^[0-9]{" + inMinimiumLength + "," + intMaximiumLength + "}$", RegexOptions.CultureInvariant);
        }

        public static bool CheckIsRFC(string stValue)
        {
            return Regex.IsMatch(stValue, "^[A-Z,Ñ,&]{3,4}[0-9]{2}[0-1][0-9][0-3][0-9][A-Z,0-9]?[A-Z,0-9]?[0-9,A-Z]?$", RegexOptions.CultureInvariant);
        }

        public static bool CheckIsPasswordAndLength(string stValue, int inMinimiumLength, int intMaximiumLength)
        {
            return Regex.IsMatch(stValue, @"^([0-9A-Za-z]){" + inMinimiumLength + "," + intMaximiumLength + "}$", RegexOptions.CultureInvariant);
        }
    }
}