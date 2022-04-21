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
            var count = LocalStorageService.GetItem<int>("cartItemsCount");
            return count;
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
