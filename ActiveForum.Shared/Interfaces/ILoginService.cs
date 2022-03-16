namespace ActiveForum.Shared.Interfaces
{
using System.Threading.Tasks;
using ActiveForum.Shared.DTOs;

public interface ILoginService
{
    Task Login(UserToken userToken);
    
    Task Logout();
    
    Task TryRenewToken();

    Task<string> GetToken();
}
}
