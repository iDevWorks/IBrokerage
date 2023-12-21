using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portal.Entities;
using System.ComponentModel.DataAnnotations;

namespace iBrokerage.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<Broker> _userManager;
        private readonly SignInManager<Broker> _signInManager;

        public LoginModel(UserManager<Broker> userManager, SignInManager<Broker> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        [Required]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        public string Password { get; set; }

        public PageResult OnGet()
        {
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Email);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, Password, false, lockoutOnFailure: true);

                    if (result.Succeeded)
                    {
                        // Authentication succeeded
                        return RedirectToPage("Dashboard");
                    }
                }
            }

            // Authentication failed
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }
    }
}
