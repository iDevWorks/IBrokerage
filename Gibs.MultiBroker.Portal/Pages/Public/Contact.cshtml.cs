using Gibs.Infrastructure.Email;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Gibs.Portal.Pages.Public
{
    [BindProperties]
    public class ContactModel(EmailService emailService) : RootPageModel
    {
        [EmailAddress]
        public string Sender { get; set; }

        [MinLength(5)]
        public string Subject { get; set; }

        [MinLength(10)]
        public string Message { get; set; }

        public async Task<ActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return RedirectToPage();

            try
            {
                await emailService.SendEmailAsync(Sender, Subject, Message);
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
