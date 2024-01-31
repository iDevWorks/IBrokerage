using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Gibs.Portal.Pages
{
    public class UnderwriterViewModel(BrokerContext context) : BrokerPageModel(context)
    {
        [BindProperty(SupportsGet = true)]
        public string ApiKeyUsername { get; set; }

        public Underwriter Underwriter { get; private set; }

        public List<Policy> UnderwriterPolicies { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Underwriter = await context.Underwriter
                    .Include(u => u.Insurer)
                    .SingleOrDefaultAsync(u => u.ApiKey1Username == ApiKeyUsername)
                    ?? throw new Exception("No policy was found.");

                UnderwriterPolicies =  await context.Policies
                    .Include(p => p.Product)
                    .Where(p => p.Insurer.Id == Underwriter.Insurer.Id)
                    .ToListAsync()
                    ?? throw new Exception("No policy was found for this insurer.");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateUnderwriterAsync()
        {
            if (!ModelState.IsValid)
                return RedirectToPage();

            try
            {
                var underwriter = await context.Underwriter.FindAsync(ApiKeyUsername)
                    ?? throw new Exception("Underwriter was not found");

                // update underwriter

                await context.SaveChangesAsync();
                ShowInfo("The underwriter was updated successfully");
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
