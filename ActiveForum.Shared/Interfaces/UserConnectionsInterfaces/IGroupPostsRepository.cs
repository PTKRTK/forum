namespace ActiveForum.Shared.Interfaces.UserConnectionsInterfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ActiveForum.Shared.DTOs;
    using ActiveForum.Shared.Models.GroupModels;

    public interface IGroupPostsRepository
    {
        Task CreatePost(GroupPost post);

        Task DeletePost(int postId);

        Task<PaginatedResponse<List<GroupPost>>> GetTopicPosts(PaginationDTO paginationDTO, int postId);
    }
}
