namespace ActiveForum.Shared.Interfaces
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveForum.Shared.DTOs;
    
public interface IAccountsRepository
{
    Task<UserToken> Login(UserInfo userInfo);
    
    Task<UserToken> Register(UserInfo userInfo);
    
    Task<UserToken> RenewToken();
}
}