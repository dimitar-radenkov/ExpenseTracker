using ExpenseTracker.BlazorClient.Services;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Blazored.LocalStorage;

namespace ExpenseTracker.BlazorClient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {           
            services.AddSingleton<IAuthService, AuthService>();
            services.AddBlazoredLocalStorage();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
