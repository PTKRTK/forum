namespace ActiveForum.Server.Helpers
{
    using System.Linq;
    using ActiveForum.Database.DbContexts;
    using ActiveForum.Database.Entities.ForumEntities;
    using ActiveForum.Shared.DTOs;
    using Microsoft.EntityFrameworkCore;

    public class TopicsFilterHelper
    {
        public static IQueryable<TopicEntity> CheckFilters(int sportId, TopicsFilterDTO topicsFilterDTO, IQueryable<TopicEntity> queryable, ActiveForumDbContext context)
        {
            if (topicsFilterDTO.Categories.Count > 0)
            {
                if (topicsFilterDTO.Title.Length > 2 && !string.IsNullOrEmpty(topicsFilterDTO.Title) && topicsFilterDTO.Status != 0)
                {
                    queryable = context.TopicsCategories.Include(x => x.Topic).ThenInclude(x => x.Posts)
                        .Where(x => topicsFilterDTO.Categories.Contains(x.CategoryId)).Select(x => x.Topic)
                        .Where(x => x.SportId == sportId).Where(x => x.Title.Contains(topicsFilterDTO.Title)).Where(x => x.Status == topicsFilterDTO.Status);
                }
                else if (topicsFilterDTO.Title.Length > 2 && !string.IsNullOrEmpty(topicsFilterDTO.Title))
                {
                    queryable = context.TopicsCategories.Include(x => x.Topic).ThenInclude(x => x.Posts)
                        .Where(x => topicsFilterDTO.Categories.Contains(x.CategoryId)).Select(x => x.Topic).Where(x => x.SportId == sportId).Where(x => x.Title.Contains(topicsFilterDTO.Title));
                }
                else if (topicsFilterDTO.Status != 0)
                {
                    queryable = context.TopicsCategories.Include(x => x.Topic).ThenInclude(x => x.Posts)
                        .Where(x => topicsFilterDTO.Categories.Contains(x.CategoryId)).Select(x => x.Topic).Where(x => x.SportId == sportId).Where(x => x.Status == topicsFilterDTO.Status);
                }
                else
                {
                    queryable = context.TopicsCategories.Include(x => x.Topic).ThenInclude(x => x.Posts).Where(x => topicsFilterDTO.Categories.Contains(x.CategoryId)).Select(x => x.Topic).Distinct().Where(x => x.SportId == sportId);
                }
            }
            else if (topicsFilterDTO.Title.Length > 2 && !string.IsNullOrEmpty(topicsFilterDTO.Title) && topicsFilterDTO.Status != 0)
            {
                queryable = context.Topics.Include(x => x.Posts).Where(x => x.Title.Contains(topicsFilterDTO.Title)).Where(x => x.Status == topicsFilterDTO.Status);
            }
            else if (topicsFilterDTO.Title.Length > 2 && !string.IsNullOrEmpty(topicsFilterDTO.Title))
            {
                queryable = context.Topics.Include(x => x.Posts).Where(x => x.Title.Contains(topicsFilterDTO.Title));
            }
            else if (topicsFilterDTO.Status != 0)
            {
                queryable = context.Topics.Include(x => x.Posts).Where(x => x.Status == topicsFilterDTO.Status);
            }

            return queryable;
        }
    }
}
