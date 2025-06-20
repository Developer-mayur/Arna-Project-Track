using Arna_Project_Track.Models;
using System;

namespace Arna_Project_Track.services
{
    public class ProjectServices : IProject
    {
        private readonly MyDBContext _context;

        public ProjectServices(MyDBContext context)
        {
            _context = context;
        }

        //add Project
        public void AddProject(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
        }


       

        //get all project

        public List<Project> GetAllProjects()
        {

            return _context.Projects
                           .OrderByDescending(p => p.Id)
                           .ToList();

        }

        //get project by Id
        public Project GetProjectById(int id)
        {
            return _context.Projects.FirstOrDefault(p => p.Id == id);
        }


        //update project by ID
        public void UpdateProject(Project project)
        {
            _context.Projects.Update(project);
            _context.SaveChanges();
        }

        //Delete project
        public void DeleteProject(Project project)
        {
            _context.Projects.Remove(project);
            _context.SaveChanges();
        }

        //serach project by ID
        public Project? SearchProjectById(int id)
        {
            return _context.Projects.FirstOrDefault(p => p.Id == id);
        }

    }
}
