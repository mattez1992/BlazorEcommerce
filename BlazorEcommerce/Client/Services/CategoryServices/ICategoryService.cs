
namespace BlazorEcommerce.Client.Services.CategoryServices
{
    public interface ICategoryService
    {
        List<Category> Categories { get; set; }

        Task<ServiceResponse<List<Category>>> GetCategoriesAsync();
        Task<ServiceResponse<Category>> GetCategoryAsync(int id);
    }
}