using System.Threading.Tasks;

namespace NorthwindTraders.Application.Interfaces
{
    public interface IGoogleMapsService
    {
        /// <summary>
        /// Validates the given address components and returns a boolean indicating if the address is valid.
        /// </summary>
        /// <param name="street">The street address.</param>
        /// <param name="city">The city name.</param>
        /// <param name="state">The state or region.</param>
        /// <param name="postalCode">The postal or ZIP code.</param>
        /// <param name="country">The country name.</param>
        /// <returns>A Task that resolves to true if the address is valid, otherwise false.</returns>
        Task<bool> ValidateAddressAsync(string street, string city, string state, string postalCode, string country);

        /// <summary>
        /// Retrieves geocoded coordinates (latitude and longitude) for the given address.
        /// </summary>
        /// <param name="street">The street address.</param>
        /// <param name="city">The city name.</param>
        /// <param name="state">The state or region.</param>
        /// <param name="postalCode">The postal or ZIP code.</param>
        /// <param name="country">The country name.</param>
        /// <returns>A Task that resolves to a tuple containing latitude and longitude.</returns>
        Task<(double Latitude, double Longitude)> GetGeocodedCoordinatesAsync(string street, string city, string state, string postalCode, string country);
    }
}