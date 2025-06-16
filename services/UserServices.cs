using Arna_Project_Track.Models;
using Microsoft.EntityFrameworkCore;

namespace Arna_Project_Track.services
{
    public class UserServices:IUserServices
    {
        private readonly MyDBContext _context;

        public UserServices(MyDBContext context) {
            _context = context;
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users
                .Include(u => u.EmployeeRoleNavigation) 
                .ToListAsync();
        }


        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public void EditUserAsync(User updatedUser)
        {
            _context.Users.Update(updatedUser);

            _context.SaveChangesAsync();

        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true; 
        }

    }
}
