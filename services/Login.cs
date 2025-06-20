using Arna_Project_Track.Models;
using Microsoft.EntityFrameworkCore;

namespace Arna_Project_Track.services
{
    public class Login: ILogin
    {
        private readonly MyDBContext _context;
        public Login(MyDBContext context)
        {
            _context = context;
        }
        public async Task<User> AuthenticateAsync(string email, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }



    }
}
