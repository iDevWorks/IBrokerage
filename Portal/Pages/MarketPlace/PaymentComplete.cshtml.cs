using Gibs.Portal.Domain.Entities;
using Gibs.Portal.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gibs.Portal.Pages.MarketPlace
{
    public class PaymentCompleteModel(PaystackService paystackService) : PageModel
    {
        public TransactionVerifyResponse? ResponseData { get; set; }
        public string? ErrorMessage { get; set; }

        private readonly PaystackService _paystackService = paystackService;

        public async Task<PageResult> OnGetAsync(string reference)
        {
            try
            {
                ResponseData = await _paystackService.VerifyTransaction(reference);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return Page();
        }
    }
}
