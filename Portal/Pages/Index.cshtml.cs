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

namespace iBrokerage.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IBrokerageContext _context;
        private readonly ILogger<LoginModel> _logger;
        private readonly IPasswordHasher<Broker> _passwordHasher;

        public LoginModel(ILogger<LoginModel> logger, IBrokerageContext context, IPasswordHasher<Broker> passwordHasher)
        {
            _logger = logger;
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user authentication.");
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            ModelState.AddModelError("", "User details do not match. Please check login details.");
            return Page();
        }

        private async Task<Broker?> AuthenticateBrokerAsync()
        {
            var broker = await _context.Brokers.SingleOrDefaultAsync(b => b.Email == Email);

            if (broker != null && _passwordHasher.VerifyHashedPassword(broker, broker.Password, Password) == PasswordVerificationResult.Success)
            {
                return broker;
            }

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
