using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Gibs.Portal.Pages
{
    public class InsuredModel(BrokerContext context) : AdminPageModel
    {
        public List<Insured> Insureds { get; set; } = [];

        [BindProperty, Required]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty, Required]
        public string LastName { get; set; } = string.Empty;

        [BindProperty, Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [BindProperty, Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [BindProperty, Required]
        public string IsCorporate { get; set; }

        [BindProperty]
        public string CompanyName { get; set; } = string.Empty;

        [BindProperty, Required]
        public string Password { get; set; } = string.Empty;

        public async Task<PageResult> OnGetAsync()
        {
          Insureds = await context.Insureds.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAddInsured()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var clientExists = await Insureds.AnyAsync(c => c.BrokerId == _currUserId && c.Email == Email);

                    //if (clientExists)
                    //    throw new Exception("A client with this email already exists.");

                    Insured client;

                    if (IsCorporate == "true")
                    {
                        client = new Insured(true, CompanyName, DateOnly.FromDateTime(DateTime.UtcNow), FirstName, LastName, Email, PhoneNumber, Password);
                    }
                    else
                    {
                        client = new Insured(false, CompanyName, DateOnly.FromDateTime(DateTime.UtcNow), FirstName, LastName, Email, PhoneNumber, Password);
                    }

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
