using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Gibs.Portal.Pages
{
    [Authorize(Roles = "broker")]
    public abstract class BrokerPageModel(BrokerContext context) : RootPageModel
    {
        public string? BrokerId => User.GetCurrentId();

        public async Task<Domain.Entities.Broker> GetCurrentBroker()
        {
            return await context.Brokers.FindAsync(BrokerId) 
                ?? throw new Exception("invalid broker id");
        }
    }
}
