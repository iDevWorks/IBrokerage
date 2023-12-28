namespace Gibs.Domain.Entities
{
    public class Coverage
    {
        public Coverage() { }

        public Coverage(Policy policy, string type, string description) 
        {
            ArgumentNullException.ThrowIfNull(policy);
            ArgumentException.ThrowIfNullOrWhiteSpace(type);
            ArgumentException.ThrowIfNullOrWhiteSpace(description);

            Policy = policy;
            PolicyId = policy.Id;
            Type = type;
            Description = description;
        }

        public string Id { get; private set; }
        public string PolicyId { get; private set; }
        public string Type { get; private set; }
        public string Description { get; private set; }

        public virtual Policy Policy { get; private set; }
    }
}