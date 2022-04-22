using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Client.Shared
{
    public partial class ShopNavMenu : ComponentBase, IDisposable
    {
        [Inject]
        public ICategoryService CategoryService { get; set; }
        private bool collapseNavMenu = true;

        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await CategoryService.GetCategoriesAsync();
            CategoryService.OnChange += StateHasChanged;

        }
        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        public void Dispose()
        {
            CategoryService.OnChange -= StateHasChanged;
        }
    }
}
