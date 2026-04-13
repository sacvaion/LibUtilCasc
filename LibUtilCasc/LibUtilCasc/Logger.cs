using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace LibUtilCasc
{
    /// <summary>
    /// Clase de logging que encapsula NLog con utilidades para remover acentos
    /// </summary>
    public class Logger
    {
        private static readonly NLog.Logger _log = LogManager.GetCurrentClassLogger();

        private const string ConSignos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇçÑñ";
        private const string SinSignos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCcNn";

        /// <summary>
        /// Elimina tildes para la impresion en archivo plano (optimizado con StringBuilder)
        /// </summary>
        /// <param name="texto">Texto con posibles acentos</param>
        /// <returns>Texto sin acentos</returns>
        public static string RemoverSignosAcentos(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;

            var textoSinAcentos = new StringBuilder();

            foreach (var caracter in texto)
            {
                var indexConAcento = ConSignos.IndexOf(caracter);
                if (indexConAcento > -1)
                    textoSinAcentos.Append(SinSignos.Substring(indexConAcento, 1));
                else
                    textoSinAcentos.Append(caracter);
            }
            return textoSinAcentos.ToString();
        }

        /// <summary>
        /// Escribe un mensaje de información
        /// </summary>
        /// <param name="msj">Mensaje a registrar</param>
        public static void Info(string msj)
        {
            if (!string.IsNullOrEmpty(msj))
            {
                msj = RemoverSignosAcentos(msj);
                _log.Info(msj);
            }
        }

        /// <summary>
        /// Escribe un mensaje de advertencia
        /// </summary>
        /// <param name="msj">Mensaje a registrar</param>
        public static void Warn(string msj)
        {
            if (!string.IsNullOrEmpty(msj))
            {
                msj = RemoverSignosAcentos(msj);
                _log.Warn(msj);
            }
        }

        /// <summary>
        /// Escribe un mensaje de error
        /// </summary>
        /// <param name="msj">Mensaje a registrar</param>
        /// <param name="ex">Excepción asociada (opcional)</param>
        public static void Error(string msj, Exception ex = null)
        {
            if (!string.IsNullOrEmpty(msj))
            {
                msj = RemoverSignosAcentos(msj);
                if (ex != null)
                    _log.Error(ex, msj);
                else
                    _log.Error(msj);
            }
        }

        /// <summary>
        /// [Obsoleto] Escribe el msj enviado con la fecha de proceso. Use Info() en su lugar.
        /// </summary>
        /// <param name="msj"></param>
        [Obsolete("Use Logger.Info() instead.", false)]
        public static void EscribirMsj(String msj)
        {
            Info(msj);
        }
    }
}
