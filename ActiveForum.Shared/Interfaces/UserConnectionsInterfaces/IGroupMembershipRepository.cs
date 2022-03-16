namespace ActiveForum.Shared.Interfaces.UserConnectionsInterfaces
{
    using System.Threading.Tasks;
    using ActiveForum.Shared.DTOs;

    public interface IGroupMembershipRepository
    {
        Task<GroupMembershipDTO> GetUserMembershipInfo(int groupId);

        Task<GroupMembershipDTO> GetUserMembershipInfoByGroupTopicId(int groupTopicId);

        Task RemoveMember(GroupMemberDTO groupMemberDTO);

        Task AcceptMember(GroupMemberDTO groupMemberDTO);
    }
}
