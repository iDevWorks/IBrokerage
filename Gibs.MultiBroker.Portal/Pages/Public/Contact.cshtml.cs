using Gibs.Infrastructure.Email;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Gibs.MultiBroker.Portal.Pages.Public
{
    public class ContactModel(EmailService emailService) : PageModel
    {
        [BindProperty, EmailAddress]
        public string Sender { get; set; }

        [BindProperty, MinLength(5)]
        public string Subject { get; set; }

        [BindProperty, MinLength(10)]
        public string Message { get; set; }

        private readonly EmailService _emailService = emailService;

        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPostAsync() 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _emailService.SendEmailAsync(Sender, Subject, Message);
                    return RedirectToPage("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return Page();

        }

    }
}
