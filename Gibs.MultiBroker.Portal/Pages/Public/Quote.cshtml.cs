using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Gibs.Portal.Pages.Public
{
    [BindProperties]
    public class GetQuoteModel : PageModel
    {
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone {  get; set; }

        [StringLength(30, MinimumLength = 5)]
        public string FirstName { get; set; }

        [StringLength(30, MinimumLength = 5)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

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

      
        public ActionResult OnPostGetQuote() 
        {
            if(!ModelState.IsValid)
            {
                
            }
            return RedirectToPage("QuoteSummary");
        }
    }
}
