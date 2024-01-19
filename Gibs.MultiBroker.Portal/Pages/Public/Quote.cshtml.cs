using Gibs.Domain.Entities;
using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Gibs.Portal.Pages.Public
{
    public class GetQuoteModel(BrokerContext context) : RootPageModel
    {
        [BindProperty]
        public VehicleModel Quote { get; set; } = new();

        public ActionResult OnPostGetQuote()
        {
            if (!ModelState.IsValid) return RedirectToPage();

            try
            {
                TempData.Put("quote", Quote);
                

                return RedirectToPage("QuoteSummary");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            return RedirectToPage();
        }
    }

    public class VehicleModel()
    {
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

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

        public string VehicleClass { get; set; }

        public string RegistrationNo { get; set; }

        public string CarBrand { get; set; }

        public string CarModel { get; set; }

        public string ChasisNo { get; set; }

        public string EngineNo { get; set; }

        public string YearOfMake { get; set; }

        public string Color { get; set; }
    }
}
