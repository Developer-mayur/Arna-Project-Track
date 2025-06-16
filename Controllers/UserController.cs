using Arna_Project_Track.Models;
using Arna_Project_Track.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Arna_Project_Track.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly MyDBContext _context;

        public UserController(IUserServices userServices, MyDBContext context)
        {
            _userServices = userServices;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddUser()
        {
            ViewBag.EmployeeRole = new SelectList(await _context.EmployeeRoles.ToListAsync(), "RoleId", "RoleName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                await _userServices.AddUser(user);
                TempData["Success"] = "User added successfully!";
                return RedirectToAction("AddUser");
            }

            ViewBag.EmployeeRole = new SelectList(_context.EmployeeRoles.ToList(), "RoleId", "RoleName");
            return View(user);
        }
        // POST: AddUser
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddUser(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Users.Add(user);
        //        await _context.SaveChangesAsync();
        //        TempData["Success"] = "User added successfully!";
        //        return RedirectToAction("GetAllUsers");
        //    }

        //    ViewBag.EmployeeRole = new SelectList(await _context.EmployeeRoles.ToListAsync(), "RoleId", "RoleName");
        //    return View(user);
        //}


        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userServices.GetAllUsers();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userServices.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();
            ViewBag.EmployeeRole = new SelectList(_context.EmployeeRoles.ToList(), "RoleId", "RoleName", user.EmployeeRole);
            return View(user);
        }
       
        [HttpGet]
        public async Task<IActionResult> EditUser(int id)
        {
            var user = await _userServices.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            ViewBag.EmployeeRole = new SelectList(_context.EmployeeRoles.ToList(), "RoleId", "RoleName", user.EmployeeRole);
            return View(user);
        }

        [HttpPost]

        public async Task<IActionResult> EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                _userServices.EditUserAsync(user);
                TempData["Success"] = "User updated successfully!";
                return RedirectToAction("GetAllUsers");
            }

            ViewBag.EmployeeRole = new SelectList(_context.EmployeeRoles.ToList(), "RoleId", "RoleName", user.EmployeeRole);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            bool isDeleted = await _userServices.DeleteUserAsync(id);
            if (isDeleted)
            {
                TempData["Success"] = "User deleted successfully.";
            }
            else
            {
                TempData["Error"] = "User not found.";
            }

            return RedirectToAction("GetAllUsers");
        }

    }
}