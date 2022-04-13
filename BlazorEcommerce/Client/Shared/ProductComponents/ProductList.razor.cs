using BlazorEcommerce.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorEcommerce.Client.Shared.ProductComponents
{
    public partial class ProductList : ComponentBase
    {
        [Inject]
        public HttpClient _client { get; set; }

        private static List<Product> _products = new List<Product>();

        protected override async Task OnInitializedAsync()
        {
            var result = await _client.GetFromJsonAsync<List<Product>>("api/Product");
            if(result !=null)
                   _products = result;
        }

    }
}
