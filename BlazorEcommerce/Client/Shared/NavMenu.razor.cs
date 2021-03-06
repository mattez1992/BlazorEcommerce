using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Client.Shared
{
    public partial class NavMenu : ComponentBase
    {
        [Inject]
        public ICategoryService CategoryService { get; set; }
        private bool collapseNavMenu = true;

        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;
        protected override async Task OnInitializedAsync()
        {
            await CategoryService.GetCategoriesAsync();
            await base.OnInitializedAsync();
        }
        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }
    }
}
