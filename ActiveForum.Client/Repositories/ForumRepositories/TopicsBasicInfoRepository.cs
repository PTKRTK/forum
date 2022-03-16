namespace ActiveForum.Client.Repositories.ForumRepositories
{
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActiveForum.Client.HttpHelpers;
using ActiveForum.Shared.DTOs;
using ActiveForum.Shared.Interfaces.ForumRepositories;
using ActiveForum.Shared.Models;

public class TopicsBasicInfoRepository : ITopicsBasicInfoRepository
    {
        private readonly IHttpService httpService;
        private readonly string url = "api/topicsBoard";

        public TopicsBasicInfoRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<TopicBasicInfoDTO> GetTopic(int topicId)
        {
            var response = await httpService.Get<TopicBasicInfoDTO>(url + $"/topic/{topicId}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task<PaginatedResponse<List<TopicBasicInfoDTO>>> GetTopics(PaginationDTO paginationDTO, int sportId)
        {
            string newUrl = url + $"/{sportId}" + $"?currentPage={paginationDTO.CurrentPage}&pageObjectsNumber={paginationDTO.PageObjectsNumber}";

            var response = await httpService.Get<PaginatedResponse<List<TopicBasicInfoDTO>>>(newUrl);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task<PaginatedResponse<List<TopicBasicInfoDTO>>> GetTopics(PaginationDTO paginationDTO, int sportId, TopicsFilterDTO topicsFilterDTO)
        {
            string pagUrl = $"?currentPage={paginationDTO.CurrentPage}&pageObjectsNumber={paginationDTO.PageObjectsNumber}";

            var responseHttp = await httpService.Post<TopicsFilterDTO, PaginatedResponse<List<TopicBasicInfoDTO>>>($"{url}/{sportId}/filter" + pagUrl, topicsFilterDTO);

            var response = responseHttp.Response;

            return response;
        }
    }
}
