using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Gibs.Portal.Pages
{
    public class ProductViewModel(BrokerContext context) : BrokerPageModel(context)
    {
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        [BindProperty]
        public string ShortName { get; set; }

        [BindProperty]
        public string ProductName { get; set; }

        [BindProperty]
        public string ClassId { get; set; }

        public Product Product { get; set; }

        public List<Order> ProductPurchases { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Product = await context.Products
                    .SingleOrDefaultAsync(p => p.Id == Id)
                    ?? throw new Exception("No product was found.");

                ProductPurchases = await context.Orders
                        .Include(o => o.Insured)
                        .Where(o => o.Policies.Any(p => p.Product.Id == Id)
                        && o.PaymentStatus == OrderStatus.SUCCESS)
                        .ToListAsync();

                ProductName = Product.ProductName;
                ShortName = Product.ShortName;
                ClassId = Product.ClassId;
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateProductAsync()
        {
            if (!ModelState.IsValid)
                return RedirectToPage();

            try
            {
                var product = await context.Products.FindAsync(Id)
                    ?? throw new Exception("product was not found");

                // update product

                await context.SaveChangesAsync();
                ShowInfo("The product was updated successfully");
                return RedirectToPage("Index");
            }
            catch (DbUpdateException ex)
            {
                ShowError(ex.Message);
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
                return RedirectToPage();
            }
        }
    }
}
