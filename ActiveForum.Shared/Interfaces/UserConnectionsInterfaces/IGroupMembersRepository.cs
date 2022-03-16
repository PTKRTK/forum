namespace ActiveForum.Shared.Interfaces.UserConnectionsInterfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ActiveForum.Shared.DTOs;

    public interface IGroupMembersRepository
    {
        Task AssignGroupRole(EditGroupRoleDTO editRole);

        Task<List<RoleDTO>> GetGroupRoles();

        Task<PaginatedResponse<List<GroupMemberDTO>>> GetGroupMembers(PaginationDTO paginationDTO, int groupId);

        Task RemoveRole(EditGroupRoleDTO editRole);
    }
}
