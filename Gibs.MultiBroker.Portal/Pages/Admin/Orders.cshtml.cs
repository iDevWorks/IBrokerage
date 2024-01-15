using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Gibs.Portal.Pages.Admin
{
    public class OrdersModel(BrokerContext context) : PageModel
    {
        private readonly BrokerContext _context = context;

        public List<Order> Orders { get; set; } = [];

        public async Task<PageResult> OnGet()
        {
            Orders = await _context.Orders.ToListAsync();

            return Page();
        }
    }
}
