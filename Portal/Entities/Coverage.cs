namespace Portal.Entities
{
    public class Coverage
    {
        public Coverage() { }

        public Coverage(Policy policy, string type, string description) 
        {
            ArgumentNullException.ThrowIfNull(policy);

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type));

            if(string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException(nameof(description));

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