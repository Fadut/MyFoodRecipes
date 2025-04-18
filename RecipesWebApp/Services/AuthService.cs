using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using static System.Net.WebRequestMethods;

namespace RecipesWebApp.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly AuthenticationStateProvider _authProvider;
        private const string _baseUri = "api/account";

        public AuthService(HttpClient httpClient, NavigationManager navigationManager, AuthenticationStateProvider authProvider)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _authProvider = authProvider;
        }

        public async Task<bool> LogInAsync(string username, string password)
        {
            var loginData = new
            {
                Username = username,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync("/api/account/login", loginData);

            if (response.IsSuccessStatusCode)
            {
                ((CustomAuthStateProvider)_authProvider).MarkUserAsAuthenticated(loginData.Username);
                _navigationManager.NavigateTo("/myrecipes");
                return true;
            }

            return false;
        }

        public async Task<bool> RegisterAsync(string username, string password)
        {
            var registerData = new
            {
                Username = username,
                Password = password,
            };

            var response = await _httpClient.PostAsJsonAsync("/api/account/register", registerData);

            return response.IsSuccessStatusCode;
        }

        public async Task LogOutAsync()
        {
            await _httpClient.GetAsync("/api/account/logout");
        }

        //await ((CustomAuthStateProvider)_authProvider).MarkUserAsLoggedOut();
        //_navigationManager.NavigateTo("/");
    }

}
