namespace BlazorEcommerce.Client.Pages.Auth
{
    public partial class Register : ComponentBase
    {
        [Inject]
        public IAuthService AuthService { get; set; }
        UserRegristrationDTO _userRegristrationDTO = new();

        string _message = string.Empty;
        string _messageCss = string.Empty;
        async Task HandleOnSubmit()
        {
            var result = await AuthService.Register(_userRegristrationDTO);
            _message = result.Message;  
            if (result.Success)
            {
                _messageCss = "text-success";
            }
            else
            {
                _messageCss = "text-danger";
            }
        }
    }
}
