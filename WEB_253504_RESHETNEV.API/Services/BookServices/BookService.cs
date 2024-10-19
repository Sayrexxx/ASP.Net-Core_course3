using Microsoft.EntityFrameworkCore;
using WEB_253504_RESHETNEV.API.Data;
using WEB_253504_RESHETNEV.Domain.Entities;
using WEB_253504_RESHETNEV.Domain.Models;

namespace WEB_253504_RESHETNEV.API.Services.BookServices
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            return await _context.Books.Include(b => b.Genre).ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books.Include(b => b.Genre).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Book>> GetBooksByGenreAsync(string genreName)
        {
            return await _context.Books.Where(b => b.Genre!.NormalizedName == genreName).Include(b => b.Genre).ToListAsync();
        }
        public async Task<Book> CreateBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Books.Any(b => b.Id == book.Id))
                    return false;

                throw;
            }
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
