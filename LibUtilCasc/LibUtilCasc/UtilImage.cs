using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace LibUtilCasc
{
    public class UtilImage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public byte[] hex2bin(String hex)
        {

            int NumberChars = hex.Length;
            byte[] bytes = new byte[0];
            try
            {
                bytes = new byte[NumberChars / 2];
                for (int i = 0; i < NumberChars; i += 2)
                {
                    bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
                }
                return bytes;
            }
            catch (Exception)
            {
                bytes = new byte[0];
                return bytes;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            //byte[] imageBytes = Encoding.ASCII.GetBytes(base64String);

            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageBytes"></param>
        /// <returns></returns>
        public Image Base64ToImage2(byte[] imageBytes)
        {
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public String bin2Hex1(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder(bytes.Length * 2);

            for (int i = 0; i < bytes.Length; i++) hex.Append(bytes[i].ToString("X2"));

            return hex.ToString();
        }

        /// <summary>
        /// bin2hex
        /// </summary>
        /// <param name="bindata"></param>
        /// <returns></returns>
        public String bin2hex2(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString().Replace("-", "");
        }

        //Convertir de Array a Hexa
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ba"></param>
        /// <returns></returns>
        public string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        /// <summary>
        /// Obtengo el Path de la Imagen
        /// </summary>
        /// <param name="TipoFoto"></param>
        /// <param name="NombreImagen"></param>
        /// <returns></returns>
        public string GetPathImagen(int TipoFoto, string NombreImagen)
        {
            return "D:\\HTMLFOTOS\\ImgEmpleados" + NombreImagen + ".jpg";
        }

        /// <summary>
        /// Obtengo la imagen del Arreglo de Byte
        /// </summary>
        /// <param name="ImgBytes"></param>
        /// <returns></returns>
        public Image Bytes_A_Imagen(Byte[] ImgBytes)
        {
            Bitmap imagen = null;
            Byte[] bytes = (Byte[])(ImgBytes);
            MemoryStream ms = new MemoryStream(bytes);
            imagen = new Bitmap(ms);
            return imagen;
        }


        //Extraer de System.Drawing.Image el mapa de bytes
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageIn"></param>
        /// <returns></returns>
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        //Extraer de Image el mapa de bytes
        /// <summary>
        /// 
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public byte[] Convertir_Imagen_Bytes(Image img)
        {
            string sTemp = Path.GetTempFileName();
            FileStream fs = new FileStream(sTemp, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            img.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
            fs.Position = 0;

            int imgLength = Convert.ToInt32(fs.Length);
            byte[] bytes = new byte[imgLength];
            fs.Read(bytes, 0, imgLength);
            fs.Close();
            return bytes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public static System.Drawing.Bitmap CombineBitmap(string[] files)
        {
            //read all images into memory
            List<System.Drawing.Bitmap> images = new List<System.Drawing.Bitmap>();
            System.Drawing.Bitmap finalImage = null;

            try
            {
                int width = 0;
                int height = 0;

                foreach (string image in files)
                {
                    //create a Bitmap from the file and add it to the list
                    System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(image);

                    //update the size of the final bitmap
                    //width += bitmap.Width;
                    //height = bitmap.Height > height ? bitmap.Height : height;
                    height += bitmap.Height;
                    width = bitmap.Width > width ? bitmap.Width : width;

                    images.Add(bitmap);
                }

                //create a bitmap to hold the combined image
                finalImage = new System.Drawing.Bitmap(width, height);

                //get a graphics object from the image so we can draw on it
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(finalImage))
                {
                    //set background color
                    g.Clear(System.Drawing.Color.Black);

                    //go through each image and draw it on the final image
                    int offset = 0;
                    int height2 = 0;
                    foreach (System.Drawing.Bitmap image in images)
                    {
                        g.DrawImage(image,
                          new System.Drawing.Rectangle(0, height2, image.Width, image.Height));
                        offset += image.Width;
                        height2 += image.Height;
                    }
                }

                return finalImage;
            }
            catch (Exception ex)
            {
                if (finalImage != null)
                    finalImage.Dispose();

                throw ex;
            }
            finally
            {
                //clean up memory
                foreach (System.Drawing.Bitmap image in images)
                {
                    image.Dispose();
                }
            }
        }
    }
}
