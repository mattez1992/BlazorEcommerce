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
                var result = await _dataContext.Categories.Where(c => !c.Deleted && c.Visible)
                .ToListAsync();
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
        #region Admin Service
        public async Task<ServiceResponse<List<Category>>> UpdateCategory(Category category)
        {
            var dbCategory = (await GetCategoryByIdAsync(category.Id)).Data;
            if (dbCategory == null)
            {
                return new ServiceResponse<List<Category>>
                {
                    Success = false,
                    Message = "Category not found."
                };
            }

            dbCategory.Name = category.Name;
            dbCategory.Url = category.Url;
            dbCategory.Visible = category.Visible;

            await _dataContext.SaveChangesAsync();

            return await GetAdminCategories();

        }
        public async Task<ServiceResponse<List<Category>>> GetAdminCategories()
        {
            var categories = await _dataContext.Categories
                .Where(c => !c.Deleted)
                .ToListAsync();
            return new ServiceResponse<List<Category>>
            {
                Data = categories
            };
        }
        public async Task<ServiceResponse<List<Category>>> AddCategory(Category category)
        {
            category.Editing = category.IsNew = false;
            _dataContext.Categories.Add(category);
            await _dataContext.SaveChangesAsync();
            return await GetAdminCategories();
        }

        public async Task<ServiceResponse<List<Category>>> DeleteCategory(int id)
        {
            Category category = (await GetCategoryByIdAsync(id)).Data;
            if (category == null)
            {
                return new ServiceResponse<List<Category>>
                {
                    Success = false,
                    Message = "Category not found."
                };
            }

            category.Deleted = true;
            await _dataContext.SaveChangesAsync();

            return await GetAdminCategories();
        }
        #endregion
    }
}
