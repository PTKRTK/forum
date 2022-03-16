namespace ActiveForum.Client.Repositories.ForumRepositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ActiveForum.Client.HttpHelpers;
    using ActiveForum.Shared.DTOs;
    using ActiveForum.Shared.Interfaces.ForumRepositories;
    using ActiveForum.Shared.Models.ForumModels;

    public class PostsRepository : IPostsRepository
    {
        private readonly IHttpService httpService;
        private readonly string url = "api/posts";

        public PostsRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task CreatePost(Post post)
        {
                var response = await httpService.Post(url, post);
                if (!response.Success)
                {
                    throw new ApplicationException(await response.GetBody());
                }
        }

        public async Task DeletePost(int postId)
        {
            var response = await httpService.Delete($"{url}/{postId}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task<PaginatedResponse<List<Post>>> GetTopicPosts(PaginationDTO paginationDTO, int postId)
        {
            string newUrl = url + $"/{postId}" + $"?currentPage={paginationDTO.CurrentPage}&pageObjectsNumber={paginationDTO.PageObjectsNumber}";

            var response = await httpService.Get<PaginatedResponse<List<Post>>>(newUrl);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }
    }
}