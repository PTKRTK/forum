namespace ActiveForum.Shared.Interfaces.ForumRepositories
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveForum.Shared.DTOs;

public interface ITopicsBasicInfoRepository
    {
        Task<TopicBasicInfoDTO> GetTopic(int topicId);

        Task<PaginatedResponse<List<TopicBasicInfoDTO>>> GetTopics(PaginationDTO paginationDTO, int sportId);

        Task<PaginatedResponse<List<TopicBasicInfoDTO>>> GetTopics(PaginationDTO paginationDTO, int sportId, TopicsFilterDTO topicsFilterDTO);
    }
}
