using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using ExpenseTracker.Common.Models.BindingModels;
using ExpenseTracker.Common.Models.Responses;
using Microsoft.AspNetCore.Components;

namespace ExpenseTracker.BlazorClient.Services
{
    public interface IAuthService
    {
        Task LoginAsync(string email, string pass);

        Task LogoutAsync();
    }

    public class AuthService : IAuthService
    {
        //this should NOT be haredcoded
        private const string URL = "https://localhost:44382/api/authorization/login";

        private HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;

        public AuthService(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public async Task LoginAsync(string email, string pass)
        {
            try
            {
                using (this.httpClient = new HttpClient())
                {
                    var content = new LoginBindingModel { Email = email, Password = pass };
                    var response = await this.httpClient.PostJsonAsync<LoginResponse>(URL, content);  

                    await this.SaveToStorage(response.User, response.User);
                    await this.SetAuthorizationHeader();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error while loggin: {e.Message}");
            }
        }

        public async Task LogoutAsync()
        {
            await this.localStorageService.RemoveItemAsync(AppConstants.AUTH_KEY);
            await this.localStorageService.RemoveItemAsync(AppConstants.LOGGED_USER_KEY);
        }

        private async Task SaveToStorage(string token, string user)
        {
            if (string.IsNullOrEmpty(token) ||
                string.IsNullOrEmpty(user))
            {
                throw new ArgumentException("must provide token and user");
            }

            await this.localStorageService.SetItemAsync(AppConstants.AUTH_KEY, token);
            await this.localStorageService.SetItemAsync(AppConstants.LOGGED_USER_KEY, user);
        }

        //this should be in other service
        private async Task SetAuthorizationHeader()
        {
            if (!this.httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                var token = await this.localStorageService.GetItemAsync<string>(AppConstants.AUTH_KEY);
                this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
