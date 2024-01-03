using Gibs.Infrastructure.EntityFramework;
using Gibs.Portal.Domain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Gibs.Portal.Pages.Admin
{
    public class OrdersModel(IBrokerageContext context) : PageModel
    {
        private readonly IBrokerageContext _context = context;

        public List<Order> Orders { get; set; } = [];

        public async Task<PageResult> OnGet()
        {
            Orders = await _context.Orders.ToListAsync();

            return Page();
        }
    }
}
