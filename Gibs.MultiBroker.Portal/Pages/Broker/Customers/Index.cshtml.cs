using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Gibs.Portal.Pages
{
    [BindProperties]
    public class InsuredModel(BrokerContext context) : BrokerPageModel(context)
    {
        public List<Insured> InsuredData { get; set; } = [];

        //form inputs
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public bool IsCorporate { get; set; }

        [BindProperty]
        public string? CompanyName { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }


        public async Task<PageResult> OnGetAsync()
        {
            try
            {
                var broker = await GetCurrentBroker();

                await context.Entry(broker).Collection(x => x.Insureds).LoadAsync();
                InsuredData = broker.Insureds.ToList();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddInsured()
        {
            if (!ModelState.IsValid) return RedirectToPage();

            try
            {
                var broker = await GetCurrentBroker();

                var password = "password";

                var client = new Insured(IsCorporate, CompanyName,
                    DateOfBirth, FirstName, LastName, Email, PhoneNumber, password);

                broker.Insureds.Add(client);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            return RedirectToPage();
        }
    }
}
