using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using Gibs.Infrastructure.EntityFramework;
using Gibs.Domain.Entities;

namespace Gibs.Portal.Pages.Public
{
    public class LoginModel(BrokerContext context) : PageModel
    {
        [BindProperty, Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [BindProperty, Required]
        public string Password { get; set; } = string.Empty;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            try
            {
                var broker = await context.Brokers.SingleOrDefaultAsync(
                    b => b.Email == Email);

                if (broker != null && broker.IsValidPassword(Password))
                {
                    await SignInAsync(broker);
                    return RedirectToPage("../Dashboard");
                }

                throw new Exception("Invalid Username or Password");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return Page();
        }

        private Task SignInAsync(Broker broker)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, broker.Id.ToString()),
                new(ClaimTypes.Email, broker.Email)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));
        }
    }
}
