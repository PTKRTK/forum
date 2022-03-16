namespace ActiveForum.Client.Repositories.ForumRepositories
{
    using System;
    using System.Threading.Tasks;
    using ActiveForum.Client.HttpHelpers;
    using ActiveForum.Shared.Interfaces.ForumRepositories;
    using ActiveForum.Shared.Models.ForumModels;

    public class TopicRatingsRepository : ITopicRatingsRepository
    {
        private readonly IHttpService httpService;

        private readonly string url = "api/topicRatings";

        public TopicRatingsRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task AddRating(TopicRating topicRating)
        {
            var httpResponse = await httpService.Post(url, topicRating);

            if (!httpResponse.Success)
            {
                throw new ApplicationException(await httpResponse.GetBody());
            }
        }

        public async Task<TopicRating> GetUserRating(int topicId)
        {
            var response = await httpService.Get<TopicRating>(url + $"/userRating/{topicId}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task<TopicRating> GetAvgRating(int topicId)
        {
            var response = await httpService.Get<TopicRating>(url + $"/avgRating/{topicId}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }
    }
}
