using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Client.Shared.ProductComponents
{
    public partial class FeaturedProducts : ComponentBase, IDisposable
    {
        [Inject]
        public IProductSerivice PorductService { get; set; }

        public void Dispose()
        {
            PorductService.ProductsChanged -= StateHasChanged;
        }

        protected override void OnInitialized()
        {
            PorductService.ProductsChanged += StateHasChanged;
            base.OnInitialized();
        }
    }
}
