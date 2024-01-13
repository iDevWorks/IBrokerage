using Gibs.Domain.Entities;

namespace Gibs.Domain.Entities
{
    public class Product
    {
        public Product() { }

        public Product(string productName, string classId, string shortName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(productName);
            ArgumentException.ThrowIfNullOrWhiteSpace(classId);
            ArgumentException.ThrowIfNullOrWhiteSpace(shortName);

            ProductName = productName;
            ClassId = classId;
            ShortName = shortName;
        }

        public string Id { get; private set; }
        public string ClassId { get; private set; }
        public string? MidClassId { get; private set; }
        public string ProductName { get; private set; }
        public string ShortName { get; private set; }
        public string? NaiconTypeId { get; private set; }


    }
}
