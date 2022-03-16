namespace ActiveForum.Server.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using ActiveForum.Database.DbContexts;
    using ActiveForum.Database.Entities.ForumEntities;
    using ActiveForum.Server.Helpers;
    using ActiveForum.Shared.DTOs;
    using ActiveForum.Shared.Interfaces;
    using ActiveForum.Shared.Interfaces.ForumRepositories;
    using ActiveForum.Shared.Models.ForumModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/sportsMenagment")]

    public class SportsMenagmentController : ControllerBase
    {
        private readonly ActiveForumDbContext context;

        public SportsMenagmentController(ActiveForumDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Sport sport)
        {
            await CreateSport(sport);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Sport sport)
        {
            await EditSport(sport);
            return NoContent();
        }

        [HttpDelete("{sportId}")]
        public async Task<ActionResult> Delete(int sportId)
        {
            var postRatings = context.PostsRatings.Where(x => x.Post.Topic.SportId == sportId).ToList();
            postRatings.ForEach(x => context.Remove(x));
            var topicRatings = context.TopicsRatings.Where(x => x.Topic.SportId == sportId).ToList();
            topicRatings.ForEach(x => context.Remove(x));
            var topicCategories = context.TopicsCategories.Where(x => x.Topic.SportId == sportId).ToList();
            topicCategories.ForEach(x => context.Remove(x));
            var posts = context.Posts.Where(x => x.Topic.SportId == sportId).ToList();
            posts.ForEach(x => context.Remove(x));
            var topics = context.Topics.Where(x => x.SportId == sportId).ToList();
            topics.ForEach(x => context.Remove(x));
            var sport = context.Sports.Where(x => x.Id == sportId).FirstOrDefault();
            context.Remove(sport);
            context.SaveChanges();
            await Task.CompletedTask;
            
            return NoContent();
        }

        public async Task CreateSport(Sport sport)
        {
            SportEntity entitySport = new ()
            {
                Name = sport.Name,
                ImageUrl = sport.ImageUrl,
                Description = sport.Description
            };

            context.Add(entitySport);
            await context.SaveChangesAsync();
        }

        public async Task EditSport(Sport sport)
        {
            SportEntity entitySport = new ()
            {
                Name = sport.Name,
                ImageUrl = sport.ImageUrl,
                Description = sport.Description
            };

            context.Attach(entitySport).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteSport(int id)
        {
            var sport = await context.Sports.FirstOrDefaultAsync(x => x.Id == id);
            if (sport != null)
            {
                context.Remove(sport);
                await context.SaveChangesAsync();
            }
        } 
    }
}
