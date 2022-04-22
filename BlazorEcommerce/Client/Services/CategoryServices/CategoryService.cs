using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Client.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Category> AdminCategories { get; set; } = new List<Category>();
        public event Action OnChange;
        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
       
        public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/category");
            if (result == null)
            {
                ServiceResponse<List<Category>> response = new()
                {
                    Success = false,
                    Message = "Something went wrong try again later"
                };
                return response;
            }
            else
            {
                Categories = result.Data;
                return result;
            }
        }

        public async Task<ServiceResponse<Category>> GetCategoryAsync(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Category>>($"api/category/{id}");
            if (result == null)
            {
                ServiceResponse<Category> response = new()
                {
                    Success = false,
                    Message = "Something went wrong try again later"
                };
                return response;
            }
            else
            {
                return result;
            }
        }

        #region Admin
        public async Task AddCategory(Category category)
        {
            var response = await _httpClient.PostAsJsonAsync("api/category/admin", category);
            AdminCategories = (await response.Content
                .ReadFromJsonAsync<ServiceResponse<List<Category>>>()).Data;
            await GetCategoriesAsync();
            OnChange.Invoke();
        }
        // for client side only
        public Category CreateNewCategory()
        {
            var newCategory = new Category { IsNew = true, Editing = true };
            AdminCategories.Add(newCategory);
            OnChange.Invoke();
            return newCategory;
        }

        public async Task DeleteCategory(int categoryId)
        {
            var response = await _httpClient.DeleteAsync($"api/category/admin/{categoryId}");
            AdminCategories = (await response.Content
                .ReadFromJsonAsync<ServiceResponse<List<Category>>>()).Data;
            await GetCategoriesAsync();
            OnChange.Invoke();
        }

        public async Task GetAdminCategories()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/category/admin");
            if (response != null && response.Data != null)
                AdminCategories = response.Data;
        }
        public async Task UpdateCategory(Category category)
        {
            var response = await _httpClient.PutAsJsonAsync("api/category/admin", category);
            AdminCategories = (await response.Content
                .ReadFromJsonAsync<ServiceResponse<List<Category>>>()).Data;
            await GetCategoriesAsync();
            OnChange.Invoke();
        }
        #endregion
    }
}
