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

    public void CustomMethodTest()
    {
        ServiceRepository.CustomMethodTest();
    }
    
    public async Task UpdateTrackWithRelationsAsync(App.BLL.DTO.Track track)
    {
        var dalTrack = BLLMapper.Map(track)!;
        await ServiceRepository.UpdateTrackWithRelationsAsync(dalTrack);
    }
}