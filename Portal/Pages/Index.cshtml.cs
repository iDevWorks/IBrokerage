using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gibs.Portal.Pages
{
    public class IndexModel : PageModel
    {
        public ActionResult OnGet()
        {
            return RedirectToPage("/MarketPlace/GetQuote");
        }
    }
}
