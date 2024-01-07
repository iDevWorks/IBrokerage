using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gibs.Portal.Pages
{
    public class IndexModel : PageModel
    {
        public ActionResult OnGet()
        {
            //can we handle invalid Tenets in the middleware and return string error?

            if (Tenant.IsValid)
                return RedirectToPage("/Public/Products");

            //show error page
            return Page();
        }
    }

    public class Tenant
    {
        //public enum TenentTypeEnum { TENENT, OWNER, ERROR}
        public static bool IsValid { get; } = true;
        public static string Hostname { get; } = "tetpak";

        //other settings
    }
}
