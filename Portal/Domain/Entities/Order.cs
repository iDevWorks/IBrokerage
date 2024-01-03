using Gibs.Domain.Entities;

namespace Gibs.Portal.Domain.Entities
{
    public class Order
    {
        public Order() { }

        public Order(Product product, decimal totalAmount)
        {
            ProductId = product.Id;
            Product = product;
            DateOrdered = DateTimeOffset.UtcNow;
            TotalAmount = totalAmount;
        }

        public int Id { get; private set; }
        public string ProductId { get; private set; }
        public DateTimeOffset DateOrdered { get; private set; }
        public decimal TotalAmount { get; private set; }

        public virtual Product Product { get; private set; }
    }
}
