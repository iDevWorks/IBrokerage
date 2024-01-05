using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Gibs.Portal.Pages.Admin
{
    public class PolicyModel(IBrokerageContext context) : PageModel
    {
        private readonly IBrokerageContext _context = context;

        [BindProperty(SupportsGet = true)]
        public string BrokerId { get; set; }

        public string IssuingCompany {  get; set; }
        public string Name { get; set; }
        public DateTimeOffset EndDate {  get; set; }
        public decimal PremiumAmount { get; set; }

        public List<Policy> Policies { get; set; } = [];

        public async Task<PageResult> OnGetAsync()
        {
            Policies = await _context.Policies
                .Include(p => p.Client)
                .Include(p => p.Underwriter).ToListAsync();

            return Page();
        }

        public async Task<ActionResult> OnPostDeletePolicy(string policyId)
        {
            var policy = Policies.FirstOrDefault(p => p.PolicyNo == policyId);
            if (policy != null)
            {
                _context.Policies.Remove(policy);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
