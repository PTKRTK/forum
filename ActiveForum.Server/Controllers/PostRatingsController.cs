namespace ActiveForum.Server.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ActiveForum.Database.DbContexts;
    using ActiveForum.Database.Entities.ForumEntities;
    using ActiveForum.Shared.DTOs;
    using ActiveForum.Shared.Interfaces.ForumRepositories;
    using ActiveForum.Shared.Models.ForumModels;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/postRatings")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostRatingsController : ControllerBase
    {
        private readonly ActiveForumDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public PostRatingsController(
            ActiveForumDbContext context,
            UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult> AddRating(PostRating postRating)
        {
            var user = await userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var userId = user.Id;

            var existingRating = context.PostsRatings.Where(x => x.PostId == postRating.PostId && x.UserId == userId).FirstOrDefault();

            if (existingRating == null)
            {
                if (postRating.Rating != 0)
                {
                    PostRatingEntity postRatingE = new ()
                    {
                        PostId = postRating.PostId,
                        UserId = userId,
                        Rating = postRating.Rating,
                    };
                    context.Add(postRatingE);
                    await context.SaveChangesAsync();
                }      
            }
            else
            {
                if (postRating.Rating == 0)
                {
                    context.Remove(existingRating);
                    await context.SaveChangesAsync();
                }
                else
                {
                    existingRating.Rating = postRating.Rating;
                    await context.SaveChangesAsync();
                }
            }

            return NoContent();
        }

        [HttpGet("userRating/{postId}")]
        public async Task<ActionResult<PostRating>> GetUserRating(int postId)
        {
            var user = await userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var userId = user.Id;

            var existingRating = await context.PostsRatings
                .FirstOrDefaultAsync(x => x.PostId == postId &&
                x.UserId == userId);

            if (existingRating == null)
            {
                return null;
            }
            else
            {
                var rating = new PostRating()
                {
                    Id = existingRating.Id,
                    Rating = existingRating.Rating,
                    PostId = postId
                };

                return rating;
            }
        }
        
        [HttpGet("avgRating/{postId}")]
        public ActionResult<double> GetAvgRating(int postId)
        {
            var avgRating = context.PostsRatings.Where(x => x.PostId == postId).Average(x => x.Rating);

            return avgRating;
        }
    }
}