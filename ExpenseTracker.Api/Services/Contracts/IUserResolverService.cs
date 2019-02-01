using System.Security.Claims;

namespace ExpenseTracker.Api.Services.Contracts
{
    public interface IUserResolverService
    {
        ClaimsPrincipal User { get; }
    }
}
