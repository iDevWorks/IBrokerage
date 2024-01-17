using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gibs.Portal.Pages
{
    [BindProperties]
    public class PolicyModel(BrokerContext context) : BrokerPageModel(context)
    {
        private readonly BrokerContext _context = context;

        public List<SelectListItem> Products { get; set; } = [];

        public List<SelectListItem> Insureds { get; set; } = [];

        [Required]
        public string ProductId { get; set; }
        [Required]
        public string InsuredId { get; set; }
        [Required]
        public string PolicyNo {  get; set; }
        [Required]
        public DateOnly StartDate {  get; set; }
        [Required]
        public DateOnly EndDate {  get; set; }
        [Required]
        public decimal SumInsured { get; set; }
        [Required]
        public decimal PremiumAmount { get; set; }

        public List<Policy> Policies { get; set; } = [];

        public async Task<PageResult> OnGetAsync()
        {
            try
            {
                var broker = await GetCurrentBroker();

                await context.Entry(broker).Collection(x => x.Policies).LoadAsync();
                await context.Entry(broker).Collection(x => x.Products).LoadAsync();
                await context.Entry(broker).Collection(x => x.Insureds).LoadAsync();

                // Load the list of available products into the Products property
                Products = broker.Products.Select(i => new SelectListItem
                {
                    Value = i.ProductId,
                    Text = i.ProductName
                }).ToList();

                // Load the list of insureds into the Insureds property
                Insureds = broker.Insureds.Select(i => new SelectListItem
                {
                    Value = i.Id,
                    Text = i.FullName
                }).ToList();

                Policies = broker.Policies.ToList();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            return Page();
        }

        public async Task<ActionResult> OnPostAddPolicy()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var broker = await GetCurrentBroker();

                    var insured = broker.Insureds.SingleOrDefault(i => i.Id == InsuredId);

                    var product = broker.Products.SingleOrDefault(p => p.ProductId == ProductId);

                    var policy = new Policy(product, insured, PolicyNo, StartDate, EndDate, SumInsured, PremiumAmount);

                    broker.Policies.Add(policy);
                    await _context.SaveChangesAsync();
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
