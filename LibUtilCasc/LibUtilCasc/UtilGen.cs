using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace LibUtilCasc
{
    public class UtilGen
    {
        /// <summary>
        /// Retorna el valor de una clave desde el archivo de configuración
        /// </summary>
        /// <param name="key">Clave a buscar en AppSettings</param>
        /// <returns>Valor de la configuración, o string.Empty si no existe</returns>
        /// <exception cref="ArgumentNullException">Si key es null</exception>
        public static string GetKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key), "La clave no puede ser nula o vacía");

            string keySett = System.Configuration.ConfigurationManager.AppSettings[key];

            if (keySett == null) {
                keySett = string.Empty;
            }

            return keySett;
        }

        /// <summary>
        /// Extrae números y convierte letras a su posición en el alfabeto
        /// Ej: "ABD123456" → "123245123456" (A=1, B=2, D=4)
        /// </summary>
        /// <param name="VNoDocumento">Cadena alfanumérica a procesar</param>
        /// <param name="idTipoDocumento">Tipo de documento (no usado actualmente)</param>
        /// <returns>Número decimal resultante de la extracción</returns>
        /// <exception cref="ArgumentNullException">Si VNoDocumento es null</exception>
        /// <exception cref="FormatException">Si el resultado no puede convertirse a decimal</exception>
        public static decimal ExtraerNumerico(string VNoDocumento, string idTipoDocumento)
        {
            if (string.IsNullOrEmpty(VNoDocumento))
                throw new ArgumentNullException(nameof(VNoDocumento), "El documento no puede ser nulo o vacío");

            string Alfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            decimal NoDocumento = 0;
            string NoDocumentoV = "";
            decimal aux = 0;
            bool Numerico;
            int posicion = 0;

            foreach (char item in VNoDocumento)
            {
                Numerico = decimal.TryParse(item.ToString(), out aux);
                if (Numerico)
                {
                    NoDocumentoV = NoDocumentoV + item;
                }
                else
                {
                    posicion = Alfabeto.IndexOf(item, 0) + 1;
                    if (posicion > 0)
                        NoDocumentoV = NoDocumentoV + posicion;
                }
            }

            try
            {
                NoDocumento = Convert.ToDecimal(NoDocumentoV.ToString());
            }
            catch (FormatException ex)
            {
                Logger.Error($"No se pudo convertir '{NoDocumentoV}' a decimal", ex);
                throw;
            }
            return NoDocumento;
        }

        /// <summary>
        /// Parsea una fecha en formato "dd/MM/yyyy HH:mm:ss tt" o general
        /// </summary>
        /// <param name="fecha">Cadena de fecha a parsear</param>
        /// <returns>DateTime parseado, o DateTime.Now si está vacío, o DateTime.MinValue si hay error</returns>
        public static DateTime FechaFormato(string fecha)
        {
            try
            {
                CultureInfo en = new CultureInfo("en-US");
                DateTime dt = DateTime.Now;

                if (string.IsNullOrEmpty(fecha))
                    return dt;

                // Intentar con formato específico primero
                if (DateTime.TryParseExact(fecha, "dd/MM/yyyy HH:mm:ss tt", en, DateTimeStyles.None, out dt))
                    return dt;

                // Intentar con formato general
                if (DateTime.TryParse(fecha, en, DateTimeStyles.None, out dt))
                    return dt;

                // Si falla, loguear y retornar valor por defecto
                Logger.Warn($"No se pudo parsear la fecha: '{fecha}'");
                return DateTime.MinValue;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error parseando fecha: '{fecha}'", ex);
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Format MM/dd/YYYY HH:mm:ss tt
        /// </summary>
        /// <returns></returns>
        public static string DateToString(DateTime date)
        {
            return date.ToString("MM/dd/yyyy HH:mm:ss tt");
        }

        /// <summary>
        /// Genera un ID único basado en la MAC address y la fecha/hora
        /// Formato: XXXX + fecha en hexadecimal (últimos 4 caracteres de MAC + fecha convertida a hex)
        /// </summary>
        /// <param name="date">Fecha/hora para el ID</param>
        /// <returns>ID de transacción única, o string.Empty si hay error</returns>
        public static string CreateIdTransaction(DateTime date)
        {
            string idTransaction = "";

            try
            {
                string mac = "";
                string dateFormat = "";
                List<string> lstMac = UtilNetWork.GetLstMac();

                if (lstMac == null || lstMac.Count == 0)
                {
                    Logger.Warn("No se encontró ninguna dirección MAC en el sistema");
                    return idTransaction;
                }

                // Limpiar la MAC: remover "-" y ":" en una sola operación
                mac = lstMac[0].Replace("-", "").Replace(":", "");
                mac = mac.Substring(mac.Length - 4);

                dateFormat = GetDate(date);
                if (!string.IsNullOrEmpty(dateFormat))
                {
                    idTransaction = mac + Convert.ToInt64(dateFormat).ToString("X");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error creando ID de transacción", ex);
            }
            return idTransaction;
        }

        /// <summary>
        /// Retorna la fecha en formato YYMMDDHHMMSS (últimos 2 dígitos del año)
        /// </summary>
        /// <param name="date">Fecha a formatear</param>
        /// <returns>Fecha formateada como string YYMMDDHHMMSS</returns>
        public static string GetDate(DateTime date)
        {
            string dateFormat = "";
            try
            {
                dateFormat = date.Year.ToString().Substring(2) +
                             date.Month.ToString().PadLeft(2, '0') +
                             date.Day.ToString().PadLeft(2, '0') +
                             date.Hour.ToString().PadLeft(2, '0') +
                             date.Minute.ToString().PadLeft(2, '0') +
                             date.Second.ToString().PadLeft(2, '0');
            }
            catch (Exception ex)
            {
                Logger.Error($"Error formateando fecha: {date}", ex);
            }
            return dateFormat;
        }

        /// <summary>
        /// Converter to Object to Json 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ConvertJson(object obj)
        {
            try
            {
                string json = JsonConvert.SerializeObject(obj,
                    Formatting.None,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        Formatting = Formatting.Indented
                    });
                return json;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        
        /// <summary>
        /// Identifica el tipo de placa de vehículo según su formato.
        /// Tipo 1: AAA###  (3 letras + 3 números) = longitud 6
        /// Tipo 2: AAA##L  (3 letras + 2 números + 1 letra) o AA## = longitud 5
        /// Tipo 0: Formato inválido
        /// </summary>
        /// <param name="sPlate">Número de placa sin formato (solo caracteres alfanuméricos)</param>
        /// <returns>0 = inválido, 1 = Tipo AAA###, 2 = Tipo AAA##L o AA##</returns>
        public static int GetVehicleType(string sPlate)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sPlate))
                    return 0;

                string sInput = sPlate.ToUpper().Trim();

                // Solo longitudes 5 o 6 son válidas
                if (sInput.Length != 5 && sInput.Length != 6)
                    return 0;

                // Primeros 3 caracteres deben ser letras (AAA)
                if (!Regex.IsMatch(sInput.Substring(0, 3), @"^[A-Z]{3}$"))
                    return 0;

                // Longitud 6: AAA + 3 números
                if (sInput.Length == 6)
                {
                    if (Regex.IsMatch(sInput.Substring(3, 3), @"^\d{3}$"))
                        return 1; // Tipo AAA###
                    else
                        return 0; // No coincide
                }

                // Longitud 5: AAA + 2 números (+ letra opcional)
                if (sInput.Length == 5)
                {
                    if (Regex.IsMatch(sInput.Substring(3, 2), @"^\d{2}$"))
                        return 2; // Tipo AA## o AAA##
                    else
                        return 0; // No coincide
                }

                return 0;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error identificando tipo de placa: '{sPlate}'", ex);
                return 0;
            }
        }
    }
}
