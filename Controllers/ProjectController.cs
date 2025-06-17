using Arna_Project_Track.Models;
using Arna_Project_Track.Services;
using Microsoft.AspNetCore.Mvc;
using static Arna_Project_Track.Services.IProject;

namespace Arna_Project_Track.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProject _project;
        public ProjectController(IProject project)
        {
            _project = project;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddProject()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }

        //add Project in the database 

        [HttpPost]
        public IActionResult AddProject(Project project)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("Login", "Auth");
            }
            if (ModelState.IsValid)
            {
                _project.AddProject(project);
                _project.Save();
                return RedirectToAction("ProjectView");
            }
            return View(project);
        }

        // Display all data on the table
        public IActionResult ProjectView(Users users)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("Login", "Auth");
            }
            var projects = _project.GetAllProjects();
            return View(projects);
        }



        //Update project by id


        // GET: Edit
        [HttpGet]
        public IActionResult EditProject(int id)
        {
            var project = _project.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project); // View: Edit.cshtml
        }

        // POST: Edit
        [HttpPost]
        public IActionResult EditProject(Project project)
        {
            if (ModelState.IsValid)
            {
                _project.UpdateProject(project);
                // TempData["Success"] = "Updata data Successfull";
                return RedirectToAction("ProjectView");
            }

            return View(project);
        }

        //Delete project by id
        [HttpPost]
        public IActionResult DeleteProject(int id)
        {
            var project = _project.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }

            _project.DeleteProject(project);
            return RedirectToAction("ProjectView");
        }


        //Search by ID
        [HttpGet]
        public IActionResult ProjectGetById()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }

        [HttpPost]
        public IActionResult ProjectGetById(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("Login", "Auth");
            }
            var project = _project.GetProjectById(id);

            if (project == null)
            {
                ViewBag.Message = "No project found for the given ID.";
                return View();
            }

            return View(project);
        }


    }

}