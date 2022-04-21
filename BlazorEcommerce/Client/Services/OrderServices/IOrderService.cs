
namespace BlazorEcommerce.Client.Services.OrderServices
{
    public interface IOrderService
    {
        Task<OrderDetailsDto> GetOrderDetails(int orderId);
        Task<List<OrderOverViewDto>> GetOrders();
        Task PlaceOrder();
    }
}