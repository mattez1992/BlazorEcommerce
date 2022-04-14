

namespace BlazorEcommerce.Client.Shared.ProductComponents
{
    public partial class ProductList : ComponentBase, IDisposable
    {
        [Inject]
        public IProductSerivice ProductSerivice { get; set; }

        protected override void OnInitialized()
        {
            ProductSerivice.ProductsChanged += StateHasChanged;
            base.OnInitialized();
        }
        public void Dispose()
        {
            ProductSerivice.ProductsChanged -= StateHasChanged;
        }
    }
}
