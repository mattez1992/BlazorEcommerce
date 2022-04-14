

using BlazorEcommerce.Client.Services.ProductServices;

namespace BlazorEcommerce.Client.Pages
{
    public partial class ProductDetails : ComponentBase
    {
        [Inject]
        public IProductSerivice? ProductSerivice { get; set; }

        private Product? _product = null;
        private string? _message = string.Empty;
        [Parameter]
        public int Id { get; set; }


        protected override async Task OnParametersSetAsync()
        {
            var response = await ProductSerivice.GetProductById(Id);
            if(response != null && response.Data != null && response.Success)
            {
                _product = response.Data;
            }
            else
            {
                _message = response.Message;
            }
            
        }
    }
}
