using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface ITrackInPlaylistRepository : IBaseRepository<DTO.TrackInPlaylist>, ITrackInPlaylistRepositoryCustom
{
    
}

public interface ITrackInPlaylistRepositoryCustom
{
}