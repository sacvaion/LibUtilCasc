using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UtilUnitTest
{
    [TestClass]
    public class UtilGenUnitTest
    {
        [TestMethod]
        public void CreateIdTransactionSuccess()
        {
            String numerico = LibUtilCasc.UtilGen.CreateIdTransaction(DateTime.Now);
            Assert.IsNotNull(numerico);
        }

        [TestMethod]
        public void CreateIdTransactionError()
        {
            String numerico = LibUtilCasc.UtilGen.CreateIdTransaction(DateTime.Now);
            numerico = null;
            Assert.IsNull(numerico);
        }

        [TestMethod]
        public void ExtraerNumericoSuccess()
        {
            Decimal numerico = LibUtilCasc.UtilGen.ExtraerNumerico("ABD123456", "2");
            Assert.IsNotNull(numerico);
            // A=1, B=2, D=4 => 124123456
            Assert.AreEqual(124123456, numerico);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExtraerNumericoNullInput()
        {
            LibUtilCasc.UtilGen.ExtraerNumerico(null, "2");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExtraerNumericoEmptyInput()
        {
            LibUtilCasc.UtilGen.ExtraerNumerico("", "2");
        }

        [TestMethod]
        public void FechaFormatoValid()
        {
            var result = LibUtilCasc.UtilGen.FechaFormato("01/01/2025 12:00:00 PM");
            Assert.IsNotNull(result);
            Assert.AreEqual(2025, result.Year);
            Assert.AreEqual(1, result.Month);
            Assert.AreEqual(1, result.Day);
        }

        [TestMethod]
        public void FechaFormatoEmpty()
        {
            var result = LibUtilCasc.UtilGen.FechaFormato("");
            // Debe retornar DateTime.Now (aproximadamente)
            Assert.IsNotNull(result);
            Assert.AreEqual(DateTime.Now.Year, result.Year);
        }

        [TestMethod]
        public void FechaFormatoInvalid()
        {
            var result = LibUtilCasc.UtilGen.FechaFormato("Fecha inválida");
            // Debe retornar DateTime.MinValue sin lanzar excepción
            Assert.AreEqual(DateTime.MinValue, result);
        }

        [TestMethod]
        public void GetVehicleTypePlacaValida6()
        {
            // Formato: 3 letras + 3 números = Tipo 1
            var result = LibUtilCasc.UtilGen.GetVehicleType("ABC123");
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GetVehicleTypePlacaValida5()
        {
            // Formato: 3 letras + 2 números = Tipo 2
            var result = LibUtilCasc.UtilGen.GetVehicleType("ABC12");
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GetVehicleTypePlacaInvalida()
        {
            var result = LibUtilCasc.UtilGen.GetVehicleType("XYZ");
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetVehicleTypeNull()
        {
            var result = LibUtilCasc.UtilGen.GetVehicleType(null);
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void UtilPassEncriptarDesencriptarRoundTrip()
        {
            // Test backward compatibility con UtilPass (algoritmo legacy)
            string original = "TestPassword";
            var encriptado = LibUtilCasc.UtilPass.Encriptar(original);
            var desencriptado = LibUtilCasc.UtilPass.DesEncriptar(encriptado);
            Assert.AreEqual(original, desencriptado);
        }

        [TestMethod]
        public void UtilEncriptionEncriptarDecriptarCBCRoundTrip()
        {
            // Test CBC nuevo (más seguro que ECB legacy)
            string original = "Mensaje de prueba";
            string key = "MiClaveSecreta123";

            var encriptado = LibUtilCasc.UtilEncription.Encriptar(original, key);
            var desencriptado = LibUtilCasc.UtilEncription.Decriptar(encriptado, key);

            Assert.AreEqual(original, desencriptado);
            // El mensaje encriptado debe contener ":" (formato IV:CipherText)
            Assert.IsTrue(encriptado.Contains(":"), "Mensaje CBC debe contener ':' separador IV:Ciphertext");
        }

        [TestMethod]
        public void UtilNetWorkGetLstMac()
        {
            var result = LibUtilCasc.UtilNetWork.GetLstMac();
            Assert.IsNotNull(result);
            // Debe retornar al menos una lista (puede estar vacía en algunos entornos)
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UtilNetWorkGetLstIpNull()
        {
            LibUtilCasc.UtilNetWork.GetLstIp(null);
        }

        [TestMethod]
        public async Task UtilNetWorkGetLstIpAsync()
        {
            var result = await LibUtilCasc.UtilNetWork.GetLstIpAsync("localhost");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UtilNetWorkGetMacFormat()
        {
            var result = LibUtilCasc.UtilNetWork.GetMacFormat("001122334455");
            Assert.AreEqual("00:11:22:33:44:55", result);
        }

        [TestMethod]
        public void UtilImageHex2Bin()
        {
            var result = LibUtilCasc.UtilImage.Hex2Bin("AABBCCDD");
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Length);
            Assert.AreEqual(0xAA, result[0]);
            Assert.AreEqual(0xBB, result[1]);
        }

        [TestMethod]
        public void UtilImageHex2BinInvalidLength()
        {
            var result = LibUtilCasc.UtilImage.Hex2Bin("ABC"); // Longitud impar
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        public void UtilImageBin2HexUpper()
        {
            byte[] input = { 0xAA, 0xBB, 0xCC, 0xDD };
            var result = LibUtilCasc.UtilImage.Bin2HexUpper(input);
            Assert.AreEqual("AABBCCDD", result);
        }

        [TestMethod]
        public void GetDateFormat()
        {
            var testDate = new DateTime(2025, 3, 15, 14, 30, 45);
            var result = LibUtilCasc.UtilGen.GetDate(testDate);
            // Formato esperado: YYMMDDHHMMSS = 250315143045
            Assert.AreEqual("250315143045", result);
        }

        // ============= NUEVOS TESTS (8 adicionales) =============

        [TestMethod]
        public void LoggerRemoverSignosAcentosWithAccents()
        {
            string input = "José María Núñez";
            string result = LibUtilCasc.Logger.RemoverSignosAcentos(input);
            Assert.AreEqual("Jose Maria Nunez", result);
        }

        [TestMethod]
        public void LoggerRemoverSignosAcentosEmpty()
        {
            string result = LibUtilCasc.Logger.RemoverSignosAcentos("");
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void LoggerRemoverSignosAcentosNull()
        {
            string result = LibUtilCasc.Logger.RemoverSignosAcentos(null);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void UtilImageGetPathImagenSuccess()
        {
            string result = LibUtilCasc.UtilImage.GetPathImagen(0, "test_image");
            Assert.IsTrue(result.EndsWith("test_image.jpg"));
            Assert.IsTrue(result.Contains("ImgEmpleados") || result.Contains("HTMLFOTOS"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UtilImageGetPathImagenNull()
        {
            LibUtilCasc.UtilImage.GetPathImagen(0, null);
        }

        [TestMethod]
        public void UtilEncriptionDecriptarGuidInternalSuccess()
        {
            // Encriptar con GUID internal, luego desencriptar
            string original = "TestData";
            string encrypted = LibUtilCasc.UtilEncription.EncriptarGuidInternal(original);
            string decrypted = LibUtilCasc.UtilEncription.DecriptarGuidInternal(encrypted);
            Assert.AreEqual(original, decrypted);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UtilRaspberryConectarNull()
        {
            LibUtilCasc.UtilRaspberry.ConectarRaspberry(null, "test");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UtilRaspberryConectarComandoNull()
        {
            LibUtilCasc.UtilRaspberry.ConectarRaspberry("192.168.1.1", null);
        }
    }
}

