using System.ComponentModel.DataAnnotations;

namespace NorthwindTraders.Application.DTOs
{
    public class AddressDto
    {
        [Required]
        [StringLength(60, ErrorMessage = "Address cannot exceed 60 characters.")]
        public string Street { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "City cannot exceed 15 characters.")]
        public string City { get; set; }

        [StringLength(15, ErrorMessage = "Region cannot exceed 15 characters.")]
        public string Region { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "PostalCode cannot exceed 10 characters.")]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "Country cannot exceed 15 characters.")]
        public string Country { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsAddressValid { get; set; }
        public string ValidationMessage { get; set; }

        public string FormattedAddress { get; set; }
    }
}
