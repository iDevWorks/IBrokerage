using Microsoft.AspNetCore.Mvc;
using iDevWorks.Paystack;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Gibs.Portal.Pages;


namespace Gibs.MultiBroker.Portal.Pages.Public
{
    public class QuoteSummaryModel(IConfiguration configuration) : RootPageModel
    {
        private readonly PaystackClient paystack = new(configuration["Paystack:Key"]);
        [BindProperty]
        public decimal Price { get; set; } = 15000;

        public void OnGet()
        {

        }

        public async Task<ActionResult> OnPostBuyInsurance()
        {
            try
            {
                var reference = Guid.NewGuid().ToString();

         var callbackUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Public/Complete";
                var result = await paystack.InitializeTransaction("omomowosymeon45@gmail.com", Price, callbackUrl, reference);
                // add the reference to the order entity
                //var order = new Order
                //{
                //    TransReference = reference;
                //};

                // Save the order to the database
                //_dbContext.Orders.Add(order);
                //await _dbContext.SaveChangesAsync();
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
