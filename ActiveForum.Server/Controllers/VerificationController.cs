namespace ActiveForum.Server.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using ActiveForum.Database.DbContexts;
    using ActiveForum.Database.Entities.ForumEntities;
    using ActiveForum.Shared.DTOs;
    using ActiveForum.Shared.Enums;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/verification")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VerificationController : ControllerBase
    {
        private readonly ActiveForumDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public VerificationController(
                 ActiveForumDbContext context,
                 UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult> Verify(VerifiedDTO verifiedDTO)
        {
            var entityPosts = context.Posts.Where(x => x.TopicId == verifiedDTO.TopicID).ToList();
            var entityRatings = new List<PostRatingEntity>();
            var postsForDeleting = new List<PostEntity>();

            foreach (var v in entityPosts)
            {
                if (verifiedDTO.PostIds.Contains(v.Id) == false)
                {
                    postsForDeleting.Add(v);
                    var postRatings = context.PostsRatings.Where(x => x.PostId == v.Id).ToList();
                    entityRatings.AddRange(postRatings);
                }
            }

            foreach (var rating in entityRatings)
            {
               context.Remove(rating);
            }
            
            foreach (var post in postsForDeleting)
            {
                context.Remove(post);
            }

            var topic = context.Topics.Where(x => x.Id == verifiedDTO.TopicID).FirstOrDefault();

            topic.Status = (int)StatusEnum.Zweryfikowany;

            context.Attach(topic).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}