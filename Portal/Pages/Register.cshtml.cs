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
using System.Security.Cryptography;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace IBrokerage.Pages
{
    public class RegisterModel : PageModel
    {
        public RegisterModel(ILogger<RegisterModel> logger, IBrokerageContext context)
        {
            _logger = logger;
            _context = context;
            _passwordHasher = passwordHasher;
        }

        private readonly IPasswordHasher<Broker> _passwordHasher;
        private readonly IBrokerageContext _context;
        private readonly ILogger<RegisterModel> _logger;

        [BindProperty, Required]
        public string Email { get; set; }

        [BindProperty, Required]
        [StringLength(20, MinimumLength = 5)]
        public string FirstName { get; set; }

        [BindProperty, Required]
        [StringLength(20, MinimumLength = 5)]
        public string LastName { get; set; }

        [BindProperty, Required]
        [StringLength(100)]
        public string Address { get; set; }

        [BindProperty, Required]
        [StringLength(11, MinimumLength = 11)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [BindProperty, Required]
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; }

        [BindProperty, Required]
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
                if (Password != ConfirmPassword)
                {
                    throw new Exception("The password and confirmation password must match.");
                }

                var brokerExists = await _context.Brokers.AnyAsync(u => u.Email == Email);

                if (brokerExists)
                {
                    throw new Exception("A user with this email already exists.");
                }

                var fullName = $"{FirstName} {LastName}";
                var broker = new Broker(Email, PhoneNumber, fullName, Address, Password);

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

                // Generate confirmation token
                var code = GenerateConfirmationToken();

                // Store confirmation token in Broker entity
                broker.ConfirmationToken = code;

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Send confirmation email
                await SendConfirmationEmailAsync(broker.Email, code);

                // Redirect to a page indicating that confirmation email has been sent
                return RedirectToPage("ConfirmationSent");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                _logger.LogError(ex, ex.Message);
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
