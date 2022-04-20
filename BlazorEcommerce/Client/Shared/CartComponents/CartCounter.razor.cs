namespace BlazorEcommerce.Client.Shared.CartComponents
{
    public partial class CartCounter : ComponentBase, IDisposable
    {

        [Inject]
        public ICartItemService CartItemService { get; set; }
        [Inject]
        public ISyncLocalStorageService LocalStorageService { get; set; }

        private int GetCartItemsCount()
        {
            var cart = LocalStorageService.GetItem<List<CartItem>>("cartItems");
            return cart != null ? cart.Count : 0;
        }
        protected override void OnInitialized()
        {
            CartItemService.OnChange += StateHasChanged;
            base.OnInitialized();
        }
        public void Dispose()
        {
            CartItemService.OnChange -= StateHasChanged;
        }
    }
}
