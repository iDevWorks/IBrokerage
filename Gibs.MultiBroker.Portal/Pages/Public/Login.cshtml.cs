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
    [BindProperties]
    public class LoginModel(BrokerContext context) : PageModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            try
            {
                if(Role == "broker")
                {
                    var broker = await context.Brokers.SingleOrDefaultAsync(
                    b => b.Email == Email);

                    if (broker != null && broker.IsValidPassword(Password))
                    {
                        await SignInAsync(broker, Role);
                        return RedirectToPage("/Admin/Dashboard");
                    }
                }

                var insured = await context.Insureds.SingleOrDefaultAsync(
                    b => b.Email == Email);

                if (insured != null && insured.IsValidPassword(Password))
                {
                    await SignInAsync(insured, Role);
                    return RedirectToPage("/Customer/Dashboard");
                }

                throw new Exception("Invalid Username or Password");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return Page();
        }

        private Task SignInAsync(Person user, string role)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, role)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));
        }
    }
}
