using Hangfire;
using SemihBeyOvguMachine;
using System.Net.Mail;
using System.Net;
using Hangfire.MemoryStorage;

class Program
{
    static void Main()
    {
        GlobalConfiguration.Configuration.UseMemoryStorage();

        using (new BackgroundJobServer())
        {
            RecurringJob.AddOrUpdate(() => SendMail(), "0 12 * * *");

            Console.WriteLine("Hangfire başlatıldı. Çıkış yapmak için bir tuşa basın.");
            Console.ReadKey();
        }

    }

    public static void SendMail()
    {

        string toAddress = "semih.nurdagi@farmasi.com.tr";
        string subject = "Övgü";
        string body = "Çok iyisin,avrupanın en iyi yazılımcısısın.Keşke sen olsam....";

        using (var message = new MailMessage("brky_emr92@windowslive.com", toAddress))
        {
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            using (var client = new SmtpClient("smtp.office365.com"))
            {
                client.Port = 587; 
                client.Credentials = new NetworkCredential("--", "--");
                client.EnableSsl = true;

                try
                {
                    client.Send(message);
                    Console.WriteLine($"E-posta gönderildi: {DateTime.Now}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"E-posta gönderme hatası: {ex.Message}");
                }
            }
        }
    }
}
