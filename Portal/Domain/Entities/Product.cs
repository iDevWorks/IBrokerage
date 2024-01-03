using Gibs.Domain.Entities;

namespace Gibs.Portal.Domain.Entities
{
    public class Product
    {
        public Product() { }

        public Product(Policy policy, string name, string description)
        {
            ArgumentNullException.ThrowIfNull(policy);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(description);

            Policy = policy;
            PolicyId = policy.Id;
            Name = name;
            Description = description;
        }

        public string Id { get; private set; }
        public string PolicyId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public virtual Policy Policy { get; private set; }
    }
}
