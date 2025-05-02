using System.Text.Json;
using Microsoft.Extensions.Configuration;
using NorthwindTraders.Application.DTOs;
using NorthwindTraders.Application.Interfaces;

namespace NorthwindTraders.Infrastructure.Services
{
    public class GoogleGeolocationService : IGeolocationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GoogleGeolocationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GoogleMaps:ApiKey"] ?? throw new ArgumentNullException(nameof(configuration), "Google Maps API key is missing from configuration");
        }

        public async Task<GeolocationDto> GeocodeAddressAsync(string address)
        {
            try
            {
                var requestUrl = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={_apiKey}";

                var response = await _httpClient.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonDocument = JsonDocument.Parse(responseContent);
                var root = jsonDocument.RootElement;

                if (root.TryGetProperty("status", out var status) && status.GetString() == "OK" &&
                    root.TryGetProperty("results", out var results) && results.GetArrayLength() > 0)
                {
                    var firstResult = results[0];

                    if (firstResult.TryGetProperty("geometry", out var geometry) &&
                        geometry.TryGetProperty("location", out var location) &&
                        location.TryGetProperty("lat", out var lat) &&
                        location.TryGetProperty("lng", out var lng))
                    {
                        return new GeolocationDto
                        {
                            Latitude = lat.GetDouble(),
                            Longitude = lng.GetDouble(),
                            Success = true
                        };
                    }
                }

                return new GeolocationDto
                {
                    Success = false,
                    ErrorMessage = "Could not geocode the address."
                };
            }
            catch (Exception ex)
            {
                return new GeolocationDto
                {
                    Success = false,
                    ErrorMessage = $"Error geocoding address: {ex.Message}"
                };
            }
        }
    }
}