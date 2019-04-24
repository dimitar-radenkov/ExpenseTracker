using ExpenseTracker.Common.Models.BindingModels;
using ExpenseTracker.Common.Models.Responses;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExpenseTracker.BlazorClient.Services
{
    public interface IAuthService
    {
        Task<string> Login(string email, string pass);
    }

    public class AuthService : IAuthService
    {
        private const string url = "https://localhost:44380/api/authorization/login";

        private HttpClient client;

        public async Task<string> Login(string email, string pass)
        {
            using (this.client = new HttpClient())
            {
                var content = new LoginBindingModel { Email = email, Password = pass };
                var response = await this.client.PostJsonAsync<LoginResponse>(url, content);

                return response.Token;
            }
        }
    }
}
