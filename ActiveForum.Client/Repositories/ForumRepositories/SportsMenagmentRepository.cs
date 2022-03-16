namespace ActiveForum.Client.Repositories.ForumRepositories
{
    using System;
    using System.Threading.Tasks;
    using ActiveForum.Client.HttpHelpers;
    using ActiveForum.Shared.Interfaces.ForumRepositories;
    using ActiveForum.Shared.Models.ForumModels;

    public class SportsMenagmentRepository : ISportsMenagmentRepository
    {
        private readonly IHttpService httpService;
        private readonly string url = "api/sportsMenagment";

        public SportsMenagmentRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task CreateSport(Sport sport)
        {
            var response = await httpService.Post(url, sport);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task EditSport(Sport sport)
        {
            var response = await httpService.Put(url, sport);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task DeleteSport(int id)
        {
            var response = await httpService.Delete($"{url}/{id}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
    }
}
