namespace ActiveForum.Client.Repositories.ForumRepositories
{
    using System;
    using System.Threading.Tasks;
    using ActiveForum.Client.HttpHelpers;
    using ActiveForum.Shared.Interfaces.ForumRepositories;
    using ActiveForum.Shared.Models.ForumModels;

    public class TopicsMenagmentRepository : ITopicsMenagmentRepository
    {
        private readonly IHttpService httpService;
        private readonly string url = "api/topicsMenagment";

        public TopicsMenagmentRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<int> CreateTopic(Topic topic)
        {
            var response = await httpService.Post<Topic, int>(url, topic);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task DeleteTopic(int id)
        {
            var response = await httpService.Delete($"{url}/{id}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task EditTopic(Topic topic)
        {
            var response = await httpService.Put(url, topic);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
    }
}
