using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;

namespace App.BLL.Services;

public class MoodsInPlaylistService : BaseService<App.BLL.DTO.MoodsInPlaylist, App.DAL.DTO.MoodsInPlaylist, App.DAL.Interfaces.IMoodsInPlaylistRepository>, IMoodsInPlaylistService
{
    public MoodsInPlaylistService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.MoodsInPlaylist, MoodsInPlaylist, Guid> bllMapper) : base(serviceUOW, serviceUOW.MoodsInPlaylistRepository, bllMapper)
    {
    }
    
}