using SendGrid;
using SendGrid.Helpers.Mail;
using SkaneRegionalPlaces.App.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaneRegionalPlaces.App.Server.Services
{
    public interface ISendEmailService
    {
        Task<bool> SendEmail(Contact contact);
    }
    public class RegionalPlaceEmailService : ISendEmailService
    {
        private readonly ISendGridClient _sendGridClient;

        public RegionalPlaceEmailService(ISendGridClient sendGridClient)
        {
            _sendGridClient = sendGridClient ?? throw new ArgumentNullException(nameof(sendGridClient));
        }
        public async Task<bool> SendEmail(Contact contact)
        {
            SendGridMessage msg = new();
            EmailAddress from = new("souciance.eqdam.rashti@pulsen.se", contact.ToName);
            List<EmailAddress> recipients = new() { new EmailAddress(contact.ToEmailAddress, "SoucianceBlazorApp") };            
            msg.SetSubject(contact.Subject);
            msg.SetFrom(from);
            msg.AddTos(recipients);
            msg.PlainTextContent = contact.Body;

            Response response = await _sendGridClient.SendEmailAsync(msg);
            if (Convert.ToInt32(response.StatusCode) >= 400)
            {
                
                return false;
            }
            return true;
        }
    }
}