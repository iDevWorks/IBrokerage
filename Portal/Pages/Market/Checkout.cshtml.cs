using Gibs.Portal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Gibs.Portal.Pages.Market
{
    public class CheckoutModel(PaystackService paystackService) : PageModel
    {
        private readonly PaystackService _paystackService = paystackService;

        public string Name { get; set; }

        [BindProperty, EmailAddress]
        public string Email { get; set; }

        public void OnGet()
        {

        }

        public async Task<ActionResult> OnPostInitializePaymentAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var callbackUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/PaymentComplete";
                    var result = await _paystackService.InitializeTransaction("omomowosymeon45@gmail.com", 30000, callbackUrl);

                    return Redirect(result.AuthorizationUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return Page();
        }
    }
}
