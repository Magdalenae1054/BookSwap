using Microsoft.EntityFrameworkCore;

namespace BookSwap.Models
{
    public class BookSwapContext : DbContext
    {
        public BookSwapContext(DbContextOptions<BookSwapContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

    
    }
}
