using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

using General_Desktop_Application.Enumerables;

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

        public static bool DeleteTemporalFiles()
        {
            try
            {
                if (File.Exists(Preferences.PathScriptInitializerFile))
                    File.Delete(Preferences.PathScriptInitializerFile);

                return true;
            }
            catch { }

            return false;
        }

        public static string ConvertToStringUtf8(string stValue)
        {
            try
            {
                byte[] utf8Bytes = Encoding.UTF8.GetBytes(stValue);

                return Encoding.UTF8.GetString(utf8Bytes);
            }
            catch
            {
                return null;
            }
        }

        public static byte[] ConvertirImagenAByte(Image objImagen)
        {
            if (objImagen != null)
            {
                // Reducimos el tamaño de la imágen para evitar desbordamientos de datos.
                CambiarTamanoImagen(objImagen, 100, 100);

                // Obtenemos los datos del logo y realizamos la conversión del logo.
                byte[] logo = null;

                if (objImagen != null)
                {
                    try
                    {
                        MemoryStream memoriaImagen = new MemoryStream();
                        objImagen.Save(memoriaImagen, ImageFormat.Jpeg);

                        logo = new byte[memoriaImagen.Length];
                        memoriaImagen.Position = 0;
                        memoriaImagen.Read(logo, 0, Convert.ToInt32(memoriaImagen.Length));

                        return logo;
                    }
                    catch { }
                }
            }
            return null;
        }

        public static Image ConvertirByteAImagen(byte[] imagen)
        {
            try
            {
                return Image.FromStream(new MemoryStream(imagen));
            }
            catch
            {
                return null;
            }
        }

        public static Image CambiarTamanoImagen(Image objImagen, int ancho, int alto)
        {
            //creamos un bitmap con el nuevo tamaño

            Bitmap vBitmap = new Bitmap(ancho, alto);

            //creamos un graphics tomando como base el nuevo Bitmap

            using (Graphics vGraphics = Graphics.FromImage((Image)vBitmap))
            {

                //especificamos el tipo de transformación, se escoge esta para no perder calidad.

                vGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                //Se dibuja la nueva imagen

                vGraphics.DrawImage(objImagen, 0, 0, ancho, alto);

            }

            //retornamos la nueva imagen

            return (Image)vBitmap;
        }
    }
}