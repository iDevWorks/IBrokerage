﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Gibs.Portal.Pages
{
    [BindProperties]
    public class PolicyModel(BrokerContext context) : BrokerPageModel(context)
    {
        public List<SelectListItem> ProductOptions { get; set; } = [];
        public List<SelectListItem> InsuredOptions { get; set; } = [];
        public List<Policy> PoliciesData { get; set; } = [];

        [Required]
        public string ProductId { get; set; }
        [Required]
        public string InsuredId { get; set; }
        [Required]
        public string PolicyNo { get; set; }
        [Required]
        public DateOnly StartDate { get; set; }
        [Required]
        public DateOnly EndDate { get; set; }
        [Required]
        public decimal SumInsured { get; set; }
        [Required]
        public decimal PremiumAmount { get; set; }


        public async Task<PageResult> OnGetAsync()
        {
            try
            {
                var broker = await GetCurrentBroker();

                await context.Entry(broker).Collection(x => x.Policies).LoadAsync();
                await context.Entry(broker).Collection(x => x.Products).LoadAsync();
                await context.Entry(broker).Collection(x => x.Insureds).LoadAsync();

                // Load the list of available products into the ProductOptions property
                ProductOptions = broker.Products.Select(i => new SelectListItem
                {
                    Value = i.Id,
                    Text = i.ProductName
                }).ToList();

                // Load the list of insureds into the InsuredOptions property
                InsuredOptions = broker.Insureds.Select(i => new SelectListItem
                {
                    Value = i.Id,
                    Text = i.FullName
                }).ToList();

                PoliciesData = broker.Policies.ToList();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            return Page();
        }

        public async Task<ActionResult> OnPostAddPolicy()
        {
            if (!ModelState.IsValid) return RedirectToPage();

            try
            {
                var broker = await GetCurrentBroker();

                var insured = await context.Insureds.FindAsync(InsuredId)
                    ?? throw new InvalidOperationException("Insured was not found");

                var product = await context.Products.FindAsync(ProductId)
                    ?? throw new InvalidOperationException("product was not found");

                var policy = new Policy(product, insured, PolicyNo,
                    StartDate, EndDate, SumInsured, PremiumAmount);

                broker.Policies.Add(policy);
                await context.SaveChangesAsync();
                ShowInfo("Policy was added successfully.");
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
