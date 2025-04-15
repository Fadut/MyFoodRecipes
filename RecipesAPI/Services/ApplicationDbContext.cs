using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecipesAPI.Models;

namespace RecipesAPI.Services
{
    //public class ApplicationDbContext : DbContext
    //{
    //    public ApplicationDbContext(DbContextOptions options) : base(options)
    //    {
    //    }

    //    public required DbSet<Recipe> Recipes { get; set; }
    //}

    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public required DbSet<Recipe> Recipes { get; set; }
    }
}
