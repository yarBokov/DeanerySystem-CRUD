using DeanerySystem.Data;
using DeanerySystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeanerySystem.Services
{
    public class GroupService : IGroupService
    {
        private readonly DeaneryContext _context;

        public GroupService(DeaneryContext context)
        {
            _context = context;
        }

        public async Task<string> GetGroupNameById(int? groupId)
        {
            var result =  await _context.Groups.FindAsync(groupId);
            return result.Name;
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync()
        {
            return await _context.Groups.AsNoTracking().ToListAsync();
        }

        public IEnumerable<Group> GetStudentGroups()
        {
            return _context.Groups.Where(g => g.Name.Contains("TEACH") == false);
        }

        public IEnumerable<Group> GetTeacherGroups()
        {
            return _context.Groups.Where(g => g.Name.Contains("TEACH"));
        }
    }
}
