using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ApiProcolombiaPQR.COMMON.Models;


namespace ApiProcolombiaPQR.COMMON.Utilities
{
    public class SendEmail : ISendEmail<EmailViewModel>
    {
        public void SendAsync(EmailViewModel modeloMail)
        {
            try
            {
                MailMessage mail = new MailMessage();

                if (modeloMail.Destinations != null)
                {
                    // Añadimos todos los destinaarios
                    for (int i = 0; i < modeloMail.Destinations.Length; i++)
                    {
                        mail.To.Add(new MailAddress(modeloMail.Destinations[i]));
                    }
                }
                else if (modeloMail.Destination != string.Empty)
                {
                    mail.To.Add(modeloMail.Destination);
                }

                // Datos en el Html
                string bodyHtml = modeloMail.Message;

                // Configuración del servidor SMTP
                string smtpServer = "smtp.office365.com";
                int smtpPort = 587;
                string smtpUsername = "pqr@procolombia.co";
                string smtpPassword = "G3st10n2017..";

                // Cliente correo
                SmtpClient SmtpServer = new SmtpClient(smtpServer, smtpPort);
                mail.From = new MailAddress(smtpUsername);
                mail.Subject = modeloMail.Suject;
                mail.IsBodyHtml = modeloMail.IsHtml;
                mail.Body = bodyHtml;

                // If attachment is not null
                if (null != modeloMail.Attachments)
                {
                    Attachment attachment = new Attachment(modeloMail.Attachments);
                    mail.Attachments.Add(attachment);
                }

                SmtpServer.Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword);
                SmtpServer.EnableSsl = true;


                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                //TODO: Guardar error en db
            }
        }
    }
}
