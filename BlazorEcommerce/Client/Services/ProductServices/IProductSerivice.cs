
namespace BlazorEcommerce.Client.Services.ProductServices
{
    public interface IProductSerivice
    {
        List<Product> Products { get; set; }

        event Action ProductsChanged;
        Task<ServiceResponse<Product>> GetProductById(int id);
        Task GetProducts(string? categoryUrl = null);
    }
}