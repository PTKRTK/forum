namespace ActiveForum.Server.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ActiveForum.Database.DbContexts;
    using ActiveForum.Database.Entities.ForumEntities;
    using ActiveForum.Server.Helpers;
    using ActiveForum.Shared.DTOs;
    using ActiveForum.Shared.Interfaces.ForumRepositories;
    using ActiveForum.Shared.Models.ForumModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/topicsBoard")]
    public class TopicsBasicInfoController : ControllerBase 
    {
        private readonly ActiveForumDbContext context;

        public TopicsBasicInfoController(ActiveForumDbContext context)
        {
            this.context = context;
        }

        [HttpGet("topic/{topicId}")]
        public ActionResult<TopicBasicInfoDTO> Get(int topicId)
        {
            double avgRating;
            var topicEntity = context.Topics.Where(x => x.Id == topicId).Include(x => x.Posts).FirstOrDefault();
            var userName = context.Users.Where(x => x.Id == topicEntity.UserId).Select(x => x.UserName).FirstOrDefault();
            List<CategoryEntity> categoriesEntity = context.TopicsCategories.Where(x => x.TopicId == topicId).Select(x => x.Category).ToList();

            var toppicRatings = context.TopicsRatings.Where(x => x.TopicId == topicId).ToList();

            if (toppicRatings.Count() >= 1)
            {
                 avgRating = toppicRatings.Average(x => x.Rating);
            }
            else
            {
                avgRating = 0;
            }
           
            TopicBasicInfoDTO topicBasicInfo = new ()
            {
                AvgRating = avgRating, 
                PostsNumber = topicEntity.Posts.Count,
                Categories = categoriesEntity.Select(x => new CategoryOfTopic()
                {
                    Id = x.Id,
                    Description = x.Description,
                    Name = x.Name
                }).ToList(),
                Topic = new Topic()
                {
                    Id = topicEntity.Id,
                    Title = topicEntity.Title,
                    TopicContent = topicEntity.TopicContent,
                    CreationDate = topicEntity.CreationDate,
                    Status = topicEntity.Status,
                    SportId = topicEntity.SportId,
                    UserName = userName                 
                }            
            };
            return topicBasicInfo;
        }

        [HttpGet("{sportId}")]
        public async Task<ActionResult<PaginatedResponse<List<TopicBasicInfoDTO>>>> Get([FromQuery] PaginationDTO paginationDTO, int sportId)
        {
            var queryable = context.Topics.Where(x => x.SportId == sportId).Include(x => x.Posts).AsQueryable();

            var pagesAmount = await PaginationHelpers.CountPagesAmount(queryable, paginationDTO.PageObjectsNumber);
            var entitiesList = await queryable.Paginate(paginationDTO).ToListAsync();
            PaginatedResponse<List<TopicBasicInfoDTO>> result = Transform(pagesAmount, entitiesList);
            return result;
        }

        [HttpPost("{sportId}/filter")]
        public async Task<ActionResult<PaginatedResponse<List<TopicBasicInfoDTO>>>> Gets([FromQuery] PaginationDTO paginationDTO, int sportId, TopicsFilterDTO topicsFilterDTO)
        {
            IQueryable<TopicEntity> queryable = context.Topics.Where(x => x.SportId == sportId);
            queryable = TopicsFilterHelper.CheckFilters(sportId, topicsFilterDTO, queryable, context);

            var pagesAmount = await PaginationHelpers.CountPagesAmount(queryable, paginationDTO.PageObjectsNumber);
            var entitiesList = await queryable.Paginate(paginationDTO).ToListAsync();

            PaginatedResponse<List<TopicBasicInfoDTO>> result = Transform(pagesAmount, entitiesList);

            return result;
        }

        private PaginatedResponse<List<TopicBasicInfoDTO>> Transform(double pagesAmount, List<TopicEntity> entitiesList)
        {
            List<TopicBasicInfoDTO> response = new ();
            foreach (var x in entitiesList)
            {
                var userName = context.Users.Where(z => z.Id == x.UserId).Select(z => z.UserName).FirstOrDefault();
                var categoriesEntity = context.TopicsCategories.Where(z => z.TopicId == x.Id).Select(z => z.Category).ToList();
                response = entitiesList.Select(x => new TopicBasicInfoDTO()
                {
                    Topic = new Topic()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        TopicContent = x.TopicContent,
                        Status = x.Status,
                        CreationDate = x.CreationDate,
                        SportId = x.SportId,
                        UserName = userName
                    },
                    PostsNumber = x.Posts.Count,
                    Categories = categoriesEntity.Select(x => new CategoryOfTopic()
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Name = x.Name
                    }).ToList(),
                }).ToList();
            }

            PaginatedResponse<List<TopicBasicInfoDTO>> result = new ()
            {
                PagesAmount = (int)pagesAmount,
                Response = response
            };
            return result;
        }
    }
}
