using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;

namespace App.BLL.Services;

public class PlaylistService : BaseService<App.BLL.DTO.Playlist, App.DAL.DTO.Playlist, App.DAL.Interfaces.IPlaylistRepository>, IPlaylistService
{
    public PlaylistService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.Playlist, Playlist, Guid> bllMapper) : base(serviceUOW, serviceUOW.PlaylistRepository, bllMapper)
    {
    }
    
}