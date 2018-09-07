using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibUtilCasc
{
    public class Logger
    {
        private const string ConSignos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇçÑñ";
        private const string SinSignos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCcNn";

        /// <summary>
        /// Elimina tildes para la impresion en archivo plano
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string RemoverSignosAcentos(string texto)
        {
            var textoSinAcentos = string.Empty;

            foreach (var caracter in texto)
            {
                var indexConAcento = ConSignos.IndexOf(caracter);
                if (indexConAcento > -1)
                    textoSinAcentos = textoSinAcentos + (SinSignos.Substring(indexConAcento, 1));
                else
                    textoSinAcentos = textoSinAcentos + (caracter);
            }
            return textoSinAcentos;
        }

        /// <summary>
        ///  Escribe el msj enviado con la fecha de proceso
        /// </summary>
        /// <param name="msj"></param>
        public static void EscribirMsj(String msj)
        {
            msj = RemoverSignosAcentos(msj);
            Console.WriteLine("[" + DateTime.Now.ToString() + "] --->" + msj);
        }
    }
}
