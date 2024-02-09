using API.Models.Classes;
using Microsoft.EntityFrameworkCore;

namespace API.Models.Context
{
    public class AnimeDbContext : DbContext
    {
        public AnimeDbContext(DbContextOptions<AnimeDbContext> options) : base(options)
        {

        }

        public DbSet<Anime> Animes { get; set; }    
        public DbSet<Movies> Movies { get; set; }
        public DbSet<Actors> Actors { get; set; }
    }
}
