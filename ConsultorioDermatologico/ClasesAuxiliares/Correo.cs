using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ConsultorioDermatologico.ClasesAuxiliares
{   
    /// <summary>
    /// Clase para manejo de correos
    /// </summary>
    public class Correo
    {
        /// <summary>
        /// Envio de correo con la clave nueva
        /// </summary>
        /// <param name="nombreCorreo">dirección de correo a enviarse</param>
        /// <param name="asunto">Asunto del correo</param>
        /// <param name="contenido">Clave y texto</param>
        /// <param name="rutaError">archivo tct donde se guardan los correos que no se han podido enviar</param>
        /// <returns></returns>
        public static int enviarCorreo(string nombreCorreo, string asunto, string contenido, string rutaError)
        {
            int rpta=0;
            try
            {
                //obtención de credenciales desde Web.config
                string correo = ConfigurationManager.AppSettings["correo"];
                string clave = ConfigurationManager.AppSettings["clave"];
                string servidor = ConfigurationManager.AppSettings["servidor"];
                int puerto = int.Parse(ConfigurationManager.AppSettings["puerto"]);


                //Datos del correo
                MailMessage mail = new MailMessage();
                mail.Subject = asunto;
                mail.IsBodyHtml = true;
                mail.Body = contenido;
                mail.From = new MailAddress(correo);
                mail.To.Add(new MailAddress(nombreCorreo));
                //Envio del correo
                SmtpClient smtp = new SmtpClient();
                smtp.Host = servidor;
                smtp.EnableSsl = true;
                smtp.Port = puerto;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(correo, clave);
                smtp.Send(mail);
                rpta = 1;
            }
            catch (Exception ex)
            {
                rpta = 0;
                Console.WriteLine(ex.Message);
                File.AppendAllText(rutaError, nombreCorreo);
            }
            return rpta;
        }

    }
}