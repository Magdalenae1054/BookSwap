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
    public class BooksController : Controller
    {
        private static List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "Zločin i kazna" , Author = "Fjodor Mihajlovič Dostojevski", Description = "Zločin i kazna je klasični roman Fjodora Mihajloviča Dostojevskog koji istražuje psihološke dubine ljudske duše kroz priču o Rodionu Raskoljnikovu, studentu koji se suočava s moralnim dilemama nakon što počini zločin",ImageUrl = "/images/Zlocin-i-kazna-II.-izdanje.jpg", Genre = "Drama"},
            new Book { Id = 2, Title = "Vječita Noć", Author = "Agatha Christie", Description = "Beskrajna noć prati ambicije Michaela Rogersa, nemirnog muškarca iz radničke klase koji upoznaje i ženi se bogatom nasljednicom Ellie Guteman. Zajedno grade modernu kuću – Gypsy`s Acre – na zemljištu okruženom lokalnim legendama o prokletstvu. Tajanstvena gatara ih upozorava, a ubrzo neobjašnjive nesreće počinju remetiti idiličan život koji je Michael zamišljao. Pripovijedana u prvom licu iz Michaelove perspektive, priča se razvija u psihološki triler u kojem paranoja, pohlepa i izdaja kulminiraju šokantnim preokretom – otkrivajući da ništa nije onako kako se čini i da san postaje smrtonosna zamka.", ImageUrl = "/images/VjecitaNoc.jpg", Genre = "Psihološki triler" },
            new Book { Id = 3,Title = "Frankenstein",Author = "Mary Shelley", Description = "Kad je Mary Shelley 1816. počela pisati Frankensteina, bila je tek devetnaestogodišnjakinja, kći dvoje znamenitih mislilaca, filozofa Williama Godwina i feministkinje Mary Wollstonecraft. No njezin roman, nastao gotovo slučajno u krugu pjesnika i vizionara okupljenih oko lorda Byrona, pokazao se jednim od najtrajnijih tekstova modernog doba, ubrzo nadmašio okvire gotske književnosti i postao djelo koje definira trenutak kada čovjek, opijen vlastitim otkrićima, počinje sumnjati u granice vlastite moći. Shelley je, spajajući filozofiju, znanost i poeziju, ispisala priču o stvaranju i odgovornosti, o znanju koje se pretvara u teret te samoći uma koji više ne poznaje suosjećanje.", ImageUrl = "/images/Frankenstein.jpg", Genre = "Horor" },

             new Book
    {
        Id = 4,
        Title = "Gospodar prstenova: Prstenova družina",
        Author = "J. R. R. Tolkien",
        Description = "Prvi dio epske trilogije o putovanju Froda Bagginsa koji mora uništiti Jedinstveni Prsten i spriječiti Sauronovu moć da zavlada Međuzemljem.",
        ImageUrl = "/images/lotr.jpg",
        Genre = "Fantastika"
    },

    new Book
    {
        Id = 5,
        Title = "1984",
        Author = "George Orwell",
        Description = "Distopijski klasik koji istražuje totalitarizam, nadzor i manipulaciju društvom kroz priču o Winston Smithu u svijetu pod kontrolom Velikog Brata.",
        ImageUrl = "/images/1984.jpg",
        Genre = "Distopija"
    },

    new Book
    {
        Id = 6,
        Title = "Ubiti pticu rugalicu",
        Author = "Harper Lee",
        Description = "Dirljiva priča o odrastanju, nepravdi i rasizmu u američkom Jugu, ispričana očima mlade djevojčice Scout Finch.",
        ImageUrl = "/images/tokillamockingbird.jpg",
        Genre = "Drama"
    }


    };
        public IActionResult Index()
        {
            // Dohvati username iz sessiona
            var username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username; // šaljemo u view

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
