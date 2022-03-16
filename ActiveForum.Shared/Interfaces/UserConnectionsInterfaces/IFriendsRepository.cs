namespace ActiveForum.Shared.Interfaces.UserConnectionsInterfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ActiveForum.Shared.DTOs;

    public interface IFriendsRepository
    {
        Task AccepptInvite(FriendInfo askingUser);
      
        Task<ActionMessage> Invite(string invitedUser);
      
        Task DeleteFriend(FriendInfo exFriend);
       
        Task<List<FriendInfo>> GetFriends();
      
        Task<List<FriendInfo>> GetRequests();
    }
}
