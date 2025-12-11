using Microsoft.AspNetCore.Mvc;
using BookSwap.Models;
using Microsoft.EntityFrameworkCore;

public class BooksController : Controller
{
    private readonly BookSwapContext _context;

    public BooksController(BookSwapContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var books = _context.Books.ToList();
        return View(books);
    }

    public IActionResult Details(int id)
    {
        var book = _context.Books.FirstOrDefault(b => b.BookId == id);
        if (book == null) return NotFound();

        return View(book);
    }
}
