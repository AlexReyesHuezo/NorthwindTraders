using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NorthwindTraders.Application.DTOs;

namespace NorthwindTraders.Application.Interfaces
{
    public interface IPdfService
    {
        /// <summary>
        /// Generates a PDF report based on the provided data.
        /// </summary>
        /// <param name="reportData">The data to include in the report.</param>
        /// <returns>A byte array representing the generated PDF file.</returns>
        Task<byte[]> GeneratePdfReportAsync(OrderDto reportData);

        /// <summary>
        /// Generates a PDF report for a list of items.
        /// </summary>
        /// <param name="items">The list of items to include in the report.</param>
        /// <returns>A byte array representing the generated PDF file.</returns>
        Task<byte[]> GeneratePdfForItemsAsync(IEnumerable<ItemDto> items);

        /// <summary>
        /// Saves the generated PDF to a specified file path.
        /// </summary>
        /// <param name="pdfOrders">The byte array of the orders in the PDF file.</param>
        /// <param name="filePath">The file path to save the PDF.</param>
        /// <returns>A task representing the save operation.</returns>
        Task SavePdfToFileAsync(byte[] pdfOrders, string filePath);
    }
}
