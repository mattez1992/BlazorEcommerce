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
        #region GET Services
        public async Task<ServiceResponse<List<Product>>> GetFeaturedProducts()
        {
            ServiceResponse<List<Product>> response = new();
            try
            {
                var products = await _context.Products.Where(x => x.Featured == true).ToListAsync();
                if (products.Count > 0)
                {
                    response.Data = products;
                }
                else
                {
                    response.Success = false;
                    response.Message = "No Featured Products Found";
                }
                return response;
            }
            catch (Exception e)
            {

                response.Success = false;
                response.Message = e.Message;
                return response;
            }
           
        }
        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            ServiceResponse<List<Product>> response = new()
            {
                Data = await _context.Products.Include(p => p.Variants).ToListAsync()
            };
            return response;
        }
        public async Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl)
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products
                   .Where(p => p.Category.Url.Equals(categoryUrl))
                   .Include(p => p.Variants)
                   .ToListAsync()
            };

            return response;
        }
        public async Task<ServiceResponse<Product>> GetByIdAsync(int id)
        {
            ServiceResponse<Product> response = new();
            var product = await _context.Products.Include(p => p.Variants).ThenInclude(pv => pv.ProductType).FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
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
        #endregion

        // Search Methods
        public async Task<ServiceResponse<ProductSearchDTO>> SearchProducts(string searchText, int page)
        {
            // pagination shenaigans
            // number of items per page
            var pageResults = 2f;
            // the page we are at
            var pageCount = Math.Ceiling((await FindProductsBySearchText(searchText)).Count / pageResults);
            // grab the products belonging to the right page with skip and take.
            var products = await _context.Products
                                .Where(p => p.Title.ToLower().Contains(searchText.ToLower()) ||
                                    p.Description.ToLower().Contains(searchText.ToLower()) &&
                                    p.Visible && !p.Deleted)
                                .Include(p => p.Variants)
                                .Skip((page - 1) * (int)pageResults)
                                .Take((int)pageResults)
                                .ToListAsync();

            var response = new ServiceResponse<ProductSearchDTO>();
            if (products == null)
            {
                response.Success = false;
                response.Message = "No product found";
            }
            else
            {
                response.Data = new ProductSearchDTO
                {
                    Products = products,
                    CurrentPage = page,
                    Pages = (int)pageCount
                };
            }

            return response;
        }
        public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText)
        {
            var products = await FindProductsBySearchText(searchText);

            List<string> result = new List<string>();

            foreach (var product in products)
            {
                if (product.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(product.Title);
                }

                if (product.Description != null)
                {
                    var punctuation = product.Description.Where(char.IsPunctuation)
                        .Distinct().ToArray();
                    // split the description then trim away the dots
                    var words = product.Description.Split()
                        .Select(s => s.Trim(punctuation));

                    foreach (var word in words)
                    {
                        if (word.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                            && !result.Contains(word))
                        {
                            result.Add(word);
                        }
                    }
                }
            }

            return new ServiceResponse<List<string>> { Data = result };
        }
        private async Task<List<Product>> FindProductsBySearchText(string searchText)
        {
            return await _context.Products
                                .Where(p => p.Title.ToLower().Contains(searchText.ToLower()) ||
                                    p.Description.ToLower().Contains(searchText.ToLower()) &&
                                    p.Visible && !p.Deleted)
                                .Include(p => p.Variants)
                                .ToListAsync();
        }

    }
}
