using Arna_Project_Track.Models;
using Arna_Project_Track.Services;
using System.Collections.Generic;
using System.Linq;

namespace Arna_Project_Track.Repositories
{
    public class ProjectRepository : IProject
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddProject(Project project)
        {
            _context.Projects.Add(project);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public List<Project> GetAllProjects()
        {
       
            return _context.Projects
                           .OrderByDescending(p => p.Id) 
                           .ToList();

        }

        public Project GetProjectById(int id)
        {
            return _context.Projects.FirstOrDefault(p => p.Id == id);
        }

        public void UpdateProject(Project project)
        {
            _context.Projects.Update(project);
            _context.SaveChanges();
        }

        public void DeleteProject(Project project)
        {
            _context.Projects.Remove(project);
            _context.SaveChanges();
        }

        public Project? SearchProjectById(int id)
        {
            return _context.Projects.FirstOrDefault(p => p.Id == id);
        }



        public Users? Login(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}
