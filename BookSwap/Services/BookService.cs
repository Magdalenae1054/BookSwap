
using BookSwap.Models;
using BookSwap.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookSwap.Services
{
    public class BookService : IBookService
    {
        private readonly BookSwapContext _context;

        public BookService(BookSwapContext context)
        {
            _context = context;
        }

        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book? GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(b => b.BookId == id);
        }
    }
}
