namespace ActiveForum.Client.Repositories.ForumRepositories
{
    using System;
    using System.Threading.Tasks;
    using ActiveForum.Client.HttpHelpers;
    using ActiveForum.Shared.DTOs;
    using ActiveForum.Shared.Interfaces.ForumRepositories;

    public class VerificationRepository : IVerificationRepository
    {
        private readonly IHttpService httpService;
        private readonly string url = "api/verification";

        public VerificationRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task Verify(VerifiedDTO verifiedDTO)
        {
            var response = await httpService.Post(url, verifiedDTO);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
    }
}
