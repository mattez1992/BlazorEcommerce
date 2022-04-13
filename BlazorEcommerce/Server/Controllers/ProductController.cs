using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return Ok(_context.Products);
        }
        [HttpGet("id")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            return Ok(_context.Products.FirstOrDefault(x => x.Id == id));
        }
    }
}
