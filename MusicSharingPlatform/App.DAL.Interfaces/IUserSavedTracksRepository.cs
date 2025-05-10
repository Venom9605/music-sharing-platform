using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface IUserSavedTracksRepository : IBaseRepository<DTO.UserSavedTracks>, IUserSavedTracksRepositoryCustom
{
    
}

public interface IUserSavedTracksRepositoryCustom
{
    Task<IEnumerable<Track>> GetFullSavedTracksAsync(string userId);
    
    Task<bool> ExistsByTrackAndUserAsync(Guid trackId, string userId);
    
    Task RemoveByTrackIdAsync(Guid trackId, string userId);
}