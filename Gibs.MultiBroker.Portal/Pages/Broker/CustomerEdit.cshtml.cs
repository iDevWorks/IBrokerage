using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gibs.Portal.Pages
{
    public class CustomerEditModel(BrokerContext context) : RootPageModel
    {
        [BindProperty]
        public Insured Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insured = await context.Insureds.FindAsync(id)
                ?? throw new Exception("no insured with this id was found.");

            Customer = insured;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            context.Attach(Customer).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
                ShowInfo("The insured was updated successfully");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!InsuredExists(Customer.Id))
                {
                    ShowError($"No insured was found with this id {Customer.Id}");
                }
                else
                {
                    ShowError(ex.Message);
                }
                return Page();
            }

            return RedirectToPage("Customers");
        }

        private bool InsuredExists(string id)
        {
            return context.Insureds.Any(i => i.Id == id);
        }
    }
}
