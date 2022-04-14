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
        public event Action ProductsChanged;
        public ProductSerivice(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task GetProducts(string? categoryUrl = null)
        {
            var result = categoryUrl == null ?
               await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product") :
               await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{categoryUrl}");

            if (result != null && result.Data != null && result.Success)
            {
                Products = result.Data;             
            }
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
    }
}
