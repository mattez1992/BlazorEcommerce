﻿namespace BlazorEcommerce.Client.Pages
{
    public partial class Cart : ComponentBase
    {
        [Inject]
        public ICartItemService CartItemService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        string _message = "Loading cart...";
        List<CartProductResponseDTO> _cartItems = null;
        protected override async Task OnInitializedAsync()
        {
            await LoadCart();
        }
        private async Task UpdateQuantity(ChangeEventArgs e, CartProductResponseDTO product)
        {
            product.Quantity = int.Parse(e.Value.ToString());
            if (product.Quantity < 1)
                product.Quantity = 1;
            await CartItemService.UpdateQuantity(product);
        }
        private async Task RemoveProductFromCart(int productId, int productTypeId)
        {
            await CartItemService.RemoveProductFromCart(productId, productTypeId);
            await LoadCart();
        }
        private async Task LoadCart()
        {
            await CartItemService.GetCartItemsCount();
            _cartItems = await CartItemService.GetCartProducts();
            if (_cartItems == null || _cartItems.Count == 0)
            {
                _message = "Your cart is empty.";
            }
        }
    }
}
