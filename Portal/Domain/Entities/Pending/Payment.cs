namespace Gibs.Domain.Entities
{
    public class Payment
    {
        public Payment() { }

        public Payment(Client client, Policy policy, decimal amount)
        {
            ArgumentNullException.ThrowIfNull(client);
            ArgumentNullException.ThrowIfNull(policy);

            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

            Client = client;
            ClientId = client.Id;
            Policy = policy;
            PolicyId = policy.Id;
            Amount = amount;
        }

        public int Id { get; private set; }
        public string ClientId { get; private set; }
        public string PolicyId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime DatePaid { get; private set; }

        public virtual Client Client { get; private set; }
        public virtual Policy Policy { get; private set;}
    }
}