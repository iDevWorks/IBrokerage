using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Gibs.Portal.Pages.Public
{
    [BindProperties]
    public class LoginModel(BrokerContext context) : RootPageModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return RedirectToPage();

            try
            {
                if(Role == "broker")
                {
                    var broker = await context.Brokers.SingleOrDefaultAsync(
                    b => b.Email == Email);

                    if (broker != null && broker.IsValidPassword(Password))
                    {
                        var principal = broker.AsPrincipal("broker");
                        await HttpContext.SignInAsync(principal);
                        return RedirectToPage("/Broker/Index");
                    }
                }

                //var insured = await context.Insureds.SingleOrDefaultAsync(
                //    b => b.Email == Email);

                //if (insured != null && insured.IsValidPassword(Password))
                //{
                //    var principal = insured.AsPrincipal("customer");
                //    await HttpContext.SignInAsync(principal);
                //    return RedirectToPage("/Customer/Dashboard");
                //}

                throw new Exception("Invalid Username or Password");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            return RedirectToPage();
        }
    }
}
