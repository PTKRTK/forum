namespace ActiveForum.Client.Repositories.ForumRepositories
{
using System;
using System.Threading.Tasks;
using ActiveForum.Client.HttpHelpers;
using ActiveForum.Shared.Interfaces.ForumRepositories;
using ActiveForum.Shared.Models.ForumModels;

public class PostRatingRepository : IPostRatingsRepository
    {
        private readonly IHttpService httpService;

        private readonly string url = "api/postRatings";

        public PostRatingRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task AddRating(PostRating postRating)
        {
            var httpResponse = await httpService.Post(url, postRating);

            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetBody());
            }
        }

        public async Task<PostRating> GetUserRating(int postId)
        {
                var response = await httpService.Get<PostRating>(url + $"/userRating/{postId}");
                if (!response.Success)
                {
                    throw new ApplicationException(await response.GetBody());
                }

                return response.Response;
        }

        public async Task<double> GetAvgRating(int postId)
        {
            var response = await httpService.Get<double>(url + $"/avgRating/{postId}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task DeleteRating(int id)
        {
            var response = await httpService.Delete($"{url}/{id}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
    }
}
