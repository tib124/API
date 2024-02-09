using API.Models.Classes;

namespace API.Models
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movies>> GetAllMoviesAsync();
        Task<Movies> GetByIdAsync(int id);
        Task<Movies> GetByNameAsync(string name);
        Task DeleteMovieAsync(int id);
        Task<Movies> UpdateMovieAsync(Movies movie);
        Task<Movies> AddMovieAsync(Movies movie);
        Task<Movies> ExistByName(Movies movie);
    }
}
