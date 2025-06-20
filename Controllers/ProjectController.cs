using Arna_Project_Track.Models;
using Arna_Project_Track.services;
using Microsoft.AspNetCore.Mvc;

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
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        //add Project in the database 

        [HttpPost]
        public IActionResult AddProject(Project project)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("Login", "Login");
            }
            if (ModelState.IsValid)
            {
                _project.AddProject(project);
                
                return RedirectToAction("ProjectView");
            }
            return View(project);
        }

        // Display all data on the table
        public IActionResult ProjectView()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("Login", "Login");
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
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        [HttpPost]
        public IActionResult ProjectGetById(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("Login", "Login");
            }
            var project = _project.GetProjectById(id);

            if (project == null)
            {
                ViewBag.Message = "No project found for the given ID.";
                return View();
            }

            return View(project);
        }

        //public IActionResult ViewAllProjects()
        //{
        //    if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
        //    {
        //        return RedirectToAction("Login", "Login");
        //    }
        //    var projects = _project.GetAllProjects();
        //    return View(projects);
        //}


        public IActionResult ViewAllProjects(string? name, string? status)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("Login", "Login");
            }

            var projects = _project.GetAllProjects(); 

            // Filter by Name
            if (!string.IsNullOrEmpty(name))
            {
                projects = projects
                    .Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Filter by Status
            if (!string.IsNullOrEmpty(status))
            {
                projects = projects
                    .Where(p => p.Status != null && p.Status.Contains(status, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return View(projects);
        }



    }
}

