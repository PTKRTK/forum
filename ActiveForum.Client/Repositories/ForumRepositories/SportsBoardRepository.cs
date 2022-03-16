namespace ActiveForum.Client.Repositories.ForumRepositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ActiveForum.Client.HttpHelpers;
    using ActiveForum.Shared.DTOs;
    using ActiveForum.Shared.Interfaces.ForumRepositories;
    using ActiveForum.Shared.Models.ForumModels;

    public class SportsBoardRepository : ISportsBoardRepository
    {
        private readonly IHttpService httpService;
        private readonly string url = "api/sportsBoard";

        public SportsBoardRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<PaginatedResponse<List<Sport>>> GetSports(PaginationDTO paginationDTO)
        {
            string newUrl = url + $"?currentPage={paginationDTO.CurrentPage}&pageObjectsNumber={paginationDTO.PageObjectsNumber}";

            var response = await httpService.Get<PaginatedResponse<List<Sport>>>(newUrl);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task<PaginatedResponse<List<Sport>>> GetSports(PaginationDTO paginationDTO, string name)
        {
            string newUrl = url + $"/search/{name}" + $"?currentPage={paginationDTO.CurrentPage}&pageObjectsNumber={paginationDTO.PageObjectsNumber}";

            var response = await httpService.Get<PaginatedResponse<List<Sport>>>(newUrl);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }
    }
}
