using WEB_253504_RESHETNEV.Domain.Entities;

namespace WEB_253504_RESHETNEV.API.Services.GenreServices;

public interface IGenreService
{
    Task<IEnumerable<Genre>> GetGenresAsync();
    Task<Genre> GetGenreByIdAsync(int id);
    Task<Genre> CreateGenreAsync(Genre genre);
    Task<bool> UpdateGenreAsync(Genre genre);
    Task<bool> DeleteGenreAsync(int id);
}
