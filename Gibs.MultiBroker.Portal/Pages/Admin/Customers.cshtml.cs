using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Gibs.Portal.Pages
{
    public class InsuredModel(BrokerContext context) : PageModel
    {
        public List<Insured> Clients { get; set; } = [];

        [BindProperty, Required]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty, Required]
        public string LastName { get; set; } = string.Empty;

        [BindProperty, Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [BindProperty, Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [BindProperty, Required]
        public string Address { get; set; } = string.Empty;

        public async Task<PageResult> OnGetAsync()
        {
          // Insureds = await context.Insureds.Where(c => c.BrokerId == _currUserId).ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var clientExists = await context.Insureds.AnyAsync(c => c.BrokerId == _currUserId && c.Email == Email);

                    //if (clientExists)
                    //    throw new Exception("A client with this email already exists.");

                    var client = new Customer(FirstName, LastName, Address, Email, PhoneNumber);

                    context.Insureds.Add(client);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToPage(); 
        }
    }
}
