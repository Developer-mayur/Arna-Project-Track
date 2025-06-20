using Arna_Project_Track.Models;
using Microsoft.AspNetCore.Mvc;

namespace Arna_Project_Track.services
{
    public interface ILogin
    {
        Task<User> AuthenticateAsync(string email, string password);
        //Task<IActionResult> Login(User user);
    }
}
