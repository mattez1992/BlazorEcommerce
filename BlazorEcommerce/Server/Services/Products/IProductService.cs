
using BlazorEcommerce.Shared.Dtos;

namespace BlazorEcommerce.Server.Services.Products
{
    public interface IProductService
    {
        Task<ServiceResponse<Product>> CreateProduct(Product product);
        Task<ServiceResponse<bool>> DeleteProduct(int productId);
        Task<ServiceResponse<List<Product>>> GetAdminProducts();
        Task<ServiceResponse<Product>> GetByIdAsync(int id);
        Task<ServiceResponse<List<Product>>> GetFeaturedProducts();
        Task<ServiceResponse<List<Product>>> GetProductsAsync();
        Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl);
        Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText);
        Task<ServiceResponse<ProductSearchDTO>> SearchProducts(string searchText, int page);
        Task<ServiceResponse<Product>> UpdateProduct(Product product);
    }
}