namespace ActiveForum.Shared.Interfaces.ForumRepositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ActiveForum.Shared.DTOs;
    using ActiveForum.Shared.Models.ForumModels;

    public interface ISportsBoardRepository
    {
        Task<PaginatedResponse<List<Sport>>> GetSports(PaginationDTO paginationDTO);
    
        Task<PaginatedResponse<List<Sport>>> GetSports(PaginationDTO paginationDTO, string name);
    }
}
