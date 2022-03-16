namespace ActiveForum.Client.Repositories.ForumRepositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ActiveForum.Client.HttpHelpers;
    using ActiveForum.Shared.Interfaces.ForumRepositories;
    using ActiveForum.Shared.Models.ForumModels;
    
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly IHttpService httpService;
        private readonly string url = "api/categories";

        public CategoriesRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<int> CreateCategory(CategoryOfTopic categoryOfTopic)
        {
            var response = await httpService.Post<CategoryOfTopic, int>(url, categoryOfTopic);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task DeleteCategory(int id)
        {
            var response = await httpService.Delete($"{url}/{id}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task<List<CategoryOfTopic>> GetCategories()
        {
            string newUrl = url;

            var response = await httpService.Get<List<CategoryOfTopic>>(newUrl);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }
    }
}
