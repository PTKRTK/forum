namespace ActiveForum.Shared.Interfaces.ForumRepositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ActiveForum.Shared.Models.ForumModels;

    public interface ITopicsMenagmentRepository
    {
        Task<int> CreateTopic(Topic topic);

        Task DeleteTopic(int id);

        Task EditTopic(Topic topic);
    }
}
