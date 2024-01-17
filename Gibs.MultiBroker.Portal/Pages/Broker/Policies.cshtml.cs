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

                var products = broker.Products.ToList();
                // Load the list of available insurers into the Insurers property
                Products = products.Select(i => new SelectListItem
                {
                    Value = i.ProductId,
                    Text = i.ProductName
                }).ToList();

                var insureds = broker.Insureds.ToList();
                foreach (var insured in insureds)
                {
                    Insureds.Add(new SelectListItem
                    {
                        Value = insured.Id,
                        Text = insured.FullName
                    });
                }

                Policies = broker.Policies.ToList();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
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
