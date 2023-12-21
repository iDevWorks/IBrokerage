using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Portal.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IBrokerage.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<Broker> _signInManager;
        private readonly UserManager<Broker> _userManager;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(UserManager<Broker> userManager, SignInManager<Broker> signInManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

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
        [StringLength(100, MinimumLength = 8)]
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
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "Email address is already in use.");
                    return Page();
                }

                var fullname = FirstName.Trim() + " " + LastName.Trim();
                var broker = new Broker(fullname, Address);

                await _userManager.SetUserNameAsync(broker, Email);
                await _userManager.SetPhoneNumberAsync(broker, PhoneNumber);
                await _userManager.SetEmailAsync(broker, Email);

                var result = await _userManager.CreateAsync(broker, Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    // Email verification logic
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(broker);
                    token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                    var confirmationLink = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = broker.Id, code = token },
                        protocol: Request.Scheme);

                    // Use your email service to send the confirmationLink to the user's email address.
                    // Example: await _emailService.SendEmailAsync(Email, "Confirm your email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(confirmationLink)}'>clicking here</a>.");

                    return RedirectToPage("/Account/RegistrationSuccess");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
