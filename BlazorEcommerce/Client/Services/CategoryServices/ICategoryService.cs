
namespace BlazorEcommerce.Client.Services.CategoryServices
{
    public interface ICategoryService
    {
        List<Category> Categories { get; set; }
        List<Category> AdminCategories { get; set; }

        event Action OnChange;

        Task AddCategory(Category category);
        Category CreateNewCategory();
        Task DeleteCategory(int categoryId);
        Task GetAdminCategories();
        Task<ServiceResponse<List<Category>>> GetCategoriesAsync();
        Task<ServiceResponse<Category>> GetCategoryAsync(int id);
        Task UpdateCategory(Category category);
    }
}