using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;

namespace App.BLL.Services;

public class TrackInPlaylistService : BaseService<App.BLL.DTO.TrackInPlaylist, App.DAL.DTO.TrackInPlaylist, App.DAL.Interfaces.ITrackInPlaylistRepository>, ITrackInPlaylistService
{
    public TrackInPlaylistService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.TrackInPlaylist, TrackInPlaylist, Guid> bllMapper) : base(serviceUOW, serviceUOW.TrackInPlaylistRepository, bllMapper)
    {
    }
    
}