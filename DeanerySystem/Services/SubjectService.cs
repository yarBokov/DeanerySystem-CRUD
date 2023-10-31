using DeanerySystem.Data;
using DeanerySystem.Data.Entities;
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
            return await _context.Subjects.AsNoTracking().ToListAsync();
        }
    }
}
