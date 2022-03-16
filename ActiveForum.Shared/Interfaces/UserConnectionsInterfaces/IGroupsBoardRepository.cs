namespace ActiveForum.Shared.Interfaces.UserConnectionsInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ActiveForum.Shared.DTOs;
    using ActiveForum.Shared.Models.GroupModels;

    public interface IGroupsBoardRepository
    {
        Task<PaginatedResponse<List<GroupDTO>>> GetGroups(PaginationDTO paginationDTO);

        Task<PaginatedResponse<List<GroupDTO>>> GetGroups(PaginationDTO paginationDTO, string searchText);

        Task<PaginatedResponse<List<GroupDTO>>> GetJoinedGroups(PaginationDTO paginationDTO);

        Task<PaginatedResponse<List<GroupDTO>>> GetJoinedGroups(PaginationDTO paginationDTO, string searchText);
    }
}
