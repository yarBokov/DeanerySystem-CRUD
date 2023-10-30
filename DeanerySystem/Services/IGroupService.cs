using DeanerySystem.Data.Entities;

namespace DeanerySystem.Services
{
    public interface IGroupService
    {
        Task<IEnumerable<Group>> GetGroupsAsync();

        Task<string> GetGroupNameById(int? groupId);
        IEnumerable<Group> GetStudentGroups();
        IEnumerable<Group> GetTeacherGroups();
    }
}