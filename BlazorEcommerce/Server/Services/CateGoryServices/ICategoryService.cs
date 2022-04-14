
namespace BlazorEcommerce.Server.Services.CateGoryServices
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<Category>>> GetAllCategoriesAsync();
        Task<ServiceResponse<Category>> GetCategoryByIdAsync(int id);
    }
}