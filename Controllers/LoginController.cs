using Arna_Project_Track.Models;
using Arna_Project_Track.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arna_Project_Track.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogin _login;
        private readonly MyDBContext _context;
        public LoginController(ILogin login , MyDBContext context )
        {
            _login = login;
            _context = context;
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


        //public async Task<IActionResult> Login(User user)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //        var validUser = await _login.AuthenticateAsync(user.Email, user.Password);
        //        if (validUser != null)
        //        {
        //            // Set session
        //            HttpContext.Session.SetString("UserEmail", validUser.Email);
        //            HttpContext.Session.SetInt32("UserId", validUser.UserId);

        //            // Track in ActiveUser
        //            var activeEntry = _context.ActiveUsers.FirstOrDefault(a => a.UserId == validUser.UserId);

        //            if (activeEntry == null)
        //            {
        //                _context.ActiveUsers.Add(new ActiveUser
        //                {
        //                    UserId = validUser.UserId,
        //                    LoginTime = DateTime.Now,
        //                    LastActiveTime = DateTime.Now,
        //                    IsOnline = true
        //                });
        //            }
        //            else
        //            {
        //                activeEntry.LoginTime = DateTime.Now;
        //                activeEntry.LastActiveTime = DateTime.Now;
        //                activeEntry.IsOnline = true;
        //                _context.ActiveUsers.Update(activeEntry);
        //            }

        //            await _context.SaveChangesAsync();

        //            return RedirectToAction("Index", "Home");
        //        }

        //        ViewBag.Message = "Invalid login";


        //    return View(user);
        //}

        //public IActionResult Logout()
        //{
        //    var userId = HttpContext.Session.GetInt32("UserId");
        //    if (userId != null)
        //    {
        //        var activeEntry = _context.ActiveUsers.FirstOrDefault(a => a.UserId == userId.Value);
        //        if (activeEntry != null)
        //        {
        //            activeEntry.IsOnline = false;
        //            activeEntry.LastActiveTime = DateTime.Now;
        //            _context.ActiveUsers.Update(activeEntry);
        //            _context.SaveChanges();
        //        }
        //    }

        //    HttpContext.Session.Clear();
        //    return RedirectToAction("Login", "Login");
        //}


    }
}
