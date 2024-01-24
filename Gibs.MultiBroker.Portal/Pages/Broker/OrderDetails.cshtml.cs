using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using iDevWorks.Paystack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Gibs.Portal.Pages
{
    public class OrderDetailsModel(BrokerContext context) : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public Order Order { get; set; }

        public async Task<PageResult> OnGet()
        {
            var order = await context.Orders
                .Include(o => o.Policies)
                    .ThenInclude(p => p.Product)
                .Include(o => o.Insured)
                .SingleOrDefaultAsync(o => o.Id == Id);

            if (order == null)
            {
                return Page();
            }

            Order = order;
            return Page();
        }
    }
}
