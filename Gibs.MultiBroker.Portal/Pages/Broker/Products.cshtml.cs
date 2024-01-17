using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Gibs.Portal.Pages
{
    public class ProductModel(BrokerContext context) : BrokerPageModel(context)
    {
        public List<Product> Products { get; set; } = [];

        [BindProperty, Required]
        public string ProductId { get; set; }

        [BindProperty, Required]
        public string ProductName { get; set; }

        [BindProperty, Required]
        public string ShortName { get; set; }

        [BindProperty, Required]
        public string ClassId { get; set; }

        [BindProperty]
        public string MidClassId { get; set; } = string.Empty;

        public async Task<PageResult> OnGet()
        {
            try
            {
                var broker = await GetCurrentBroker();

                await context.Entry(broker).Collection(x => x.Products).LoadAsync();
                Products = broker.Products.ToList();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return Page();
        }

        public async Task<ActionResult> OnPostAddProduct()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var broker = await GetCurrentBroker();

                    var product = new Product(ProductId, ClassId, MidClassId, ProductName, ShortName);

                    broker.Products.Add(product);
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
