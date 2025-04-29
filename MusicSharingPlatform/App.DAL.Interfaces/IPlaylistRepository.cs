using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface IPlaylistRepository : IBaseRepository<DTO.Playlist>, IPlaylistRepositoryCustom
{
    
}

public interface IPlaylistRepositoryCustom
{
}