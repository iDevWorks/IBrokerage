using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Portal.Entities;
using Portal.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Claim = System.Security.Claims.Claim;

namespace IBrokerage.Pages
{
    public class RegisterModel : PageModel
    {
        public RegisterModel(ILogger<RegisterModel> logger, IBrokerageContext context, IPasswordHasher<Broker> passwordHasher)
        {
            _logger = logger;
            _context = context;
            _passwordHasher = passwordHasher;
        }

        private readonly IPasswordHasher<Broker> _passwordHasher;
        private readonly IBrokerageContext _context;
        private readonly ILogger<RegisterModel> _logger;

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        [StringLength(20, MinimumLength = 5)]
        public string FirstName { get; set; }

        [BindProperty]
        [StringLength(20, MinimumLength = 5)]
        public string LastName { get; set; }

        [BindProperty]
        [StringLength(100)]
        public string Address { get; set; }

        [BindProperty]
        [StringLength(11, MinimumLength = 11)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [BindProperty]
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; }

        [BindProperty]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        public PageResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var brokerExists = await CheckIfBrokerExistsAsync();
                if (brokerExists)
                {
                    ModelState.AddModelError("", "A user with this email already exists.");
                    return Page();
                }

                if (Password != ConfirmPassword)
                {
                    ModelState.AddModelError("", "The password and confirmation password must match.");
                    return Page();
                }

                var fullName = $"{FirstName} {LastName}";
                var broker = CreateBroker(Email, PhoneNumber, fullName, Address, Password);
                broker.Password = HashPassword(broker, Password);

                _context.Brokers.Add(broker);
                await _context.SaveChangesAsync();

                // Manually sign in the user after successful signup
                var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, broker.Id),
                    new(ClaimTypes.Email, broker.Email)
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToPage("Dashboard");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                _logger.LogError(ex, "An unexpected error occurred during registration.");
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return Page();
        }

        private async Task<bool> CheckIfBrokerExistsAsync()
        {
            var user = await _context.Brokers.ToListAsync();
            return user.Any(u => u.Email == Email);
        }

        private static Broker CreateBroker(string email, string phoneNumber, string fullname, string address,   string password)
        {
            var broker = new Broker(email, phoneNumber, fullname, address, password);
            return broker;
        }

        private string HashPassword(Broker broker, string password)
        {
            return _passwordHasher.HashPassword(broker, password);
        }
    }
}
