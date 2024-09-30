using ProjectMVC.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace ProjectMVC.PL.Helpers
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com",587);
            //client.EnableSsl = false;
            client.Credentials = new NetworkCredential("ahmedsa2522003@gmail.com", "EmailCode");
            client.Send("ahmedsa2522003@gmail.com", email.Recepients, email.Subject, email.Body);
        }
    }
}
