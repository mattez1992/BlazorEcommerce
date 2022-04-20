namespace BlazorEcommerce.Client.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        public IProductSerivice ProductSerivice { get; set; }
        [Parameter]
        public string? CategoryUrl { get; set; } = null;
        [Parameter]
        public string? SearchText { get; set; } = null;
        [Parameter]
        public int Page { get; set; } = 1;

        protected override async Task OnParametersSetAsync()
        {

            if (SearchText != null)
            {
                await ProductSerivice.SearchProducts(SearchText, Page);
            }
            else
            {
                await ProductSerivice.GetProducts(CategoryUrl);
            }
            await base.OnParametersSetAsync();
        }
    }
}
