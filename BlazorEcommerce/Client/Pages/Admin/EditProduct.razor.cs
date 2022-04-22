using Microsoft.JSInterop;

namespace BlazorEcommerce.Client.Pages.Admin
{
    public partial class EditProduct : ComponentBase
    {

        [Inject]
        public IProductSerivice ProductService { get; set; }
        [Inject]
        public IProductTypeService ProductTypeService { get; set; }
        [Inject]
        public ICategoryService CategoryService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public int Id { get; set; }

        Product _product = new Product();
        bool _loading = true;
        string _btnText = "";
        string _msg = "Loading...";

        protected override async Task OnInitializedAsync()
        {
            await ProductTypeService.GetProductTypes();
            await CategoryService.GetAdminCategories();
            await base.OnInitializedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id == 0)
            {
                _product = new Product
                {
                    IsNew = true,
                };
                _btnText = "Create Product";
            }
            else
            {
                Product dbProduct = (await ProductService.GetProductById(Id)).Data;
                if(dbProduct == null)
                {
                    _msg =$"Product with id {Id} does not exist.";
                    return;
                }
                _product = dbProduct;
                _product.Editing = true;
                _btnText = "Update Product";
            }
            _loading = false;
             base.OnParametersSetAsync();
        }

        private void RemoveVariant(int productTypeId)
        {
            var variant = _product.Variants.FirstOrDefault(x => x.ProductTypeId == productTypeId);
            if(variant == null)
            {
                return;
            }
            if (variant.IsNew)
            {
                _product.Variants.Remove(variant);
            }
            else
            {
                variant.Deleted = true;
            }
        }
        private void AddVariant()
        {
            _product.Variants
                .Add(new ProductVariant { IsNew = true, ProductId = _product.Id });
        }
        async void AddOrUpdateProduct()
        {
            if (_product.IsNew)
            {
                var result = await ProductService.CreateProduct(_product);
                NavigationManager.NavigateTo("admin/products");
            }
            else
            {
                _product.IsNew = false;
                _product = await ProductService.UpdateProduct(_product);
                NavigationManager.NavigateTo("admin/products");
            }
        }
        async void DeleteProduct()
        {
            bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm",
                $"Do you really want to delete '{_product.Title}'?");
            if (confirmed)
            {
                await ProductService.DeleteProduct(_product);
                NavigationManager.NavigateTo("admin/products");
            }
        }
    }
}
