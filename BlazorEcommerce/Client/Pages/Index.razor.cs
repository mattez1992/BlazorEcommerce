namespace BlazorEcommerce.Client.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public IProductSerivice ProductSerivice { get; set; }
        [Parameter]
        public string? CategoryUrl { get; set; } = null;
        protected override async Task OnParametersSetAsync()
        {
            await ProductSerivice.GetProducts(CategoryUrl);
            await base.OnParametersSetAsync();
        }
    }
}
