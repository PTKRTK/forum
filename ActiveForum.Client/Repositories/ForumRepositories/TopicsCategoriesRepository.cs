namespace ActiveForum.Client.Repositories.ForumRepositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ActiveForum.Client.HttpHelpers;
    using ActiveForum.Shared.Interfaces.ForumRepositories;
    using ActiveForum.Shared.Models.ForumModels;

    public class TopicsCategoriesRepository : ITopicsCategoriesRepository
    {
        private readonly IHttpService httpService;
        private readonly string url = "api/sportsCategories";

        public TopicsCategoriesRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<List<CategoryOfTopic>> GetAllCategories()
        {
            var response = await httpService.Get<List<CategoryOfTopic>>(url);

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task<List<CategoryOfTopic>> GetTopicCategories(int topicId)
        {
            var response = await httpService.Get<List<CategoryOfTopic>>(url + $"/{topicId}");

            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }
    }
}