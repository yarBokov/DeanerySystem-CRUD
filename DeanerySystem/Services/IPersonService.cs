using DeanerySystem.Data.Entities;
using DeanerySystem.Models;

namespace DeanerySystem.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetPeopleAsync();

        Task<IEnumerable<Person>> GetStudentsAsync();
        Task<IEnumerable<Person>> GetTeachersAsync();
        Task<MethodResult> SavePersonAsync(Person person);
        Task<MethodResult> DeletePersonAsync(Person person);
        void CheckEntries(Person person);
    }
}