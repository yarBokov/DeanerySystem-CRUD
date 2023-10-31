using DeanerySystem.Data.Entities;

namespace DeanerySystem.Services
{
    public interface ISubjectService
    {
        Task<IEnumerable<Subject>> GetSubjectsAsync();
    }
}