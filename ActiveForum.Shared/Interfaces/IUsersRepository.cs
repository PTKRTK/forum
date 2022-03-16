namespace ActiveForum.Shared.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ActiveForum.Shared.DTOs;
    
    public interface IUsersRepository
    {
        Task AssignRole(EditRoleDTO editRole);
        
        Task<List<RoleDTO>> GetRoles();
        
        Task<PaginatedResponse<List<UserDTO>>> GetUsers(PaginationDTO paginationDTO);
        
        Task RemoveRole(EditRoleDTO editRole);
    }
}
