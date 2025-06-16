using Arna_Project_Track.Models;
using Arna_Project_Track.Services;

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
            return _context.Projects.ToList();
        }


        //Upate project by id
        public Project GetProjectById(int id)
        {
            return _context.Projects.FirstOrDefault(p => p.Id == id);
        }

        public void UpdateProject(Project project)
        {
            _context.Projects.Update(project);
            _context.SaveChanges();
        }


        //Delete project by id

        public void DeleteProject(Project project)
        {
            _context.Projects.Remove(project);
            _context.SaveChanges();
        }

    }
}
