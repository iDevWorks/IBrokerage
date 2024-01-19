using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Gibs.Portal.Pages
{
    public class UnderwritersModel(BrokerContext context) : BrokerPageModel(context)
    {
        public List<Underwriter> UnderWriters { get; set; } = [];

        public List<SelectListItem> InsurersSelectList { get; set; } 

        [BindProperty, Required]
        public string InsurerId { get; set; }

        [BindProperty,Required]
        public string ApiKeyUsername { get; set; }

        [BindProperty, Required]
        public string ApiKeyPassword { get; set; }

        public async Task<PageResult> OnGetAsync()
        {
            try
            {
                // Retrieve the broker using the provided BrokerId
                var broker = await context.Brokers
                    .Include(x => x.Underwriters)
                        .ThenInclude(x => x.Insurer)
                    .SingleOrDefaultAsync(x => x.Id == BrokerId);

                if (broker != null)
                {
                    // Load the list of available insurers into the Insurers property
                    InsurersSelectList = await context.Insurers
                        .Select(i => new SelectListItem
                        {
                            Value = i.Id,
                            Text = i.InsurerName
                        })
                        .ToListAsync();

                    // Populate the UnderWriters property with the underwriters associated with the broker
                    UnderWriters = broker.Underwriters.ToList();
                }
                else
                {
                    ShowError("Broker not found."); // Handle the case where the broker is not found
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }

            return Page();
        }

        public async Task<ActionResult> OnPostAddUnderwriter()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var broker = await GetCurrentBroker();

                    var insurer = await context.Insurers.FindAsync(InsurerId) 
                        ?? throw new Exception("invalid insurer id.");

                    var underwriter = new Underwriter(insurer, ApiKeyUsername, ApiKeyPassword);

                    broker.Underwriters.Add(underwriter);
                    await context.SaveChangesAsync();
                    ShowInfo("the underwriter was added successfully.");
                }
            }
            catch (SqlException ex)
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
