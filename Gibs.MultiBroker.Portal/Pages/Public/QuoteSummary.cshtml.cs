using Microsoft.AspNetCore.Mvc;
using iDevWorks.Paystack;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Gibs.MultiBroker.Portal.Pages.Public
{
    public class QuoteSummaryModel(IConfiguration configuration) : PageModel
    {
        private readonly PaystackClient paystack = new(configuration["Paystack:Key"]);
        [BindProperty]
        public decimal Price { get; set; }

        public void OnGet()
        {

        }

        public async Task<ActionResult> OnPostBuyInsurance()
        {
            try
            {
                var callbackUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Complete";
                var result = await paystack.InitializeTransaction("omomowosymeon45@gmail.com", Price, callbackUrl);
                return Redirect(result.AuthorizationUrl);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return Page();
        }
    }
}
