
namespace BlazorEcommerce.Client.Services.ProductServices
{
    public interface IProductSerivice
    {
        List<Product> Products { get; set; }

        Task GetProducts();
    }
}