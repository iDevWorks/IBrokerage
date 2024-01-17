using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gibs.Portal.Pages
{
    //[Authorize]
    public class DashboardModel(BrokerContext context) : BrokerPageModel(context)
    {
        public void OnGet()
        {
        }
    }
}
