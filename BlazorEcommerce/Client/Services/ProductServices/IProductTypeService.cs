
namespace BlazorEcommerce.Client.Services.ProductServices
{
    public interface IProductTypeService
    {
        List<ProductType> ProductTypes { get; set; }

        event Action OnChange;

        Task AddProductType(ProductType productType);
        ProductType CreateNewProductType();
        Task GetProductTypes();
        Task UpdateProductType(ProductType productType);
    }
}