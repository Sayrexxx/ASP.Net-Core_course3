using Microsoft.AspNetCore.Mvc;
using WEB_253504_RESHETNEV.API.Services.GenreServices;
using WEB_253504_RESHETNEV.Domain.Entities;
using WEB_253504_RESHETNEV.Domain.Models;

namespace WEB_253504_RESHETNEV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<List<Genre>>>> GetGenres()
        {
            var genres = await _genreService.GetGenresAsync();
            if (genres == null)
                return NotFound(ResponseData<List<Genre>>.Error("Genres not found"));

            return Ok(ResponseData<List<Genre>>.Success((List<Genre>)genres));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseData<Genre>>> GetGenre(int id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            if (genre == null)
                return NotFound(ResponseData<Genre>.Error("Genre not found"));

            return Ok(ResponseData<Genre>.Success(genre));
        }

        [HttpPost]
        public async Task<ActionResult<ResponseData<Genre>>> PostGenre(Genre genre)
        {
            var createdGenre = await _genreService.CreateGenreAsync(genre);
            return CreatedAtAction(nameof(GetGenre), new { id = createdGenre.Id }, ResponseData<Genre>.Success(createdGenre));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenre(int id, Genre genre)
        {
            if (id != genre.Id)
                return BadRequest(ResponseData<Genre>.Error("ID mismatch"));

            var result = await _genreService.UpdateGenreAsync(genre);
            if (!result)
                return NotFound(ResponseData<Genre>.Error("Genre not found"));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var result = await _genreService.DeleteGenreAsync(id);
            if (!result)
                return NotFound(ResponseData<Genre>.Error("Genre not found"));

            return NoContent();
        }
    }
}
