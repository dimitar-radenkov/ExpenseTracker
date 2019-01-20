using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Api.Services.Contracts
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string email, string password);

        Task<IdentityUser> RegisterAsync(string email, string password);

        Task<bool> LogoutAsync();
    }
}
