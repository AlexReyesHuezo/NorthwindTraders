using NorthwindTraders.Application.DTOs;

namespace NorthwindTraders.Application.Interfaces
{
    public interface IPdfGenerationService
    {
        /// <summary>
        /// Generates a PDF report for a specific order with complete details
        /// </summary>
        /// <param name="order">The order to generate a report for</param>
        /// <returns>PDF file as a byte array</returns>
        byte[] GenerateOrderDetailsPdf(OrderDto order);

        /// <summary>
        /// Generates a PDF report containing all orders summary
        /// </summary>
        /// <param name="orders">List of orders to include in the report</param>
        /// <returns>PDF file as a byte array</returns>
        byte[] GenerateAllOrdersPdf(List<OrderDto> orders);
    }
}
