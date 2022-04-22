
namespace BlazorEcommerce.Server.Services.OrderServices
{
    public interface IOrderService
    {
        Task<ServiceResponse<OrderDetailsDto>> GetOrderDetails(int orderId);
        Task<ServiceResponse<List<OrderOverViewDto>>> GetOrders();
        Task<ServiceResponse<bool>> PlaceOrder(int userId);
    }
}