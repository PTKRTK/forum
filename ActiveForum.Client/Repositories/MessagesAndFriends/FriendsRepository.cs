namespace ActiveForum.Client.Repositories.UserConnectionsRepositories
{
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActiveForum.Client.HttpHelpers;
using ActiveForum.Shared.DTOs;
using ActiveForum.Shared.Interfaces.UserConnectionsInterfaces;

public class FriendsRepository : IFriendsRepository
    {
        private readonly IHttpService httpService;
        private readonly string url = "api/friends";

        public FriendsRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task AccepptInvite(FriendInfo friend)
        {
            string newUrl = url + $"/accept";

            var response = await httpService.Put<FriendInfo>(newUrl, friend);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task<ActionMessage> Invite(string invitedUser)
        {
            FriendInfo friendInfo = new ()
            {
                Email = invitedUser
            };

            string newUrl = url + $"/invite/";

            var response = await httpService.Post<FriendInfo, ActionMessage>(newUrl, friendInfo);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task<List<FriendInfo>> GetFriends()
        {
            string newUrl = url + $"/getFriends";

            var response = await httpService.Get<List<FriendInfo>>(newUrl);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task<List<FriendInfo>> GetRequests()
        {
            string newUrl = url + $"/requests";

            var response = await httpService.Get<List<FriendInfo>>(newUrl);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task DeleteFriend(FriendInfo exFriend)
        {
            string newUrl = url + $"/delete";

            var response = await httpService.Delete($"{newUrl}/{exFriend.Email}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
    }
}
