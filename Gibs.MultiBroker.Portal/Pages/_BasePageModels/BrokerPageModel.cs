using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Authorization;

namespace Gibs.Portal.Pages
{
    [Authorize(Roles = "broker")]
    public abstract class BrokerPageModel(BrokerContext context) : RootPageModel
    {
        private string Id { get; set; } = "OMOMOWO_RELIANCE";

        public async Task<Domain.Entities.Broker> GetCurrentBroker()
        {
            return await context.Brokers.FindAsync(Id) 
                ?? throw new Exception("invalid broker id");
        }
    }
}
