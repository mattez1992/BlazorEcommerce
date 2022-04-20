


namespace BlazorEcommerce.Client.Pages
{
    public partial class ProductDetails : ComponentBase
    {
        [Inject]
        public IProductSerivice? ProductSerivice { get; set; }
        [Inject]
        public ICartItemService CartItemService { get; set; }
        private Product? _product = null;
        private string? _message = string.Empty;
        private int currentTypeId = 1;
        [Parameter]
        public int Id { get; set; }


        protected override async Task OnParametersSetAsync()
        {
            var response = await ProductSerivice.GetProductById(Id);
            if(response != null && response.Data != null && response.Success)
            {
                _product = response.Data;
                if (_product.Variants.Count > 0)
                {
                    currentTypeId = _product.Variants[0].ProductTypeId;
                }
            }
            else
            {
                _message = response.Message;
            }        
        }
        private async Task AddTOCart()
        {
            var productVariant = GetSelectedVariant();
            var cartItem = new CartItem
            {
                ProductId = productVariant.ProductId,
                ProductTypeId = productVariant.ProductTypeId,
            };
            await CartItemService.AddToCart(cartItem);
        }
        private ProductVariant GetSelectedVariant()
        {
            var variant = _product.Variants.FirstOrDefault(v => v.ProductTypeId == currentTypeId);
            return variant;
        }
    }
}
