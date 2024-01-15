namespace Gibs.Domain.Entities
{
    public class KycInfo
    {
        #pragma warning disable CS8618
        public KycInfo() { }
        #pragma warning restore CS8618

        public KycInfo(KycTypeEnum kycType, string kycNumber, DateOnly? issueDate, DateOnly? expiryDate, string? taxNumber)
        {
            KycType = kycType;
            KycNumber = kycNumber;
            IssueDate = issueDate;
            ExpiryDate = expiryDate;
            TaxNumber = taxNumber;
        }

        public KycTypeEnum? KycType { get; private set; }
        public string? KycNumber { get; private set; }
        public DateOnly? IssueDate { get; private set; }
        public DateOnly? ExpiryDate { get; private set; }
        public string? TaxNumber { get; private set; }
    }

    public enum KycTypeEnum
    {
        NIN, BVN, DRIVERS_LICENSE, PASSPORT
    }
}
