using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Client.Services.CartItemServices
{
    public class CartItemService : ICartItemService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;

        public CartItemService(ILocalStorageService localStorageService, HttpClient httpClient, IAuthService authService)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
            _authService = authService;
        }
        public event Action OnChange;

        public async Task AddToCart(CartItem cartItem)
        {
            if (await _authService.IsUserAuthenticated())
            {
                await _httpClient.PostAsJsonAsync("api/cart/add", cartItem);
            }
            else
            {
                var cart = await _localStorageService.GetItemAsync<List<CartItem>>("cartItems");
                if (cart == null)
                {
                    cart = new List<CartItem>();
                }

                var existingItem = cart.Find(x => x.ProductId == cartItem.ProductId &&
                    x.ProductTypeId == cartItem.ProductTypeId);
                if (existingItem == null)
                {
                    cart.Add(cartItem);
                }
                else
                {
                    existingItem.Quantity += cartItem.Quantity;
                }

                await _localStorageService.SetItemAsync("cartItems", cart);
            }
           

            await GetCartItemsCount();
        }
        public async Task GetCartItemsCount()
        {

            if (await _authService.IsUserAuthenticated())
            {
                var result = await _httpClient.GetFromJsonAsync<ServiceResponse<int>>("api/cart/count");
                var count = result.Data;

                await _localStorageService.SetItemAsync<int>("cartItemsCount", count);
            }
            else
            {
                var cart = await _localStorageService.GetItemAsync<List<CartItem>>("cartItems");
                await _localStorageService.SetItemAsync<int>("cartItemsCount", cart != null ? cart.Count : 0);
            }

            OnChange.Invoke();

            OnChange.Invoke();
        }

        public async Task<List<CartProductResponseDTO>> GetCartProducts()
        {
            if (await _authService.IsUserAuthenticated())
            {
                var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<CartProductResponseDTO>>>("api/cart");
                return response.Data;
            }
            else
            {
                var cartItems = await _localStorageService.GetItemAsync<List<CartItem>>("cartItems");
                if (cartItems == null)
                {
                    return new List<CartProductResponseDTO>();
                }
                var response = await _httpClient.PostAsJsonAsync("api/cart/products", cartItems);
                var cartProducts =
                    await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponseDTO>>>();
                return cartProducts.Data;
            }
        }

        public async Task RemoveProductFromCart(int productId, int productTypeId)
        {

            if (await _authService.IsUserAuthenticated())
            {
                await _httpClient.DeleteAsync($"api/cart/{productId}/{productTypeId}");
            }
            else
            {
                var cart = await _localStorageService.GetItemAsync<List<CartItem>>("cartItems");
                if (cart == null)
                {
                    return;
                }

                var cartItem = cart.Find(x => x.ProductId == productId
                    && x.ProductTypeId == productTypeId);
                if (cartItem != null)
                {
                    cart.Remove(cartItem);
                    await _localStorageService.SetItemAsync("cartItems", cart);
                }
            }           
        }

        public async Task StoreCartItems(bool emptyLocalCart = false)
        {
            var localCart = await _localStorageService.GetItemAsync<List<CartItem>>("cartItems");
            if (localCart == null)
            {
                return;
            }

            await _httpClient.PostAsJsonAsync("api/cart", localCart);

            if (emptyLocalCart)
            {
                await _localStorageService.RemoveItemAsync("cartItems");
            }
        }

        public async Task UpdateQuantity(CartProductResponseDTO product)
        {
            if (await _authService.IsUserAuthenticated())
            {
                var request = new CartItem
                {
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                    ProductTypeId = product.ProductTypeId
                };
                await _httpClient.PutAsJsonAsync("api/cart/update-quantity", request);
            }
            else
            {
                var cart = await _localStorageService.GetItemAsync<List<CartItem>>("cartItems");
                if (cart == null)
                {
                    return;
                }

                var cartItem = cart.Find(x => x.ProductId == product.ProductId
                    && x.ProductTypeId == product.ProductTypeId);
                if (cartItem != null)
                {
                    cartItem.Quantity = product.Quantity;
                    await _localStorageService.SetItemAsync("cartItems", cart);
                }
            }       
        }
    }
}
