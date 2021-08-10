using System.Net;
using System.Net.Mail;

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
            client.Credentials = new NetworkCredential("", "");

            var message = new MailMessage();
            message.From = new MailAddress("TestOnReplyPK@gmail.com");
            message.To.Add(new MailAddress(ToAddress));
            message.IsBodyHtml = true;
            message.Subject = "Account conformation";
            string link = $"<a href=\"https://localhost:44360/AcceptAccount?tokenID={Token}\"> Activation link </a> ";
            message.Body = $"Account was created on this email. Please klik on this link to conform your account {link}" ;

            client.Send(message);
        }
        public static void SendPasswordResetMail(string ToAddress, string Token)
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
            message.IsBodyHtml = true;
            string link = $"<a href=\"https://localhost:44360/ResetPassword?tokenID={Token}\"> Reset Link </a> ";
            message.Body = $"If you send request password change click this link if you don't send request you can safely ignore message. Please klik on this link to conform your account {link}";

            client.Send(message);
        }
    }
}
