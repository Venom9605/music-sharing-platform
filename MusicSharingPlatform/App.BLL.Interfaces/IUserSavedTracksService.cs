using App.DAL.Interfaces;
using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface IUserSavedTracksService : IBaseService<App.BLL.DTO.UserSavedTracks>, IUserSavedTracksRepositoryCustom
{
    
}