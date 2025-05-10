using App.DAL.Interfaces;
using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface IUserSavedTracksService : IBaseService<App.BLL.DTO.UserSavedTracks>
{
    Task<IEnumerable<App.BLL.DTO.Track>> GetFullSavedTracksAsync(string userId);
    
    Task<bool> ExistsByTrackAndUserAsync(Guid trackId, string userId);
    
    Task RemoveByTrackIdAsync(Guid trackId, string userId);
}