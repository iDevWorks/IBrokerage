using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using iDevWorks.Paystack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Gibs.Portal.Pages.Public
{
    [BindProperties]
    public class GetQuoteModel(BrokerContext context) : PageModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone {  get; set; }

        [StringLength(30, MinimumLength = 5)]
        public string FirstName { get; set; }

        [StringLength(30, MinimumLength = 5)]
        public string LastName { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string Gender { get; set; }

        [StringLength(100, MinimumLength = 10)]
        public string Address { get; set; }


        public string City { get; set; }

        public string State { get; set; }

        public string VehicleClass {  get; set; }

        public string RegistrationNo { get; set; }

        public string CarBrand { get; set; }

        public string CarModel { get; set; }

        public string ChasisNo { get; set; }

        public string EngineNo { get; set; }

        public string YearOfMake { get; set; }

        public string Color { get; set; }

      
        public async Task<ActionResult> OnPostGetQuote() 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var ExistingInsured = await context.Insureds
                        .SingleOrDefaultAsync(x => x.Email == Email);

                    if (ExistingInsured != null)
                    {
                        return RedirectToPage("QuoteSummary", new
                        {
                            InsuredId = ExistingInsured.Id
                        });
                    }

                    var insured = new Insured(false, string.Empty, DateOfBirth, FirstName, LastName, Email, Phone, Guid.NewGuid().ToString());
                    context.Add(insured);
                    await context.SaveChangesAsync();

                    return RedirectToPage("QuoteSummary", new
                    {
                        InsuredId = insured.Id
                    });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return Page();
        }
    }
}
