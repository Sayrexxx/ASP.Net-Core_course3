using WEB_253504_RESHETNEV.Domain.Entities;
using WEB_253504_RESHETNEV.Domain.Models;

namespace WEB_253504_RESHETNEV.Services.GenreServices;

public class MemoryGenreService : IGenreService
{
    public Task<ResponseData<List<Genre>>> GetGenreListAsync()
    {
        var genres = new List<Genre>
        {
            new Genre { Id = 1, Name = "Фантастика", NormalizedName = "sci-fi" },
            new Genre { Id = 2, Name = "Фэнтези", NormalizedName = "fantasy" },
            new Genre { Id = 3, Name = "Детектив", NormalizedName = "detective" },
            new Genre { Id = 4, Name = "Романтика", NormalizedName = "romance" },
            new Genre { Id = 5, Name = "Научная фантастика", NormalizedName = "sci-fi" },
            new Genre { Id = 6, Name = "Исторический роман", NormalizedName = "historical" },
            new Genre { Id = 7, Name = "Ужасы", NormalizedName = "horror" },
            new Genre { Id = 8, Name = "Приключения", NormalizedName = "adventure" },
            new Genre { Id = 9, Name = "Психологический триллер", NormalizedName = "psychological_thriller" },
            new Genre { Id = 10, Name = "Молодежная литература", NormalizedName = "young_adult" }
            
        };
        var result = ResponseData<List<Genre>>.Success(genres);
        return Task.FromResult(result);
    }
}