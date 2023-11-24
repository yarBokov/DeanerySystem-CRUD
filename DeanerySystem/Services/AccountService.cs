using DeanerySystem.Data;
using DeanerySystem.Models.Authentication;

namespace DeanerySystem.Services
{
    public class AccountService
    {
        private readonly DeaneryContext _context;

        public AccountService(DeaneryContext context)
        {
            _context = context;
        }

        public async Task<LoggedInAdmin?> LoginAsync(LoginModel model)
        {
            var dbUser = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == model.Username);
            if (dbUser is not null)
            {
                return new LoggedInAdmin(dbUser.Id, $"{dbUser.FirstName} {dbUser.LastName}".Trim());
            }
            else
            {
                return null;
            }
        }
    }
}
