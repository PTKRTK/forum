namespace ActiveForum.Shared.Interfaces.ForumRepositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ActiveForum.Shared.DTOs;
    using ActiveForum.Shared.Models.ForumModels;

    public interface IPostsRepository
    {
        Task CreatePost(Post post);

        Task DeletePost(int postId);

        Task<PaginatedResponse<List<Post>>> GetTopicPosts(PaginationDTO paginationDTO,  int topicId);
    }
}
