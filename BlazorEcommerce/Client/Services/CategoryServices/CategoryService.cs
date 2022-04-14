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
        public List<Category> Categories { get; set; }
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
    }
}
