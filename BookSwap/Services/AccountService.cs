using BookSwap.Models;
using BookSwap.Models.ViewModels;
using BookSwap.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;

namespace BookSwap.Services
{
    public class AccountService : IAccountService
    {
        private readonly BookSwapContext _context;

        public AccountService(BookSwapContext context)
        {
            _context = context;
        }

        public AuthResult Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return AuthResult.Fail("Email i lozinka su obavezni.");

            var user = _context.Users.FirstOrDefault(x => x.Email == email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return AuthResult.Fail("Pogrešan email ili lozinka.");

            return AuthResult.Ok();
        }

        public AuthResult Register(RegisterViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) ||
             string.IsNullOrWhiteSpace(model.Password) ||
             string.IsNullOrWhiteSpace(model.FullName))
                return AuthResult.Fail("Sva polja su obavezna.");

            if (_context.Users.Any(x => x.Email == model.Email))
                return AuthResult.Fail("Ovaj email već postoji.");

            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Role = "User",
            };

            _context.Users.Add(user);
            _context.SaveChanges();


            return AuthResult.Ok();
        }
        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public  void Logout()
        { }

        public User? GetUserById(int id)
        {

            return _context.Users.FirstOrDefault(u => u.UserId == id);

        }

        public List<User> GetAllUsers()
        {

            return _context.Users.ToList();
        }
    }
}
