
using API.Models.Classes;
using API.Models.Context;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace API.Models
{
    public class AnimeRepository : IAnimesRepository
    {
        private readonly AnimeDbContext _anime;

        public AnimeRepository(AnimeDbContext anime)
        {
            _anime = anime;
        }


        public async Task<Anime> AddAnime(Anime anime)
        {
            var animeToAdd = await _anime.Animes.AddAsync(anime);
            await _anime.SaveChangesAsync();

            return animeToAdd.Entity;
        }


        public async Task DeleteAnime(int id)
        {
            var animeToDelete = await GetById(id);

            if(animeToDelete != null)
            {
                 _anime.Remove(animeToDelete);
                await _anime.SaveChangesAsync();

            }
        }

        public async Task<IEnumerable<Anime>> GetAll() => await _anime.Animes.ToListAsync();


        public async Task<Anime> GetById(int id) => await _anime.Animes.FindAsync(id)!;


        public async Task<Anime> UpdateAnime(Anime anime)
        {
            var animeToUpdate = await _anime.Animes.FirstOrDefaultAsync(a => a.Id == anime.Id);

            if(animeToUpdate != null)
            {
                animeToUpdate.Name = anime.Name;
                animeToUpdate.Streaming = anime.Streaming;
                animeToUpdate.Ratintg = anime.Ratintg;
                animeToUpdate.Review = anime.Review;
                await _anime.SaveChangesAsync();
                
            }
            return animeToUpdate;
        }

        public async Task<Anime> VeryfyName(Anime anime) => await _anime.Animes.FirstOrDefaultAsync(a => a.Name == anime.Name);

    }
}
