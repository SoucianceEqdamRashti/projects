using BlazorApplicationInsights;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SkaneRegionalPlaces.App.Client.Services;
using SkaneRegionalPlaces.App.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkaneRegionalPlaces.App.Client.Shared
{
    public partial class MailForm
    {
        [Inject]
        private ISnackbar Snackbar { get; set; }
        [Inject] 
        private IApplicationInsights AppInsights { get; set; }
        [Inject]
        private IEmailService EmailService { get; set; }
        private MudForm contactForm;
        readonly ContactModelValidator contactModelValidator = new();
        private readonly Contact contact = new();

        private async Task Submit()
        {
            await contactForm.Validate();

            if (contactForm.IsValid)
            {
                await AppInsights.TrackEvent("Sending email", new Dictionary<string, object>() { { "To", contact.ToName } });
                await AppInsights.Flush();
                var sent = await EmailService.SendEmail(contact);
                if (sent)
                {
                    await AppInsights.TrackEvent("Emal sent", new Dictionary<string, object>() { { "To", contact.ToName } });
                    await AppInsights.Flush();
                    Snackbar.Add("Tack för kontakten. Emailet har skickats!");
                    Reset();
                }
                else
                {
                    await AppInsights.TrackEvent("Email failed", new Dictionary<string, object>() { { "To", contact.ToName } });
                    Snackbar.Add("Ditt email kunde inte skickas. Prova igen!");
                }
            }
        }

        private void Reset()
        {
            contact.Body = "";
            contact.Subject = "";
            contact.ToEmailAddress = "";
            contact.ToName = "";

        }

    }

    public class ContactModelValidator : AbstractValidator<Contact>
    {
        public ContactModelValidator()
        {
            RuleFor(x => x.ToName)
                .NotEmpty()
                .Length(1, 100)
                .WithMessage("Namn får inte vara tomt och max 100 tecken lång"); ;
            RuleFor(x => x.ToEmailAddress)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Ogiltigt email address"); ;
            RuleFor(x => x.Subject)
                .NotEmpty()
                .WithMessage("Ärende text får inte vara tomt");
            RuleFor(x => x.Body)
                .Length(1, 500)
                .WithMessage("Texten får inte vara tomt och max 500 tecken lång"); ;
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<Contact>.CreateWithOptions((Contact)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
