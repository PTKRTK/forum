namespace ActiveForum.Shared.Interfaces.ForumRepositories
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveForum.Shared.Models.ForumModels;

public interface ITopicsCategoriesRepository
    {
        Task<List<CategoryOfTopic>> GetAllCategories();

        Task<List<CategoryOfTopic>> GetTopicCategories(int topicId);
    }
}
