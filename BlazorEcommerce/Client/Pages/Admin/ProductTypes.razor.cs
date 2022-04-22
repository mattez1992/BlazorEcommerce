namespace BlazorEcommerce.Client.Pages.Admin
{
    public partial class ProductTypes : ComponentBase, IDisposable
    {
        [Inject]
        public IProductTypeService ProductTypeService { get; set; }
        ProductType _editingProductType = null;

        protected override async Task OnInitializedAsync()
        {
            await ProductTypeService.GetProductTypes();
            ProductTypeService.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            ProductTypeService.OnChange -= StateHasChanged;
        }
        private async Task CancelEditing()
        {
            _editingProductType = null;
            await ProductTypeService.GetProductTypes();
        }
        private void EditProductType(ProductType productType)
        {
            productType.Editing = true;
            _editingProductType = productType;
        }

        private void CreateNewProductType()
        {
            _editingProductType = ProductTypeService.CreateNewProductType();
        }

        private async Task UpdateProductType()
        {
            if (_editingProductType.IsNew)
            {
                await ProductTypeService.AddProductType(_editingProductType);
            }            
            else
            {
                await ProductTypeService.UpdateProductType(_editingProductType);
            }
                
            _editingProductType = null;
        }
    }
}
