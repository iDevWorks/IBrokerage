using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;

namespace Gibs.Portal.Pages
{
    public class RegisterModel(ILogger<RegisterModel> logger, BrokerContext context) : PageModel
    {
        [BindProperty, Required]
        public string Email { get; set; } = string.Empty;

        [BindProperty, Required]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty, Required]
        public string LastName { get; set; } = string.Empty;

        [BindProperty, Required]
        public string RegistrationNo { get; set; } = string.Empty;

        [BindProperty, Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = string.Empty;

        [BindProperty, Required]
        public string Password { get; set; } = string.Empty;

        [BindProperty, Required]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            try
            {
                var exists = await context.Brokers.AnyAsync(u => u.Email == Email);

                if (exists)
                    throw new Exception("A user with this email already exists");

                var fullName = $"{FirstName} {LastName}";

                var broker = new Broker(fullName, RegistrationNo, FirstName, LastName, Email, PhoneNumber, Password);
                context.Add(broker);

                await context.SaveChangesAsync();
                return RedirectToPage("Login");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception in : {Message}", ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return Page();
        }
    }
}
