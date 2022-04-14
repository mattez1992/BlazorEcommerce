using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Server.Services.CateGoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _dataContext;

        public CategoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<List<Category>>> GetAllCategoriesAsync()
        {
            ServiceResponse<List<Category>> serviceResponse = new();
            try
            {
                var result = await _dataContext.Categories.ToListAsync();
                if (result.Count == 0)
                {
                    serviceResponse.Message = "No categories found";
                    serviceResponse.Success = false;
                }
                else
                {
                    serviceResponse.Data = result;
                }
                return serviceResponse;
            }
            catch (Exception e)
            {

                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<Category>> GetCategoryByIdAsync(int id)
        {
            ServiceResponse<Category> serviceResponse = new();
            try
            {
                var result = await _dataContext.Categories.FirstOrDefaultAsync(category => category.Id == id);
                if (result == null)
                {
                    serviceResponse.Message = "No categories found";
                    serviceResponse.Success = false;
                }
                else
                {
                    serviceResponse.Data = result;
                }
                return serviceResponse;
            }
            catch (Exception e)
            {

                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
                return serviceResponse;
            }
        }
    }
}
