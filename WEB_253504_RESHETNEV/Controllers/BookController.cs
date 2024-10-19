using Microsoft.AspNetCore.Mvc;
using WEB_253504_RESHETNEV.Services.GenreServices;
using WEB_253504_RESHETNEV.Services.BookServices;
using WEB_253504_RESHETNEV.Domain.Entities;
using WEB_253504_RESHETNEV.Domain.Models;

namespace WEB_253504_RESHETNEV.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IGenreService _genreService;

        public BookController(IBookService bookService, IGenreService genreService)
        {
            _bookService = bookService;
            _genreService = genreService;
        }

        public async Task<IActionResult> Index(string? genre, int? page)
        {
            int pageNumber = page ?? 1;
            var genres = await _genreService.GetGenreListAsync();
            var bookResponse = await _bookService.GetBookListAsync(genre, pageNumber);


            if (!bookResponse.Successfull)
                return NotFound(bookResponse.ErrorMessage);

            var currentGenre = genre != "all" ? genres.Data?.Find(g => g.NormalizedName!.Equals(genre))?.Name : "Все";
            ViewData["currentGenre"] = currentGenre;
            ViewData["genres"] = genres.Data;
            ViewData["totalPages"] = bookResponse.Data!.TotalPages;

            return View(new ProductListModel<Book> { Items = bookResponse.Data.Items, CurrentPage = pageNumber, TotalPages = bookResponse.Data.TotalPages });
        }
    }
}