using Gibs.Domain.Entities;

namespace Gibs.Portal.Domain.Entities
{
    public class Order
    {
        public Order() { }

        public Order(Product product, Client client, string transReference, decimal totalAmount)
        {
            ArgumentNullException.ThrowIfNull(product);
            ArgumentNullException.ThrowIfNull(client);
            ArgumentException.ThrowIfNullOrWhiteSpace(transReference);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(totalAmount);

            TransReference = transReference;
            Client = client;
            Product = product;
            CreatedUtc = DateTime.UtcNow;
            TotalAmount = totalAmount;
            PaymentStatus = OrderStatus.PENDING;
            PaymentMethod = "PAYSTACK";
        }

        public void PaymentFailed(string errorMessage)
        {
            Remark = errorMessage;
            PaymentStatus = OrderStatus.FAILED;
        }

        public void PaymentSuccess(decimal amount)
        {
            if(TotalAmount != amount)
            {
                throw new ArgumentException("the amount expected is not the same");
            }
            PaymentDate = DateTime.UtcNow;
            PaymentStatus = OrderStatus.SUCCESS;
        }

        public int OrderId { get; private set; }
        public DateTime CreatedUtc { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string PaymentMethod { get; private set; }
        public OrderStatus PaymentStatus { get; private set; }
        public string TransReference { get; private set; }
        public string? Remark { get; private set; }
        public DateTime? PaymentDate { get; private set; }

        public virtual Client Client { get; private set; }
        public virtual Product Product { get; private set; }
    }

    public enum OrderStatus
    {
        PENDING,
        FAILED,
        SUCCESS
    }
}
