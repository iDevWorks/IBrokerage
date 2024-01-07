using Gibs.Domain.Entities;

namespace Gibs.Portal.Domain.Entities
{
    public class Order
    {
        public Order() { }

        public Order(Product product, Client client, string transReference, decimal totalAmount)
        {
            TransReference = transReference;
            Client = client;
            Product = product;
            DateOrdered = DateTime.UtcNow;
            TotalAmount = totalAmount;
            PaymentStatus = "PENDING";
            PaymentMethod = "PAYSTACK";
        }

        public int OrderId { get; private set; }
        public DateTime DateOrdered { get; private set; }
        public decimal TotalAmount { get; private set; }
        public Client Client {  get; private set; }
        public string PaymentMethod { get; private set; }
        public string PaymentStatus { get; private set; }
        public string TransReference { get; private set; }

        public virtual Product Product { get; private set; }
    }
}
