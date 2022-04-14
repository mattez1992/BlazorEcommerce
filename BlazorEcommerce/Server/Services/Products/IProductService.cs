
namespace BlazorEcommerce.Server.Services.Products
{
    public interface IProductService
    {
        Task<ServiceResponse<Product>> GetByIdAsync(int id);
        Task<ServiceResponse<List<Product>>> GetProductsAsync();
        Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl);
    }
}