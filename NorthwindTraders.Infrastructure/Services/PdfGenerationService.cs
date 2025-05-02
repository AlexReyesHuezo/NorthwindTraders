using NorthwindTraders.Application.DTOs;
using NorthwindTraders.Application.Interfaces;
using System.Text;
using IronPdf;
using System.Globalization;

namespace NorthwindTraders.Application.Services
{
    public class PdfGenerationService : IPdfGenerationService
    {
        public byte[] GenerateOrderDetailsPdf(OrderDto order)
        {
            var renderer = new ChromePdfRenderer();
            renderer.RenderingOptions.PaperSize = IronPdf.Rendering.PdfPaperSize.A4;
            renderer.RenderingOptions.MarginTop = 25;
            renderer.RenderingOptions.MarginBottom = 25;
            renderer.RenderingOptions.MarginLeft = 25;
            renderer.RenderingOptions.MarginRight = 25;

            var html = GenerateOrderDetailsHtml(order);
            var pdf = renderer.RenderHtmlAsPdf(html);

            return pdf.BinaryData;
        }

        public byte[] GenerateAllOrdersPdf(List<OrderDto> orders)
        {
            var renderer = new ChromePdfRenderer();
            renderer.RenderingOptions.PaperSize = IronPdf.Rendering.PdfPaperSize.A4;
            renderer.RenderingOptions.MarginTop = 25;
            renderer.RenderingOptions.MarginBottom = 25;
            renderer.RenderingOptions.MarginLeft = 25;
            renderer.RenderingOptions.MarginRight = 25;

            var html = GenerateAllOrdersHtml(orders);
            var pdf = renderer.RenderHtmlAsPdf(html);

            return pdf.BinaryData;
        }

        private string GenerateOrderDetailsHtml(OrderDto order)
        {
            var culture = CultureInfo.GetCultureInfo("en-US");
            var sb = new StringBuilder();

            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"en\">");
            sb.AppendLine("<head>");
            sb.AppendLine("    <meta charset=\"UTF-8\">");
            sb.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            sb.AppendLine("    <title>Order Details</title>");
            sb.AppendLine("    <style>");
            sb.AppendLine("        body { font-family: Arial, sans-serif; color: #333; margin: 0; padding: 20px; }");
            sb.AppendLine("        .header { display: flex; justify-content: space-between; border-bottom: 2px solid #86cfa3; padding-bottom: 10px; margin-bottom: 20px; }");
            sb.AppendLine("        .logo { font-size: 24px; font-weight: bold; color: #3498db; }");
            sb.AppendLine("        .order-info { background-color: #f9f9f9; padding: 15px; border-radius: 5px; margin-bottom: 20px; }");
            sb.AppendLine("        .section-title { color: #2c8fb5; border-bottom: 1px solid #ddd; padding-bottom: 5px; margin-top: 20px; }");
            sb.AppendLine("        table { width: 100%; border-collapse: collapse; margin: 15px 0; }");
            sb.AppendLine("        th { background-color: #86cfa3; color: white; text-align: left; padding: 10px; }");
            sb.AppendLine("        td { padding: 8px; border-bottom: 1px solid #ddd; }");
            sb.AppendLine("        .totals { text-align: right; margin-top: 20px; }");
            sb.AppendLine("        .totals p { margin: 5px 0; }");
            sb.AppendLine("        .total-price { font-weight: bold; font-size: 18px; color: #2c8fb5; }");
            sb.AppendLine("        .footer { margin-top: 30px; text-align: center; font-size: 12px; color: #888; }");
            sb.AppendLine("        .map-container { margin-top: 20px; border: 1px solid #ddd; padding: 10px; }");
            sb.AppendLine("        .map-title { color: #2c8fb5; margin-bottom: 10px; }");
            sb.AppendLine("    </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");

            // Header
            sb.AppendLine("    <div class=\"header\">");
            sb.AppendLine("        <div class=\"logo\">Northwind Traders</div>");
            sb.AppendLine($"        <div>Order #{order.OrderId}</div>");
            sb.AppendLine("    </div>");

