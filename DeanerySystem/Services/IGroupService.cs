using DeanerySystem.Data.Entities;
using DeanerySystem.Models;

namespace DeanerySystem.Services
{
    public interface IGroupService
    {
        Task<IEnumerable<Group>> GetGroupsAsync();

        Task<Group> GetGroupByIdAsync(int? groupId);
        IEnumerable<Group> GetStudentGroups();
        IEnumerable<Group> GetTeacherGroups();
        Task<MethodResult> SaveGroupAsync(Group group);
    }
}