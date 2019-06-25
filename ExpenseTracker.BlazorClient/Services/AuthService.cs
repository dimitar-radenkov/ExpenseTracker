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
    public class AuthService : IAuthService
    {
        //this should NOT be haredcoded
        private const string URL = "https://localhost:44382/api/authorization/login";

        private string token;

        private HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;

        public bool IsLoggedIn { get; private set; }

        public string Username { get; private set; }

        public AuthService(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;

            this.localStorageService
                .GetItemAsync<string>(AppConstants.AUTH_KEY)
                .ContinueWith(t => 
                {
                    this.token = t.Result;
                    this.IsLoggedIn = this.token != null;
                });

            this.localStorageService
                .GetItemAsync<string>(AppConstants.LOGGED_USER_KEY)
                .ContinueWith(t => this.Username = t.Result);
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
                    this.IsLoggedIn = true;
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

            this.Username = string.Empty;
            this.token = string.Empty;
            this.IsLoggedIn = false;
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
