using DeanerySystem.Data;
using DeanerySystem.Data.Entities;
using DeanerySystem.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DeanerySystem.Services
{
    public class GroupService : IGroupService
    {
        private readonly DeaneryContext _context;

        public GroupService(DeaneryContext context)
        {
            _context = context;
        }

        public async Task<MethodResult> SaveGroupAsync(Group group)
        {
            try
            {
                if (group.Id > 0)
                {
                    _context.Update(group);
                }
                else
                {
                    await _context.AddAsync(group);
                }
                await _context.SaveChangesAsync();
                await _context.DisposeAsync();
                return MethodResult.Success();
            }
            catch (Exception ex)
            {
                return MethodResult.Failure(ex.Message);
            }
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync()
        {
            var result =  await _context.Groups.AsNoTracking().ToListAsync();
            return result.OrderByDescending(result=> result.Name);
        }

        public IEnumerable<Group> GetStudentGroups()
        {
            return _context.Groups.Where(g => g.Name.Contains("TEACH") == false);
        }

        public IEnumerable<Group> GetTeacherGroups()
        {
            return _context.Groups.Where(g => g.Name.Contains("TEACH"));
        }

        public async Task<Group> GetGroupByIdAsync(int? groupId)
        {
            return await _context.Groups.FindAsync(groupId);
        }
    }
}
