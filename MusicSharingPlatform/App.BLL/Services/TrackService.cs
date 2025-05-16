using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Services;

public class TrackService : BaseService<App.BLL.DTO.Track, App.DAL.DTO.Track, App.DAL.Interfaces.ITrackRepository>, ITrackService
{
    public TrackService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.Track, Track, Guid> bllMapper) : base(serviceUOW, serviceUOW.TrackRepository, bllMapper)
    {
    }
    
    public async Task UpdateTrackWithRelationsAsync(App.BLL.DTO.Track track)
    {
        var dalTrack = BLLMapper.Map(track)!;
        await ServiceRepository.UpdateTrackWithRelationsAsync(dalTrack);
    }

    public async Task<DTO.Track?> GetRandomTrackFilteredAsync(IEnumerable<Guid> tagIds, IEnumerable<Guid> moodIds)
    {
        var track = await ServiceRepository.GetRandomTrackFilteredAsync(tagIds, moodIds);
        return BLLMapper.Map(track);
    }

    
    public async Task<bool> IncrementPlayCountAsync(Guid trackId)
    {
        var track = await ServiceRepository.FindTrackedDomainAsync(trackId);
        if (track == null) return false;

        track.TimesPlayed += 1;
        return true;
    }
    
    public async Task<List<DTO.Track>> SearchTracksAsync(string query)
    {
        var res = await ServiceRepository.SearchTracksAsync(query);
        return res.Select(BLLMapper.Map).ToList()!;
    }
    
}