using NorthwindTraders.Application.DTOs;

namespace NorthwindTraders.Application.Interfaces
{
    public interface IAddressValidationService
    {
        /// <summary>
        /// Validates an address using Google Maps Address Validation API
        /// </summary>
        /// <param name="address">The address to validate</param>
        /// <returns>A validated address with standardized components</returns>
        Task<AddressDto> ValidateAddressAsync(string address);

        /// <summary>
        /// Gets address suggestions based on a partial address input
        /// </summary>
        /// <param name="partialAddress">The partial address input</param>
        /// <returns>A list of address suggestions</returns>
        Task<List<string>> GetAddressSuggestionsAsync(string partialAddress);
    }
}
