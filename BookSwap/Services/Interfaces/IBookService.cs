using BookSwap.Models;

namespace BookSwap.Services.Interfaces
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
        Book? GetBookById(int id);
    }
}
