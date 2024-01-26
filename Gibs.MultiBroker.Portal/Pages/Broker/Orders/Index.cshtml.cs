using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Gibs.Portal.Pages
{
    public class OrdersModel(BrokerContext context) : BrokerPageModel(context)
    {
        public List<Order> OrdersGrid { get; set; } = [];

        public async Task<PageResult> OnGet()
        {
            try
            {
                var broker = await GetCurrentBroker();

                await context.Entry(broker).Collection(x => x.Orders).LoadAsync();
                OrdersGrid = broker.Orders.ToList();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            return Page();
        }
    }
}
