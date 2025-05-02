using NorthwindTraders.Application.DTOs;

namespace NorthwindTraders.Application.Interfaces
{
    public interface IGeolocationService
    {
        /// <summary>
        /// Geocodes an address to get coordinates
        /// </summary>
        /// <param name="address">The address to geocode</param>
        /// <returns>Geocoded coordinates (latitude and longitude)</returns>
        Task<GeolocationDto> GeocodeAddressAsync(string address);
    }
}
