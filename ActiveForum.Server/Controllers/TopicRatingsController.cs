namespace ActiveForum.Server.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using ActiveForum.Database.DbContexts;
    using ActiveForum.Database.Entities.ForumEntities;
    using ActiveForum.Shared.Models.ForumModels;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/topicRatings")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TopicRatingsController : ControllerBase
    {
        private readonly ActiveForumDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public TopicRatingsController(
            ActiveForumDbContext context,
            UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult> AddRating(TopicRating topicRating)
        {
            var user = await userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var userId = user.Id;

            var existingRating = await context.TopicsRatings
                .FirstOrDefaultAsync(x => x.TopicId == topicRating.TopicId &&
                x.UserId == userId);

            if (existingRating == null)
            {
                if (topicRating.Rating != 0)
                {
                    TopicRatingEntity topicRatingE = new ()
                    {
                        TopicId = topicRating.TopicId,
                        UserId = userId,
                        Rating = topicRating.Rating,
                    };
                    context.Add(topicRatingE);
                    await context.SaveChangesAsync();
                }
            }
            else
            {
                if (topicRating.Rating == 0)
                {
                    context.Remove(existingRating);
                    await context.SaveChangesAsync();
                }
                else
                {
                existingRating.Rating = topicRating.Rating;
                await context.SaveChangesAsync();
                }
            }

            return NoContent();
        }

        [HttpGet("userRating/{topicId}")]
        public async Task<ActionResult<TopicRating>> GetUserRating(int topicId)
        {
            var x = topicId;
            var user = await userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var userId = user.Id;

            var existingRating = await context.TopicsRatings
                .FirstOrDefaultAsync(x => x.TopicId == topicId &&
                x.UserId == userId);

            if (existingRating == null)
            {
                  return null;
            }
            else
            {
                var rating = new TopicRating()
                {
                    Id = existingRating.Id,
                    Rating = existingRating.Rating,
                    TopicId = topicId
                };

                return rating;
            }
        }

        [HttpGet("/avgRating/{postId}")]
        public async Task<ActionResult<double>> GetAvgRating(int topicId)
        {
            double ratingsAmount = await context.TopicsRatings.Where(x => x.TopicId == topicId).CountAsync();
            var allRatings = context.TopicsRatings.Where(x => x.TopicId == topicId).Select(x => x.Rating).ToList();
            double sumRatings = allRatings.Sum();
            double avgRatings = ratingsAmount / sumRatings;

            return avgRatings;
        }
    }
}