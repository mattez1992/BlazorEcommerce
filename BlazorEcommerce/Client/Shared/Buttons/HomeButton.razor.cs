using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Client.Shared.Buttons
{
    public partial class HomeButton : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        private void GoToHome()
        {
            NavigationManager.NavigateTo("");
        }
    }
}
