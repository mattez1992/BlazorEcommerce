
namespace BlazorEcommerce.Client.Services.ProductServices
{
    public interface IProductSerivice
    {
        List<Product> Products { get; set; }
        string Message { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public string LastSearchText { get; set; }

        event Action ProductsChanged;
        Task<ServiceResponse<Product>> GetProductById(int id);
        Task GetProducts(string? categoryUrl = null);
        Task<List<string>> GetProductSearchSuggestions(string searchText);
        Task SearchProducts(string searchText, int page);
    }
}