using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;

namespace App.BLL.Services;

public class UserLinkService : BaseService<App.BLL.DTO.UserLink, App.DAL.DTO.UserLink, App.DAL.Interfaces.IUserLinkRepository>, IUserLinkService
{
    public UserLinkService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.UserLink, UserLink, Guid> bllMapper) : base(serviceUOW, serviceUOW.UserLinkRepository, bllMapper)
    {
    }
    
}