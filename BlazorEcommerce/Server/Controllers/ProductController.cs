using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
        {   
            return Ok(await _productService.GetProductsAsync());
        }
        [HttpGet("id")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductById(int id)
        {
            return Ok(await _productService.GetByIdAsync(id));
        }
    }
}
