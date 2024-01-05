namespace Gibs.Portal.Domain.Entities
{
    public class Order
    {
        public Order() { }

        public Order(Product product, decimal totalAmount)
        {
            ProductId = product.Id;
            Product = product;
            DateOrdered = DateTime.UtcNow;
            TotalAmount = totalAmount;
        }

        public int OrderId { get; private set; }
        public string ProductId { get; private set; }
        public DateTime DateOrdered { get; private set; }
        public decimal TotalAmount { get; private set; }

        public virtual Product Product { get; private set; }

        public enum PaymentStatus
        {
            Success,
            Pending,
            Failed
        }
    }
}
