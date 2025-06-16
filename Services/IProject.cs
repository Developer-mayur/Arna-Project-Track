using Arna_Project_Track.Models;

namespace Arna_Project_Track.Services
{
    public interface IProject
    {

        //Add project in the database
        void AddProject(Project project);
        void Save();

        //Get all projects from the database
        List<Project> GetAllProjects();

        //Upate project by id
        Project GetProjectById(int id);
        void UpdateProject(Project project);


        //Delete project by id

        void DeleteProject(Project project);


    }
}
