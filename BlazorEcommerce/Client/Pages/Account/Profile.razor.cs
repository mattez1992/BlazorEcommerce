namespace BlazorEcommerce.Client.Pages.Account
{
    public partial class Profile:ComponentBase
    {
        [Inject]
        public IAuthService AuthService { get; set; }
        private UpdateUserPasswordDto _updateUserPasswordDto = new();
        private string _message = string.Empty;

        private async Task ChangePassword()
        {
            var result = await AuthService.ChangePassword(_updateUserPasswordDto);
            _message = result.Message;
            if (result.Success)
            {
                _updateUserPasswordDto = new();
            }
        }
    }
}
