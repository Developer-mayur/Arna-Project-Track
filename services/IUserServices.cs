using Arna_Project_Track.Models;

namespace Arna_Project_Track.services
{
    public interface IUserServices
    {

        Task AddUser(User user);
        Task<List<User>> GetAllUsers();

    
        Task<User> GetUserByIdAsync(int id);

        void EditUserAsync(User updatedUser);

        Task<bool> DeleteUserAsync(int id);
       
    }
}
