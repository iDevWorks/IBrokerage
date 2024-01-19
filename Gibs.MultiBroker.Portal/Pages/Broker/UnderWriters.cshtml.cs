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
    [BindProperties]
    public class UnderwritersModel(BrokerContext context) : BrokerPageModel(context)
    {
        public List<Underwriter> UnderwriterData { get; set; } = [];
        public List<SelectListItem> InsurerOptions { get; set; } = [];

        //
        [Required]
        public string InsurerId { get; set; }

        [Required]
        public string ApiKeyUsername { get; set; }

        [Required]
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

                if (broker == null)
                {
                    ShowError("Broker not found");
                    return Page();
                }

                // Load the list of available insurers into the Insurers property
                InsurerOptions = await context.Insurers
                    .Select(i => new SelectListItem
                    {
                        Value = i.Id,
                        Text = i.InsurerName
                    })
                    .ToListAsync();

                // Populate the UnderwriterData property with the
                // underwriters associated with the broker
                UnderwriterData = broker.Underwriters.ToList();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            return Page();
        }

        public async Task<ActionResult> OnPostAddUnderwriter()
        {
            if (!ModelState.IsValid) return RedirectToPage();

            try
            {
                var broker = await GetCurrentBroker();

                var insurer = await context.Insurers.FindAsync(InsurerId)
                    ?? throw new Exception("invalid insurer id");

                var underwriter = new Underwriter(insurer, ApiKeyUsername, ApiKeyPassword);

                broker.Underwriters.Add(underwriter);
                await context.SaveChangesAsync();

                ShowInfo("the underwriter was added successfully");
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
