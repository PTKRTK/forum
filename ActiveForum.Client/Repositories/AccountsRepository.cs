namespace ActiveForum.Client.Repositories
{
using System;
using System.Threading.Tasks;
using ActiveForum.Client.HttpHelpers;
using ActiveForum.Shared.DTOs;
using ActiveForum.Shared.Interfaces;

public class AccountsRepository : IAccountsRepository
    {
        private readonly IHttpService httpService;
        private readonly string url = "api/accounts";

        public AccountsRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<UserToken> Register(UserInfo userInfo)
        {
            var httpResponse = await httpService.Post<UserInfo, UserToken>($"{url}/create", userInfo);

            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetBody());
            }

            return httpResponse.Response;
        }

        public async Task<UserToken> Login(UserInfo userInfo)
        {
            var httpResponse = await httpService.Post<UserInfo, UserToken>($"{url}/login", userInfo);

            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetBody());
            }

            return httpResponse.Response;
        }

        public async Task<UserToken> RenewToken()
        {
            var response = await httpService.Get<UserToken>($"{url}/renewToken");

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }
    }
}
