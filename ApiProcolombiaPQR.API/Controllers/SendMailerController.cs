using ApiProcolombiaPQR.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace ApiProcolombiaPQR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailerController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> SendMail(MailModel _objModelMail)
        {
            try
            {
                // Configuración del servidor SMTP
                string smtpServer = "smtp.office365.com";
                int smtpPort = 587;
                string smtpUsername = "pqr@procolombia.co";
                string smtpPassword = "G3st10n2017..";

                // Creación del objeto MailMessage
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(smtpUsername);
                mail.To.Add(_objModelMail.To);
                mail.Subject = _objModelMail.Subject;
                mail.Body = _objModelMail.Body;
                
                // Configuración del servidor SMTP
                SmtpClient smtp = new SmtpClient(smtpServer, smtpPort);
                smtp.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtp.EnableSsl = true;

                // Enviar el correo
                await smtp.SendMailAsync(mail);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        }
}
