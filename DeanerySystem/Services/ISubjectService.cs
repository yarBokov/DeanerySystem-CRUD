using DeanerySystem.Data.Entities;
using DeanerySystem.Models;

namespace DeanerySystem.Services
{
    public interface ISubjectService
    {
        Task<MethodResult> DeleteSubjectAsync(int subjectId);
        Task<Subject> GetSubjectById(int subjectId);
        Task<IEnumerable<Subject>> GetSubjectsAsync();
        Task<MethodResult> SaveSubjectAsync(Subject subject);
    }
}