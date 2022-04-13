using BlazorEcommerce.Shared.Models;
using BlazorEcommerce.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using BlazorEcommerce.Client.Services.ProductServices;

namespace BlazorEcommerce.Client.Shared.ProductComponents
{
    public partial class ProductList : ComponentBase
    {
        [Inject]
        public IProductSerivice ProductSerivice { get; set; }

        private static List<Product> _products = new List<Product>();

        protected override async Task OnInitializedAsync()
        {
            await ProductSerivice.GetProducts();

        }

    }
}
