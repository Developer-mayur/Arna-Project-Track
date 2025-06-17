using Arna_Project_Track.Models;

namespace Arna_Project_Track.Services
{
    public interface IAuth
    {
        Users Login(string email, string password);
    }
    
}
