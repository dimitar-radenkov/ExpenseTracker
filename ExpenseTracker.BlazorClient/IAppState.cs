using System;

using Blazored.LocalStorage;

namespace ExpenseTracker.BlazorClient
{
    public interface IAppState
    {
        bool IsLoggedIn { get; }

        string User { get; }
    }

    public class AppState : IAppState
    {
        private readonly ILocalStorageService localStorageService;

        public bool IsLoggedIn => localStorageService.GetItemAsync<string>("authToken").Result != null;

        public string User
        {
            get
            {
                var loggedUser = this.localStorageService.GetItemAsync<string>("loggedUser").Result;
                if (string.IsNullOrWhiteSpace(loggedUser))
                {
                    throw new ArgumentException("There is no logged user");
                }

                return loggedUser;
            }
        }

        public AppState(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }
    }
}
