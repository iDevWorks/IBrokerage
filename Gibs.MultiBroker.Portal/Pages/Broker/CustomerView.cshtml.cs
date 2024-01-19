using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Gibs.MultiBroker.Portal.Pages.Broker
{
    public class CustomerViewModel(BrokerContext context) : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        public Insured Customer { get; set; } = new();

        public async Task<PageResult> OnGet()
        {
            var customer = await context.Insureds.FindAsync(Id);

            if (customer == null)
            {
                return Page();
            }
            Customer = customer;
            return Page();
        }
    }
}
