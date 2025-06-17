using Arna_Project_Track.Models;
using Arna_Project_Track.services;
using Microsoft.AspNetCore.Mvc;

namespace Arna_Project_Track.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogin _login;

        public LoginController(ILogin login) {
            _login = login;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Message = TempData["Success"];
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            //if (ModelState.IsValid)
            //{
                var users = await _login.AuthenticateAsync(user.Email, user.Password);
                if (users != null)
                {
                HttpContext.Session.SetString("UserEmail", users.Email);
               
                TempData["Success"] = "Login successful!";
                    return RedirectToAction("Index", "Home"); 
                }

                ModelState.AddModelError(string.Empty, "Invalid email or password.");
            //}

            return View(user);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Success"] = "Logged out successfully!";
            return RedirectToAction("Index", "Home");
        }

    }
}
