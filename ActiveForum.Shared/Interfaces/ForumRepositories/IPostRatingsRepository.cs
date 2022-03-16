namespace ActiveForum.Shared.Interfaces.ForumRepositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ActiveForum.Shared.Models.ForumModels;

    public interface IPostRatingsRepository
    {
        Task AddRating(PostRating postRating);
       
        Task DeleteRating(int id);
        
        Task<double> GetAvgRating(int postId);
        
        Task<PostRating> GetUserRating(int postId);
    }
}