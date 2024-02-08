﻿using Microsoft.EntityFrameworkCore;

namespace AnimeAPI.Models.Context
{
    public class AnimeDbContext : DbContext
    {
        public AnimeDbContext(DbContextOptions<AnimeDbContext> options) : base(options)
        {

        }

        public DbSet<Anime> Animes { get; set; }    
    }
}
