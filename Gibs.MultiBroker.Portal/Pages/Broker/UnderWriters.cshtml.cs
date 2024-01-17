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

        public List<SelectListItem> Insurers { get; set; } 

        [BindProperty, Required]
        public string SelectedInsurerId { get; set; }

        [BindProperty,Required]
        public string ApiKeyUsername { get; set; }

        [BindProperty, Required]
        public string ApiKeyPassword { get; set; }

        public async Task<PageResult> OnGetAsync()
        {
            try
            {
                var broker = await GetCurrentBroker();

                await context.Entry(broker).Collection(x => x.Underwriters).LoadAsync();
                await context.Entry(broker).Collection(x => x.Insurers).LoadAsync();

                var insurers = broker.Insurers.ToList();

                // Load the list of available insurers into the Insurers property
                Insurers = insurers.Select(i => new SelectListItem
                {
                    Value = i.InsurerId,
                    Text = i.InsurerName
                }).ToList();

                UnderWriters = broker.Underwriters.ToList();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
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

                    var insurer = broker.Insurers.SingleOrDefault(i => i.InsurerId == SelectedInsurerId);

                    var underwriter = new Underwriter(broker, insurer, ApiKeyUsername, ApiKeyPassword);

                    broker.Underwriters.Add(underwriter);
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
