
namespace BlazorEcommerce.Server.Services.CateGoryServices
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<Category>>> AddCategory(Category category);
        Task<ServiceResponse<List<Category>>> DeleteCategory(int id);
        Task<ServiceResponse<List<Category>>> GetAdminCategories();
        Task<ServiceResponse<List<Category>>> GetAllCategoriesAsync();
        Task<ServiceResponse<Category>> GetCategoryByIdAsync(int id);
        Task<ServiceResponse<List<Category>>> UpdateCategory(Category category);
    }
}