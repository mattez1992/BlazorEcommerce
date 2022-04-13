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

        public List<Product> Products { get; set; } = new List<Product>();

        public ProductSerivice(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task GetProducts()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product");

            if (result != null && result.Data != null && result.Success)
            {
                Products = result.Data;
            }
        }

    }
}
