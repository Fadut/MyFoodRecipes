using RecipesAPI.Models;

namespace RecipesWebApp.Services
{
    public class RecipeService
    {
        private readonly HttpClient _httpClient;
        private const string _baseUri = "api/recipes";

        public RecipeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Recipe>> GetRecipesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Recipe>>(_baseUri);
        }

        public async Task<Recipe?> GetRecipeByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Recipe>($"api/recipes/{id}");
        }

        public async Task CreateRecipeAsync(Recipe recipe)
        {
            await _httpClient.PostAsJsonAsync("api/recipes", recipe);
        }

        public async Task UpdateRecipeAsync(int id, Recipe recipe)
        {
            await _httpClient.PutAsJsonAsync($"api/recipes/{id}", recipe);
        }

        public async Task DeleteRecipeAsync(int id)
        {
            //var deleteUri = $"{_baseUri}/{id}"; Consider adding.
            await _httpClient.DeleteAsync($"api/recipes/{id}");
        }
    }
}
