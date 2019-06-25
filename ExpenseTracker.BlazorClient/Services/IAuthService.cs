using System.Threading.Tasks;

namespace ExpenseTracker.BlazorClient.Services
{
    public interface IAuthService
    {
        Task LoginAsync(string email, string pass);

        Task LogoutAsync();

        bool IsLoggedIn { get; }

        string Username { get; }
    }
}