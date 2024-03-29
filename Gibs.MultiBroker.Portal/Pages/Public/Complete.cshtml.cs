using Gibs.Infrastructure.EntityFramework;
using iDevWorks.Paystack;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;

namespace Gibs.Portal.Pages.Public
{
    public class PaymentCompleteModel(BrokerContext context, PaystackClient paystack) : RootPageModel
    {
        public Transaction? Transaction { get; set; }
        public string? ErrorMessage { get; set; }

        public async Task<PageResult> OnGetAsync(string reference)
        {
            try
            {
                var order = await context.Orders
                        .Where(o => o.Reference == reference)
                        .SingleOrDefaultAsync()
                    ?? throw new InvalidOperationException("wrong payment reference");

                Transaction = await paystack.VerifyTransaction(reference);

                if (Transaction.Status == "success")
                    order.PaymentSuccess(Transaction.AmountInKobo / 100);
                else
                    order.PaymentFailed(Transaction.GatewayResponse);

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return Page();
        }
    }
}
