namespace BlazorEcommerce.Client.Shared.Buttons
{
    public partial class UserButton : ComponentBase
    {
        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }
        private bool _showUserMenu = false;
        private string UserMenuCssClass => _showUserMenu ? "d-block" : null;
        private async Task Logout()
        {
            await LocalStorageService.RemoveItemAsync("authToken");
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
            Navigation.NavigateTo("");
        }
        private void HandleOnClick()
        {
            _showUserMenu = !_showUserMenu;
        }

        private async Task HandleOnFocusOut()
        {
            await Task.Delay(200);
            _showUserMenu = false;
        }

  
    }
}
