using API.Models.Classes;

namespace API.Models
{
    public interface IAnimesRepository
    {

        Task<Anime> GetById(int id);
        Task<IEnumerable<Anime>> GetAll();  
        Task<Anime> UpdateAnime(Anime anime);
        Task DeleteAnime(int id);
        Task<Anime> AddAnime(Anime anime);
        Task<Anime> VeryfyName(Anime anime);


    }
}
