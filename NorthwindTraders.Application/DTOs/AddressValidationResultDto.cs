using System.ComponentModel.DataAnnotations;

namespace NorthwindTraders.Application.DTOs
{
    public class AddressValidationResultDto
    {
        [Required]
        public bool IsValid { get; set; }

        [Required]
        public AddressDto validatedAddress { get; set; }

        [Required]
        public GeolocationDto Geolocation { get; set; }

        public string ValidationMessage { get; set; }
        public List<string> ValidationIssues { get; set; } = new List<string>();

    }
}
