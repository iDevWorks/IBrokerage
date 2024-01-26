using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using iDevWorks.Paystack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Gibs.Portal.Pages
{
    public class PolicyDetailModel(BrokerContext context) : RootPageModel
    {
        [BindProperty(SupportsGet = true)]
        public string PolicyNo { get; set; }

        [BindProperty]
        public Policy Policy { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Policy = await context.PoliciesData
                .Include(p => p.Insured)
                .FirstOrDefaultAsync(m => m.PolicyNo == PolicyNo)
                ?? throw new Exception("no policy was found matching the provided policy No.");

            return Page();
        }

        public async Task<IActionResult> OnPostUpdatePolicyAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            context.Attach(Policy).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
                ShowInfo("The policy was updated successfully");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!PolicyExists(Policy.PolicyNo))
                {
                    ShowError($"No policy was found with this policyNo {Policy.PolicyNo}");
                }
                else
                {
                    ShowError(ex.Message);
                }
                return Page();
            }

            return RedirectToPage("Policies");
        }

        private bool PolicyExists(string policyNo)
        {
            return context.PoliciesData.Any(p => p.PolicyNo == policyNo);
        }
    }
}
