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
    using ActiveForum.Shared.Enums;
    using ActiveForum.Shared.Models.ForumModels;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/posts")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController : ControllerBase
    {
        private readonly ActiveForumDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public PostsController(ActiveForumDbContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult> AddPost(Post post)
        {
            var user = await userManager.FindByEmailAsync(HttpContext.User.Identity.Name);
            var userId = user.Id;

            PostEntity entityPost = new ()
            {
                CreationDate = DateTime.Now,
                PostContent = post.PostContent,
                Status = (int)StatusEnum.Sugestia,
                TopicId = post.TopicId,
                UserId = userId         
            };

            context.Add(entityPost);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{topicId}")]
        public async Task<ActionResult<PaginatedResponse<List<Post>>>> Get([FromQuery] PaginationDTO paginationDTO, int topicId)
        {
            List<Post> response = new ();

            IQueryable<PostEntity> queryable = context.Posts.Where(x => x.TopicId == topicId).AsQueryable();

            var pagesAmount = await PaginationHelpers.CountPagesAmount(queryable, paginationDTO.PageObjectsNumber);
            var entitiesList = await queryable.Paginate(paginationDTO).ToListAsync();

            foreach (var x in entitiesList)
            {
                response.Add(new Post
                {
                    Id = x.Id,
                    CreationDate = x.CreationDate,
                    PostContent = x.PostContent,
                    Status = x.Status,
                    TopicId = x.TopicId,
                    UserName = context.Users.Where(z => z.Id == x.UserId).Select(z => z.UserName).FirstOrDefault().ToString()
                });
            }

            PaginatedResponse<List<Post>> result = new ()
            {
                PagesAmount = (int)pagesAmount,
                Response = response
            };

            return result;
        }

        [HttpDelete("{postId}")]
        public async Task<ActionResult> Delete(int postId)
        {
            var post = await context.Posts.FirstOrDefaultAsync(x => x.Id == postId);

            var postRatings = context.PostsRatings.Where(x => x.PostId == postId).ToList();
            postRatings.ForEach(x => context.Remove(x));

            context.Remove(post);
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}