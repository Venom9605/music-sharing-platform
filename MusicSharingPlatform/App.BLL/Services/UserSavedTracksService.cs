using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;

namespace App.BLL.Services;

public class UserSavedTracksService : BaseService<App.BLL.DTO.UserSavedTracks, App.DAL.DTO.UserSavedTracks, App.DAL.Interfaces.IUserSavedTracksRepository>, IUserSavedTracksService
{
    public UserSavedTracksService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.UserSavedTracks, UserSavedTracks, Guid> bllMapper) : base(serviceUOW, serviceUOW.UserSavedTracksRepository, bllMapper)
    {
    }
    
}