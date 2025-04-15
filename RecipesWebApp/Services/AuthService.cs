using static System.Net.WebRequestMethods;

namespace RecipesWebApp.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private const string _baseUri = "api/account";

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> LogInAsync(string username, string password)
        {
            var loginData = new
            {
                Username = username,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7280/api/account/login", loginData);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RegisterAsync(string username, string password)
        {
            var registerData = new
            {
                Username = username,
                Password = password,
            };

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7280/api/account/register", registerData);

            return response.IsSuccessStatusCode;
        }
    }

}
