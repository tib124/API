using AnimeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimeAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AnimeController : ControllerBase
    {

        private readonly IAnimesRepository _animesresp;

        public AnimeController(IAnimesRepository animesresp)
        {
            _animesresp = animesresp;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Anime>>> GetAll()
        {
            try
            {
                return Ok(await _animesresp.GetAll());

            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Anime>> GetById(int id)
        {
            var anime = await _animesresp.GetById(id);

            if (anime != null)
            {
                return Ok(anime);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<Anime>> AddAnimeAsync(Anime anime)
        {
            try
            {
                if (anime == null)
                {
                    return BadRequest();
                }
                var sameAnime = await _animesresp.VeryfyName(anime);

                if(sameAnime != null)
                {
                    return BadRequest("Anime Alredy Exist");
                }

                var createAnime = await _animesresp.AddAnime(anime);
                return CreatedAtAction(nameof(GetAll), new { id = createAnime.Id }, createAnime);

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Anime>> UpdateAnime(int id, Anime anime)
        {
            try
            {
                if (id != anime.Id)
                {
                    return BadRequest("Id Doest Match");
                }

                var AnimeToUpdate = await _animesresp.GetById(id);

                if (AnimeToUpdate == null)
                {
                    return NotFound("Id Not Found");
                }

                //var sameAnime = await _animesresp.VeryfyName(anime);

                //if (sameAnime != null)
                //{
                //    return BadRequest("Anime's name Alredy Exist");
                //}

                return await _animesresp.UpdateAnime(anime);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteAnime(int id)
        {
            var StudentToDelete = await _animesresp.GetById(id);

            if(StudentToDelete == null)
            {
                return NotFound();
            }

            await _animesresp.DeleteAnime(id);

            return Ok($"Anime With Id : {id} Deleted");
        }
    }
}
