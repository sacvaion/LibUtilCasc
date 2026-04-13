using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        /// Obtiene el GUID interno desde AppSettings (o usa valor por defecto)
        /// </summary>
        private static string GetGuidInternalDLL()
        {
            string guid = ConfigurationManager.AppSettings["GuidInternalDLL"];
            if (string.IsNullOrEmpty(guid))
                guid = "c2095be7-491f-4aec-99d6-debe6b3ac6db"; // Default value
            return guid;
        }

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
        /// Encrypt (legacy ECB mode - deprecated, kept for backward compatibility)
        /// </summary>
        /// <param name="message"></param>
        /// <param name="algorithm"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [Obsolete("Use EncryptCBC instead. This method uses insecure ECB mode.", false)]
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
        /// Encrypt using CBC mode with random IV (secure)
        /// </summary>
        /// <param name="message">Mensaje a encriptar</param>
        /// <param name="algorithm">Algoritmo simétrico configurado</param>
        /// <param name="key">Llave en formato Base64</param>
        /// <returns>IV_Base64:CipherText_Base64</returns>
        private static string EncryptCBC(string message, SymmetricAlgorithm algorithm, string key)
        {
            algorithm.Key = Convert.FromBase64String(key);
            algorithm.Mode = CipherMode.CBC;
            algorithm.Padding = PaddingMode.PKCS7;

            // Generar IV aleatorio
            algorithm.GenerateIV();
            byte[] iv = algorithm.IV;

            ICryptoTransform encryptor = algorithm.CreateEncryptor(algorithm.Key, iv);
            byte[] data = Encoding.Unicode.GetBytes(message);
            byte[] dataEncrypted = encryptor.TransformFinalBlock(data, 0, data.Length);
            encryptor.Dispose();

            // Retornar IV:CipherText en Base64 para poder leer el IV en decryption
            string ivBase64 = Convert.ToBase64String(iv);
            string cipherTextBase64 = Convert.ToBase64String(dataEncrypted);
            return ivBase64 + ":" + cipherTextBase64;
        }

        /// <summary>
        /// Decrypt (detects CBC vs ECB automatically)
        /// Si el mensaje contiene ":", asume CBC mode (IV:CipherText)
        /// Si no contiene ":", asume ECB mode legacy para backward compatibility
        /// </summary>
        /// <param name="message">Mensaje encriptado</param>
        /// <param name="algorithm">Algoritmo simétrico configurado</param>
        /// <param name="key">Llave en formato Base64</param>
        /// <returns>Mensaje desencriptado</returns>
        private static string Decrypt(string message, SymmetricAlgorithm algorithm, string key)
        {
            // Detectar si es CBC (contiene ":") o ECB legacy
            if (message.Contains(":"))
            {
                return DecryptCBC(message, algorithm, key);
            }
            else
            {
                return DecryptECB(message, algorithm, key);
            }
        }

        /// <summary>
        /// Decrypt ECB mode (legacy, for backward compatibility)
        /// </summary>
        private static string DecryptECB(string message, SymmetricAlgorithm algorithm, string key)
        {
            algorithm.Key = Convert.FromBase64String(key);
            algorithm.Mode = CipherMode.ECB;

            ICryptoTransform decryptor = algorithm.CreateDecryptor();
            byte[] data = Convert.FromBase64String(message);
            byte[] dataDecrypted = decryptor.TransformFinalBlock(data, 0, data.Length);
            decryptor.Dispose();
            return Encoding.Unicode.GetString(dataDecrypted);
        }

        /// <summary>
        /// Decrypt CBC mode with IV extraction
        /// </summary>
        private static string DecryptCBC(string message, SymmetricAlgorithm algorithm, string key)
        {
            try
            {
                // Extraer IV y CipherText
                var parts = message.Split(':');
                if (parts.Length != 2)
                    throw new FormatException("Formato inválido de mensaje encriptado CBC");

                string ivBase64 = parts[0];
                string cipherTextBase64 = parts[1];

                byte[] iv = Convert.FromBase64String(ivBase64);
                byte[] data = Convert.FromBase64String(cipherTextBase64);

                algorithm.Key = Convert.FromBase64String(key);
                algorithm.Mode = CipherMode.CBC;
                algorithm.Padding = PaddingMode.PKCS7;
                algorithm.IV = iv;

                ICryptoTransform decryptor = algorithm.CreateDecryptor(algorithm.Key, iv);
                byte[] dataDecrypted = decryptor.TransformFinalBlock(data, 0, data.Length);
                decryptor.Dispose();
                return Encoding.Unicode.GetString(dataDecrypted);
            }
            catch (Exception ex)
            {
                Logger.Error("Error desencriptando con CBC", ex);
                throw;
            }
        }

        /// <summary>
        /// Encriptar usando CBC mode (seguro).
        /// Los datos encriptados con ECB legacy pueden desencriptarse automáticamente.
        /// </summary>
        /// <param name="Mensaje">Mensaje a encriptar</param>
        /// <param name="Key">Clave para encriptación</param>
        /// <returns>IV_Base64:CipherText_Base64</returns>
        public static string Encriptar(string Mensaje, string Key)
        {
            RijndaelManaged rij = new RijndaelManaged();
            Key = UtilEncription.GenerarKey(rij, 256, Key);
            return UtilEncription.EncryptCBC(Mensaje, rij, Key);
        }

        /// <summary>
        /// Decriptar - detecta automáticamente CBC o ECB legacy
        /// </summary>
        /// <param name="Mensaje">Mensaje encriptado</param>
        /// <param name="Key">Clave para desencriptación</param>
        /// <returns>Mensaje original desencriptado</returns>
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
            return Decriptar(sMensaje, GetGuidInternalDLL());
        }

        /// <summary>
        /// Encripta usando el GUID de la DLL
        /// </summary>
        /// <param name="sMensaje"></param>
        /// <returns></returns>
        public static string EncriptarGuidInternal(string sMensaje)
        {
            return Encriptar(sMensaje, GetGuidInternalDLL());
        }


        /// <summary>
        /// Desencripta usando el GUID de la DLL para SQL Server
        /// </summary>
        /// <param name="sMensaje"></param>
        /// <returns></returns>
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static System.Data.SqlTypes.SqlString DecriptarGuidInternalSQL(string sMensaje)
        {
            return new SqlString(Decriptar(sMensaje, GetGuidInternalDLL()));
        }
    }
}
