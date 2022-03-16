namespace ActiveForum.Shared.Interfaces.UserConnectionsInterfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ActiveForum.Shared.Models.CommunicatorModels;

    public interface IMessagesRepository
    {
        Task<List<MessageModel>> GetMessages(string friendMail);
    }
}
