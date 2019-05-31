using System.Threading.Tasks;

namespace ExpenseTracker.BlazorClient.Services
{
    public interface IAuthService
    {
        Task LoginAsync(string email, string pass);

        Task LogoutAsync();

        Task<LoginInfo> GetLoginInfo();
    }
}