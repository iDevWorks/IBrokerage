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

namespace IBrokerage.Pages
{
    public class RegisterModel : PageModel
    {
        public RegisterModel(ILogger<RegisterModel> logger, IBrokerageContext context)
        {
            _logger = logger;
            _context = context;
        }

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

                return RedirectToPage("Dashboard");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                _logger.LogError(ex, ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return Page();
        }

    }
}
