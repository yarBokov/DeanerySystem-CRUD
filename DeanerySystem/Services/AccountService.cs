using DeanerySystem.Data;
using DeanerySystem.Data.Entities;
using DeanerySystem.Models.Authentication;
using Microsoft.EntityFrameworkCore;

namespace DeanerySystem.Services
{
    public class AccountService
    {
        private readonly DeaneryContext _context;

        public AccountService(DeaneryContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.Include(u => u.Person).FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
