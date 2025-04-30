using App.DAL.Interfaces;
using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface IPlaylistService : IBaseService<App.BLL.DTO.Playlist>, IPlaylistRepositoryCustom
{
    
}