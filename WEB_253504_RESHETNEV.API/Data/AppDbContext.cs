using Microsoft.EntityFrameworkCore;
using WEB_253504_RESHETNEV.Domain.Entities;

namespace WEB_253504_RESHETNEV.API.Data;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) {}
    
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Book> Books { get; set; }
}