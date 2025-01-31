using Eticaret.Core.Entities;
using Humanizer;
using MailKit.Net.Smtp;
using MimeKit;
//using System.Net;
//using System.Net.Mail;

namespace Eticaret.WebUI.Utils
{
    public class MailHelper
    {
        //public static async Task<bool> SendMailAsync(Contact contact)
        //{
       
        //    SmtpClient smtpClient = new SmtpClient("mail.cartify.com",587);
        //    smtpClient.Credentials = new NetworkCredential("mutabakate921@zohomail.eu", "E-mutabakat066");
        //    smtpClient.EnableSsl = false;
        //    MailMessage message = new MailMessage();
        //    message.From = new MailAddress("mutabakate921@zohomail.eu");
        //    message.To.Add("emustafaerkekli.ee@gmail.com");
        //    message.Subject = "Siteden mesaj geldi";
        //    message.Body = $"İsim: {contact.Name} - Soyisim: {contact.Surname}- Mail: {contact.Email}- Telefon: {contact.Phone}- Mesaj: {contact.Message}";
        //    message.IsBodyHtml = true;
        //    try
        //    {
        //        await smtpClient.SendMailAsync(message);
        //        smtpClient.Dispose();
        //        return true;
        //    }
        //    catch (Exception)
        //    {

        //        return true;
        //    }
           
        //}
        //public static async Task<bool> SendMailAsync(string email,string subject,string mailBody)
        //{
        //    SmtpClient smtpClient = new SmtpClient("mail.cartify.com", 587);
        //    smtpClient.Credentials = new NetworkCredential("mutabakate921@zohomail.eu", "E-mutabakat066");
        //    smtpClient.EnableSsl = false;
        //    MailMessage message = new MailMessage();
        //    message.From = new MailAddress("mutabakate921@zohomail.eu");
        //    message.To.Add(email);
        //    message.Subject = subject;
        //    message.Body = mailBody;
        //    message.IsBodyHtml = true;
        //    try
        //    {
        //        await smtpClient.SendMailAsync(message);
        //        smtpClient.Dispose();
        //        return true;
        //    }
        //    catch (Exception)
        //    {

        //        return true;
        //    }

        //}
        public static async Task<bool> SendMailAsync(string email, string subject, string mailBody,string senderMail)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", "emustafaerkekli.ee@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);
            MailboxAddress mailboxAddressTo = new MailboxAddress("User", email);
            mimeMessage.To.Add(mailboxAddressTo);
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = mailBody;
            mimeMessage.Body = bodyBuilder.ToMessageBody();

            //mimeMessage.Body = mailBody.ToString();
            mimeMessage.Subject = subject;  

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Connect("smtp.gmail.com",587,false);
            smtpClient.Authenticate("emustafaerkekli.ee@gmail.com", "jqncrcneayiaszpp");
            smtpClient.Send(mimeMessage);
            smtpClient.Disconnect(true);
            return true;

        }
    }
}
