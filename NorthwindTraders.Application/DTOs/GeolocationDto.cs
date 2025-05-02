using System.ComponentModel.DataAnnotations;

namespace NorthwindTraders.Application.DTOs
{
    public class GeolocationDto
    {
        [Required]
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
        public double Latitude { get; set; }
        [Required]
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
        public double Longitude { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public string FormattedCoordinates
        {
            get
            {
                // Format the coordinates to 6 decimal places, because that's the precision of most GPS systems
                // For example: 37.774929, -122.419418
                return $"{Latitude.ToString("F6")}, {Longitude.ToString("F6")}";
            }
        }
    }
}
