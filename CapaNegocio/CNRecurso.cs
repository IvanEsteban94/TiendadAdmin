using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CNRecurso
    {
        public static  string GeneralClave()
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0,6);
            return clave;
        }
        public static string ConvertirSha256(string text)
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding encoder = Encoding.UTF8;
                byte[] result = hash.ComputeHash(encoder.GetBytes(text));
                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            } 
        }
        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {
            bool resultado = false;
            try
            {
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("pruebacodigo123@gmail.com"),
                    Subject = asunto,
                    Body = mensaje,
                    IsBodyHtml = true
                };
                mail.To.Add(correo);

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("pruebacodigo123@gmail.com", "wsdwswwwww");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }

                resultado = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al enviar el correo: " + e.Message);
                resultado = false;
            }
            return resultado;
        }
        public static bool ConvertirBase64(string ruta, out string base64)
        {
            base64 = string.Empty;

            // Validar que la ruta no sea nula ni vacía
            if (string.IsNullOrWhiteSpace(ruta))
            {
                Console.WriteLine("La ruta del archivo es nula o vacía.");
                return false;
            }

            try
            {
                if (!File.Exists(ruta))
                {
                    Console.WriteLine($"El archivo no existe en la ruta especificada: {ruta}");
                    return false;
                }

                // Leer el archivo y convertirlo a Base64
                byte[] archivoBytes = File.ReadAllBytes(ruta);
                base64 = Convert.ToBase64String(archivoBytes);

                return true;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("No tienes permisos para acceder al archivo.");
            }
            catch (IOException ioEx)
            {
                Console.WriteLine($"Error de lectura/escritura: {ioEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado al convertir a Base64: {ex.Message}");
            }

            return false;
        }

    }
}
