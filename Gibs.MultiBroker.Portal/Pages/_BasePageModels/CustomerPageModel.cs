using Gibs.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Gibs.Portal.Pages
{
    [Authorize(Roles = "customer")]
    public abstract class CustomerPageModel(BrokerContext context) : RootPageModel
    {
        private string? CustomerId => User.GetCurrentId();

        public async Task<Domain.Entities.Insured> GetCurrentBroker()
        {
            return await context.Insureds.FindAsync(CustomerId)
                ?? throw new Exception("invalid customer id");
        }

    }
}
