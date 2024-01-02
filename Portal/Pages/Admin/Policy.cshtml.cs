using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;

namespace Gibs.Portal.Pages.Admin
{
    public class PolicyModel(IBrokerageContext context) : PageModel
    {
        public List<Policy> Policy { get; set; } = [];

        public async Task<PageResult> OnGetAsync()
        {
            Policy = await context.Policies
                .Include(p => p.Client)
                .Include(p => p.IssuingCompany).ToListAsync();

            return Page();
        }
    }
}
