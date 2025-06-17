using Arna_Project_Track.Models;

namespace Arna_Project_Track.services
{
    public interface ILogin
    {
        Task<User> AuthenticateAsync(string email, string password);
    }
}
