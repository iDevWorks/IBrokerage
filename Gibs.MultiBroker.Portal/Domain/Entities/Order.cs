namespace Gibs.Domain.Entities
{
    public class Order
    {
        #pragma warning disable CS8618
        public Order() { }
        #pragma warning restore CS8618

        public Order(ICollection<Policy> policies, Insured customer)
        {
            ArgumentNullException.ThrowIfNull(customer);
            ArgumentNullException.ThrowIfNull(policies);
            ArgumentOutOfRangeException.ThrowIfZero(policies.Count);

            Policies = policies;
            Insured = customer;
            Reference = Guid.NewGuid();
            CreatedUtc = DateTime.UtcNow;
            TotalAmount = policies.Sum(x => x.GrossPremium);
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

        public Insured Insured { get; private set; } //customer
        public ICollection<Policy> Policies { get; } = [];
    }

    public enum OrderStatus
    {
        PENDING,
        FAILED,
        SUCCESS
    }
}