            // Order Information
            sb.AppendLine("    <div class=\"order-info\">");
            sb.AppendLine($"        <p><strong>Order Date:</strong> {order.OrderDate.ToString("MMMM dd, yyyy")}</p>");
            sb.AppendLine($"        <p><strong>Required Date:</strong> {order.RequiredDate?.ToString("MMMM dd, yyyy") ?? "Not specified"}</p>");
            sb.AppendLine($"        <p><strong>Shipped Date:</strong> {order.ShippedDate?.ToString("MMMM dd, yyyy") ?? "Not shipped yet"}</p>");
            sb.AppendLine("    </div>");

            // Shipping Details
            sb.AppendLine("    <h3 class=\"section-title\">Shipping Details</h3>");
            sb.AppendLine("    <div>");
            sb.AppendLine($"        <p><strong>Ship Name:</strong> {order.ShipName}</p>");
            sb.AppendLine($"        <p><strong>Ship Address:</strong> {order.ShipAddress}</p>");
            sb.AppendLine($"        <p><strong>Ship City:</strong> {order.ShipCity}</p>");
            sb.AppendLine($"        <p><strong>Ship Region:</strong> {order.ShipRegion ?? "N/A"}</p>");
            sb.AppendLine($"        <p><strong>Ship Postal Code:</strong> {order.ShipPostalCode}</p>");
            sb.AppendLine($"        <p><strong>Ship Country:</strong> {order.ShipCountry}</p>");

            // Geocoded info if available
            if (order.Latitude.HasValue && order.Longitude.HasValue)
            {
                sb.AppendLine($"        <p><strong>Coordinates:</strong> {order.Latitude.Value.ToString("F6", culture)}, {order.Longitude.Value.ToString("F6", culture)}</p>");
            }

            sb.AppendLine("    </div>");

            // Order Details
            sb.AppendLine("    <h3 class=\"section-title\">Order Items</h3>");
            sb.AppendLine("    <table>");
            sb.AppendLine("        <thead>");
            sb.AppendLine("            <tr>");
            sb.AppendLine("                <th>Product</th>");
            sb.AppendLine("                <th>Unit Price</th>");
            sb.AppendLine("                <th>Quantity</th>");
            sb.AppendLine("                <th>Discount</th>");
            sb.AppendLine("                <th>Total</th>");
            sb.AppendLine("            </tr>");
            sb.AppendLine("        </thead>");
            sb.AppendLine("        <tbody>");

            foreach (var item in order.OrderDetail)
            {
                decimal lineTotal = item.UnitPrice * item.Quantity * (1 - (decimal)item.Discount);
                sb.AppendLine("            <tr>");
                sb.AppendLine($"                <td>{item.ProductName}</td>");
                sb.AppendLine($"                <td>${item.UnitPrice.ToString("F2", culture)}</td>");
                sb.AppendLine($"                <td>{item.Quantity}</td>");
                sb.AppendLine($"                <td>{item.Discount.ToString("P0", culture)}</td>");
                sb.AppendLine($"                <td>${lineTotal.ToString("F2", culture)}</td>");
                sb.AppendLine("            </tr>");
            }

            sb.AppendLine("        </tbody>");
            sb.AppendLine("    </table>");

            // Order Totals
            sb.AppendLine("    <div class=\"totals\">");
            sb.AppendLine($"        <p><strong>Subtotal:</strong> ${(order.GetTotalPrice() - order.Freight).ToString("F2", culture)}</p>");
            sb.AppendLine($"        <p><strong>Freight:</strong> ${order.Freight.ToString("F2", culture)}</p>");
            sb.AppendLine($"        <p class=\"total-price\"><strong>Total:</strong> ${order.GetTotalPrice().ToString("F2", culture)}</p>");
            sb.AppendLine("    </div>");

            // Footer
            sb.AppendLine("    <div class=\"footer\">");
            sb.AppendLine("        <p>Northwind Traders, Inc. | Global Shipping Solutions | Document generated on " + DateTime.Now.ToString("MMMM dd, yyyy") + "</p>");
            sb.AppendLine("    </div>");

            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }

