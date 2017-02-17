using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32;
using System.Diagnostics;

namespace General_Desktop_Application.Classes
{
    public static class SQLServer
    {
        public static string[] fjol()
        {
            try
            {
                var ghyt = PreferencesStraad.ArchitectDatabase64 ? RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64) : RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                var wk7 = ghyt.OpenSubKey("SOFTWARE\\Microsoft\\Microsoft SQL Server");

                string[] wpi4l = (String[])wk7.GetValue("InstalledInstances");

                if (wpi4l != null && wpi4l.Length > 0)
                {
                    for (int i = 0; i < wpi4l.Length; i++)
                    {
                        if (wpi4l[i] == "MSSQLSERVER")
                            wpi4l[i] = "(local)";
                        else
                            wpi4l[i] = @"(local)\" + wpi4l[i];
                    }

                    return wpi4l;
                }
                else
                    return null;
            }
            catch { return null; }
        }

        public static Process zmct(string stRmy, bool boAutomatic)
        {
            string uko = DateTime.Now.Year.ToString().Substring(2);
            string ujlo = (DateTime.Now.Month > 9) ? DateTime.Now.Month.ToString() : "0" + DateTime.Now.Month;
            string e1s2d5 = (DateTime.Now.Day > 9) ? DateTime.Now.Day.ToString() : "0" + DateTime.Now.Day;
            string drme = (DateTime.Now.Hour > 9) ? DateTime.Now.Hour.ToString() : "0" + DateTime.Now.Hour;
            string sei = (DateTime.Now.Minute > 9) ? DateTime.Now.Minute.ToString() : "0" + DateTime.Now.Minute;

            ProcessStartInfo he4ys = new ProcessStartInfo(stRmy);
            Process df56c = new Process();

            if (boAutomatic)
            {
                // It install automatic way

                he4ys.Arguments = @" /QS /IACCEPTSQLSERVERLICENSETERMS /INSTANCEID=STRAAD" + uko + ujlo + e1s2d5 + drme + sei + @" /SAPWD=" + PreferencesStraad.PasswordInstanceSAPWD + " /SECURITYMODE=SQL /INSTANCENAME=STRAAD" + uko + ujlo + e1s2d5 + drme + sei + @" /ACTION=Install /FEATURES=SQLEngine,Replication /ASCOLLATION=Latin1_General_CI_AS /SQLCOLLATION=Latin1_General_CS_AS /ADDCURRENTUSERASSQLADMIN=True /UpdateEnabled=1 /TCPENABLED=1 /NPENABLED=1";

                df56c.StartInfo = he4ys;
            }
            else
            {
                // Only it load default values on wizard, but not install

                he4ys.Arguments = @" /INSTANCEID=STRAAD" + uko + ujlo + e1s2d5 + drme + sei + @" /SAPWD=" + PreferencesStraad.PasswordInstanceSAPWD + " /SECURITYMODE=SQL /INSTANCENAME=STRAAD" + uko + ujlo + e1s2d5 + drme + sei + @" /ACTION=Install /FEATURES=SQLEngine,Replication /ASCOLLATION=Latin1_General_CI_AS /SQLCOLLATION=Latin1_General_CS_AS /ADDCURRENTUSERASSQLADMIN=True /UpdateEnabled=1 /TCPENABLED=1 /NPENABLED=1";

                df56c.StartInfo = he4ys;
            }

            return df56c;
        }
    }
}