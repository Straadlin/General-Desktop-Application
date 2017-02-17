using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;//
using System.Net;//

namespace General_Desktop_Application.Classes
{
    public static class NetStraad
    {
        public static string GetExternalIP()
        {
            // https://social.msdn.microsoft.com/Forums/es-ES/56bab566-d204-429d-a8e0-8f8ccaa2505d/obtener-ip-publica?forum=vcses

            try
            {
                var request = (HttpWebRequest)WebRequest.Create("http://ifconfig.me");
                request.UserAgent = "curl"; string publicIPAddress; request.Method = "GET";
                using (WebResponse response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        publicIPAddress = reader.ReadToEnd();
                    }
                }

                return publicIPAddress.Replace("\n", "");
            }
            catch { return null; }
        }
    }
}