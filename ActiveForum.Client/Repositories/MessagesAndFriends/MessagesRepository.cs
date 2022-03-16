namespace ActiveForum.Client.Repositories.UserConnectionsRepositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ActiveForum.Client.HttpHelpers;
    using ActiveForum.Shared.Interfaces.UserConnectionsInterfaces;
    using ActiveForum.Shared.Models.CommunicatorModels;

    public class MessagesRepository : IMessagesRepository
    {
        private readonly IHttpService httpService;
        private readonly string url = "api/messages";

        public MessagesRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<List<MessageModel>> GetMessages(string friendMail)
        {
            string newUrl = url + $"/getMessages/{friendMail}";

            var response = await httpService.Get<List<MessageModel>>(newUrl);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }
    }
}
