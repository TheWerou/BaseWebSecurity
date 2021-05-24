using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OBiBiapp.Handlers.MailHandling
{
    public class MailSender
    {
        public static void SendConfirmationMail(string ToAddress, string Token)
        {
            var client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("TestOnReplyPK@gmail.com", "!WsXeDc321");

            var message = new MailMessage();
            message.From = new MailAddress("TestOnReplyPK@gmail.com");
            message.To.Add(new MailAddress(ToAddress));
            message.Subject = "Account conformation";
            message.Body = "Account was created on this email. Please klik on this link to conform your account " + Token;

            client.Send(message);
        }
    }
}
