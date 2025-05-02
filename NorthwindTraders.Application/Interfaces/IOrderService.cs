using NorthwindTraders.Application.DTOs;

namespace NorthwindTraders.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(int orderId);
        Task<bool> CreateOrderAsync(OrderDto order);
        Task<bool> UpdateOrderAsync(int orderId, OrderDto order);
        Task<bool> DeleteOrderAsync(int orderId);
    }
}
