using DeanerySystem.Data;
using DeanerySystem.Data.Entities;
using DeanerySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DeanerySystem.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly DeaneryContext _context;

        public SubjectService(DeaneryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subject>> GetSubjectsAsync()
        {
            var result = await _context.Subjects.ToListAsync();
            return result.OrderBy(result => result.Id);
        }

        public async Task<MethodResult> SaveSubjectAsync(Subject subject)
        {
            try
            {
                if (subject.Id > 0)
                {
                    _context.Update(subject);
                }
                else
                {
                    await _context.AddAsync(subject);
                }
                await _context.SaveChangesAsync();
                return MethodResult.Success();
            }
            catch (Exception ex)
            {
                return MethodResult.Failure(ex.Message);
            }
        }
        public async Task<Subject> GetSubjectById(int subjectId)
        {
            return await _context.Subjects.FirstOrDefaultAsync(s => s.Id == subjectId);
        }

        public async Task<MethodResult> DeleteSubjectAsync(int subjectId)
        {
            try
            {
                var result = await _context.Groups.FirstOrDefaultAsync(g => g.Id == subjectId);
                if (result != null)
                {
                    _context.Groups.Remove(result);
                    await _context.SaveChangesAsync();
                    return MethodResult.Success();
                }
                return MethodResult.Failure($"Не найден предмет с Id: {subjectId}");
            }
            catch (Exception ex)
            {
                return MethodResult.Failure(ex.Message);
            }
        }

        public bool CheckIfNonEditable(Subject subject)
        {
            var marksWithSubjects = _context.Marks.Include("Subject").ToList();
            var mark = marksWithSubjects.FirstOrDefault(m => m.SubjectId == subject.Id);
            return mark is not null;
        }
    }
}
