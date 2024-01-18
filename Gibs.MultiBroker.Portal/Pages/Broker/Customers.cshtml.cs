using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Gibs.Portal.Pages
{
    public class InsuredModel(BrokerContext context) : BrokerPageModel(context)
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
        public bool IsCorporate { get; set; }

        [BindProperty]
        public string? CompanyName { get; set; }

        [BindProperty, Required]
        public DateOnly DateOfBirth { get; set; }


        public async Task<PageResult> OnGetAsync()
        {
            //var broker = await context.Brokers
            //      .Include(x => x.InsuredSelectItems)
            //      .Where(x => x.Id == BrokerId)
            //      //.SelectMany(x => x.InsuredSelectItems)
            //      .SingleOrDefaultAsync();
            try
            {
                var broker = await GetCurrentBroker();

                await context.Entry(broker).Collection(x => x.Insureds).LoadAsync();
                Insureds = broker.Insureds.ToList();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddInsured()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var broker = await GetCurrentBroker();

                    var client = new Insured(IsCorporate, CompanyName, DateOfBirth, FirstName, LastName, Email, PhoneNumber, "password");

                    broker.Insureds.Add(client);
                    await context.SaveChangesAsync();
                }
            }
            catch (SqlException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToPage(); 
        }
    }
}
