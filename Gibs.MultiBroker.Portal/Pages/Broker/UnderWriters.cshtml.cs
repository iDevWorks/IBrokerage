using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using iDevWorks.Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace Gibs.Portal.Pages
{
    public class UnderWritersModel(BrokerContext context) : BrokerPageModel(context)
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
                // var broker = await GetCurrentBroker();


                var broker = await context.Brokers
                    .Include(x => x.Underwriters)
                    .ThenInclude(x => x.Insurer)
                    .SingleOrDefaultAsync(x => x.Id == BrokerId);

                // var insurers = await context.Insurers.ToListAsync();

                // Load the list of available insurers into the Insurers property
                InsurersSelectList = await context.Insurers
                    .Select(i => new SelectListItem
                {
                    Value = i.InsurerId,
                    Text = i.InsurerName
                }).ToListAsync();

                UnderWriters = broker.Underwriters.ToList();
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

                    context.Add(underwriter);
                    //broker.Underwriters.Add(underwriter);
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
