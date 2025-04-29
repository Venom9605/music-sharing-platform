using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface IUserSavedTracksRepository : IBaseRepository<DTO.UserSavedTracks>, IUserSavedTracksRepositoryCustom
{
    
}

public interface IUserSavedTracksRepositoryCustom
{
}