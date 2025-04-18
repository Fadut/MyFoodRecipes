using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipesAPI.Migrations;
using RecipesAPI.Models;
using RecipesAPI.Services;
using System.Security.Claims;

namespace RecipesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipes()
        {
            var recipes = await _context.Recipes.OrderByDescending(e => e.Id).ToListAsync();
            return Ok(recipes);
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetRecipesByCategory(string category)
        {
            var recipes = await _context.Recipes
                .Where(e => e.Category.ToLower() == category.ToLower())
                .OrderByDescending(r => r.Id)
                .ToListAsync();

            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipe(int id)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(e => e.Id == id);
            if (recipe is null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }

        [HttpGet("myrecipes")]
        [Authorize]
        public async Task<IActionResult> GetMyRecipes()
        {
            var ownerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (ownerId is null)
            {
                return Unauthorized();
            }

            var recipes = await _context.Recipes
                .Where(r => r.OwnerId == ownerId)
                .OrderByDescending(r => r.Id)
                .ToListAsync();

            return Ok(recipes);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRecipe([FromBody] RecipeDto recipeDto)
        {
            var ownerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (ownerId is null)
            {
                return Unauthorized();
            }

            var recipe = new Recipe
            {
                Title = recipeDto.Title,
                Description = recipeDto.Description,
                Ingredients = recipeDto.Ingredients,
                Instructions = recipeDto.Instructions,
                PreparationTime = recipeDto.PreparationTime,
                ImageUrl = recipeDto.ImageUrl,
                Category = recipeDto.Category,
                CuisineType = recipeDto.CuisineType,
                OwnerId = ownerId
            };

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipe);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipe(int id, [FromBody] RecipeDto recipeDto)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe is null)
            {
                return NotFound();
            }

            recipe.Title = recipeDto.Title;
            recipe.Description = recipeDto.Description;
            recipe.Ingredients = recipeDto.Ingredients;
            recipe.Instructions = recipeDto.Instructions;
            recipe.PreparationTime = recipeDto.PreparationTime;
            recipe.ImageUrl = recipeDto.ImageUrl;
            recipe.Category = recipeDto.Category;
            recipe.CuisineType = recipeDto.CuisineType;



            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe is null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRecipes(
            [FromQuery] string? title,
            [FromQuery] string? ingredient,
            [FromQuery] int? preparationTime,
            [FromQuery] string? category,
            [FromQuery] string? cuisineType)
        {
            var query = _context.Recipes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(r => r.Title.Contains(title));

            if (!string.IsNullOrWhiteSpace(ingredient))
                query = query.Where(r => r.Ingredients.Any(i => i.Contains(ingredient)));

            if (preparationTime.HasValue)
                query = query.Where(r => r.PreparationTime <= preparationTime.Value);

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(r => r.Category == category);

            if (!string.IsNullOrWhiteSpace(cuisineType))
                query = query.Where(r => r.CuisineType == cuisineType);

            var results = await query.ToListAsync();
            return Ok(results);
        }



    }
}
