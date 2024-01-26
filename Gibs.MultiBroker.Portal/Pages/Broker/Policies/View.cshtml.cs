using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using iDevWorks.Paystack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Gibs.Portal.Pages
{
    public class PolicyDetailModel(BrokerContext context) : BrokerPageModel(context)
    {
        [BindProperty(SupportsGet = true)]
        public string PolicyNo { get; set; }

        [BindProperty]
        public Policy Policy { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Policy = await context.Policies
               .Include(p => p.Insured)
               .SingleOrDefaultAsync(m => m.PolicyNo == PolicyNo)
               ?? throw new Exception("No policy was found.");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostUpdatePolicyAsync()
        {
            if (!ModelState.IsValid)
                return RedirectToPage();

            try
            {
                var policy = await context.Policies.FindAsync(PolicyNo)
                    ?? throw new Exception("Policy was not found");

                // update policy

                await context.SaveChangesAsync();
                ShowInfo("The policy was updated successfully");
                return RedirectToPage("Index");
            }
            catch (DbUpdateException ex)
            {
                ShowError(ex.Message);
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                return RedirectToPage();
            }
        }

    }
}
