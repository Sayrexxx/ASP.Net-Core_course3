using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEB_253504_RESHETNEV.Domain.Entities;

    public class BookContext : DbContext
    {
        public BookContext (DbContextOptions<BookContext> options)
            : base(options)
        {
        }

        public DbSet<WEB_253504_RESHETNEV.Domain.Entities.Book> Books { get; set; } = default!;
    }
