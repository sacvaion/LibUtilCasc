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
        private byte[] currentImageBytes = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void AppendResult(string message)
        {
            txtResultado.AppendText(DateTime.Now.ToString("HH:mm:ss") + " - " + message + Environment.NewLine);
        }

        // TAB 1: LOGGER
        private void btnLogInfo_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.Info(txtLogMsj.Text);
                AppendResult("✓ Logger.Info: " + txtLogMsj.Text);
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnLogWarn_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.Warn(txtLogMsj.Text);
                AppendResult("✓ Logger.Warn: " + txtLogMsj.Text);
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnLogError_Click(object sender, EventArgs e)
        {
            try
            {
                Logger.Error(txtLogMsj.Text);
                AppendResult("✓ Logger.Error: " + txtLogMsj.Text);
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnRemoverAcentos_Click(object sender, EventArgs e)
        {
            try
            {
                string result = Logger.RemoverSignosAcentos(txtAcentos.Text);
                AppendResult("Entrada: " + txtAcentos.Text);
                AppendResult("Salida: " + result);
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        // TAB 2: ENCRIPTACIÓN
        private void btnEncriptarCBC_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEncMsg.Text) || string.IsNullOrEmpty(txtEncKey.Text))
                {
                    AppendResult("✗ Mensaje y Clave son requeridos");
                    return;
                }
                string encrypted = UtilEncription.Encriptar(txtEncMsg.Text, txtEncKey.Text);
                txtEncResult.Text = encrypted;
                AppendResult("✓ Encriptado (CBC): " + encrypted);
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnDesencriptarCBC_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEncResult.Text) || string.IsNullOrEmpty(txtEncKey.Text))
                {
                    AppendResult("✗ Texto encriptado y Clave son requeridos");
                    return;
                }
                string decrypted = UtilEncription.Decriptar(txtEncResult.Text, txtEncKey.Text);
                AppendResult("✓ Desencriptado (CBC): " + decrypted);
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnEncriptarGUID_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEncMsg.Text))
                {
                    AppendResult("✗ Mensaje requerido");
                    return;
                }
                string encrypted = UtilEncription.EncriptarGuidInternal(txtEncMsg.Text);
                txtEncResult.Text = encrypted;
                AppendResult("✓ Encriptado (GUID): " + encrypted);
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnDesencriptarGUID_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEncResult.Text))
                {
                    AppendResult("✗ Texto encriptado requerido");
                    return;
                }
                string decrypted = UtilEncription.DecriptarGuidInternal(txtEncResult.Text);
                AppendResult("✓ Desencriptado (GUID): " + decrypted);
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnEncriptarLegacy_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEncMsg.Text))
                {
                    AppendResult("✗ Mensaje requerido");
                    return;
                }
                string encrypted = UtilPass.Encriptar(txtEncMsg.Text);
                txtEncResult.Text = encrypted;
                AppendResult("✓ Encriptado (Legacy ECB): " + encrypted);
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnDesencriptarLegacy_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEncResult.Text))
                {
                    AppendResult("✗ Texto encriptado requerido");
                    return;
                }
                string decrypted = UtilPass.DesEncriptar(txtEncResult.Text);
                AppendResult("✓ Desencriptado (Legacy): " + decrypted);
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        // TAB 3: UTILGEN
        private void btnExtraerNumerico_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDoc.Text))
                {
                    AppendResult("✗ Documento requerido");
                    return;
                }
                decimal result = UtilGen.ExtraerNumerico(txtDoc.Text, "2");
                AppendResult("Entrada: " + txtDoc.Text);
                AppendResult("Resultado: " + result);
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnTipoVehiculo_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPlaca.Text))
                {
                    AppendResult("✗ Placa requerida");
                    return;
                }
                int tipo = UtilGen.GetVehicleType(txtPlaca.Text);
                string tipoDesc = tipo == 0 ? "Inválido" : tipo == 1 ? "Tipo 1 (AAA###)" : "Tipo 2 (AAA## o AA##)";
                AppendResult("Placa: " + txtPlaca.Text + " → " + tipoDesc + " (" + tipo + ")");
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnParsearFecha_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime result = UtilGen.FechaFormato(txtFecha.Text);
                AppendResult("Entrada: " + txtFecha.Text);
                AppendResult("Resultado: " + result.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnFechaActual_Click(object sender, EventArgs e)
        {
            try
            {
                string result = UtilGen.DateToString(DateTime.Now);
                AppendResult("Fecha Actual (formateada): " + result);
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnCrearIdTransaccion_Click(object sender, EventArgs e)
        {
            try
            {
                string result = UtilGen.CreateIdTransaction(DateTime.Now);
                AppendResult("ID Transacción: " + result);
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnGetDate_Click(object sender, EventArgs e)
        {
            try
            {
                string result = UtilGen.GetDate(DateTime.Now);
                AppendResult("GetDate (YYMMDDHHMMSS): " + result);
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnObtenerKey_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtKey.Text))
                {
                    AppendResult("✗ Key requerida");
                    return;
                }
                string result = UtilGen.GetKey(txtKey.Text);
                AppendResult("Key: " + txtKey.Text);
                AppendResult("Valor: " + (string.IsNullOrEmpty(result) ? "[vacío]" : result));
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnSerializarJSON_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new { Name = "Carlos", Year = 2025, Company = "UtilCasc" };
                string json = UtilGen.ConvertJson(obj);
                AppendResult("JSON Serializado:");
                AppendResult(json);
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        // TAB 4: RED
        private void btnObtenerMACs_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> macs = UtilNetWork.GetLstMac();
                if (macs == null || macs.Count == 0)
                {
                    AppendResult("No se encontraron direcciones MAC");
                    return;
                }
                AppendResult("MACs encontradas (" + macs.Count + "):");
                foreach (var mac in macs)
                {
                    AppendResult("  - " + mac);
                }
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnObtenerIPs_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtHost.Text))
                {
                    AppendResult("✗ Hostname requerido");
                    return;
                }
                List<string> ips = UtilNetWork.GetLstIp(txtHost.Text);
                if (ips == null || ips.Count == 0)
                {
                    AppendResult("No se encontraron IPs para: " + txtHost.Text);
                    return;
                }
                AppendResult("IPs encontradas para " + txtHost.Text + " (" + ips.Count + "):");
                foreach (var ip in ips)
                {
                    AppendResult("  - " + ip);
                }
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private async void btnObtenerIPsAsync_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtHost.Text))
                {
                    AppendResult("✗ Hostname requerido");
                    return;
                }
                AppendResult("Obteniendo IPs (async)...");
                List<string> ips = await UtilNetWork.GetLstIpAsync(txtHost.Text);
                if (ips == null || ips.Count == 0)
                {
                    AppendResult("No se encontraron IPs para: " + txtHost.Text);
                    return;
                }
                AppendResult("✓ IPs encontradas (async) para " + txtHost.Text + " (" + ips.Count + "):");
                foreach (var ip in ips)
                {
                    AppendResult("  - " + ip);
                }
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnFormattedMAC_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMac.Text))
                {
                    AppendResult("✗ MAC requerida");
                    return;
                }
                string result = UtilNetWork.GetMacFormat(txtMac.Text);
                AppendResult("MAC sin formato: " + txtMac.Text);
                AppendResult("MAC formateada: " + result);
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        // TAB 5: IMÁGENES
        private void btnHex2Bin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtHex.Text))
                {
                    AppendResult("✗ Hexadecimal requerido");
                    return;
                }
                byte[] bytes = UtilImage.Hex2Bin(txtHex.Text);
                AppendResult("Entrada Hex: " + txtHex.Text);
                AppendResult("Bytes (" + bytes.Length + "): [" + string.Join("][", bytes.Select(b => b.ToString("X2"))) + "]");
                currentImageBytes = bytes;
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnBin2Hex_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentImageBytes == null || currentImageBytes.Length == 0)
                {
                    AppendResult("✗ Primero debe convertir Hex → Bytes");
                    return;
                }
                string hex = UtilImage.Bin2HexUpper(currentImageBytes);
                AppendResult("Bytes a Hex Upper: " + hex);
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnGetPathImagen_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtImgName.Text))
                {
                    AppendResult("✗ Nombre de imagen requerido");
                    return;
                }
                string path = UtilImage.GetPathImagen(0, txtImgName.Text);
                AppendResult("Ruta de imagen: " + path);
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnLoadImagen_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp|Todos|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    picBox.Image = Image.FromFile(dialog.FileName);
                    currentImageBytes = System.IO.File.ReadAllBytes(dialog.FileName);
                    AppendResult("✓ Imagen cargada: " + dialog.FileName + " (" + currentImageBytes.Length + " bytes)");
                }
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnImg2BytesJPEG_Click(object sender, EventArgs e)
        {
            try
            {
                if (picBox.Image == null)
                {
                    AppendResult("✗ Debe cargar una imagen primero");
                    return;
                }
                byte[] bytes = UtilImage.ImageToByteArray(picBox.Image);
                AppendResult("Image → Bytes (JPEG): " + bytes.Length + " bytes");
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnImg2BytesPNG_Click(object sender, EventArgs e)
        {
            try
            {
                if (picBox.Image == null)
                {
                    AppendResult("✗ Debe cargar una imagen primero");
                    return;
                }
                byte[] bytes = UtilImage.ImageToByteArrayPNG(picBox.Image);
                AppendResult("Image → Bytes (PNG): " + bytes.Length + " bytes");
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        // TAB 6: RASPBERRY
        private void btnEjecutarComando_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbComando.SelectedIndex < 0)
                {
                    AppendResult("✗ Seleccione un comando");
                    return;
                }
                string resultado = UtilRaspberry.ConectarRaspberry(txtRaspIP.Text, cmbComando.SelectedItem.ToString());
                AppendResult("Comando: " + cmbComando.SelectedItem.ToString());
                AppendResult("Resultado: " + resultado);
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private async void btnEjecutarAsync_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbComando.SelectedIndex < 0)
                {
                    AppendResult("✗ Seleccione un comando");
                    return;
                }
                AppendResult("Ejecutando (async)...");
                string resultado = await UtilRaspberry.ConectarRaspberryAsync(txtRaspIP.Text, cmbComando.SelectedItem.ToString());
                AppendResult("✓ Comando (async): " + cmbComando.SelectedItem.ToString());
                AppendResult("Resultado: " + resultado);
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        private void btnRaspInit_Click(object sender, EventArgs e)
        {
            try
            {
                UtilRaspberry.Init();
                AppendResult("✓ UtilRaspberry inicializado");
            }
            catch (Exception ex)
            {
                AppendResult("✗ Error: " + ex.Message);
            }
        }

        // GLOBAL
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtResultado.Clear();
        }
    }
}
