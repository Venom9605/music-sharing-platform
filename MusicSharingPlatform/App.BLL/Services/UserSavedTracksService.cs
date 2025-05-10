using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;

namespace App.BLL.Services;

public class UserSavedTracksService : BaseService<App.BLL.DTO.UserSavedTracks, App.DAL.DTO.UserSavedTracks, App.DAL.Interfaces.IUserSavedTracksRepository>, IUserSavedTracksService
{
    private readonly IBLLMapper<App.BLL.DTO.Track, App.DAL.DTO.Track> _trackMapper;
    public UserSavedTracksService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.UserSavedTracks, UserSavedTracks, Guid> bllMapper,
        IBLLMapper<App.BLL.DTO.Track, App.DAL.DTO.Track> trackMapper
        ) : base(serviceUOW, serviceUOW.UserSavedTracksRepository, bllMapper)
    {
        _trackMapper = trackMapper;
    }

    public async Task<IEnumerable<App.BLL.DTO.Track>> GetFullSavedTracksAsync(string userId)
    {
        var dalTracks = await ServiceRepository.GetFullSavedTracksAsync(userId);
        return dalTracks.Select(t => _trackMapper.Map(t)!);
    }

    public async Task<bool> ExistsByTrackAndUserAsync(Guid trackId, string userId)
    {
        return await ServiceRepository.ExistsByTrackAndUserAsync(trackId, userId);
    }

    public Task RemoveByTrackIdAsync(Guid trackId, string userId)
    {
        return ServiceRepository.RemoveByTrackIdAsync(trackId, userId);
    }
}