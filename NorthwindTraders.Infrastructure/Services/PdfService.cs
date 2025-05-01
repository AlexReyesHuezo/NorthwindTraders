using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NorthwindTraders.Application.DTOs;
using NorthwindTraders.Application.Interfaces;

namespace NorthwindTraders.Infrastructure.Services
{
    public class PdfService : IPdfService
    {
        private readonly IConfiguration _configuration;

        public PdfService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<byte[]> GeneratePdfReportAsync(OrderDto order)
        {
            var html = new StringBuilder();

            // Add company header and styles
            html.Append(@"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Order Report</title>
                <style>
                    body { font-family: Arial, sans-serif; margin: 0; padding: 20px; color: #333; }
                    .header { background-color: #f0f8ff; padding: 10px; margin-bottom: 20px; }
                    .logo { float: left; }
                    .company-info { float: right; text-align: right; }
                    h1 { color: #4682b4; margin-top: 0; }
                    table { width: 100%; border-collapse: collapse; margin: 20px 0; }
                    th { background-color: #4682b4; color: white; text-align: left; padding: 8px; }
                    td { padding: 8px; border-bottom: 1px solid #ddd; }
                    .total { font-weight: bold; text-align: right; }
                    .footer { margin-top: 30px; font-size: 0.8em; text-align: center; color: #777; }
                    .map-container { margin: 20px 0; }
                </style>
            </head>
            <body>");

            // Add header section with order details
            html.Append($@"
                <div class='header'>
                    <div class='logo'>
                        <h1>Northwind Traders</h1>
                    </div>
                    <div class='company-info'>
                        <p>Order Report</p>
                        <p>Generated: {DateTime.Now:yyyy-MM-dd HH:mm}</p>
                    </div>
                    <div style='clear: both;'></div>
                </div>
                
                <h2>Order #{order.OrderId}</h2>");

            // Add order details and shipping info
            html.Append($@"
                <h3>Order Information</h3>
                <table>
                    <tr>
                        <td><strong>Order Date:</strong></td>
                        <td>{order.OrderDate:yyyy-MM-dd}</td>
                    </tr>
                    <tr>
                        <td><strong>Ship To:</strong></td>
                        <td>{order.ShipName}<br>
                            {order.ShipAddress}<br>
                            {order.ShipCity}, {(string.IsNullOrEmpty(order.ShipRegion) ? "" : order.ShipRegion + " ")}{order.ShipPostalCode}<br>
                            {order.ShipCountry}
                        </td>
                    </tr>
                </table>");

            // Add location coordinates if available
            if (order.Latitude.HasValue && order.Longitude.HasValue)
            {
                html.Append($@"
                    <div class='map-container'>
                        <p><strong>Location Coordinates:</strong> {order.Latitude}, {order.Longitude}</p>
                    </div>");
            }

            // Add order items table
            html.Append(@"
                <h3>Order Items</h3>
                <table>
                    <thead>
                        <tr>
                            <th>Product</th>
                            <th>Unit Price</th>
                            <th>Quantity</th>
                            <th>Total</th>
                        </tr>
                    </thead>
                    <tbody>");

            foreach (var item in order.OrderDetail)
            {
                html.Append($@"
                        <tr>
                            <td>{item.ProductId}</td>
                            <td>${item.UnitPrice:F2}</td>
                            <td>{item.Quantity}</td>
                            <td>${item.TotalPrice:F2}</td>
                        </tr>");
            }

            // Add total
            html.Append($@"
                        <tr>
                            <td colspan='3' class='total'>Total:</td>
                            <td class='total'>${order.TotalPrice:F2}</td>
                        </tr>
                    </tbody>
                </table>
                <div class='footer'>
                    <p>Thank you for your business!</p>
                    <p>Northwind Traders - Global Shipping Excellence</p>
                </div>
            </body>
            </html>");

            return await Task.FromResult(Encoding.UTF8.GetBytes(html.ToString()));
        }

        public async Task<byte[]> GeneratePdfForItemsAsync(IEnumerable<ItemDto> items)
        {
            var html = new StringBuilder();

            // Add header and style
            html.Append(@"
            <!DOCTYPE html>
            <html>
            <head>
                <title>Items Report</title>
                <style>
                    body { font-family: Arial, sans-serif; margin: 0; padding: 20px; color: #333; }
                    .header { background-color: #f0f8ff; padding: 10px; margin-bottom: 20px; }
                    table { width: 100%; border-collapse: collapse; margin: 20px 0; }
                    th { background-color: #4682b4; color: white; text-align: left; padding: 8px; }
                    td { padding: 8px; border-bottom: 1px solid #ddd; }
                    .total { font-weight: bold; text-align: right; }
                </style>
            </head>
            <body>
                <div class='header'>
                    <h1>Items Report</h1>
                    <p>Generated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + @"</p>
                </div>
                <table>
                    <thead>
                        <tr>
                            <th>Product Name</th>
                            <th>Unit Price</th>
                            <th>In Stock</th>
                            <th>On Order</th>
                            <th>Total Value</th>
                        </tr>
                    </thead>
                    <tbody>");

            foreach (var item in items)
            {
                html.Append($@"
                    <tr>
                        <td>{item.ProductName}</td>
                        <td>${item.UnitPrice:F2}</td>
                        <td>{item.UnitsInStock}</td>
                        <td>{item.UnitsOnOrder}</td>
                        <td>${item.TotalValue:F2}</td>
                    </tr>");
            }

            html.Append(@"
                    </tbody>
                </table>
            </body>
            </html>");

            return await Task.FromResult(Encoding.UTF8.GetBytes(html.ToString()));
        }

        public async Task SavePdfToFileAsync(byte[] pdfOrders, string filePath)
        {
            await File.WriteAllBytesAsync(filePath, pdfOrders);
        }
    }
}
