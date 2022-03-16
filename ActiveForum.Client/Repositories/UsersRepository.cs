namespace ActiveForum.Client.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;   
    using ActiveForum.Client.HttpHelpers;
    using ActiveForum.Shared.DTOs;
    using ActiveForum.Shared.Interfaces;

    public class UsersRepository : IUsersRepository
        {
        private readonly IHttpService httpService;
        private readonly string url = "api/users";

        public UsersRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<PaginatedResponse<List<UserDTO>>> GetUsers(PaginationDTO paginationDTO)
        {
            string newUrl = url + $"?currentPage={paginationDTO.CurrentPage}&pageObjectsNumber={paginationDTO.PageObjectsNumber}";

            var response = await httpService.Get<PaginatedResponse<List<UserDTO>>>(newUrl);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task<List<RoleDTO>> GetRoles()
        {
            var response = await httpService.Get<List<RoleDTO>>($"{url}/roles");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task AssignRole(EditRoleDTO editRole)
        {
            var response = await httpService.Post($"{url}/assignRole", editRole);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task RemoveRole(EditRoleDTO editRole)
        {
            var response = await httpService.Post($"{url}/removeRole", editRole);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
    }
}
