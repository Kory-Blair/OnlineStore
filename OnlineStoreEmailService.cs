using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace CodingTemple.CodingCookware.Web
{
    internal class OnlineStoreEmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            string apiKey = System.Configuration.ConfigurationManager.AppSettings["SendGrid.Key"];
            SendGrid.SendGridClient client = new SendGrid.SendGridClient(apiKey);

            SendGrid.Helpers.Mail.SendGridMessage mail = new SendGrid.Helpers.Mail.SendGridMessage();
            mail.SetFrom(new SendGrid.Helpers.Mail.EmailAddress { Name = "OnlineStore Admin", Email = "team@ITRL.com" });
            mail.AddTo(message.Destination);
            mail.SetSubject(message.Subject);
            mail.AddContent("text/plain", message.Body);
            mail.AddContent("text/html", message.Body);
            mail.SetTemplateId("2e677ad2-7c53-4caa-802f-8afa6f9a6ec4");
            

            return client.SendEmailAsync(mail);
        }
    }
}