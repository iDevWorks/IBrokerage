using Microsoft.AspNetCore.Authorization;

namespace Gibs.Portal.Pages
{
    [Authorize(Roles = "customer")]
    public abstract class CustomerPageModel : RootPageModel
    {

    }
}
