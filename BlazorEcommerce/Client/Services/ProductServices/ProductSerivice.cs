using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Client.Services.ProductServices
{
    public class ProductSerivice : IProductSerivice
    {
        private readonly HttpClient _httpClient;
        public string Message { get; set; } = "Loading products...";
        public List<Product> Products { get; set; } = new List<Product>();
        public string LastSearchText { get; set; } = string.Empty;
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;
        public List<Product> AdminProducts { get; set; }

        public event Action ProductsChanged;
        public ProductSerivice(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task GetProducts(string? categoryUrl = null)
        {
            var result = categoryUrl == null ?
               await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product/featured") :
               await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{categoryUrl}");

            if (result != null && result.Data != null && result.Success)
            {
                Products = result.Data;             
            }
            CurrentPage = 1;
            PageCount = 0;
            if (Products.Count == 0 || result.Success == false)
            {
                Message = result.Message;
                Console.WriteLine("No Products found");
            }

            ProductsChanged.Invoke();
        }

        public async Task<ServiceResponse<Product>> GetProductById(int id)
        {
            ServiceResponse<Product> response = new()
            {
                Success = false,
                Message = "Somethiing went wrong"
            };
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{id}");
            return result;
        }
        // search methods
        public async Task<List<string>> GetProductSearchSuggestions(string searchText)
        {
            var result = await _httpClient
                .GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/searchsuggestions/{searchText}");
            return result.Data;
        }

        public async Task SearchProducts(string searchText, int page)
        {
            LastSearchText = searchText;
            var result = await _httpClient
                 .GetFromJsonAsync<ServiceResponse<ProductSearchDTO>>($"api/product/search/{searchText}/{page}");
            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }
            if (Products.Count == 0) Message = "No products found.";
            ProductsChanged?.Invoke();
        }

        #region Admin methods
        public async Task<Product> CreateProduct(Product product)
        {
            var result = await _httpClient.PostAsJsonAsync("api/product", product);
            var newProduct = (await result.Content
                .ReadFromJsonAsync<ServiceResponse<Product>>()).Data;
            return newProduct;
        }

        public async Task DeleteProduct(Product product)
        {
            var result = await _httpClient.DeleteAsync($"api/product/{product.Id}");
        }

        public async Task GetAdminProducts()
        {
            var result = await _httpClient
                .GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product/admin");
            AdminProducts = result.Data;
            CurrentPage = 1;
            PageCount = 0;
            if (AdminProducts.Count == 0)
                Message = "No products found.";
        }
        public async Task<Product> UpdateProduct(Product product)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/product", product);
            var content = await result.Content.ReadFromJsonAsync<ServiceResponse<Product>>();
            return content.Data;
        }
        #endregion
    }
}
