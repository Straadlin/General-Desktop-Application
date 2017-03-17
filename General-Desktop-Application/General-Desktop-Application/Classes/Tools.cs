using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using General_Desktop_Application.Enumerables;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

namespace General_Desktop_Application.Classes
{
    class Tools
    {
        public static string GetMessageBox(int iErrorMessage)
        {
            switch (iErrorMessage)
            {
                default: return "Unknown application error, you try restart it.";
            }
        }

        public static string GetHashMD5(byte[] byaData)
        {
            MD5CryptoServiceProvider objMD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            byaData = objMD5CryptoServiceProvider.ComputeHash(byaData);

            string stMd5 = string.Empty;

            for (int i = 0; i < byaData.Length; i++)
                stMd5 += byaData[i].ToString("x2").ToLower();

            return stMd5;
        }

        static public bool ExtractToFile(Assembly objEnsamblado, string nombreDelArchivoDelRecurso, string nombreDelArchivoDeSalida)
        {
            try
            {
                string nombreCompleto = GetFullResourceName(objEnsamblado, nombreDelArchivoDelRecurso);

                if (!string.IsNullOrEmpty(nombreCompleto))
                {
                    Stream objStream = objEnsamblado.GetManifestResourceStream(nombreCompleto);
                    FileStream objArchivoDeSalida = new FileStream(nombreDelArchivoDeSalida, FileMode.Create);
                    int bufferLen = 1024;
                    byte[] buffer = new byte[bufferLen];
                    int bytesRead;
                    do
                    {
                        bytesRead = objStream.Read(buffer, 0, bufferLen);
                        objArchivoDeSalida.Write(buffer, 0, bytesRead);
                    } while (bytesRead != 0);
                    objArchivoDeSalida.Close();

                    return true;
                }
            }
            catch { }

            return false;
        }

        static private string GetFullResourceName(Assembly objEnsamblado, string stNombreDelRecurso)
        {
            foreach (string stItem in objEnsamblado.GetManifestResourceNames())
                if (stItem.EndsWith(stNombreDelRecurso))
                    return stItem;

            return null;
        }

        public static void DeleteTemporalFiles()
        {
            if (File.Exists(Preferences.PathScriptInitializerFile))
                File.Delete(Preferences.PathScriptInitializerFile);
        }
    }
}