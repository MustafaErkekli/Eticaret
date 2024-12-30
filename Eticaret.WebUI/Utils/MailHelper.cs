using Eticaret.Core.Entities;
using System.Net;
using System.Net.Mail;

namespace Eticaret.WebUI.Utils
{
    public class MailHelper
    {
        public static async Task SendMailAsync(Contact contact)
        {
            SmtpClient smtpClient = new SmtpClient("mail.cartify.com",587);
            smtpClient.Credentials = new NetworkCredential("mutabakate921@zohomail.eu", "E-mutabakat066");
            smtpClient.EnableSsl = false;
            MailMessage message = new MailMessage();
            message.From = new MailAddress("mutabakate921@zohomail.eu");
            message.To.Add("emustafaerkekli.ee@gmail.com");
            message.Subject = "Siteden mesaj geldi";
            message.Body = $"İsim: {contact.Name} - Soyisim: {contact.Surname}- Mail: {contact.Email}- Telefon: {contact.Phone}- Mesaj: {contact.Message}";
            message.IsBodyHtml = true;
            await smtpClient.SendMailAsync(message);
            smtpClient.Dispose();
        }
    }
}
