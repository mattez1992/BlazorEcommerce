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
        public async Task<ServiceResponse<Product>> GetByIdAsync(int id)
        {
            ServiceResponse<Product> response = new()
            {
                Data = await _context.Products.FirstOrDefaultAsync(x => x.Id == id)
            };
            return response;
        }
    }
}
