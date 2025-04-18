namespace RecipesAPI.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<string> Ingredients { get; set; } = new();

        public string Instructions {  get; set; } = string.Empty;

        public int PreparationTime { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string CuisineType { get; set; } = string.Empty;

        public string OwnerId {  get; set; }
    }
}
