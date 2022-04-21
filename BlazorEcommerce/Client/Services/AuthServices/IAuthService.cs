
namespace BlazorEcommerce.Client.Services.AuthServices
{
    public interface IAuthService
    {
        Task<ServiceResponse<bool>> ChangePassword(UpdateUserPasswordDto request);
        Task<bool> IsUserAuthenticated();
        Task<ServiceResponse<string>> Login(UserLoginDto request);
        Task<ServiceResponse<int>> Register(UserRegristrationDTO request);
    }
}