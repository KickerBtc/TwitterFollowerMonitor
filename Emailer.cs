using System;
using System.Net.Mail;


namespace NoahnFollowers
{
    public class Emailer
    {
        public static void SendEmail(string subject, string body) 
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("YOUR EMAIL HERE");
            mail.To.Add("RECIPIENT EMIAL");
            mail.Subject = subject;
            mail.Body = body;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("GMAIL USERNAME", "GMAIL PASSWORD");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            Console.WriteLine($"Email sent: {subject}     {body}");
        }
    }
}