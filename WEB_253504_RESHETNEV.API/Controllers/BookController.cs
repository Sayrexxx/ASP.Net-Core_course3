using Microsoft.AspNetCore.Mvc;
using WEB_253504_RESHETNEV.API.Services.BookServices;
using WEB_253504_RESHETNEV.Domain.Entities;
using WEB_253504_RESHETNEV.Domain.Models;

namespace WEB_253504_RESHETNEV.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // Получить все книги
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks([FromQuery] string? genreName)
        {
            if (string.IsNullOrEmpty(genreName) || genreName.ToLower() == "all")
            {
                // Возвращаем все книги, если параметр genre не указан или равен "all"
                var allBooks = await _bookService.GetBooksAsync();
                return Ok(allBooks);
            }

            // Проверяем, есть ли книги указанного жанра
            var booksByGenre = await _bookService.GetBooksByGenreAsync(genreName);
            if (booksByGenre == null || !booksByGenre.Any())
            {
                // Если книги с указанным жанром не найдены, возвращаем 404
                return NotFound($"Книги жанра '{genreName}' не найдены.");
            }

            // Возвращаем книги по указанному genreName
            return Ok(booksByGenre);
        }


        // Получить книгу по id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseData<Book>>> PostBook(Book book)
        {
            var createdBook = await _bookService.CreateBookAsync(book);
            return CreatedAtAction(nameof(GetBook), new { id = createdBook.Id }, ResponseData<Book>.Success(createdBook));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
                return BadRequest(ResponseData<Book>.Error("ID mismatch"));

            var result = await _bookService.UpdateBookAsync(book);
            if (!result)
                return NotFound(ResponseData<Book>.Error("Book not found"));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result)
                return NotFound(ResponseData<Book>.Error("Book not found"));

            return NoContent();
        }
    }
}
