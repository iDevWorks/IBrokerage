using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Gibs.Portal.Pages.Public
{
    public class GetQuoteModel : PageModel
    {
        [BindProperty, EmailAddress]
        public string Email { get; set; }

        [BindProperty, DataType(DataType.PhoneNumber)]
        public string Phone {  get; set; }

        [BindProperty, StringLength(30, MinimumLength = 5)]
        public string FirstName { get; set; }

        [BindProperty, StringLength(30, MinimumLength = 5)]
        public string LastName { get; set; }

        [BindProperty, DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [BindProperty]
        public string Gender { get; set; }

        [BindProperty, StringLength(100, MinimumLength = 10)]
        public string Address { get; set; }

        [BindProperty, MinLength(4)]
        public string City { get; set; }

        [BindProperty]
        public string State { get; set; }

        [BindProperty]
        public string VehicleClass {  get; set; }

        [BindProperty]
        public string RegistrationNo { get; set; }

        [BindProperty]
        public string CarBrand { get; set; }

        [BindProperty, MinLength(5)]
        public string CarModel { get; set; }

        [BindProperty]
        public string ChasisNo { get; set; }

        [BindProperty]
        public string EngineNo { get; set; }

        [BindProperty]
        public string YearOfMake { get; set; }

        [BindProperty, MinLength(3)]
        public string Color { get; set; }

        public void OnGet()
        {
        }

        public ActionResult OnPostGetQuote() 
        {
            if(!ModelState.IsValid)
            {
                
            }

            return RedirectToPage("QuoteSummary");
        }
    }
}
