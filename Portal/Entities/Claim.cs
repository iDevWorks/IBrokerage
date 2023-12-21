﻿namespace Portal.Entities
{
    public class Claim
    {
        public Claim() { }

        public Claim(Policy policy, string description) 
        {
            ArgumentNullException.ThrowIfNull(policy);

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException(nameof(description));

            PolicyId = policy.Id;
            Description = description;
            DateFiled = DateTime.UtcNow;
            Policy = policy;
        }

        public string Id { get; private set; }
        public string PolicyId { get; private set; }
        public DateTime DateFiled { get; private set; }
        public string Description { get; private set; }

        public virtual Policy Policy { get; private set; }
    }
}