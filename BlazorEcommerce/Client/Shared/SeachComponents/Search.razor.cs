using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Client.Shared.SeachComponents
{
    public partial class Search : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IProductSerivice ProductSerivice { get; set; }

        private string searchText = string.Empty;
        private List<string> suggestions = new List<string>();
        protected ElementReference searchInput;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await searchInput.FocusAsync();
            }
        }

        public void SearchProducts()
        {

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                NavigationManager.NavigateTo($"search/{searchText}/1");
            }
            else
            {
                NavigationManager.NavigateTo($"/");
            }
           
        }

        public async Task HandleSearch(KeyboardEventArgs args)
        {
            if (args.Key == null || args.Key.Equals("Enter"))
            {
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    Console.WriteLine(searchText);
                    SearchProducts();
                }
                
            }
            else if (searchText.Length > 1)
            {
                suggestions = await ProductSerivice.GetProductSearchSuggestions(searchText);
            }
        }
    }
}
