using Gibs.Domain.Entities;

namespace Gibs.Portal.Domain.Entities
{
    public class Product
    {
        public Product() { }

        public Product(Policy policy, string name, List<string> coverages)
        {
            ArgumentNullException.ThrowIfNull(policy);
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            if (coverages.Count == 0)
                throw new InvalidOperationException("The list is empty.");

            Policy = policy;
            PolicyId = policy.Id;
            Name = name;
            Coverages = coverages;
        }

        public string Id { get; private set; }
        public string PolicyId { get; private set; }
        public string Name { get; private set; }
        public List<string> Coverages { get; private set; } = [];

        public virtual Policy Policy { get; private set; }
    }
}
