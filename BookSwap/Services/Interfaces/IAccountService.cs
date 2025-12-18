using BookSwap.Models;
using BookSwap.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using System.Globalization;

namespace BookSwap.Services.Interfaces
{
    public interface IAccountService
    {
        AuthResult Login(string email, string password);
        AuthResult Register(RegisterViewModel model);
        User GetUserByEmail(string email);
        void Logout();
    }
}
