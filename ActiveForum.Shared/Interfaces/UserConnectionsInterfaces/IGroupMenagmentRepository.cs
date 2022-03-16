namespace ActiveForum.Shared.Interfaces.UserConnectionsInterfaces
{
    using System.Threading.Tasks;
    using ActiveForum.Shared.DTOs;
    using ActiveForum.Shared.Models.GroupModels;

    public interface IGroupMenagmentRepository
    {
        Task<int> CreateGroup(PrivateGroup group);

        Task DeleteGroup(int id);

        Task EditGroup(PrivateGroup group);

        Task JoinGroup(GroupRoleDTO groupJoining);
    }
}
