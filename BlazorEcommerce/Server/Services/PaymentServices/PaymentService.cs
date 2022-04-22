using BlazorEcommerce.Server.Services.OrderServices;
using Stripe;
using Stripe.Checkout;

namespace BlazorEcommerce.Server.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly ICartItemService _cartItemService;
        private readonly IAuthService _authService;
        private readonly IOrderService _orderService;
        private readonly IConfiguration _configuration;

        // service for stripe payment 
        public PaymentService(ICartItemService cartItemService, IAuthService authService, IOrderService orderService, IConfiguration configuration)
        {

            _cartItemService = cartItemService;
            _authService = authService;
            _orderService = orderService;
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration.GetSection("AppSettings:StripeApiKey").Value;
        }

        public async Task<Session> CreateCheckOutSession()
        {
            var products = (await _cartItemService.GetDbCartProducts()).Data;
            var lineItems = new List<SessionLineItemOptions>();
            products.ForEach(p => lineItems.Add(new()
            {
                PriceData = new()
                {
                    UnitAmountDecimal = p.Price * 100,
                    Currency = "usd",
                    ProductData = new()
                    {
                        Name = p.Title,
                        Images = new List<string> { p.ImageUrl }
                    }
                },
                Quantity = p.Quantity
            }));

            var options = new SessionCreateOptions
            {
                CustomerEmail = _authService.GetUserEmail(),
                ShippingAddressCollection =
                    new SessionShippingAddressCollectionOptions
                    {
                        AllowedCountries = new List<string> { "US","SE" }
                    },
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://localhost:7168/order-success",
                CancelUrl = "https://localhost:7168/cart"
            };

            SessionService service = new();
            Session session = service.Create(options);
            return session;
        }

        public async Task<ServiceResponse<bool>> FulFillOrder(HttpRequest request)
        {
            var json = await new StreamReader(request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    request.Headers["Stripe-Signature"],
                    _configuration.GetSection("AppSettings:StripeWebHook").Value
                    );
                if(stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Session;
                    var user = await _authService.GetUserByEmail(session.CustomerEmail);
                    await _orderService.PlaceOrder(user.Id);
                }
                return new ServiceResponse<bool> { Data = true };
            }
            catch (Exception e)
            {

                return new ServiceResponse<bool> { Data = false, Success = false, Message = e.Message };
            }
        }

    }
}
