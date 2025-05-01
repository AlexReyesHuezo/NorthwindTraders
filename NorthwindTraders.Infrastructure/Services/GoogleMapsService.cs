using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NorthwindTraders.Application.Interfaces;

namespace NorthwindTraders.Infrastructure.Services
{
    public class GoogleMapsService : IGoogleMapsService
    {
        private readonly HttpClient _httClient;
        private readonly string _apiKey;

        public GoogleMapsService(HttpClient httpClient, string apiKey)
        {
            _httClient = httpClient;
            _apiKey = apiKey;

            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                throw new ArgumentException("API key cannot be null or empty.", nameof(apiKey));
            }
        }

        public async Task<bool> ValidateAddressAsync(string street, string city, string state, string postalCode, string country)
        {
            var address = $"{street}, {city}, {state}, {postalCode}, {country}";
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={_apiKey}";
            var response = await _httClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var content = await response.Content.ReadAsStringAsync();
            return content.Contains("\"status\" : \"OK\"");
        }

        public async Task<(double Latitude, double Longitude)> GetGeocodedCoordinatesAsync(string street, string city, string state, string postalCode, string country)
        {
            if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Street cannot be empty", nameof(street));
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City cannot be empty", nameof(city));

            var address = $"{street}, {city}, {state}, {postalCode}, {country}".Trim(',', ' ');
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={_apiKey}";

            using var response = await _httClient.GetAsync(url);
            response.EnsureSuccessStatusCode(); // This will throw HttpRequestException for non-success status codes

            var content = await response.Content.ReadAsStringAsync();

            using var document = JsonDocument.Parse(content);
            var root = document.RootElement;

            // Check the status of the response
            var status = root.GetProperty("status").GetString();
            if (status != "OK")
            {
                throw new GoogleMapsApiException($"Geocoding failed with status: {status}");
            }

            // Get the first result (most relevant)
            var results = root.GetProperty("results");
            if (!results.GetArrayLength().Equals(0))
            {
                var location = results[0]
                    .GetProperty("geometry")
                    .GetProperty("location");

                var latitude = location.GetProperty("lat").GetDouble();
                var longitude = location.GetProperty("lng").GetDouble();

                return (latitude, longitude);
            }

            throw new GoogleMapsApiException("No results found for the specified address");
        }

        public class GoogleMapsApiException : Exception
        {
            public GoogleMapsApiException(string message) : base(message) { }
            public GoogleMapsApiException(string message, Exception inner) : base(message, inner) { }
        }

    }
}
