namespace Gibs.Domain.Entities
{
    public class Order
    {
        #pragma warning disable CS8618
        public Order() { }
        #pragma warning restore CS8618

        public Order(ICollection<Policy> policies, Product product, Insured client, string transReference, decimal totalAmount)
        {
            ArgumentNullException.ThrowIfNull(product);
            ArgumentNullException.ThrowIfNull(client);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(totalAmount);
            if(policies == null || policies.Count == 0)
            {
                throw new ArgumentNullException(nameof(policies));
            }

            Reference = Guid.NewGuid();
            Insured = client;
            Product = product;
            CreatedUtc = DateTime.UtcNow;
            TotalAmount = totalAmount;
            PaymentStatus = OrderStatus.PENDING;
            PaymentMethod = "PAYSTACK";
            
        }

        public void PaymentFailed(string errorMessage)
        {
            Remarks = errorMessage;
            PaymentStatus = OrderStatus.FAILED;
        }

        public void PaymentSuccess(decimal amount)
        {
            if(TotalAmount != amount)
            {
                throw new ArgumentException("the amount expected is not the same");
            }
            PaymentUtc = DateTime.UtcNow;
            PaymentStatus = OrderStatus.SUCCESS;
        }

        public int OrderId { get; private set; }
        public DateTime CreatedUtc { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string PaymentMethod { get; private set; }
        public OrderStatus PaymentStatus { get; private set; }
        public Guid Reference { get; private set; }
        public string? Remarks { get; private set; }
        public DateTime? PaymentUtc { get; private set; }

        public virtual Insured Insured { get; private set; }
        public virtual Product Product { get; private set; }
        public virtual ICollection<Policy> Policies { get; private set; }
    }

    public enum OrderStatus
    {
        PENDING,
        FAILED,
        SUCCESS
    }
}
