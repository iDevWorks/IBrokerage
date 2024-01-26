
namespace Gibs.Domain.Entities
{
    public class Product
    {
        #pragma warning disable CS8618
        public Product() { }
        #pragma warning restore CS8618

        public Product(string productId, string classId, string? midClassId, string productName, string shortName)
        {

            ArgumentException.ThrowIfNullOrEmpty(productId);
            ArgumentException.ThrowIfNullOrEmpty(classId);
            ArgumentException.ThrowIfNullOrEmpty(productName);
            ArgumentException.ThrowIfNullOrEmpty(shortName);

            Id = productId;
            ClassId = classId;
            MidClassId = midClassId;
            ProductName = productName;
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
