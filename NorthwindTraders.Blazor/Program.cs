using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NorthwindTraders.Blazor;
using NorthwindTraders.Application.Interfaces;
using NorthwindTraders.Infrastructure;
using Microsoft.JSInterop;
using NorthwindTraders.Application.DTOs;
using NorthwindTraders.Application.Services;
using NorthwindTraders.Infrastructure.Services;
using static Google.Protobuf.Collections.MapField<TKey, TValue>;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

@code {
    private DateTime startDate = DateTime.Now.AddMonths(-1);
private DateTime endDate = DateTime.Now;
private OrderStatus? selectedStatus = null;
private List<OrderDto> reportData;
private bool isLoading = false;
private bool reportGenerated = false;

private async Task GenerateReport()
{
    try
    {
        isLoading = true;
        reportGenerated = true;

        reportData = await OrderService.GetOrdersForReportAsync(
            startDate,
            endDate,
            selectedStatus
        );
    }
    catch (Exception)
    {
        // Handle error
        reportData = new List<OrderDto>();
    }
    finally
    {
        isLoading = false;
    }
}

private async Task ExportToPdf()
{
    try
    {
        var pdfBytes = await ReportService.GenerateOrdersReportPdfAsync(
            reportData,
            startDate,
            endDate,
            selectedStatus
        );

        await JSRuntime.InvokeVoidAsync(
            "downloadFile",
            $"OrdersReport_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}.pdf",
            "application/pdf",
            pdfBytes
        );
    }
    catch (Exception)
    {
        // Handle error
        await JSRuntime.InvokeVoidAsync("alert", "Error generating PDF report");
    }
}

private string GetStatusBadgeColor(OrderStatus status)
{
    return status switch
    {
        OrderStatus.Pending => "warning",
        OrderStatus.Shipped => "success",
        OrderStatus.Delivered => "info",
        OrderStatus.Cancelled => "danger",
        _ => "secondary"
    };
}
});

// Add infrastructure services
builder.Services.AddInfrastructureServices(builder.Configuration);

// Add application services
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAddressValidationService, GoogleAddressValidationService>();
builder.Services.AddScoped<IGeolocationService, GoogleGeolocationService>();
builder.Services.AddScoped<IPdfGenerationService, ChromePdfService>();

// Add web services
builder.Services.AddScoped<ReportService>();

await builder.Build().RunAsync();