using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibUtilCasc
{
    public class UtilGen
    {
        /// <summary>
        /// Retorna el Key dado
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetKey(string key)
        {
            string keySett = System.Configuration.ConfigurationManager.AppSettings[key];
            if (keySett == null)
                keySett = string.Empty;

            return keySett;
        }

        /// <summary>
        /// compara dos fechas y devuelve un string
        /// indicando cual es la mas reciente
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        public static void ComparaFecha(DateTime date1, DateTime date2)
        {
            //date1 = new DateTime(2009, 8, 1, 0, 0, 0);
            //date2 = new DateTime(2009, 8, 1, 12, 0, 0);
            int result = DateTime.Compare(date1, date2);
            string relationship;

            if (result < 0)
                relationship = "Es mas Antigua que";
            else if (result == 0)
                relationship = "Es el mismo que";
            else
                relationship = "Es mas Reciente que";

            Console.WriteLine("{0} {1} {2}", date1, relationship, date2);

        }

        /// <summary>
        /// Funcion para hacer extraccion de alfanumerico
        /// </summary>
        public static decimal ExtraerNumerico(string VNoDocumento, string idTipoDocumento)
        {
            string Alfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            decimal NoDocumento = 0;
            string NoDocumentoV = "";
            decimal aux = 0;
            bool Numerico;
            int posicion = 0;

            //NoDocumentoV = idTipoDocumento;

            foreach (char item in VNoDocumento)
            {
                Numerico = decimal.TryParse(item.ToString(), out aux);
                if (Numerico)
                {
                    NoDocumentoV = NoDocumentoV + item;
                }
                else
                {
                    posicion = 0;
                    posicion = Alfabeto.IndexOf(item, 0) + 1;
                    NoDocumentoV = NoDocumentoV + posicion;
                }
            }
            NoDocumento = Convert.ToDecimal(NoDocumentoV.ToString());
            return NoDocumento;
        }

        /// <summary>
        /// Formatear Fecha a Formato EEUU
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static DateTime FechaFormato(string fecha)
        {
            try
            {
                CultureInfo en = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentCulture = en;
                DateTime dt = DateTime.Now;
                if (string.IsNullOrEmpty(fecha))
                    return dt;                
                else
                {
                    var dateParsed = DateTime.TryParseExact(fecha, "dd/MM/yyyy HH:mm:ss tt", en, DateTimeStyles.None, out dt);
                    if (dateParsed)
                        return dt;
                    else
                        return Convert.ToDateTime(fecha);
                }
            }
            catch
            {
                return Convert.ToDateTime(fecha);
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
        /// Generate unique Id
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string CreateIdTransaction(DateTime date)
        {
            string idTransaction = "";

            try
            {
                string mac = "";
                string dateFormat = "";
                List<string> lstMac = new List<string>();
                lstMac = UtilNetWork.GetLstMac();
                mac = lstMac[0].Replace("-", "");
                mac = lstMac[0].Replace(":", "");
                mac = mac.Substring(mac.Length - 4);

                dateFormat = GetDate(date);
                idTransaction = mac + Convert.ToInt64(dateFormat).ToString("X");
            }
            catch
            {
            }
            return idTransaction;
        }

        /// <summary>
        /// Return Date in format AAMMDDHHmmss
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetDate(DateTime date)
        {
            string dateFormat = "";
            try
            {
                dateFormat = date.Year.ToString().Substring(2) + date.Month.ToString().PadLeft(2,'0') + date.Day.ToString().PadLeft(2, '0') + date.Hour.ToString().PadLeft(2, '0') + date.Minute.ToString().PadLeft(2, '0') + date.Second.ToString().PadLeft(2, '0');
            }
            catch
            {
               
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
    }
}
