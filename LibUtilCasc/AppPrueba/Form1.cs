using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibUtilCasc;

namespace AppPrueba
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            lblResultado.Text = "";
            /*
            lblResultado.Text = "";
            decimal x = UtilGen.ExtraerNumerico("123ASD36589", "2");
            lblResultado.Text = "Cadena= 123ASD36589  Funcion Extraer Numercio: " + x.ToString();
            string clave = UtilPass.Encriptar("C4rl0s.2015");
            lblResultado.Text += "\nClave en Claro: C4rl0s.2015  Funcion Encriptar " + clave.ToString();
            string claveEnClaro = UtilPass.DesEncriptar(clave);
            lblResultado.Text += "\nClave Cifrada: "+ clave + "  Funcion DesEncriptar " + claveEnClaro.ToString();
            DateTime Fecha = UtilGen.FechaFormato(UtilGen.DateToString(DateTime.Now));
            lblResultado.Text += "\nFecha Enviada: " + DateTime.Now.ToString() + "  Funcion FechaFormato " + Fecha.ToString();
            
            UtilScanCodigo utilscan = new UtilScanCodigo();
            lblResultado.Text =  utilscan.procesarImg(2);
             */


            UtilNetWork.GetLstMac();

            UtilGen.CreateIdTransaction(DateTime.Now);

            UtilRaspberry.ConectarRaspberry("192.168.0.24", "encenderLed");
            UtilRaspberry.ConectarRaspberry("192.168.0.24", "apagarLed");

        }
    }
}
