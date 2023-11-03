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
                return MethodResult.Success();
            }
            catch (Exception ex)
            {
                return MethodResult.Failure(ex.Message);
            }
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync()
        {
            var result =  await _context.Groups.ToListAsync();
            return result.OrderBy(result => result.Id);
        }

        public async Task<Group> GetGroupById(int groupId)
        {
            return await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId);
        }

        public async Task<MethodResult> DeleteGroupAsync(int groupId)
        {
            try
            {
                var result = await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId);
                if (result != null)
                {
                    _context.Groups.Remove(result);
                    await _context.SaveChangesAsync();
                    return MethodResult.Success();
                }
                return MethodResult.Failure($"Не найдена группа с Id: {groupId}");
            }
            catch (Exception ex)
            {
                return MethodResult.Failure(ex.Message);
            }
        }

        public bool CheckIfNonEditable(Group group)
        {
            var peopleWithGroups = _context.People.Include("Group").ToList();
            var person = peopleWithGroups.FirstOrDefault(p => p.GroupId == group.Id);
            if (person is not null) return true;
            return group.Name.Equals("TEACHERS IN HUMAN") || group.Name.Equals("TEACHERS IN TECH");
        }
    }
}
