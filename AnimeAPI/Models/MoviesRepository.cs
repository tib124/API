using API.Models.Classes;
using API.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class MoviesRepository : IMovieRepository
    {
        private readonly AnimeDbContext _context;

        public MoviesRepository(AnimeDbContext context)
        {
            _context = context;
        }

        public async Task<Movies> AddMovieAsync(Movies movie)
        {
            var movieToAdd = await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            return movieToAdd.Entity;
        }

        public async Task DeleteMovieAsync(int id)
        {
            var MovieToDelete = await GetByIdAsync(id);

            if(MovieToDelete != null)
            {
                _context.Movies.Remove(MovieToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Movies> ExistByName(Movies movie) => await _context.Movies.FirstOrDefaultAsync(m => m.Tittle == movie.Tittle);


        public async Task<IEnumerable<Movies>> GetAllMoviesAsync()
        {
            var moviesWithActors = await _context.Movies
                .Include(m => m.Actors)
                .ToListAsync();
            
            return moviesWithActors;
        }


        public async Task<Movies> GetByIdAsync(int id)
        {
            var movies = await _context.Movies
                .Include(m => m.Actors)
                .FirstOrDefaultAsync(m => m.IdMovie == id);

            return movies!;
        }


        public async Task<Movies> GetByNameAsync(string name) 
        { 
            var movies = await _context.Movies
                .Include(m => m.Actors)
                .FirstOrDefaultAsync(m => m.Tittle == name);

            return movies!;
        
        }


        public async Task<Movies> UpdateMovieAsync(Movies movies)
        {
            var animeToUpdate = await _context.Movies.FirstOrDefaultAsync(a => a.IdMovie == movies.IdMovie);

            if (animeToUpdate != null)
            {
                animeToUpdate.Tittle = movies.Tittle;
                animeToUpdate.Review = movies.Review;
                animeToUpdate.Rating = movies.Rating;
                animeToUpdate.ReleaseAt = movies.ReleaseAt;
                await _context.SaveChangesAsync();

            }
            return animeToUpdate;
        }
    }
}
