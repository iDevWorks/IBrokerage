using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gibs.Portal.Pages
{
    public class IndexModel : PageModel
    {
        public ActionResult OnGet()
        {
            //can we handle invalid Tenets in the middleware and return string error?
            //invalid Tenants can be routed to OUR own portal perhaps?

            if (Tenant.IsValid)
                return RedirectToPage("/Public/Index");

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
