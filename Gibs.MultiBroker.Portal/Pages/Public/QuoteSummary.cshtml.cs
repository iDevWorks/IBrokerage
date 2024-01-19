using Microsoft.AspNetCore.Mvc;
using iDevWorks.Paystack;
using Gibs.Portal.Pages;


namespace Gibs.MultiBroker.Portal.Pages.Public
{
    public class QuoteSummaryModel(PaystackClient paystack) : RootPageModel
    {
        [BindProperty]
        public decimal Price { get; set; } = 15000;

        public async Task<ActionResult> OnPostBuyInsurance()
        {
            if (!ModelState.IsValid) return RedirectToPage();

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
                //_dbContext.OrdersGrid.Add(order);
                //await _dbContext.SaveChangesAsync();
                return Redirect(result.AuthorizationUrl);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            return RedirectToPage();
        }
    }
}
