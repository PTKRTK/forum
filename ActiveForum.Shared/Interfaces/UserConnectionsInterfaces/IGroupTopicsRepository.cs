namespace ActiveForum.Shared.Interfaces.UserConnectionsInterfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ActiveForum.Shared.DTOs;
    using ActiveForum.Shared.Models.GroupModels;

    public interface IGroupTopicsRepository
    {
        Task<int> CreateTopic(GroupTopic topic);

        Task<int> DeleteTopic(int id);

        Task EditTopic(GroupTopic topic);
        
        Task<GroupTopic> GetTopic(int topicId);
        
        Task<PaginatedResponse<List<GroupTopic>>> GetTopics(PaginationDTO paginationDTO, int groupId);
        
        Task<PaginatedResponse<List<GroupTopic>>> GetTopics(PaginationDTO paginationDTO, GroupTopicFilterDTO groupTopicFilterDTO);
    }
}
