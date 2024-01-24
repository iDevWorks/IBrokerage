using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Gibs.Portal.Pages
{
    public class CustomerDetailsModel(BrokerContext context) : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        public Insured Customer { get; set; }
        public List<Policy> Policies { get; set; }
        public List<Order> Orders { get; set; }

        public async Task<PageResult> OnGet()
        {
            var customer = await context.Insureds.FindAsync(Id);

            if (customer == null)
            {
                return Page();
            }

            // Fetch policies associated with the customer
            Policies = await context.PoliciesData
                .Include(p => p.Insured)
                .Include(p => p.Product)
                .Where(p => p.Insured == customer)
                .ToListAsync();

            // Fetch orders associated with the customer
            Orders = await context.Orders
                .Include(o => o.Policies)
                .Where(o => o.Insured.Id == Id)
                .ToListAsync();

            Customer = customer;
            return Page();
        }
    }
}
