using DeanerySystem.Data;
using DeanerySystem.Data.Entities;
using DeanerySystem.Models;
using Microsoft.EntityFrameworkCore;
using System;

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
                                       .Include(m => m.Subject)
                                       .Include(m => m.Student).ToListAsync();
            return result.OrderBy(mark => mark.Id);
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
    }
}
