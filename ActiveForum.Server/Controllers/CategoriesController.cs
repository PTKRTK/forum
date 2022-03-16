namespace ActiveForum.Server.Controllers
{
    using System.Collections.Generic;
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
    [Route("api/categories")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase
    {
        private readonly ActiveForumDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public CategoriesController(ActiveForumDbContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(CategoryOfTopic categoryOfTopic)
        {
            CategoryEntity categoryOfTopicEntity = new () { Description = categoryOfTopic.Description, Id = categoryOfTopic.Id, Name = categoryOfTopic.Name };
            context.Add(categoryOfTopicEntity);
            await context.SaveChangesAsync();

            return categoryOfTopicEntity.Id;
        }

        [HttpDelete("{categoryId}")]
        public async Task DeleteCategory(int categoryId)
        {
            var categoriesTopics = context.TopicsCategories.Where(x => x.CategoryId == categoryId).ToList();

            categoriesTopics.ForEach(x => context.Remove(x));

            var category = context.Categories.Where(x => x.Id == categoryId).FirstOrDefault();

            context.Remove(category);

            await context.SaveChangesAsync();
        }

        public async Task<ActionResult<List<CategoryOfTopic>>> GetCategories()
        {
            var categoriesEntity = await context.Categories.ToListAsync();
            List<CategoryOfTopic> categories = new ();

            foreach (var category in categoriesEntity)
            {
                categories.Add(
                       new CategoryOfTopic()
                       {
                           Description = category.Description,
                           Id = category.Id,
                           Name = category.Name
                       });
            }

            return categories;
        }
    }
}
