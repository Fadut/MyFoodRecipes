using System.ComponentModel.DataAnnotations;

namespace RecipesAPI.Models
{
    public class RecipeDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public List<string> Ingredients { get; set; } = new();

        [Required]
        public string Instructions {  get; set; } = string.Empty;

        [Range(1, 1440)]
        public int PreparationTime { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string CuisineType { get; set; } = string.Empty;

    }
}
