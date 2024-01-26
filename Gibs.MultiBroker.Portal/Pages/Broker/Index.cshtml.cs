using Gibs.Infrastructure.EntityFramework;

namespace Gibs.Portal.Pages
{
    public class DashboardModel(BrokerContext context) : BrokerPageModel(context)
    {
        public void OnGet()
        {
        }
    }
}
