using System.Globalization;

namespace NorthwindTraders.Domain.Entities
{
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsAddressValid { get; set; }

        public string FormattedAddress => $"{Street}, {City}, {Region}, {PostalCode}, {Country}";
    }
}
