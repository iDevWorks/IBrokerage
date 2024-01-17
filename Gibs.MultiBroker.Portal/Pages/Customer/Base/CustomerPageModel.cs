using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gibs.Portal.Pages
{
    [Authorize(Roles = "customer")]
    public class CustomerPageModel : PageModel
    {

    }
}
