namespace ActiveForum.Server.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ActiveForum.Database.DbContexts;
    using ActiveForum.Database.Entities.ForumEntities;
    using ActiveForum.Server.Helpers;
    using ActiveForum.Shared.DTOs;
    using ActiveForum.Shared.Interfaces;
    using ActiveForum.Shared.Models.ForumModels;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/topicsMenagment")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TopicsMenagmentController : ControllerBase
    {
        private readonly ActiveForumDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public TopicsMenagmentController(ActiveForumDbContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Topic topic)
        {
            var user = await userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var userId = user.Id;

            List<TopicCategoryEntity> categories = new ();

            foreach (var tc in topic.Topic_Categories)
            {
                categories.Add(new TopicCategoryEntity()
                {
                    CategoryId = tc.TopicCategoryId,
                    TopicId = tc.TopicId
                });
            }
                
            TopicEntity entityTopic = new ()
            {
                Title = topic.Title,
                TopicContent = topic.TopicContent,
                SportId = topic.SportId,
                Status = topic.Status,
                CreationDate = DateTime.Now,
                UserId = userId,
                TopicsCategories = categories
            };

            context.Add(entityTopic);
            await context.SaveChangesAsync();

            int id = entityTopic.Id;

            return id;
        }

        [HttpPut]
        public async Task<ActionResult> Put(Topic topic)
        {
            TopicEntity entityTopic = new ()
            {
                Title = topic.Title,
                TopicContent = topic.TopicContent,
                SportId = topic.SportId,
                Status = topic.Status,
            };

            context.Attach(entityTopic).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{topicId}")]
        public async Task<ActionResult> Delete(int topicId)
        {
            var topic = await context.Topics.FirstOrDefaultAsync(x => x.Id == topicId);

            var postRatings = context.PostsRatings.Where(x => x.Post.TopicId == topicId).ToList();
            postRatings.ForEach(x => context.Remove(x));
            
            var topicRatings = context.TopicsRatings.Where(x => x.TopicId == topicId).ToList();
            topicRatings.ForEach(x => context.Remove(x));

            var topicCategories = context.TopicsCategories.Where(x => x.TopicId == topicId).ToList();
            topicCategories.ForEach(x => context.Remove(x));

            var posts = context.Posts.Where(x => x.TopicId == topicId).ToList();
            posts.ForEach(x => context.Remove(x));
            
            context.Remove(topic);
            await context.SaveChangesAsync();

            return NoContent();
        } 
    }
}
