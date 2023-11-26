using DeanerySystem.Models;

namespace DeanerySystem.Abstractions
{
    public interface IAccountService
    {
        Task<User?> GetUserByPersonId(int personId);
        Task<MethodResult> SaveUserAsync(User user);
        Task<Key?> GetKeyAsync(string accessKey);
    }
}
