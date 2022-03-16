namespace ActiveForum.Server.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ActiveForum.Database.DbContexts;
    using ActiveForum.Shared.Interfaces.ForumRepositories;
    using ActiveForum.Shared.Models.ForumModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/sportsCategories")]
    public class TopicsCategoriesController : Controller
{
        private readonly ActiveForumDbContext context;

        public TopicsCategoriesController(ActiveForumDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryOfTopic>>> GetAllCategories()
        {
            var queryable = context.Categories.AsQueryable();
            var entityCategories = await queryable.ToListAsync();

            var categories = entityCategories.Select(x => new CategoryOfTopic()
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name
            }).ToList();

            return categories;
        }

        [HttpGet("{topicId}")]
        public async Task<ActionResult<List<CategoryOfTopic>>> GetTopicCategories(int topicId)
        {
            var queryable = context.TopicsCategories.Where(x => x.TopicId == topicId).Select(x => x.Category).AsQueryable();
            var entityCategories = await queryable.ToListAsync();
          
            var categories = entityCategories.Select(x => new CategoryOfTopic() 
            { 
                    Id = x.Id,
                    Description = x.Description, 
                    Name = x.Name 
            }).ToList();

            return categories;
        }
    }
}