using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gibs.Infrastructure.EntityFramework;

namespace Gibs.Portal.Pages
{
    [BindProperties]
    public class RegisterModel(BrokerContext context) : RootPageModel
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string RegistrationNo { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return RedirectToPage();

            try
            {
                var exists = await context.Brokers.AnyAsync(u => u.Email == Email);

                if (exists)
                    throw new Exception("A user with this email already exists");

                var fullName = $"{FirstName} {LastName}";

                var broker = new Domain.Entities.Broker(fullName, RegistrationNo, FirstName, LastName, Email, PhoneNumber, Password);
                context.Add(broker);

                await context.SaveChangesAsync();
                return RedirectToPage("Login");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            return RedirectToPage();
        }
    }
}
