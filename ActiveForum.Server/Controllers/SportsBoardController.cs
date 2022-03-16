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
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
   
    [ApiController]
    [Route("api/sportsBoard")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SportsBoardController : ControllerBase
    {
        private readonly ActiveForumDbContext context;

        public SportsBoardController(ActiveForumDbContext context)
        {
            this.context = context;
        }

        [HttpGet]  
        public async Task<ActionResult<PaginatedResponse<List<Sport>>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var response = await GetSports(paginationDTO);
            return response;
        }

        [HttpGet("search/{name}")]
        public async Task<ActionResult<PaginatedResponse<List<Sport>>>> Get([FromQuery] PaginationDTO paginationDTO, string name)
        {
            var response = await GetSports(paginationDTO, name);
            return response;
        }

        public async Task<PaginatedResponse<List<Sport>>> GetSports(PaginationDTO paginationDTO)
        {
            var queryable = context.Sports.AsQueryable();
            PaginatedResponse<List<Sport>> result = await GetPaginatedResult(paginationDTO, queryable);
            return result;
        }

        public async Task<PaginatedResponse<List<Sport>>> GetSports(PaginationDTO paginationDTO, string name)
        {
            var queryable = context.Sports.Where(x => x.Name.Contains(name)).AsQueryable();
            PaginatedResponse<List<Sport>> result = await GetPaginatedResult(paginationDTO, queryable);
            return result;
        }

        private static async Task<PaginatedResponse<List<Sport>>> GetPaginatedResult(PaginationDTO paginationDTO, IQueryable<SportEntity> queryable)
        {
            var pagesAmount = await PaginationHelpers.CountPagesAmount(queryable, paginationDTO.PageObjectsNumber);
            var entitiesList = await queryable.Paginate(paginationDTO).ToListAsync();

            var response = entitiesList.Select(x => new Sport()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
            }).ToList();

            PaginatedResponse<List<Sport>> result = new ()
            {
                PagesAmount = (int)pagesAmount,
                Response = response
            };
            return result;
        }
   }
}
