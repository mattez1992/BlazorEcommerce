using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Category>>>> GetAllCategories()
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            if(result.Success == false)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Category>>> GetCategoryById(int id)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id);
            if (result.Success == false)
            {
                return BadRequest(result);
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
