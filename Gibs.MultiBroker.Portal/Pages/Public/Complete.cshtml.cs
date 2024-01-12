using Gibs.Infrastructure.EntityFramework;
using iDevWorks.Paystack;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Gibs.Portal.Pages.Public
{
    public class PaymentCompleteModel(BrokerContext context, IConfiguration configuration) : PageModel
    {
        private readonly BrokerContext _brokerContext = context;
        private readonly PaystackClient paystack = new(configuration["Paystack:Key"]);
        public Transaction? Transaction { get; set; }
        public string? ErrorMessage { get; set; }

        public async Task<PageResult> OnGetAsync(string reference)
        {
            try
            {
                var order = await _brokerContext.Orders
                        .Where(o => o.TransReference == reference)
                        .SingleOrDefaultAsync();

                ArgumentNullException.ThrowIfNull(order);

                Transaction = await paystack.VerifyTransaction(reference);
                
                if (Transaction.Status == "success")
                {
                    order.PaymentSuccess(Transaction.RequestedAmount);
                }
                else
                {
                    order.PaymentFailed(Transaction.Message);
                }
                await _brokerContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return Page();
        }
    }
}
