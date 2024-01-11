using Microsoft.AspNetCore.Mvc;
using iDevWorks.Paystack;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Gibs.Portal.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Gibs.MultiBroker.Portal.Pages.Public
{
    public class QuoteSummaryModel(IConfiguration configuration) : PageModel
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
                var callbackUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Complete";
                var result = await paystack.InitializeTransaction("omomowosymeon45@gmail.com", Price, callbackUrl);
                var reference = result.Reference;
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
                ModelState.AddModelError("", ex.Message);
            }
            return Page();
        }
    }
}
