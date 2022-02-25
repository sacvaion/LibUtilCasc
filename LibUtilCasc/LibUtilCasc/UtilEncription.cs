using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibUtilCasc
{
    public static class UtilEncription
    {

        /// <summary>
        /// Guid de la DLL
        /// </summary>
        private const string GuidInternalDLL = "c2095be7-491f-4aec-99d6-debe6b3ac6db";

        /// <summary>
        /// Genera Key
        /// </summary>
        /// <param name="algorithm"></param>
        /// <param name="keySize"></param>
        /// <param name="stringKey"></param>
        /// <returns></returns>
        private static string GenerarKey(SymmetricAlgorithm algorithm, int keySize, string stringKey)
        {
            byte[] KeyString;
            if (stringKey.Length < 16)
                // de ser así, completamos la cadena hasta esos 16 bytes.
                stringKey = stringKey.PadRight(16);
            else if (stringKey.Length > 16)//longitud es mayor a 16 bytes,
                // truncamos la cadena dejándola en 16 bytes.
                stringKey = stringKey.Substring(0, 16);
            // utilizando los métodos del namespace System.Text, 
            // convertimos la cadena de caracteres en un arreglo de bytes
            // mediante el método GetBytes() del sistema de codificación UTF.
            KeyString = Encoding.UTF8.GetBytes(stringKey);

            if (algorithm.ValidKeySize(keySize))
            {
                algorithm.KeySize = keySize;
                algorithm.Key = KeyString;
                //algorithm.GenerateKey();
                return Convert.ToBase64String(algorithm.Key);
            }
            else
                throw new ArgumentException("Invalid key size");
        }

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="message"></param>
        /// <param name="algorithm"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string Encrypt(string message, SymmetricAlgorithm algorithm, string key)
        {
            algorithm.Key = Convert.FromBase64String(key);
            algorithm.Mode = CipherMode.ECB;

            ICryptoTransform encryptor = algorithm.CreateEncryptor();
            byte[] data = Encoding.Unicode.GetBytes(message);
            byte[] dataEncrypted = encryptor.TransformFinalBlock(data, 0, data.Length);
            encryptor = null;
            return Convert.ToBase64String(dataEncrypted);
        }

        /// <summary>
        /// Decrypt
        /// </summary>
        /// <param name="message"></param>
        /// <param name="algorithm"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string Decrypt(string message, SymmetricAlgorithm algorithm, string key)
        {
            algorithm.Key = Convert.FromBase64String(key);
            algorithm.Mode = CipherMode.ECB;

            ICryptoTransform decryptor = algorithm.CreateDecryptor();
            byte[] data = Convert.FromBase64String(message);
            byte[] dataDecrypted = decryptor.TransformFinalBlock(data, 0, data.Length);
            decryptor = null;
            return Encoding.Unicode.GetString(dataDecrypted);
        }

        /// <summary>
        /// Encriptar
        /// </summary>
        /// <param name="Mensaje"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string Encriptar(string Mensaje, string Key)
        {
            RijndaelManaged rij = new RijndaelManaged();
            Key = UtilEncription.GenerarKey(rij, 256, Key);
            return UtilEncription.Encrypt(Mensaje, rij, Key);
        }

        /// <summary>
        /// Decriptar
        /// </summary>
        /// <param name="Mensaje"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string Decriptar(string Mensaje, string Key)
        {
            RijndaelManaged rij = new RijndaelManaged();
            Key = UtilEncription.GenerarKey(rij, 256, Key);
            return UtilEncription.Decrypt(Mensaje, rij, Key);
        }

        /// <summary>
        /// Desencripta usando el GUID de la DLL
        /// </summary>
        /// <param name="sMensaje"></param>
        /// <returns></returns>
        public static string DecriptarGuidInternal(string sMensaje)
        {
            return Decriptar(sMensaje, GuidInternalDLL);
        }

        /// <summary>
        /// Encripta usando el GUID de la DLL
        /// </summary>
        /// <param name="sMensaje"></param>
        /// <returns></returns>
        public static string EncriptarGuidInternal(string sMensaje)
        {
            return Encriptar(sMensaje, GuidInternalDLL);
        }


        /// <summary>
        /// Desencripta usando el GUID de la DLL para SQL Server
        /// </summary>
        /// <param name="sMensaje"></param>
        /// <returns></returns>
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static System.Data.SqlTypes.SqlString DecriptarGuidInternalSQL(string sMensaje)
        {
            return new SqlString(Decriptar(sMensaje, GuidInternalDLL));
        }
    }
}
