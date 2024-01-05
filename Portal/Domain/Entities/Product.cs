using Gibs.Domain.Entities;

namespace Gibs.Portal.Domain.Entities
{
    public class Product
    {
        public Product() { }

        public Product(string name, string description)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);

            if (coverages.Count == 0)
                throw new InvalidOperationException("The list is empty.");

            Name = name;
            Coverages = coverages;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}
