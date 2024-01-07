using Gibs.Portal.Services;
using Gibs.Portal.Domain.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gibs.Portal.Pages.Public
{
    public class PaymentCompleteModel(PaystackService paystackService) : PageModel
    {
        public TransactionVerifyResponse? Result { get; set; }
        public string? ErrorMessage { get; set; }


        public async Task<PageResult> OnGetAsync(string reference)
        {
            try
            {
                Result = await paystackService.VerifyTransaction(reference);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return Page();
        }
    }
}
