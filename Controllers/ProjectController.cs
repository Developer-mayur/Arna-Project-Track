using Arna_Project_Track.Models;
using Arna_Project_Track.Services;
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
            return View();
        }

        //add Project in the database 

        [HttpPost]
        public IActionResult AddProject(Project project)
        {
            if (ModelState.IsValid)
            {
                _project.AddProject(project);
                _project.Save();
                return RedirectToAction("AddProject");
            }
            return View(project);
        }

     // Display all data on the table
        public IActionResult ProjectView()
        {
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
                TempData["Success"] = "Updata data Successfull";
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


    }
}
