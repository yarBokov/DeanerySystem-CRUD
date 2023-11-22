using DeanerySystem.Data;
using DeanerySystem.Data.Entities;
using DeanerySystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DeanerySystem.Services
{
    public class MarkService : IMarkService
    {
        private readonly DeaneryContext _context;
        public MarkService(DeaneryContext context)
        {
            _context= context;
        }

        public async Task<MethodResult> SaveMarkAsync(Mark mark)
        {
            try
            {
                if (mark.Id > 0)
                {
                    _context.Update(mark);
                }
                else
                {
                    await _context.AddAsync(mark);
                }
                await _context.SaveChangesAsync();
                return MethodResult.Success();
            }
            catch (Exception ex)
            {
                return MethodResult.Failure(ex.Message);
            }
        }

        public async Task<IEnumerable<Mark>> GetMarksAsync()
        {
            var result = await _context.Marks.Include(m => m.Teacher)
                                       .Include(m => m.Subject).ThenInclude(s => s.Marks)
                                       .Include(m => m.Student).ThenInclude(s => s.Group).ToListAsync();
            return result.OrderBy(mark => mark.Student.GroupId).ThenBy(mark => mark.Student.SecondName).ThenBy(mark => mark.Subject.Name);
        }

        public async Task<MethodResult> DeleteMarkAsync(int markId)
        {
            try
            {
                var result = await _context.Marks.FirstOrDefaultAsync(m => m.Id == markId);
                if (result != null)
                {
                    _context.Marks.Remove(result);
                    await _context.SaveChangesAsync();
                    return MethodResult.Success();
                }
                return MethodResult.Failure($"Не найдена оценка с Id: {markId}");
            }
            catch (Exception ex)
            {
                return MethodResult.Failure(ex.Message);
            }
        }
        public void CheckEntries(Mark mark)
        {
            var markEntry = _context.Entry(mark);
            if (markEntry.State == EntityState.Modified)
            {
                markEntry.CurrentValues.SetValues(markEntry.OriginalValues);
                markEntry.State = EntityState.Unchanged;
            }
        }

        public List<MarkDistrModel> GetMarkDistrs()
        {
            List<MarkDistrModel> markDistrs = new List<MarkDistrModel>();
            int marksCount = 0;
            for (int i = 2; i < 6; i++)
            {
                for (int j = 1; j < 3; j++)
                {
                    foreach (var subject in _context.Subjects.Include(s => s.Marks).Where(s => s.Name != null).ToList())
                    {
                        foreach (var group in _context.Groups.Include(g => g.People).Where(g => g.People.FirstOrDefault().Type == 'S').ToList())
                        {
                            marksCount = subject.Marks.Where(m =>
                                    m.Term == j && m.Value == i && group.People.Select(g => g.Id).ToList().Contains(m.StudentId.Value)).Count();
                            if (marksCount > 0)
                                markDistrs.Add(new MarkDistrModel
                                {
                                    Term = j,
                                    MarkNumber = i.ToString(),
                                    GroupId = group.Id,
                                    SubjectId = subject.Id,
                                    MarkCount = marksCount,
                                });
                        }
                    }
                }
            }
            return markDistrs;
        }

        public List<AvgMarkDynamicModel> GetAvgMarkDynamics()
        {
            List<AvgMarkDynamicModel> avgMarkDynamicModels= new List<AvgMarkDynamicModel>();
            foreach (var subject in _context.Subjects.Include(s => s.Marks).Where(s => s.Name != null).ToList())
            {
                foreach (var group in _context.Groups.Include(g => g.People).Where(g => 
                    g.People.FirstOrDefault().Type == 'S' && g.People.Any()).OrderBy(g => g.Year).ToList())
                {
                    if ((DateTime.Now.Year - group.Year) <= 5)
                        for (int i = 1; i < 3; i++)
                        {
                            if(_context.Marks.Include(m => m.Student).Where(m => m.Term == i && m.Student.GroupId == group.Id).Any())
                                avgMarkDynamicModels.Add(new AvgMarkDynamicModel
                                {
                                    SubjectId = subject.Id,
                                    AvgMark = subject.getAverageMarkInTerm(i),
                                    YearTerm = group.Year + " | " + i
                                });
                        }
                }
            }
            return avgMarkDynamicModels;
        }
    }
}
