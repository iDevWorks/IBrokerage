namespace Gibs.Portal.Domain.Entities
{
    public class Order
    {
        public Order() { }

        public Order(Product product, decimal totalAmount)
        {
            ProductId = product.Id;
            Product = product;
            PurchaseDate = DateTimeOffset.UtcNow;
            TotalAmount = totalAmount;
        }

        public int Id { get; set; }
        public string ProductId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTimeOffset PurchaseDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public virtual Product Product { get; set; }
    }

    public enum PaymentStatus
    {
        Success,
        Pending,
        Failed
    }
}