        private string GenerateAllOrdersHtml(List<OrderDto> orders)
        {
            var culture = CultureInfo.GetCultureInfo("en-US");
            var sb = new StringBuilder();

            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang=\"en\">");
            sb.AppendLine("<head>");
            sb.AppendLine("    <meta charset=\"UTF-8\">");
            sb.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            sb.AppendLine("    <title>All Orders Report</title>");
            sb.AppendLine("    <style>");
            sb.AppendLine("        body { font-family: Arial, sans-serif; color: #333; margin: 0; padding: 20px; }");
            sb.AppendLine("        .header { display: flex; justify-content: space-between; border-bottom: 2px solid #86cfa3; padding-bottom: 10px; margin-bottom: 20px; }");
            sb.AppendLine("        .logo { font-size: 24px; font-weight: bold; color: #3498db; }");
            sb.AppendLine("        .summary { background-color: #f9f9f9; padding: 15px; border-radius: 5px; margin-bottom: 20px; }");
            sb.AppendLine("        .section-title { color: #2c8fb5; border-bottom: 1px solid #ddd; padding-bottom: 5px; margin-top: 20px; }");
            sb.AppendLine("        table { width: 100%; border-collapse: collapse; margin: 15px 0; }");
            sb.AppendLine("        th { background-color: #86cfa3; color: white; text-align: left; padding: 10px; }");
            sb.AppendLine("        td { padding: 8px; border-bottom: 1px solid #ddd; }");
            sb.AppendLine("        .footer { margin-top: 30px; text-align: center; font-size: 12px; color: #888; }");
            sb.AppendLine("        .status-shipped { color: #27ae60; }");
            sb.AppendLine("        .status-pending { color: #e67e22; }");
            sb.AppendLine("    </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");

            // Header
            sb.AppendLine("    <div class=\"header\">");
            sb.AppendLine("        <div class=\"logo\">Northwind Traders</div>");
            sb.AppendLine("        <div>All Orders Report</div>");
            sb.AppendLine("    </div>");

            // Summary
            sb.AppendLine("    <div class=\"summary\">");
            sb.AppendLine($"        <p><strong>Total Orders:</strong> {orders.Count}</p>");
            sb.AppendLine($"        <p><strong>Shipped Orders:</strong> {orders.Count(o => o.ShippedDate.HasValue)}</p>");
            sb.AppendLine($"        <p><strong>Pending Orders:</strong> {orders.Count(o => !o.ShippedDate.HasValue)}</p>");
            sb.AppendLine($"        <p><strong>Total Revenue:</strong> ${orders.Sum(o => o.GetTotalPrice()).ToString("F2", culture)}</p>");
            sb.AppendLine("    </div>");

            // Orders Table
            sb.AppendLine("    <h3 class=\"section-title\">Orders</h3>");
            sb.AppendLine("    <table>");
            sb.AppendLine("        <thead>");
            sb.AppendLine("            <tr>");
            sb.AppendLine("                <th>Order ID</th>");
            sb.AppendLine("                <th>Customer ID</th>");
            sb.AppendLine("                <th>Order Date</th>");
            sb.AppendLine("                <th>Required Date</th>");
            sb.AppendLine("                <th>Shipped Date</th>");
            sb.AppendLine("                <th>Ship Country</th>");
            sb.AppendLine("                <th>Total</th>");
            sb.AppendLine("            </tr>");
            sb.AppendLine("        </thead>");
            sb.AppendLine("        <tbody>");

            foreach (var order in orders.OrderByDescending(o => o.OrderDate))
            {
                string statusClass = order.ShippedDate.HasValue ? "status-shipped" : "status-pending";
                string shippedText = order.ShippedDate.HasValue ? order.ShippedDate.Value.ToString("MM/dd/yyyy") : "Pending";

                sb.AppendLine("            <tr>");
                sb.AppendLine($"                <td>{order.OrderId}</td>");
                sb.AppendLine($"                <td>{order.CustomerId}</td>");
                sb.AppendLine($"                <td>{order.OrderDate.ToString("MM/dd/yyyy")}</td>");
                sb.AppendLine($"                <td>{order.RequiredDate?.ToString("MM/dd/yyyy") ?? "N/A"}</td>");
                sb.AppendLine($"                <td class=\"{statusClass}\">{shippedText}</td>");
                sb.AppendLine($"                <td>{order.ShipCountry}</td>");
                sb.AppendLine($"                <td>${order.GetTotalPrice().ToString("F2", culture)}</td>");
                sb.AppendLine("            </tr>");
            }

            sb.AppendLine("        </tbody>");
            sb.AppendLine("    </table>");

            // Footer
            sb.AppendLine("    <div class=\"footer\">");
            sb.AppendLine("        <p>Northwind Traders, Inc. | Global Shipping Solutions | Report generated on " + DateTime.Now.ToString("MMMM dd, yyyy") + "</p>");
            sb.AppendLine("    </div>");

            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }
    }
}