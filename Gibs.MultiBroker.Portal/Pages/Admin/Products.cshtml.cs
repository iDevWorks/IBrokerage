using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Gibs.Portal.Pages.Admin
{
    public class ProductModel(BrokerContext context) : AdminPageModel
    {
        private readonly BrokerContext _context = context;

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
            Products = await _context.Products.ToListAsync();
            return Page();
        }

        public async Task<ActionResult> OnPostAddProduct()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var productExists = Products.Any(p => p.ProductId == ProductId && p.ProductName == ProductName);

                    if (productExists)
                        throw new Exception("A product with this id and name already exists.");

                    var product = new Product(ProductId, ClassId, MidClassId, ProductName, ShortName);

                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToPage();
        }
    }
}
