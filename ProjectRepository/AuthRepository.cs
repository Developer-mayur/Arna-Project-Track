using Arna_Project_Track.Models;
using Arna_Project_Track.Services;

namespace Arna_Project_Track.ProjectRepository
{
    public class AuthRepository : IAuth
    {
        private readonly AppDbContext _context;
        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public Users Login(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}
