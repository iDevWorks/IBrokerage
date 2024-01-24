using Gibs.Infrastructure.Email;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Gibs.Portal.Pages.Public
{
    [BindProperties]
    public class ContactModel(EmailService emailService) : RootPageModel
    {
        [Required, EmailAddress]
        public string Sender { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }

        public async Task<ActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return RedirectToPage();

            try
            {
                var sendTo = "dejisys@idevworks.com";

                await emailService.SendContactForm(
                    Sender, Subject, Message, sendTo);

                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            return RedirectToPage();
        }

    }
}
