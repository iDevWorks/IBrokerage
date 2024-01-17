using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Gibs.Portal.Pages
{
    [Authorize(Roles = "broker")]
    public class BrokerPageModel(BrokerContext context1) : PageModel
    {
        public string Id { get; set; } = "OMOMOWO_RELIANCE";

        public async Task<Domain.Entities.Broker> GetCurrentBroker()
        {
            return await context1.Brokers.FindAsync(Id) 
                ?? throw new Exception("invalid broker id");
        }


    }
}
