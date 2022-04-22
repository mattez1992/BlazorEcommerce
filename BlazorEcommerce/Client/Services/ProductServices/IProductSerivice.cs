
namespace BlazorEcommerce.Client.Services.ProductServices
{
    public interface IProductSerivice
    {
        List<Product> Products { get; set; }
        string Message { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public string LastSearchText { get; set; }
        List<Product> AdminProducts { get; set; }

        event Action ProductsChanged;

        Task<Product> CreateProduct(Product product);
        Task DeleteProduct(Product product);
        Task GetAdminProducts();
        Task<ServiceResponse<Product>> GetProductById(int id);
        Task GetProducts(string? categoryUrl = null);
        Task<List<string>> GetProductSearchSuggestions(string searchText);
        Task SearchProducts(string searchText, int page);
        Task<Product> UpdateProduct(Product product);
    }
}