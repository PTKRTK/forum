namespace ActiveForum.Shared.Interfaces.ForumRepositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ActiveForum.Shared.Models.ForumModels;

    public interface ICategoriesRepository
    {
        Task<int> CreateCategory(CategoryOfTopic categoryOfTopic);
        
        Task DeleteCategory(int id);
        
        Task<List<CategoryOfTopic>> GetCategories();
    }
}
