using WEB_253504_RESHETNEV.Domain.Entities;

namespace WEB_253504_RESHETNEV.API.Services.BookServices

{
    public interface IBookService
    {
        Task<List<Book>> GetBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<List<Book>> GetBooksByGenreAsync(string genreName);
        Task<Book> CreateBookAsync(Book book);
        Task<bool> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int id);
    }
}
