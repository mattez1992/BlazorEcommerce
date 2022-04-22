
namespace BlazorEcommerce.Server.Services.AuthServices
{
    public interface IAuthService
    {
        Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword);
        Task<User> GetUserByEmail(string email);
        string GetUserEmail();
        int GetUserId();
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<ServiceResponse<int>> Register(UserRegristrationDTO user);
        Task<bool> UserExists(string email);
    }
}