using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Gibs.Portal.Pages
{
    public class PolicyDetailModel(BrokerContext context) : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string PolicyNo { get; set; }

        public Policy Policy { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Policy = await context.PoliciesData
                .Include(p => p.Insured)
                .FirstOrDefaultAsync(m => m.PolicyNo == PolicyNo)
                ?? throw new Exception("no policy was found matching the provided policy no.");

            return Page();
        }
    }
}
