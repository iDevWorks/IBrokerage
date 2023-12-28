using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Portal.Entities;
using Portal.EntityFramework;
using Claim = System.Security.Claims.Claim;

namespace Portal.Pages.Public
{
    public class RegisterModel(ILogger<RegisterModel> logger, IBrokerageContext context) : PageModel
    {
        [BindProperty, Required]
        public string Email { get; set; }

        [BindProperty, Required]
        public string FirstName { get; set; }

        [BindProperty, Required]
        public string LastName { get; set; }

        [BindProperty, Required]
        public string Address { get; set; }

        [BindProperty, Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [BindProperty, Required]
        public string Password { get; set; }

        [BindProperty, Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (Password != ConfirmPassword)
                {
                    throw new Exception("The password and confirmation password must match.");
                }

                var brokerExists = await context.Brokers.AnyAsync(u => u.Email == Email);

                if (brokerExists)
                {
                    throw new Exception("A user with this email already exists.");
                }

                var fullName = $"{FirstName} {LastName}";
                var broker = new Broker(Email, PhoneNumber, fullName, Address, Password);

                context.Brokers.Add(broker);
                await context.SaveChangesAsync();

                //proceed to Login
                return RedirectToPage("Index");

                //// Manually sign in the user after successful signup
                //var claims = new List<Claim>
                //{
                //    new(ClaimTypes.NameIdentifier, broker.Id),
                //    new(ClaimTypes.Email, broker.Email)
                //};

                //var claimsIdentity = new ClaimsIdentity(
                //    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //await HttpContext.SignInAsync(
                //    new ClaimsPrincipal(claimsIdentity));

                //return RedirectToPage("Dashboard");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return Page();
        }

    }
}
