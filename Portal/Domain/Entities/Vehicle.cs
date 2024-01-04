using Gibs.Domain.Entities;

namespace Gibs.Portal.Domain.Entities
{
    public class Vehicle
    {
        public Vehicle() { }

        public Vehicle(Product product, string registrationNumber, string brand, string model, string color, int seats, TransmissionType transmission, DateTime purchaseDate)
        {
            ArgumentNullException.ThrowIfNull(product);
            ArgumentException.ThrowIfNullOrWhiteSpace(registrationNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(brand);
            ArgumentException.ThrowIfNullOrWhiteSpace(model);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(seats);


            RegistrationNumber = registrationNumber;
            Brand = brand;
            Model = model;
            Color = color;
            Seats = seats;
            Transmission = transmission;
            PurchaseDate = purchaseDate;
        }

        public string Id { get; private set; }
        public string ProductId { get; private set; }
        public string RegistrationNumber { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public string Color { get; private set; }
        public int Seats { get; private set; }
        public TransmissionType Transmission { get; private set; }
        public DateTime PurchaseDate { get; private set; }

        public virtual Product Product { get; private set; }
    }

    public enum TransmissionType
    {
        Automatic,
        Manual
    }
}
