using App.DAL.Interfaces;
using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface ITrackInPlaylistService : IBaseService<App.BLL.DTO.TrackInPlaylist>, ITrackInPlaylistRepositoryCustom
{
    
}