﻿
namespace Gibs.Domain.Entities
{
    public class Broker : Person
    {
        #pragma warning disable CS8618
        public Broker() { }
        #pragma warning restore CS8618

        public Broker(string brokerName, string registrationNo, string firstName, string lastName, string email, string phone, string password)
          : base(GenerateBrokerId(brokerName), firstName, lastName, email, phone, password)
        {
            ArgumentException.ThrowIfNullOrEmpty(brokerName);
            ArgumentException.ThrowIfNullOrEmpty(registrationNo);

            BrokerName = brokerName;
            RegistrationNo = registrationNo;
        }

        private static string GenerateBrokerId(string brokerName)
        {
            return brokerName.ToUpper().Trim().Replace(" ", "_");
        }

        public string BrokerName { get; private set; }
        public string RegistrationNo { get; private set; }
        public KycInfo Kyc { get; private set; } = new();
    }
}
