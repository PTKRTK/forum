namespace ActiveForum.Shared.Interfaces.ForumRepositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ActiveForum.Shared.Models.ForumModels;

    public interface ITopicRatingsRepository
    {
        Task AddRating(TopicRating topicRating);
     
        Task<TopicRating> GetAvgRating(int topicId);
       
        Task<TopicRating> GetUserRating(int topicId);
    }
}
