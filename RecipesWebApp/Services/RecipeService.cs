using RecipesAPI.Models;
using static System.Net.WebRequestMethods;

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
            await _httpClient.DeleteAsync($"api/recipes/{id}");
        }

        public async Task<List<Recipe>> SearchRecipesAsync(string? title, string? ingredient, int? preparationTime)
        {
            var url = "api/recipes/search?";
            if (!string.IsNullOrWhiteSpace(title)) url += $"title={title}&";
            if (!string.IsNullOrWhiteSpace(ingredient)) url += $"ingredient={ingredient}&";
            if (preparationTime.HasValue) url += $"preparationTime={preparationTime}&";

            return await _httpClient.GetFromJsonAsync<List<Recipe>>(url);
        }


    }
}
