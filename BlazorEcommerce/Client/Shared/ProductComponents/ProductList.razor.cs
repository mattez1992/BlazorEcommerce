

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

        private string GetPriceText(Product product)
        {
            var variants = product.Variants;
            if(variants.Count == 0)
            {
                return string.Empty;
            }
            else if(variants.Count == 1)
            {
                return $"${variants[0].Price}";
            }

            decimal minPrice = variants.Min(x => x.Price);
            return $"Starting at ${minPrice}";
        }
    }
}
