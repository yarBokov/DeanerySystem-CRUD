using DeanerySystem.Data.Entities;
using DeanerySystem.Models;

namespace DeanerySystem.Services
{
    public interface IGroupService
    {
        Task<IEnumerable<Group>> GetGroupsAsync();
        Task<MethodResult> SaveGroupAsync(Group group);
        Task<Group> GetGroupById(int groupId);
        Task<MethodResult> DeleteGroupAsync(int personId);
        bool CheckIfNonEditable(Group group);
    }
}