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
    public static class Tools
    {
        public static string GetMessageBox(int iErrorMessage)
        {
            switch (iErrorMessage)
            {
                default: return "Unknown application error, you try restart it.";
            }
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

        public static string Encrypt(string stInput)
        {
            if (stInput.Length > 0)
            {
                // Arreglo de bytes donde guardaremos la clave
                byte[] claveArreglo;
                // Arreglo de bytes donde guardaremos el texto que vamos a encriptar
                byte[] arregloACifrar = UTF8Encoding.UTF8.GetBytes(stInput);

                // Se utilizan las clases de encriptación provistas por el Framework (Algoritmo MD5)
                MD5CryptoServiceProvider objHasHDM5 = new MD5CryptoServiceProvider();
                // Se guarda la llave para que se le realice hashing
                claveArreglo = objHasHDM5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Preferences.EncryptedDataAndHashPassword));

                objHasHDM5.Clear();

                // Algoritmo 3DES
                TripleDESCryptoServiceProvider objTDESCrypto = new TripleDESCryptoServiceProvider();

                objTDESCrypto.Key = claveArreglo;
                objTDESCrypto.Mode = CipherMode.ECB;
                objTDESCrypto.Padding = PaddingMode.PKCS7;

                // Se empieza con la transformación de la cadena
                ICryptoTransform objICTransform = objTDESCrypto.CreateEncryptor();

                // Arreglo de bytes donde se guarda la cadena cifrada
                byte[] ArrayResultado = objICTransform.TransformFinalBlock(arregloACifrar, 0, arregloACifrar.Length);

                objTDESCrypto.Clear();

                // Se regresa el resultado en forma de una cadena

                return Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
            }

            return null;
        }

        public static string Decrypt(string stInput)
        {
            if (stInput.Length > 0)
            {
                try
                {
                    byte[] claveArreglo;
                    // Convierte el texto en una secuencia de bytes
                    byte[] arregloADescifrar = Convert.FromBase64String(stInput);

                    // Se llama a las clases que tienen los algoritmos de encriptación se le aplica hashing (Algoritmo MD5)
                    MD5CryptoServiceProvider objHasHDM5 = new MD5CryptoServiceProvider();

                    claveArreglo = objHasHDM5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Preferences.EncryptedDataAndHashPassword));

                    objHasHDM5.Clear();

                    TripleDESCryptoServiceProvider objTDESCrypto = new TripleDESCryptoServiceProvider();

                    objTDESCrypto.Key = claveArreglo;
                    objTDESCrypto.Mode = CipherMode.ECB;
                    objTDESCrypto.Padding = PaddingMode.PKCS7;

                    ICryptoTransform objICTransform = objTDESCrypto.CreateDecryptor();

                    byte[] resultArray = objICTransform.TransformFinalBlock(arregloADescifrar, 0, arregloADescifrar.Length);

                    objTDESCrypto.Clear();

                    // Se regresa en forma de cadena

                    return UTF8Encoding.UTF8.GetString(resultArray);
                }
                catch { }
            }

            return null;
        }

        public static string GetDefaulHash(string stInput)
        {
            return GetHash(GetHash(stInput, new SHA256CryptoServiceProvider()), new SHA512CryptoServiceProvider());
        }

        private static string GetHashMD5(byte[] byaData)
        {
            MD5CryptoServiceProvider objMD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            byaData = objMD5CryptoServiceProvider.ComputeHash(byaData);

            string stMd5 = string.Empty;

            for (int i = 0; i < byaData.Length; i++)
                stMd5 += byaData[i].ToString("x2").ToLower();

            return stMd5;
        }

        private static string GetMD5Hash(string stInput)
        {
            return GetHash(stInput, new MD5CryptoServiceProvider());
        }

        private static string GetSHA256Hash(string stInput)
        {
            return GetHash(stInput, new SHA256CryptoServiceProvider());
        }

        private static string GetSHA512Hash(string stInput)
        {
            return GetHash(stInput, new SHA512CryptoServiceProvider());
        }

        private static string GetHash(string stInput, HashAlgorithm hashAlgorithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(stInput);
            byte[] hashedBytes = hashAlgorithm.ComputeHash(inputBytes);

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
                output.Append(hashedBytes[i].ToString("x2").ToLower());

            return output.ToString();
        }
    }
}