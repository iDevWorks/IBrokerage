using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Portal.Entities;
using Claim = System.Security.Claims.Claim;
using Portal.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace iBrokerage.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IBrokerageContext _context;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(ILogger<LoginModel> logger, IBrokerageContext context)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty, Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [BindProperty, Required]
        public string Password { get; set; } = string.Empty;

        public PageResult OnGet()
        {
            return Page();
        }

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
                _logger.LogError(ex, "Error during user authentication.");
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return Page();
        }

        private async Task<Broker?> AuthenticateBrokerAsync()
        {
            var broker = await _context.Brokers.SingleOrDefaultAsync(
                b => b.Email == Email);

            if (broker != null && broker.IsValidPassword(Password))
                return broker;

            return null;
        }

        private async Task SignInBrokerAsync(Broker broker)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, broker.Id),
                new(ClaimTypes.Email, broker.Email)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));
        }
    }
}
