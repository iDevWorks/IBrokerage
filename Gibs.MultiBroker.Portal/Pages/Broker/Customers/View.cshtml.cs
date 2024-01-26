using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Gibs.Portal.Pages
{
    public class CustomerDetailsModel(BrokerContext context) : BrokerPageModel(context)
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        [BindProperty, Required]
        public string FirstName { get; set; }

        [BindProperty, Required]
        public string LastName { get; set; }

        [BindProperty, Required, EmailAddress]
        public string Email { get; set; }

        [BindProperty, Required]
        public string Phone { get; set; }

        public string? CompanyName { get; set; }

        public bool IsCorporate { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public List<Policy> Policies { get; set; }
        public List<Order> Orders { get; set; }

        public async Task<PageResult> OnGet()
        {
            try
            {
                var customer = await context.Insureds.FindAsync(Id)
                    ?? throw new Exception("Error fetching customer.");

                FirstName = customer.FirstName;
                LastName = customer.LastName;
                Email = customer.Email;
                Phone = customer.Phone;
                DateOfBirth = customer.DateOfBirthOrReg;
                IsCorporate = customer.IsCorporate;
                CompanyName = customer.CompanyName;

                // Fetch policies associated with the customer
                Policies = await context.Policies
                    .Include(p => p.Product)
                    .Where(p => p.Insured.Id == Id)
                    .ToListAsync();

                // Fetch orders associated with the customer
                Orders = await context.Orders
                    .Include(o => o.Policies)
                    .Where(o => o.Insured.Id == Id)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateCustomerAsync()
        {
            if (!ModelState.IsValid)
                return RedirectToPage();

            try
            {
                var insured = await context.Insureds.FindAsync(Id)
                    ?? throw new Exception("Insured not found.");

                insured.UpdateProfile(insured.Title, FirstName, LastName, Email, Phone);

                await context.SaveChangesAsync();
                ShowInfo("The insured was updated successfully");
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
