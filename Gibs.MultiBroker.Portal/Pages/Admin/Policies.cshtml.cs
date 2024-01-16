using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Gibs.Portal.Pages.Admin
{
    [BindProperties]
    public class PolicyModel(BrokerContext context) : AdminPageModel
    {
        private readonly BrokerContext _context = context;

        [BindProperty(SupportsGet = true)]
        public string BrokerId { get; set; }

        [Required]
        public string ProductName { get; set; }
        [Required]
        public string InsuredName { get; set; }
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
            Policies = await _context.Policies
                .Include(p => p.Insured)
                .Include(p => p.Underwriter).ToListAsync();

            return Page();
        }

        public async Task<ActionResult> OnPostAddPolicy()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var policyExists = await context.Policies.AnyAsync(p => p.PolicyNo == PolicyNo && p.Insured.FullName == InsuredName);

                    if (policyExists)
                        throw new Exception("A client with this policy already exists.");

                    var insured = await _context.Insureds.FirstOrDefaultAsync(i => i.FullName == InsuredName) ?? throw new Exception("the insured could not be found.");

                    var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductName == ProductName) ?? throw new Exception("the product could not be found.");


                    var policy = new Policy(product, insured, PolicyNo, StartDate, EndDate, SumInsured, PremiumAmount);

                    context.Policies.Add(policy);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToPage();
        }

        public async Task<ActionResult> OnPostDeletePolicy(string policyId)
        {
            var policy = Policies.FirstOrDefault(p => p.PolicyNo == policyId);
            if (policy != null)
            {
                _context.Policies.Remove(policy);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
