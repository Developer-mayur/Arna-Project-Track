using Arna_Project_Track.Models;
using Arna_Project_Track.Services;
using Microsoft.AspNetCore.Mvc;

namespace Arna_Project_Track.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuth auth;
        public AuthController(IAuth auth)
        {
            this.auth = auth;
            // Constructor logic can go here if needed
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = auth.Login(email, password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserEmail", user.Email);
                TempData["Success"] = "Login successful!";
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Invalid email or password.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["LogoutMessage"] = "Logged out successfully!";
            return RedirectToAction("Index", "Home");
        }
    }
}
