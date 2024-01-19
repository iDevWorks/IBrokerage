using Microsoft.AspNetCore.Mvc;
using iDevWorks.Paystack;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Gibs.Portal.Pages;
using Gibs.Infrastructure.EntityFramework;
using Gibs.Domain.Entities;


namespace Gibs.MultiBroker.Portal.Pages.Public
{
    public class QuoteSummaryModel(IConfiguration configuration, BrokerContext context) : RootPageModel
    {
        private readonly PaystackClient paystack = new(configuration["Paystack:Key"]);
        [BindProperty(SupportsGet = true)]
        public string InsuredId { get; set; }

        public Insured Insured { get; set;}

        [BindProperty]
        public decimal Price { get; set; } = 15000;

        public async Task<PageResult> OnGet()
        {
            Insured = await context.Insureds.FindAsync(InsuredId);
            return Page();
        }

        public async Task<ActionResult> OnPostBuyInsurance()
        {
            try
            {
                Insured = await context.Insureds.FindAsync(InsuredId);

                var product = new Product("2001", "V", null, "Third Party Motor", "motor insurance");

                var policy = new Policy(product, Insured, "0004", DateOnly.FromDateTime(DateTime.UtcNow), DateOnly.FromDateTime(DateTime.UtcNow.AddDays(365)), 20000, Price);

                var listOfPolicy = new List<Policy>{ policy };

                var order = new Order(listOfPolicy, Insured);
                        
                context.Orders.Add(order);
                await context.SaveChangesAsync();

                // Initialize the payment transaction with Paystack
                var callbackUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Public/Complete";
                var result = await paystack.InitializeTransaction(Insured.Email, Price, callbackUrl, order.Reference);

                // Redirect the user to Paystack for payment
                return Redirect(result.AuthorizationUrl);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            return Page();
        }
    }
}
