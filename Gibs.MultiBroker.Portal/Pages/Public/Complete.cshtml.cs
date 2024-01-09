using iDevWorks.Paystack;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gibs.Portal.Pages.Public
{
    public class PaymentCompleteModel(IConfiguration configuration) : PageModel
    {
        private readonly PaystackClient paystack = new(configuration["Paystack:TestSecretKey"]);
        public Transaction? Transaction { get; set; }
        public string? ErrorMessage { get; set; }


        public async Task<PageResult> OnGetAsync(string reference)
        {
            try
            {
                Transaction = await paystack.VerifyTransaction(reference);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return Page();
        }
    }
}
