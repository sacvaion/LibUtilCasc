using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibUtilCasc
{
    public class UtilPass
    {
        private const string Patron_busqueda = "tvwxyz12345OPQRSTVWXYZabcdlmnñopqrs67890ABCDefghijkEFGHIJKLMNÑ";
        private const string Patron_encripta = "tvwxyz12345OPQRSTVWXYZabcdlmnñopqrs67890ABCDefghijkEFGHIJKLMNÑ";

        public static string Encriptar(string Password)
        {

            if (Password.Length > 0)
            {
                string pass = "";
                int idx;

                for (idx = 0; idx < Password.Length; idx++)
                {
                    pass += EncriptarCaracter(Password.Substring(idx, 1), Password.Length, idx);
                }
                return pass;
            }
            else
                return "";
        }

        public static string EncriptarCaracter(string valor, int tamañoString, int UbicaLetra)
        {
            int indice;
            int Ubicacion = Patron_busqueda.IndexOf(valor);

            if (Ubicacion != -1)
            {
                indice = (Ubicacion + tamañoString + UbicaLetra) % Patron_busqueda.Length;
                return Patron_encripta.Substring(indice, 1);
            }
            return valor;
        }

        public static string DesEncriptar(string Password)
        {
            string pass = "";
            int idx;

            for (idx = 0; idx < Password.Length; idx++)
            {
                pass += DescriptarCaracter(Password.Substring(idx, 1), Password.Length, idx);
            }
            return pass;
        }

        public static string DescriptarCaracter(string valor, int tamañoString, int UbicaLetra)
        {
            int indice;
            int Ubicacion = Patron_busqueda.IndexOf(valor);

            if (Ubicacion != -1)
            {
                if ((Ubicacion - tamañoString - UbicaLetra) > 0)
                {
                    indice = (Ubicacion - tamañoString - UbicaLetra) % Patron_busqueda.Length;
                }
                else
                {
                    indice = Patron_busqueda.Length + ((Ubicacion - tamañoString - UbicaLetra) % Patron_busqueda.Length);
                }
                return Patron_busqueda.Substring(indice, 1);
            }
            return valor;

        }

    }
}
