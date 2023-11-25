using DeanerySystem.Data;
using DeanerySystem.Data.Entities;
using DeanerySystem.Models;
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

        public async Task<User?> GetUserById(int userId) =>
            await _context.Users.Include(u => u.Person).FirstOrDefaultAsync(u => u.Id == userId);

        public async Task<User?> GetUserByPersonId(int personId) =>
            await _context.Users.Include(u => u.Person).FirstOrDefaultAsync(u => u.PersonId == personId);

        public async Task<MethodResult> SaveUserAsync(User user)
        {
            try
            {
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();
                return MethodResult.Success();
            }
            catch(Exception ex)
            {
                return MethodResult.Failure(ex.Message);
            }
        }

        public async Task<bool> CheckKeyAsync(string accessKey) =>
          await _context.Keys.ContainsAsync(new Key(accessKey));
    }
}
