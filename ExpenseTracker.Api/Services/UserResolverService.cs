using System.Security.Claims;
using ExpenseTracker.Api.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Api.Services
{
    public class UserResolverService : IUserResolverService
    {
        private readonly IHttpContextAccessor context;

        public UserResolverService(IHttpContextAccessor context)
        {
            this.context = context;
        }

        public ClaimsPrincipal User => this.context.HttpContext?.User;
    }
}