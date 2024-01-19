using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Gibs.Portal.Pages
{
    public class ProductModel(BrokerContext context) : BrokerPageModel(context)
    {
        public List<Product> ProductsData { get; set; } = [];

        [Required]
        public string ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ShortName { get; set; }

        [Required]
        public string ClassId { get; set; }

        //[Required]
        public string? MidClassId { get; set; }

        public async Task<PageResult> OnGet()
        {
            try
            {
                var broker = await GetCurrentBroker();

                await context.Entry(broker).Collection(x => x.Products).LoadAsync();
                ProductsData = broker.Products.ToList();
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            return Page();
        }

        public async Task<ActionResult> OnPostAddProduct()
        {
            if (!ModelState.IsValid) return RedirectToPage();

            try
            {
                var broker = await GetCurrentBroker();

                var product = new Product(ProductId, ClassId, MidClassId, ProductName, ShortName);

                broker.Products.Add(product);
                await context.SaveChangesAsync();

                ShowInfo("the product was created successfully.");
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
