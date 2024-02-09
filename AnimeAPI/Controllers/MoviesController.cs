using API.Models;
using API.Models.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movies;

        public MoviesController(IMovieRepository movies)
        {
            _movies = movies;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movies>>> GetAll()
        {
            try
            {
                return Ok(await _movies.GetAllMoviesAsync());

            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao buscar os filmes: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movies>> GetById(int id)
        {
            try
            {
                var movieToGet = await _movies.GetByIdAsync(id);

                if (movieToGet != null)
                {
                    return Ok(movieToGet);
                }
                return BadRequest();
            } catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao buscar os filmes: {ex.Message}");
            }
        }
        [HttpGet("{name}")]
        public async Task<ActionResult<Movies>> GetByName(string name)
        {
            try
            {
                var movieToGet = await _movies.GetByNameAsync(name);

                if(movieToGet != null)
                {
                    return Ok(movieToGet);
                }
                return BadRequest();


            }catch(Exception ex)
            {
                return BadRequest($"Ocorreu um erro ao buscar os filmes: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Movies>> AddMovie(Movies movie)
        {
            try
            {
                if (movie == null)
                {
                    return BadRequest();
                }

                var movieExist = await _movies.GetByNameAsync(movie.Tittle);

                if (movieExist != null)
                {
                    return BadRequest($"Ja Existe");
                }

                var createMovie = await _movies.AddMovieAsync(movie);
                return CreatedAtAction(nameof(GetAll), new { id = createMovie.IdMovie }, createMovie);


            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro : {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Movies>> UpdateAnime(int id, Movies movies)
        {
            try
            {
                if (id != movies.IdMovie)
                {
                    return BadRequest("Id Doest Match");
                }

                var AnimeToUpdate = await _movies.GetByIdAsync(id);

                if (AnimeToUpdate == null)
                {
                    return NotFound("Id Not Found");
                }

                return await _movies.UpdateMovieAsync(movies);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            try
            {
                var movieToDelete = await _movies.GetByIdAsync(id);

                if(movieToDelete == null)
                {
                    return NotFound();
                }

               await _movies.DeleteMovieAsync(id);

                return Ok($"Movie With {id} Deleted");




            }catch(Exception ex)
            {
                return BadRequest($"Ocorreu um erro : {ex.Message}");
            }
        }

        

        }
    }

