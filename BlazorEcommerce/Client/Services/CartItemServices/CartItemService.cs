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

        public CartItemService(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }
        public event Action OnChange;

        public async Task AddToCart(CartItem cartItem)
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

            await GetCartItemsCount();
        }
        public async Task GetCartItemsCount()
        {

            var cart = await _localStorageService.GetItemAsync<List<CartItem>>("cartItems");
            await _localStorageService.SetItemAsync<int>("cartItemsCount", cart != null ? cart.Count : 0);


            OnChange.Invoke();
        }

        public async Task<List<CartProductResponseDTO>> GetCartProducts()
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

        public async Task RemoveProductFromCart(int productId, int productTypeId)
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

        public Task StoreCartItems(bool emptyLocalCart)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateQuantity(CartProductResponseDTO product)
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
