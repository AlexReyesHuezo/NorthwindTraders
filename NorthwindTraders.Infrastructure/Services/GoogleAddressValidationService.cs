using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using NorthwindTraders.Application.DTOs;
using NorthwindTraders.Application.Interfaces;

namespace NorthwindTraders.Infrastructure.Services
{
    public class GoogleAddressValidationService : IAddressValidationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly IGeolocationService _geolocationService;

        public GoogleAddressValidationService(HttpClient httpClient, IConfiguration configuration, IGeolocationService geolocationService)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GoogleMaps:ApiKey"] ?? throw new ArgumentNullException(nameof(configuration), "Google Maps API key is missing from configuration");
            _geolocationService = geolocationService;
        }

        public async Task<AddressDto> ValidateAddressAsync(string address)
        {
            try
            {
                var requestUrl = $"https://addressvalidation.googleapis.com/v1:validateAddress?key={_apiKey}";

                var requestBody = new
                {
                    address = new
                    {
                        addressLines = new[] { address }
                    }
                };

                var response = await _httpClient.PostAsJsonAsync(requestUrl, requestBody);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonDocument = JsonDocument.Parse(responseContent);
                var root = jsonDocument.RootElement;

                // Check if the address was validated
                bool isValid = root.TryGetProperty("result", out var resultProperty) &&
                              resultProperty.TryGetProperty("verdict", out var verdictProperty) &&
                              verdictProperty.TryGetProperty("addressComplete", out var addressCompleteProperty) &&
                              addressCompleteProperty.GetBoolean();

                // Extract the formatted address and components
                var addressDto = new AddressDto
                {
                    IsAddressValid = isValid,
                    ValidationMessage = isValid ? "Address is valid" : "Address could not be fully validated"
                };

                if (root.TryGetProperty("result", out var result) &&
                    result.TryGetProperty("address", out var validatedAddress) &&
                    validatedAddress.TryGetProperty("formattedAddress", out var formattedAddress))
                {
                    addressDto.FormattedAddress = formattedAddress.GetString();

                    // Extract address components
                    if (validatedAddress.TryGetProperty("addressComponents", out var components))
                    {
                        foreach (var component in components.EnumerateArray())
                        {
                            if (component.TryGetProperty("componentType", out var componentType) &&
                                component.TryGetProperty("componentName", out var componentName))
                            {
                                var type = componentType.GetString();
                                var name = componentName.GetProperty("text").GetString();

                                switch (type)
                                {
                                    case "route":
                                        addressDto.Street = name;
                                        break;
                                    case "locality":
                                        addressDto.City = name;
                                        break;
                                    case "administrative_area_level_1":
                                        addressDto.Region = name;
                                        break;
                                    case "postal_code":
                                        addressDto.PostalCode = name;
                                        break;
                                    case "country":
                                        addressDto.Country = name;
                                        break;
                                }
                            }
                        }
                    }
                }

                // Get geocoded coordinates for the validated address
                if (isValid && !string.IsNullOrEmpty(addressDto.FormattedAddress))
                {
                    var geolocation = await _geolocationService.GeocodeAddressAsync(addressDto.FormattedAddress);
                    if (geolocation.Success)
                    {
                        addressDto.Latitude = geolocation.Latitude;
                        addressDto.Longitude = geolocation.Longitude;
                    }
                }

                return addressDto;
            }
            catch (Exception ex)
            {
                return new AddressDto
                {
                    IsAddressValid = false,
                    ValidationMessage = $"Error validating address: {ex.Message}"
                };
            }
        }

        public async Task<List<string>> GetAddressSuggestionsAsync(string partialAddress)
        {
            try
            {
                var requestUrl = $"https://maps.googleapis.com/maps/api/place/autocomplete/json?input={Uri.EscapeDataString(partialAddress)}&types=address&key={_apiKey}";

                var response = await _httpClient.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonDocument = JsonDocument.Parse(responseContent);
                var root = jsonDocument.RootElement;

                var suggestions = new List<string>();

                if (root.TryGetProperty("predictions", out var predictions))
                {
                    foreach (var prediction in predictions.EnumerateArray())
                    {
                        if (prediction.TryGetProperty("description", out var description))
                        {
                            suggestions.Add(description.GetString());
                        }
                    }
                }

                return suggestions;
            }
            catch
            {
                return new List<string>();
            }
        }
    }
}