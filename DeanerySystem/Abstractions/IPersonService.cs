using DeanerySystem.Data.Entities;
using DeanerySystem.Models;

namespace DeanerySystem.Abstractions
{
    public interface IPersonService
    {
        Task<MethodResult> SavePersonAsync(Person person);
        Task<MethodResult> DeletePersonAsync(int personId, bool isTeacher);
        void CheckEntries(Person person);
        Task<IEnumerable<Person>> GetTeachersAsync();
        Task<IEnumerable<Person>> GetStudentsAsync();
        Task<List<PersonModel>> GetPersonModelsAsync(char groupType);
    }
}