using System.Threading.Tasks;

namespace ExpenseTracker.Api.Services.Contracts
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string email, string password);

        Task RegisterAsync(string email, string password);

        Task<bool> LogoutAsync();
    }
}
