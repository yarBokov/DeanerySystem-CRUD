using DeanerySystem.Data.Entities;
using DeanerySystem.Models;

namespace DeanerySystem.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetPeopleAsync();
        Task<MethodResult> SavePersonAsync(Person person);
        Task<MethodResult> DeletePersonAsync(int personId);
        void CheckEntries(Person person);
    }
}