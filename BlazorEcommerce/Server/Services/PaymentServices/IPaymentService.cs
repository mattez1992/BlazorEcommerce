using Stripe.Checkout;

namespace BlazorEcommerce.Server.Services.PaymentServices
{
    public interface IPaymentService
    {
        Task<Session> CreateCheckOutSession();
        Task<ServiceResponse<bool>> FulFillOrder(HttpRequest request);
    }
}