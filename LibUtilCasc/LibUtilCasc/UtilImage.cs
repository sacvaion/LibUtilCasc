using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace LibUtilCasc
{
    /// <summary>
    /// Utilidades para manipulación de imágenes y conversión entre formatos (Base64, Hex, Bytes)
    /// </summary>
    public class UtilImage
    {
        /// <summary>
        /// Convierte una cadena hexadecimal a array de bytes
        /// </summary>
        /// <param name="hex">Cadena hexadecimal (sin espacios, longitud par)</param>
        /// <returns>Array de bytes decodificado, o array vacío si hay error</returns>
        /// <exception cref="ArgumentNullException">Si hex es null</exception>
        public static byte[] Hex2Bin(String hex)
        {
            if (string.IsNullOrEmpty(hex))
                return new byte[0];

            if (hex.Length % 2 != 0)
            {
                Logger.Warn($"Longitud de hex inválida (no par): {hex.Length}");
                return new byte[0];
            }

            byte[] bytes = new byte[0];
            try
            {
                bytes = new byte[hex.Length / 2];
                for (int i = 0; i < hex.Length; i += 2)
                {
                    bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
                }
                return bytes;
            }
            catch (Exception ex)
            {
                Logger.Error($"Error convirtiendo hex a bin: {hex}", ex);
                return new byte[0];
            }
        }

        /// <summary>
        /// Convierte una cadena Base64 a objeto Image
        /// </summary>
        /// <param name="base64String">Cadena Base64 con datos de imagen</param>
        /// <returns>Objeto Image decodificado</returns>
        /// <exception cref="ArgumentNullException">Si base64String es null o vacío</exception>
        /// <exception cref="FormatException">Si la cadena no es Base64 válido</exception>
        public static Image Base64ToImage(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
                throw new ArgumentNullException(nameof(base64String), "La cadena Base64 no puede ser nula o vacía");

            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    Image image = Image.FromStream(ms, true);
                    return image;
                }
            }
            catch (FormatException ex)
            {
                Logger.Error("Formato Base64 inválido", ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error("Error convirtiendo Base64 a Image", ex);
                throw;
            }
        }

        /// <summary>
        /// Convierte un array de bytes a objeto Image
        /// </summary>
        /// <param name="imageBytes">Array de bytes con datos de imagen</param>
        /// <returns>Objeto Image decodificado</returns>
        /// <exception cref="ArgumentNullException">Si imageBytes es null o vacío</exception>
        public static Image BytesToImage(byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0)
                throw new ArgumentNullException(nameof(imageBytes), "El array de bytes no puede ser nulo o vacío");

            try
            {
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    Image image = Image.FromStream(ms, true);
                    return image;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error convirtiendo bytes a Image", ex);
                throw;
            }
        }

        /// <summary>
        /// Convierte array de bytes a cadena hexadecimal en mayúsculas
        /// </summary>
        /// <param name="bytes">Array de bytes a convertir</param>
        /// <returns>Cadena hexadecimal en mayúsculas (ej: ABCDEF123456)</returns>
        /// <exception cref="ArgumentNullException">Si bytes es null</exception>
        public static String Bin2HexUpper(byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
                hex.Append(bytes[i].ToString("X2"));

            return hex.ToString();
        }

        /// <summary>
        /// Convierte array de bytes a cadena hexadecimal en minúsculas
        /// </summary>
        /// <param name="bytes">Array de bytes a convertir</param>
        /// <returns>Cadena hexadecimal en minúsculas sin guiones</returns>
        /// <exception cref="ArgumentNullException">Si bytes es null</exception>
        public static String Bin2HexLower(byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
                hex.AppendFormat("{0:x2}", b);

            return hex.ToString().Replace("-", "");
        }

        /// <summary>
        /// Convierte array de bytes a cadena hexadecimal en minúsculas
        /// Alias de Bin2HexLower para compatibilidad
        /// </summary>
        /// <param name="ba">Array de bytes a convertir</param>
        /// <returns>Cadena hexadecimal en minúsculas</returns>
        public static string ByteArrayToString(byte[] ba)
        {
            return Bin2HexLower(ba);
        }

        /// <summary>
        /// Obtiene la ruta de una imagen desde AppSettings.
        /// Clave: "ImagePath" (default: "D:\HTMLFOTOS\ImgEmpleados")
        /// </summary>
        /// <param name="TipoFoto">Tipo de foto (no usado actualmente)</param>
        /// <param name="NombreImagen">Nombre de la imagen sin extensión</param>
        /// <returns>Ruta completa: {ImagePath}\{NombreImagen}.jpg</returns>
        /// <exception cref="ArgumentNullException">Si NombreImagen es nulo o vacío</exception>
        public static string GetPathImagen(int TipoFoto, string NombreImagen)
        {
            if (string.IsNullOrEmpty(NombreImagen))
                throw new ArgumentNullException(nameof(NombreImagen), "Nombre de imagen no puede ser nulo o vacío");

            try
            {
                string basePath = ConfigurationManager.AppSettings["ImagePath"]
                    ?? "D:\\HTMLFOTOS\\ImgEmpleados";

                if (string.IsNullOrEmpty(basePath))
                    basePath = "D:\\HTMLFOTOS\\ImgEmpleados";

                return System.IO.Path.Combine(basePath, NombreImagen + ".jpg");
            }
            catch (Exception ex)
            {
                Logger.Error($"Error obteniendo ruta de imagen: {NombreImagen}", ex);
                throw;
            }
        }

        /// <summary>
        /// Convierte array de bytes a objeto Bitmap
        /// </summary>
        /// <param name="ImgBytes">Array de bytes con datos de imagen</param>
        /// <returns>Objeto Bitmap</returns>
        /// <exception cref="ArgumentNullException">Si ImgBytes es null</exception>
        public static Image BytesToBitmap(Byte[] ImgBytes)
        {
            if (ImgBytes == null || ImgBytes.Length == 0)
                throw new ArgumentNullException(nameof(ImgBytes), "El array de bytes no puede ser nulo o vacío");

            try
            {
                MemoryStream ms = new MemoryStream(ImgBytes);
                Bitmap imagen = new Bitmap(ms);
                return imagen;
            }
            catch (Exception ex)
            {
                Logger.Error("Error convirtiendo bytes a Bitmap", ex);
                throw;
            }
        }

        /// <summary>
        /// Convierte Image a array de bytes en formato JPEG
        /// </summary>
        /// <param name="imageIn">Objeto Image a convertir</param>
        /// <returns>Array de bytes en formato JPEG</returns>
        /// <exception cref="ArgumentNullException">Si imageIn es null</exception>
        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            if (imageIn == null)
                throw new ArgumentNullException(nameof(imageIn));

            try
            {
                using (var ms = new MemoryStream())
                {
                    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error convirtiendo Image a byte array", ex);
                throw;
            }
        }

        /// <summary>
        /// Convierte Image a array de bytes en formato PNG usando archivo temporal
        /// </summary>
        /// <param name="img">Objeto Image a convertir</param>
        /// <returns>Array de bytes en formato PNG</returns>
        /// <exception cref="ArgumentNullException">Si img es null</exception>
        public static byte[] ImageToByteArrayPNG(Image img)
        {
            if (img == null)
                throw new ArgumentNullException(nameof(img));

            string sTemp = Path.GetTempFileName();
            try
            {
                using (FileStream fs = new FileStream(sTemp, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    img.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                    fs.Position = 0;

                    int imgLength = Convert.ToInt32(fs.Length);
                    byte[] bytes = new byte[imgLength];
                    fs.Read(bytes, 0, imgLength);
                    return bytes;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Error convirtiendo Image a bytes (PNG) usando archivo temp: {sTemp}", ex);
                throw;
            }
            finally
            {
                // Limpiar archivo temporal
                try
                {
                    if (File.Exists(sTemp))
                        File.Delete(sTemp);
                }
                catch { }
            }
        }

        /// <summary>
        /// Combina múltiples imágenes en una sola, apiladas verticalmente
        /// </summary>
        /// <param name="files">Array de rutas de archivos de imagen</param>
        /// <returns>Bitmap con todas las imágenes apiladas verticalmente (fondo negro)</returns>
        /// <exception cref="ArgumentNullException">Si files es null o vacío</exception>
        public static System.Drawing.Bitmap CombineBitmap(string[] files)
        {
            if (files == null || files.Length == 0)
                throw new ArgumentNullException(nameof(files), "Array de archivos no puede ser nulo o vacío");

            List<System.Drawing.Bitmap> images = new List<System.Drawing.Bitmap>();
            System.Drawing.Bitmap finalImage = null;

            try
            {
                int width = 0;
                int height = 0;

                // Cargar todas las imágenes y calcular dimensiones
                foreach (string imagePath in files)
                {
                    if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
                    {
                        Logger.Warn($"Archivo de imagen no encontrado: {imagePath}");
                        continue;
                    }

                    try
                    {
                        System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(imagePath);
                        height += bitmap.Height;
                        width = bitmap.Width > width ? bitmap.Width : width;
                        images.Add(bitmap);
                    }
                    catch (Exception ex)
                    {
                        Logger.Warn($"Error cargando imagen: {imagePath}");
                    }
                }

                if (images.Count == 0)
                    throw new InvalidOperationException("No se pudieron cargar imágenes válidas");

                // Crear bitmap final
                finalImage = new System.Drawing.Bitmap(width, height);

                // Dibujar todas las imágenes apiladas verticalmente
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(finalImage))
                {
                    g.Clear(System.Drawing.Color.Black);

                    int heightOffset = 0;
                    foreach (System.Drawing.Bitmap image in images)
                    {
                        g.DrawImage(image,
                          new System.Drawing.Rectangle(0, heightOffset, image.Width, image.Height));
                        heightOffset += image.Height;
                    }
                }

                return finalImage;
            }
            catch (Exception ex)
            {
                Logger.Error("Error combinando bitmaps", ex);
                if (finalImage != null)
                    finalImage.Dispose();
                throw;
            }
            finally
            {
                // Limpiar memoria
                foreach (System.Drawing.Bitmap image in images)
                {
                    image.Dispose();
                }
            }
        }

        // Métodos obsoletos para backward compatibility
        /// <summary>
        /// [Obsoleto] Use Hex2Bin en su lugar
        /// </summary>
        [Obsolete("Use Hex2Bin instead.", false)]
        public byte[] hex2bin(String hex) => Hex2Bin(hex);

        /// <summary>
        /// [Obsoleto] Use Base64ToImage en su lugar
        /// </summary>
        [Obsolete("Use Base64ToImage (static method) instead.", false)]
        public Image Base64ToImage_Old(string base64String) => Base64ToImage(base64String);

        /// <summary>
        /// [Obsoleto] Use BytesToImage en su lugar
        /// </summary>
        [Obsolete("Use BytesToImage instead.", false)]
        public Image Base64ToImage2(byte[] imageBytes) => BytesToImage(imageBytes);

        /// <summary>
        /// [Obsoleto] Use Bin2HexUpper en su lugar
        /// </summary>
        [Obsolete("Use Bin2HexUpper instead.", false)]
        public String bin2Hex1(byte[] bytes) => Bin2HexUpper(bytes);

        /// <summary>
        /// [Obsoleto] Use Bin2HexLower en su lugar
        /// </summary>
        [Obsolete("Use Bin2HexLower instead.", false)]
        public String bin2hex2(byte[] ba) => Bin2HexLower(ba);

        /// <summary>
        /// [Obsoleto] Use BytesToBitmap en su lugar
        /// </summary>
        [Obsolete("Use BytesToBitmap instead.", false)]
        public Image Bytes_A_Imagen(Byte[] ImgBytes) => BytesToBitmap(ImgBytes);

        /// <summary>
        /// [Obsoleto] Use ImageToByteArray en su lugar
        /// </summary>
        [Obsolete("Use ImageToByteArray instead.", false)]
        public byte[] imageToByteArray(System.Drawing.Image imageIn) => ImageToByteArray(imageIn);

        /// <summary>
        /// [Obsoleto] Use ImageToByteArrayPNG en su lugar
        /// </summary>
        [Obsolete("Use ImageToByteArrayPNG instead.", false)]
        public byte[] Convertir_Imagen_Bytes(Image img) => ImageToByteArrayPNG(img);
    }
}
