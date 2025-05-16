using App.DAL.Interfaces;
using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface ITrackService : IBaseService<App.BLL.DTO.Track>
{
    Task UpdateTrackWithRelationsAsync(App.BLL.DTO.Track track);
    
    Task<DTO.Track?> GetRandomTrackFilteredAsync(IEnumerable<Guid> tagIds, IEnumerable<Guid> moodIds);
    
    Task<bool> IncrementPlayCountAsync(Guid trackId);
    
    Task<List<App.BLL.DTO.Track>> SearchTracksAsync(string query);
}