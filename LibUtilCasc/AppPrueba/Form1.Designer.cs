namespace AppPrueba
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.lblResultado = new System.Windows.Forms.Label();
            this.txtResultado = new System.Windows.Forms.TextBox();
            this.btnLimpiar = new System.Windows.Forms.Button();

            this.tabLogger = new System.Windows.Forms.TabPage();
            this.lblLogMsj = new System.Windows.Forms.Label();
            this.txtLogMsj = new System.Windows.Forms.TextBox();
            this.btnLogInfo = new System.Windows.Forms.Button();
            this.btnLogWarn = new System.Windows.Forms.Button();
            this.btnLogError = new System.Windows.Forms.Button();
            this.lblAcentos = new System.Windows.Forms.Label();
            this.txtAcentos = new System.Windows.Forms.TextBox();
            this.btnRemoverAcentos = new System.Windows.Forms.Button();

            this.tabEncriptacion = new System.Windows.Forms.TabPage();
            this.lblEncMsg = new System.Windows.Forms.Label();
            this.txtEncMsg = new System.Windows.Forms.TextBox();
            this.lblEncKey = new System.Windows.Forms.Label();
            this.txtEncKey = new System.Windows.Forms.TextBox();
            this.btnEncriptarCBC = new System.Windows.Forms.Button();
            this.btnDesencriptarCBC = new System.Windows.Forms.Button();
            this.btnEncriptarGUID = new System.Windows.Forms.Button();
            this.btnDesencriptarGUID = new System.Windows.Forms.Button();
            this.lblEncLegacy = new System.Windows.Forms.Label();
            this.btnEncriptarLegacy = new System.Windows.Forms.Button();
            this.btnDesencriptarLegacy = new System.Windows.Forms.Button();
            this.lblEncResult = new System.Windows.Forms.Label();
            this.txtEncResult = new System.Windows.Forms.TextBox();

            this.tabUtilGen = new System.Windows.Forms.TabPage();
            this.lblDoc = new System.Windows.Forms.Label();
            this.txtDoc = new System.Windows.Forms.TextBox();
            this.btnExtraerNumerico = new System.Windows.Forms.Button();
            this.lblPlaca = new System.Windows.Forms.Label();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.btnTipoVehiculo = new System.Windows.Forms.Button();
            this.lblFecha = new System.Windows.Forms.Label();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.btnParsearFecha = new System.Windows.Forms.Button();
            this.btnFechaActual = new System.Windows.Forms.Button();
            this.btnCrearIdTransaccion = new System.Windows.Forms.Button();
            this.btnGetDate = new System.Windows.Forms.Button();
            this.lblKey = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.btnObtenerKey = new System.Windows.Forms.Button();
            this.btnSerializarJSON = new System.Windows.Forms.Button();

            this.tabRed = new System.Windows.Forms.TabPage();
            this.btnObtenerMACs = new System.Windows.Forms.Button();
            this.lblHost = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.btnObtenerIPs = new System.Windows.Forms.Button();
            this.btnObtenerIPsAsync = new System.Windows.Forms.Button();
            this.lblMacFmt = new System.Windows.Forms.Label();
            this.txtMac = new System.Windows.Forms.TextBox();
            this.btnFormattedMAC = new System.Windows.Forms.Button();

            this.tabImagenes = new System.Windows.Forms.TabPage();
            this.lblHex = new System.Windows.Forms.Label();
            this.txtHex = new System.Windows.Forms.TextBox();
            this.btnHex2Bin = new System.Windows.Forms.Button();
            this.btnBin2Hex = new System.Windows.Forms.Button();
            this.lblImgName = new System.Windows.Forms.Label();
            this.txtImgName = new System.Windows.Forms.TextBox();
            this.btnGetPathImagen = new System.Windows.Forms.Button();
            this.btnLoadImagen = new System.Windows.Forms.Button();
            this.btnImg2BytesJPEG = new System.Windows.Forms.Button();
            this.btnImg2BytesPNG = new System.Windows.Forms.Button();
            this.picBox = new System.Windows.Forms.PictureBox();

            this.tabRaspberry = new System.Windows.Forms.TabPage();
            this.lblRaspIP = new System.Windows.Forms.Label();
            this.txtRaspIP = new System.Windows.Forms.TextBox();
            this.lblRaspComando = new System.Windows.Forms.Label();
            this.cmbComando = new System.Windows.Forms.ComboBox();
            this.btnEjecutarComando = new System.Windows.Forms.Button();
            this.btnEjecutarAsync = new System.Windows.Forms.Button();
            this.btnRaspInit = new System.Windows.Forms.Button();
            this.lblRaspWarning = new System.Windows.Forms.Label();

            this.tabControl1.SuspendLayout();
            this.tabLogger.SuspendLayout();
            this.tabEncriptacion.SuspendLayout();
            this.tabUtilGen.SuspendLayout();
            this.tabRed.SuspendLayout();
            this.tabImagenes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.tabRaspberry.SuspendLayout();
            this.SuspendLayout();

            this.tabControl1.Controls.Add(this.tabLogger);
            this.tabControl1.Controls.Add(this.tabEncriptacion);
            this.tabControl1.Controls.Add(this.tabUtilGen);
            this.tabControl1.Controls.Add(this.tabRed);
            this.tabControl1.Controls.Add(this.tabImagenes);
            this.tabControl1.Controls.Add(this.tabRaspberry);
            this.tabControl1.Location = new System.Drawing.Point(10, 10);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(960, 560);
            this.tabControl1.TabIndex = 0;

            // TAB LOGGER
            this.tabLogger.Controls.Add(this.lblLogMsj);
            this.tabLogger.Controls.Add(this.txtLogMsj);
            this.tabLogger.Controls.Add(this.btnLogInfo);
            this.tabLogger.Controls.Add(this.btnLogWarn);
            this.tabLogger.Controls.Add(this.btnLogError);
            this.tabLogger.Controls.Add(this.lblAcentos);
            this.tabLogger.Controls.Add(this.txtAcentos);
            this.tabLogger.Controls.Add(this.btnRemoverAcentos);
            this.tabLogger.Location = new System.Drawing.Point(4, 22);
            this.tabLogger.Name = "tabLogger";
            this.tabLogger.Size = new System.Drawing.Size(952, 534);
            this.tabLogger.TabIndex = 0;
            this.tabLogger.Text = "Logger";
            this.tabLogger.UseVisualStyleBackColor = true;

            this.lblLogMsj.AutoSize = true;
            this.lblLogMsj.Location = new System.Drawing.Point(10, 15);
            this.lblLogMsj.Text = "Mensaje:";
            this.txtLogMsj.Location = new System.Drawing.Point(70, 12);
            this.txtLogMsj.Size = new System.Drawing.Size(350, 20);
            this.btnLogInfo.Location = new System.Drawing.Point(430, 12);
            this.btnLogInfo.Size = new System.Drawing.Size(75, 23);
            this.btnLogInfo.Text = "Info";
            this.btnLogInfo.Click += new System.EventHandler(this.btnLogInfo_Click);
            this.btnLogWarn.Location = new System.Drawing.Point(510, 12);
            this.btnLogWarn.Size = new System.Drawing.Size(75, 23);
            this.btnLogWarn.Text = "Warn";
            this.btnLogWarn.Click += new System.EventHandler(this.btnLogWarn_Click);
            this.btnLogError.Location = new System.Drawing.Point(590, 12);
            this.btnLogError.Size = new System.Drawing.Size(75, 23);
            this.btnLogError.Text = "Error";
            this.btnLogError.Click += new System.EventHandler(this.btnLogError_Click);

            this.lblAcentos.AutoSize = true;
            this.lblAcentos.Location = new System.Drawing.Point(10, 55);
            this.lblAcentos.Text = "Texto con acentos:";
            this.txtAcentos.Location = new System.Drawing.Point(125, 52);
            this.txtAcentos.Size = new System.Drawing.Size(295, 20);
            this.btnRemoverAcentos.Location = new System.Drawing.Point(430, 52);
            this.btnRemoverAcentos.Size = new System.Drawing.Size(120, 23);
            this.btnRemoverAcentos.Text = "Remover Acentos";
            this.btnRemoverAcentos.Click += new System.EventHandler(this.btnRemoverAcentos_Click);

            // TAB ENCRIPTACION
            this.tabEncriptacion.Controls.Add(this.lblEncMsg);
            this.tabEncriptacion.Controls.Add(this.txtEncMsg);
            this.tabEncriptacion.Controls.Add(this.lblEncKey);
            this.tabEncriptacion.Controls.Add(this.txtEncKey);
            this.tabEncriptacion.Controls.Add(this.btnEncriptarCBC);
            this.tabEncriptacion.Controls.Add(this.btnDesencriptarCBC);
            this.tabEncriptacion.Controls.Add(this.btnEncriptarGUID);
            this.tabEncriptacion.Controls.Add(this.btnDesencriptarGUID);
            this.tabEncriptacion.Controls.Add(this.lblEncLegacy);
            this.tabEncriptacion.Controls.Add(this.btnEncriptarLegacy);
            this.tabEncriptacion.Controls.Add(this.btnDesencriptarLegacy);
            this.tabEncriptacion.Controls.Add(this.lblEncResult);
            this.tabEncriptacion.Controls.Add(this.txtEncResult);
            this.tabEncriptacion.Location = new System.Drawing.Point(4, 22);
            this.tabEncriptacion.Name = "tabEncriptacion";
            this.tabEncriptacion.Size = new System.Drawing.Size(952, 534);
            this.tabEncriptacion.TabIndex = 1;
            this.tabEncriptacion.Text = "Encriptación";
            this.tabEncriptacion.UseVisualStyleBackColor = true;

            this.lblEncMsg.AutoSize = true;
            this.lblEncMsg.Location = new System.Drawing.Point(10, 15);
            this.lblEncMsg.Text = "Mensaje:";
            this.txtEncMsg.Location = new System.Drawing.Point(70, 12);
            this.txtEncMsg.Size = new System.Drawing.Size(350, 20);
            this.lblEncKey.AutoSize = true;
            this.lblEncKey.Location = new System.Drawing.Point(10, 45);
            this.lblEncKey.Text = "Clave:";
            this.txtEncKey.Location = new System.Drawing.Point(70, 42);
            this.txtEncKey.Size = new System.Drawing.Size(350, 20);
            this.txtEncKey.Text = "MiClaveSecreta123";
            this.btnEncriptarCBC.Location = new System.Drawing.Point(430, 12);
            this.btnEncriptarCBC.Size = new System.Drawing.Size(110, 23);
            this.btnEncriptarCBC.Text = "Encriptar CBC";
            this.btnEncriptarCBC.Click += new System.EventHandler(this.btnEncriptarCBC_Click);
            this.btnDesencriptarCBC.Location = new System.Drawing.Point(545, 12);
            this.btnDesencriptarCBC.Size = new System.Drawing.Size(130, 23);
            this.btnDesencriptarCBC.Text = "Desencriptar CBC";
            this.btnDesencriptarCBC.Click += new System.EventHandler(this.btnDesencriptarCBC_Click);
            this.btnEncriptarGUID.Location = new System.Drawing.Point(430, 42);
            this.btnEncriptarGUID.Size = new System.Drawing.Size(110, 23);
            this.btnEncriptarGUID.Text = "Encriptar GUID";
            this.btnEncriptarGUID.Click += new System.EventHandler(this.btnEncriptarGUID_Click);
            this.btnDesencriptarGUID.Location = new System.Drawing.Point(545, 42);
            this.btnDesencriptarGUID.Size = new System.Drawing.Size(130, 23);
            this.btnDesencriptarGUID.Text = "Desencriptar GUID";
            this.btnDesencriptarGUID.Click += new System.EventHandler(this.btnDesencriptarGUID_Click);

            this.lblEncLegacy.AutoSize = true;
            this.lblEncLegacy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblEncLegacy.Location = new System.Drawing.Point(10, 85);
            this.lblEncLegacy.Text = "Legacy (UtilPass - Obsoleto):";
            this.btnEncriptarLegacy.Location = new System.Drawing.Point(430, 80);
            this.btnEncriptarLegacy.Size = new System.Drawing.Size(110, 23);
            this.btnEncriptarLegacy.Text = "Encriptar Legacy";
            this.btnEncriptarLegacy.Click += new System.EventHandler(this.btnEncriptarLegacy_Click);
            this.btnDesencriptarLegacy.Location = new System.Drawing.Point(545, 80);
            this.btnDesencriptarLegacy.Size = new System.Drawing.Size(130, 23);
            this.btnDesencriptarLegacy.Text = "Desencriptar Legacy";
            this.btnDesencriptarLegacy.Click += new System.EventHandler(this.btnDesencriptarLegacy_Click);

            this.lblEncResult.AutoSize = true;
            this.lblEncResult.Location = new System.Drawing.Point(10, 125);
            this.lblEncResult.Text = "Resultado:";
            this.txtEncResult.Location = new System.Drawing.Point(10, 145);
            this.txtEncResult.Multiline = true;
            this.txtEncResult.ReadOnly = true;
            this.txtEncResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEncResult.Size = new System.Drawing.Size(930, 170);

            // TAB UTILGEN
            this.tabUtilGen.Controls.Add(this.lblDoc);
            this.tabUtilGen.Controls.Add(this.txtDoc);
            this.tabUtilGen.Controls.Add(this.btnExtraerNumerico);
            this.tabUtilGen.Controls.Add(this.lblPlaca);
            this.tabUtilGen.Controls.Add(this.txtPlaca);
            this.tabUtilGen.Controls.Add(this.btnTipoVehiculo);
            this.tabUtilGen.Controls.Add(this.lblFecha);
            this.tabUtilGen.Controls.Add(this.txtFecha);
            this.tabUtilGen.Controls.Add(this.btnParsearFecha);
            this.tabUtilGen.Controls.Add(this.btnFechaActual);
            this.tabUtilGen.Controls.Add(this.btnCrearIdTransaccion);
            this.tabUtilGen.Controls.Add(this.btnGetDate);
            this.tabUtilGen.Controls.Add(this.lblKey);
            this.tabUtilGen.Controls.Add(this.txtKey);
            this.tabUtilGen.Controls.Add(this.btnObtenerKey);
            this.tabUtilGen.Controls.Add(this.btnSerializarJSON);
            this.tabUtilGen.Location = new System.Drawing.Point(4, 22);
            this.tabUtilGen.Name = "tabUtilGen";
            this.tabUtilGen.Size = new System.Drawing.Size(952, 534);
            this.tabUtilGen.TabIndex = 2;
            this.tabUtilGen.Text = "UtilGen";
            this.tabUtilGen.UseVisualStyleBackColor = true;

            this.lblDoc.AutoSize = true;
            this.lblDoc.Location = new System.Drawing.Point(10, 15);
            this.lblDoc.Text = "Documento:";
            this.txtDoc.Location = new System.Drawing.Point(80, 12);
            this.txtDoc.Size = new System.Drawing.Size(180, 20);
            this.txtDoc.Text = "ABD123456";
            this.btnExtraerNumerico.Location = new System.Drawing.Point(270, 12);
            this.btnExtraerNumerico.Size = new System.Drawing.Size(120, 23);
            this.btnExtraerNumerico.Text = "Extraer Numérico";
            this.btnExtraerNumerico.Click += new System.EventHandler(this.btnExtraerNumerico_Click);

            this.lblPlaca.AutoSize = true;
            this.lblPlaca.Location = new System.Drawing.Point(10, 45);
            this.lblPlaca.Text = "Placa:";
            this.txtPlaca.Location = new System.Drawing.Point(80, 42);
            this.txtPlaca.Size = new System.Drawing.Size(180, 20);
            this.txtPlaca.Text = "ABC123";
            this.btnTipoVehiculo.Location = new System.Drawing.Point(270, 42);
            this.btnTipoVehiculo.Size = new System.Drawing.Size(120, 23);
            this.btnTipoVehiculo.Text = "Tipo Vehículo";
            this.btnTipoVehiculo.Click += new System.EventHandler(this.btnTipoVehiculo_Click);

            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(10, 75);
            this.lblFecha.Text = "Fecha:";
            this.txtFecha.Location = new System.Drawing.Point(80, 72);
            this.txtFecha.Size = new System.Drawing.Size(180, 20);
            this.txtFecha.Text = "01/01/2025 12:00:00 PM";
            this.btnParsearFecha.Location = new System.Drawing.Point(270, 72);
            this.btnParsearFecha.Size = new System.Drawing.Size(120, 23);
            this.btnParsearFecha.Text = "Parsear Fecha";
            this.btnParsearFecha.Click += new System.EventHandler(this.btnParsearFecha_Click);
            this.btnFechaActual.Location = new System.Drawing.Point(400, 72);
            this.btnFechaActual.Size = new System.Drawing.Size(100, 23);
            this.btnFechaActual.Text = "Fecha Actual";
            this.btnFechaActual.Click += new System.EventHandler(this.btnFechaActual_Click);
            this.btnCrearIdTransaccion.Location = new System.Drawing.Point(510, 72);
            this.btnCrearIdTransaccion.Size = new System.Drawing.Size(140, 23);
            this.btnCrearIdTransaccion.Text = "ID Transacción";
            this.btnCrearIdTransaccion.Click += new System.EventHandler(this.btnCrearIdTransaccion_Click);
            this.btnGetDate.Location = new System.Drawing.Point(660, 72);
            this.btnGetDate.Size = new System.Drawing.Size(75, 23);
            this.btnGetDate.Text = "GetDate";
            this.btnGetDate.Click += new System.EventHandler(this.btnGetDate_Click);

            this.lblKey.AutoSize = true;
            this.lblKey.Location = new System.Drawing.Point(10, 115);
            this.lblKey.Text = "Key AppSettings:";
            this.txtKey.Location = new System.Drawing.Point(120, 112);
            this.txtKey.Size = new System.Drawing.Size(140, 20);
            this.txtKey.Text = "ImagePath";
            this.btnObtenerKey.Location = new System.Drawing.Point(270, 112);
            this.btnObtenerKey.Size = new System.Drawing.Size(120, 23);
            this.btnObtenerKey.Text = "Obtener Key";
            this.btnObtenerKey.Click += new System.EventHandler(this.btnObtenerKey_Click);
            this.btnSerializarJSON.Location = new System.Drawing.Point(400, 112);
            this.btnSerializarJSON.Size = new System.Drawing.Size(120, 23);
            this.btnSerializarJSON.Text = "Serializar JSON";
            this.btnSerializarJSON.Click += new System.EventHandler(this.btnSerializarJSON_Click);

            // TAB RED
            this.tabRed.Controls.Add(this.btnObtenerMACs);
            this.tabRed.Controls.Add(this.lblHost);
            this.tabRed.Controls.Add(this.txtHost);
            this.tabRed.Controls.Add(this.btnObtenerIPs);
            this.tabRed.Controls.Add(this.btnObtenerIPsAsync);
            this.tabRed.Controls.Add(this.lblMacFmt);
            this.tabRed.Controls.Add(this.txtMac);
            this.tabRed.Controls.Add(this.btnFormattedMAC);
            this.tabRed.Location = new System.Drawing.Point(4, 22);
            this.tabRed.Name = "tabRed";
            this.tabRed.Size = new System.Drawing.Size(952, 534);
            this.tabRed.TabIndex = 3;
            this.tabRed.Text = "Red";
            this.tabRed.UseVisualStyleBackColor = true;

            this.btnObtenerMACs.Location = new System.Drawing.Point(10, 12);
            this.btnObtenerMACs.Size = new System.Drawing.Size(120, 23);
            this.btnObtenerMACs.Text = "Obtener MACs";
            this.btnObtenerMACs.Click += new System.EventHandler(this.btnObtenerMACs_Click);
            this.lblHost.AutoSize = true;
            this.lblHost.Location = new System.Drawing.Point(10, 50);
            this.lblHost.Text = "Hostname:";
            this.txtHost.Location = new System.Drawing.Point(75, 47);
            this.txtHost.Size = new System.Drawing.Size(150, 20);
            this.txtHost.Text = "localhost";
            this.btnObtenerIPs.Location = new System.Drawing.Point(235, 47);
            this.btnObtenerIPs.Size = new System.Drawing.Size(100, 23);
            this.btnObtenerIPs.Text = "Obtener IPs";
            this.btnObtenerIPs.Click += new System.EventHandler(this.btnObtenerIPs_Click);
            this.btnObtenerIPsAsync.Location = new System.Drawing.Point(345, 47);
            this.btnObtenerIPsAsync.Size = new System.Drawing.Size(130, 23);
            this.btnObtenerIPsAsync.Text = "Obtener IPs Async";
            this.btnObtenerIPsAsync.Click += new System.EventHandler(this.btnObtenerIPsAsync_Click);

            this.lblMacFmt.AutoSize = true;
            this.lblMacFmt.Location = new System.Drawing.Point(10, 90);
            this.lblMacFmt.Text = "MAC (12 hex):";
            this.txtMac.Location = new System.Drawing.Point(110, 87);
            this.txtMac.Size = new System.Drawing.Size(150, 20);
            this.txtMac.Text = "001122334455";
            this.btnFormattedMAC.Location = new System.Drawing.Point(270, 87);
            this.btnFormattedMAC.Size = new System.Drawing.Size(130, 23);
            this.btnFormattedMAC.Text = "Formatear MAC";
            this.btnFormattedMAC.Click += new System.EventHandler(this.btnFormattedMAC_Click);

            // TAB IMAGENES
            this.tabImagenes.Controls.Add(this.lblHex);
            this.tabImagenes.Controls.Add(this.txtHex);
            this.tabImagenes.Controls.Add(this.btnHex2Bin);
            this.tabImagenes.Controls.Add(this.btnBin2Hex);
            this.tabImagenes.Controls.Add(this.lblImgName);
            this.tabImagenes.Controls.Add(this.txtImgName);
            this.tabImagenes.Controls.Add(this.btnGetPathImagen);
            this.tabImagenes.Controls.Add(this.btnLoadImagen);
            this.tabImagenes.Controls.Add(this.btnImg2BytesJPEG);
            this.tabImagenes.Controls.Add(this.btnImg2BytesPNG);
            this.tabImagenes.Controls.Add(this.picBox);
            this.tabImagenes.Location = new System.Drawing.Point(4, 22);
            this.tabImagenes.Name = "tabImagenes";
            this.tabImagenes.Size = new System.Drawing.Size(952, 534);
            this.tabImagenes.TabIndex = 4;
            this.tabImagenes.Text = "Imágenes";
            this.tabImagenes.UseVisualStyleBackColor = true;

            this.lblHex.AutoSize = true;
            this.lblHex.Location = new System.Drawing.Point(10, 15);
            this.lblHex.Text = "Hex:";
            this.txtHex.Location = new System.Drawing.Point(45, 12);
            this.txtHex.Size = new System.Drawing.Size(150, 20);
            this.txtHex.Text = "AABBCCDD";
            this.btnHex2Bin.Location = new System.Drawing.Point(205, 12);
            this.btnHex2Bin.Size = new System.Drawing.Size(110, 23);
            this.btnHex2Bin.Text = "Hex → Bytes";
            this.btnHex2Bin.Click += new System.EventHandler(this.btnHex2Bin_Click);
            this.btnBin2Hex.Location = new System.Drawing.Point(325, 12);
            this.btnBin2Hex.Size = new System.Drawing.Size(130, 23);
            this.btnBin2Hex.Text = "Bytes → Hex Upper";
            this.btnBin2Hex.Click += new System.EventHandler(this.btnBin2Hex_Click);

            this.lblImgName.AutoSize = true;
            this.lblImgName.Location = new System.Drawing.Point(10, 50);
            this.lblImgName.Text = "Nombre imagen:";
            this.txtImgName.Location = new System.Drawing.Point(120, 47);
            this.txtImgName.Size = new System.Drawing.Size(150, 20);
            this.txtImgName.Text = "test_image";
            this.btnGetPathImagen.Location = new System.Drawing.Point(280, 47);
            this.btnGetPathImagen.Size = new System.Drawing.Size(120, 23);
            this.btnGetPathImagen.Text = "Obtener Ruta";
            this.btnGetPathImagen.Click += new System.EventHandler(this.btnGetPathImagen_Click);
            this.btnLoadImagen.Location = new System.Drawing.Point(410, 47);
            this.btnLoadImagen.Size = new System.Drawing.Size(100, 23);
            this.btnLoadImagen.Text = "Cargar Imagen";
            this.btnLoadImagen.Click += new System.EventHandler(this.btnLoadImagen_Click);
            this.btnImg2BytesJPEG.Location = new System.Drawing.Point(520, 47);
            this.btnImg2BytesJPEG.Size = new System.Drawing.Size(150, 23);
            this.btnImg2BytesJPEG.Text = "Image → Bytes (JPEG)";
            this.btnImg2BytesJPEG.Click += new System.EventHandler(this.btnImg2BytesJPEG_Click);
            this.btnImg2BytesPNG.Location = new System.Drawing.Point(680, 47);
            this.btnImg2BytesPNG.Size = new System.Drawing.Size(150, 23);
            this.btnImg2BytesPNG.Text = "Image → Bytes (PNG)";
            this.btnImg2BytesPNG.Click += new System.EventHandler(this.btnImg2BytesPNG_Click);

            this.picBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox.Location = new System.Drawing.Point(10, 90);
            this.picBox.Size = new System.Drawing.Size(300, 250);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBox.TabStop = false;

            // TAB RASPBERRY
            this.tabRaspberry.Controls.Add(this.lblRaspIP);
            this.tabRaspberry.Controls.Add(this.txtRaspIP);
            this.tabRaspberry.Controls.Add(this.lblRaspComando);
            this.tabRaspberry.Controls.Add(this.cmbComando);
            this.tabRaspberry.Controls.Add(this.btnEjecutarComando);
            this.tabRaspberry.Controls.Add(this.btnEjecutarAsync);
            this.tabRaspberry.Controls.Add(this.btnRaspInit);
            this.tabRaspberry.Controls.Add(this.lblRaspWarning);
            this.tabRaspberry.Location = new System.Drawing.Point(4, 22);
            this.tabRaspberry.Name = "tabRaspberry";
            this.tabRaspberry.Size = new System.Drawing.Size(952, 534);
            this.tabRaspberry.TabIndex = 5;
            this.tabRaspberry.Text = "Raspberry";
            this.tabRaspberry.UseVisualStyleBackColor = true;

            this.lblRaspIP.AutoSize = true;
            this.lblRaspIP.Location = new System.Drawing.Point(10, 15);
            this.lblRaspIP.Text = "IP Raspberry:";
            this.txtRaspIP.Location = new System.Drawing.Point(90, 12);
            this.txtRaspIP.Size = new System.Drawing.Size(150, 20);
            this.txtRaspIP.Text = "192.168.0.24";
            this.lblRaspComando.AutoSize = true;
            this.lblRaspComando.Location = new System.Drawing.Point(10, 50);
            this.lblRaspComando.Text = "Comando:";
            this.cmbComando.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComando.Items.AddRange(new object[] { "encenderLed", "apagarLed", "encenderMotor", "apagarMotor" });
            this.cmbComando.Location = new System.Drawing.Point(75, 47);
            this.cmbComando.Size = new System.Drawing.Size(165, 21);
            this.btnEjecutarComando.Location = new System.Drawing.Point(250, 47);
            this.btnEjecutarComando.Size = new System.Drawing.Size(140, 23);
            this.btnEjecutarComando.Text = "Ejecutar Comando";
            this.btnEjecutarComando.Click += new System.EventHandler(this.btnEjecutarComando_Click);
            this.btnEjecutarAsync.Location = new System.Drawing.Point(400, 47);
            this.btnEjecutarAsync.Size = new System.Drawing.Size(130, 23);
            this.btnEjecutarAsync.Text = "Ejecutar Async";
            this.btnEjecutarAsync.Click += new System.EventHandler(this.btnEjecutarAsync_Click);
            this.btnRaspInit.Location = new System.Drawing.Point(540, 47);
            this.btnRaspInit.Size = new System.Drawing.Size(110, 23);
            this.btnRaspInit.Text = "Inicializar (Init)";
            this.btnRaspInit.Click += new System.EventHandler(this.btnRaspInit_Click);
            this.lblRaspWarning.AutoSize = true;
            this.lblRaspWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblRaspWarning.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblRaspWarning.Location = new System.Drawing.Point(10, 90);
            this.lblRaspWarning.Text = "⚠️ Requiere Raspberry Pi conectada a la red con SSH habilitado";

            // RESULT PANEL
            this.lblResultado.AutoSize = true;
            this.lblResultado.Location = new System.Drawing.Point(10, 580);
            this.lblResultado.Text = "Resultado:";
            this.txtResultado.Location = new System.Drawing.Point(10, 600);
            this.txtResultado.Multiline = true;
            this.txtResultado.ReadOnly = true;
            this.txtResultado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResultado.Size = new System.Drawing.Size(920, 80);
            this.btnLimpiar.Location = new System.Drawing.Point(900, 575);
            this.btnLimpiar.Size = new System.Drawing.Size(70, 23);
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);

            // FORM
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 690);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.txtResultado);
            this.Controls.Add(this.btnLimpiar);
            this.Name = "Form1";
            this.Text = "LibUtilCasc - Test UI";
            this.tabControl1.ResumeLayout(false);
            this.tabLogger.ResumeLayout(false);
            this.tabLogger.PerformLayout();
            this.tabEncriptacion.ResumeLayout(false);
            this.tabEncriptacion.PerformLayout();
            this.tabUtilGen.ResumeLayout(false);
            this.tabUtilGen.PerformLayout();
            this.tabRed.ResumeLayout(false);
            this.tabRed.PerformLayout();
            this.tabImagenes.ResumeLayout(false);
            this.tabImagenes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.tabRaspberry.ResumeLayout(false);
            this.tabRaspberry.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.TextBox txtResultado;
        private System.Windows.Forms.Button btnLimpiar;

        private System.Windows.Forms.TabPage tabLogger;
        private System.Windows.Forms.Label lblLogMsj;
        private System.Windows.Forms.TextBox txtLogMsj;
        private System.Windows.Forms.Button btnLogInfo;
        private System.Windows.Forms.Button btnLogWarn;
        private System.Windows.Forms.Button btnLogError;
        private System.Windows.Forms.Label lblAcentos;
        private System.Windows.Forms.TextBox txtAcentos;
        private System.Windows.Forms.Button btnRemoverAcentos;

        private System.Windows.Forms.TabPage tabEncriptacion;
        private System.Windows.Forms.Label lblEncMsg;
        private System.Windows.Forms.TextBox txtEncMsg;
        private System.Windows.Forms.Label lblEncKey;
        private System.Windows.Forms.TextBox txtEncKey;
        private System.Windows.Forms.Button btnEncriptarCBC;
        private System.Windows.Forms.Button btnDesencriptarCBC;
        private System.Windows.Forms.Button btnEncriptarGUID;
        private System.Windows.Forms.Button btnDesencriptarGUID;
        private System.Windows.Forms.Label lblEncLegacy;
        private System.Windows.Forms.Button btnEncriptarLegacy;
        private System.Windows.Forms.Button btnDesencriptarLegacy;
        private System.Windows.Forms.Label lblEncResult;
        private System.Windows.Forms.TextBox txtEncResult;

        private System.Windows.Forms.TabPage tabUtilGen;
        private System.Windows.Forms.Label lblDoc;
        private System.Windows.Forms.TextBox txtDoc;
        private System.Windows.Forms.Button btnExtraerNumerico;
        private System.Windows.Forms.Label lblPlaca;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Button btnTipoVehiculo;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.TextBox txtFecha;
        private System.Windows.Forms.Button btnParsearFecha;
        private System.Windows.Forms.Button btnFechaActual;
        private System.Windows.Forms.Button btnCrearIdTransaccion;
        private System.Windows.Forms.Button btnGetDate;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Button btnObtenerKey;
        private System.Windows.Forms.Button btnSerializarJSON;

        private System.Windows.Forms.TabPage tabRed;
        private System.Windows.Forms.Button btnObtenerMACs;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Button btnObtenerIPs;
        private System.Windows.Forms.Button btnObtenerIPsAsync;
        private System.Windows.Forms.Label lblMacFmt;
        private System.Windows.Forms.TextBox txtMac;
        private System.Windows.Forms.Button btnFormattedMAC;

        private System.Windows.Forms.TabPage tabImagenes;
        private System.Windows.Forms.Label lblHex;
        private System.Windows.Forms.TextBox txtHex;
        private System.Windows.Forms.Button btnHex2Bin;
        private System.Windows.Forms.Button btnBin2Hex;
        private System.Windows.Forms.Label lblImgName;
        private System.Windows.Forms.TextBox txtImgName;
        private System.Windows.Forms.Button btnGetPathImagen;
        private System.Windows.Forms.Button btnLoadImagen;
        private System.Windows.Forms.Button btnImg2BytesJPEG;
        private System.Windows.Forms.Button btnImg2BytesPNG;
        private System.Windows.Forms.PictureBox picBox;

        private System.Windows.Forms.TabPage tabRaspberry;
        private System.Windows.Forms.Label lblRaspIP;
        private System.Windows.Forms.TextBox txtRaspIP;
        private System.Windows.Forms.Label lblRaspComando;
        private System.Windows.Forms.ComboBox cmbComando;
        private System.Windows.Forms.Button btnEjecutarComando;
        private System.Windows.Forms.Button btnEjecutarAsync;
        private System.Windows.Forms.Button btnRaspInit;
        private System.Windows.Forms.Label lblRaspWarning;
    }
}

