using Microsoft.EntityFrameworkCore;

namespace BookSwap.Models
{
    public class BookSwapContext : DbContext
    {
     

        public BookSwapContext(DbContextOptions<BookSwapContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Listing> Listings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FullName = "Admin User",
                    Email = "admin@books.com",
                    PasswordHash = "admin123", 
                    Role = "Admin",
                    CreatedAt = DateTime.Now
                },
                new User
                {
                    UserId = 2,
                    FullName = "Test User",
                    Email = "test@books.com",
                    PasswordHash = "test123",
                    Role = "User",
                    CreatedAt = DateTime.Now
                }
            );

            // Seed Books
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookId = 1,
                    Title = "Harry Potter",
                    Author = "J.K. Rowling",
                    Subject = "Fantasy",
                    Description = "Čarobnjački roman.",
                    ImageUrl = "/images/hp.jpg"
                },
                new Book
                {
                    BookId = 2,
                    Title = "The Hobbit",
                    Author = "J.R.R. Tolkien",
                    Subject = "Fantasy",
                    Description = "Avantura hobita.",
                    ImageUrl = "/images/hobbit.jpg"
                }
            );

            // Seed Listings
            modelBuilder.Entity<Listing>().HasData(
                new Listing
                {
                    ListingId = 1,
                    UserId = 2,
                    BookId = 1,
                    Type = "Borrow",
                    Price = null,
                    Status = "Available",
                    CreatedAt = DateTime.Now
                }
            );
        }
    }
}
