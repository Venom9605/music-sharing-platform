using App.DAL.Interfaces;
using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface IMoodsInPlaylistService : IBaseService<App.BLL.DTO.MoodsInPlaylist>, IMoodsInPlaylistRepositoryCustom
{
    
}