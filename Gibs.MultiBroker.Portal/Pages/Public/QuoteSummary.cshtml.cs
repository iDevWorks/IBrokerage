using Microsoft.AspNetCore.Mvc;
using iDevWorks.Paystack;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Gibs.Portal.Pages;
using Gibs.Infrastructure.EntityFramework;
using Gibs.Domain.Entities;
using Gibs.Portal.Pages.Public;
using Gibs.Portal;
using Microsoft.EntityFrameworkCore;


namespace Gibs.MultiBroker.Portal.Pages.Public
{
    public class QuoteSummaryModel(PaystackClient paystack, BrokerContext context) : RootPageModel
    {
        public VehicleModel Quote { get; set; }

        [BindProperty]
        public decimal Price { get; set; } = 15000;

        public ActionResult OnGet()
        {
            var quote = TempData.Peek<VehicleModel>("quote");

            if(quote == null)
            {
                return RedirectToPage("Quote");
            }
            Quote = quote;
            return Page();
        }

        public async Task<ActionResult> OnPostBuyInsurance()
        {
            try
            {
                var quote = TempData.Get<VehicleModel>("quote");

                if (quote == null)
                {
                    return RedirectToPage("Quote");
                }

                var insured = await context.Insureds.FindAsync(quote.Email);

                var broker = await context.Brokers.FindAsync("OMOMOWO_RELIANCE")
                        ?? throw new Exception("invalid broker");

                if (insured == null)
                {
                    insured = new Insured(false, string.Empty, quote.DateOfBirth,
                        quote.FirstName, quote.LastName, quote.Email, quote.Phone, "1234");

                    broker.Insureds.Add(insured);
                    //await context.SaveChangesAsync();
                }

                // vehicle information should be saved and policy


                var product = await context.Products.FindAsync("1003")
                    ?? throw new Exception("invalid product id");

                var policyNo = DateTime.Now.Ticks.ToString();
                var policy = new Policy(product, insured, policyNo, DateOnly.FromDateTime(DateTime.UtcNow), DateOnly.FromDateTime(DateTime.UtcNow.AddDays(365)), 20000, Price);

                List<Policy> policies = [policy];

                var order = new Order(policies, insured);

                broker.Orders.Add(order);
                broker.Policies.Add(policy);
                await context.SaveChangesAsync();


                // Initialize the payment transaction with Paystack
                var callbackUrl = Url.PageLink("/Public/Complete")
                    ?? throw new InvalidOperationException("Callback URL is invalid");
                var result = await paystack.InitializeTransaction(insured.Email, Price, callbackUrl, order.Reference);

                // Redirect the user to Paystack for payment
                return Redirect(result.AuthorizationUrl);
            }
            catch (DbUpdateException)
            {
                ShowError("duplicate entry");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            return Page();
        }
    }
}
