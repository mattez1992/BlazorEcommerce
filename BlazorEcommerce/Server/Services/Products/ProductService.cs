using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Server.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<Product>>> GetProductsAsync(){
            ServiceResponse<List<Product>> response = new()
            {
                Data = await _context.Products.ToListAsync()
            };
            return response;
        }
        public async Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl)
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                   .Where(p => p.Category.Url.Equals(categoryUrl))
                   .ToListAsync()
            };

            return response;
        }
        public async Task<ServiceResponse<Product>> GetByIdAsync(int id)
        {
            ServiceResponse<Product> response = new();
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if(product == null)
            {
                response.Success = false;
                response.Message = "No product found";
            }
            else
            {
                response.Data = product;
            }
            
            return response;
        }

        
    }
}
