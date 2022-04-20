using Microsoft.AspNetCore.WebUtilities;

namespace BlazorEcommerce.Client.Pages.Auth
{
    public partial class Login : ComponentBase
    {
        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        private UserLoginDto _userLoginDto = new();
        string _message = string.Empty;
        string _messageCss = string.Empty;

        private string returnUrl = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if ((await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Claims.Any())
            {
                NavigationManager.NavigateTo("");
            }
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("returnUrl", out var url))
            {
                returnUrl = url;
            }
            
           await base.OnInitializedAsync();
        }
        //protected override void OnInitialized()
        //{
        //    var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
        //    if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("returnUrl", out var url))
        //    {
        //        returnUrl = url;
        //    }
        //}
        async Task HandleOnSubmit()
        {
            var result = await AuthService.Login(_userLoginDto);
            _message = result.Message;
            if (result.Success)
            {
                await LocalStorageService.SetItemAsync("authToken", result.Data);
                await AuthenticationStateProvider.GetAuthenticationStateAsync();
                NavigationManager.NavigateTo(returnUrl);
            }
            else
            {
                _messageCss = "text-danger";
            }
        }
    }
}
