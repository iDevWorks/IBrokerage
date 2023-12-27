using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using Portal.EntityFramework;
using Portal.Entities;

namespace Portal.Pages.Public
{
    public class LoginModel(ILogger<LoginModel> logger, IBrokerageContext context) : PageModel
    {
        [BindProperty, Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [BindProperty, Required]
        public string Password { get; set; } = string.Empty;


        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var broker = await AuthenticateBrokerAsync();

                if (broker != null)
                {
                    await SignInBrokerAsync(broker);
                    return RedirectToPage("Dashboard");
                }

                throw new Exception("User details do not match. Please check login details.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return Page();
        }

        private async Task<Broker?> AuthenticateBrokerAsync()
        {
            var broker = await context.Brokers.SingleOrDefaultAsync(
                b => b.Email == Email);

            if (broker != null && broker.IsValidPassword(Password))
                return broker;

            return null;
        }

        private Task SignInBrokerAsync(Broker broker)
        {
            var claims = new List<System.Security.Claims.Claim>
            {
                new(ClaimTypes.NameIdentifier, broker.Id),
                new(ClaimTypes.Email, broker.Email)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));
        }
    }
}
