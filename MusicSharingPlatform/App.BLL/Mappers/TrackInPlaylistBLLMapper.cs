using App.DAL.DTO;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Mappers;

public class TrackInPlaylistBLLMapper : IBLLMapper<App.BLL.DTO.TrackInPlaylist, App.DAL.DTO.TrackInPlaylist>
{
    public TrackInPlaylist? Map(DTO.TrackInPlaylist? entity)
    {
        if (entity == null) return null;
        var res = new TrackInPlaylist()
        {
            Id = entity.Id,
            TrackId = entity.TrackId,
            Track = null,
            PlaylistId = entity.PlaylistId,
            Playlist = null,

        };
        return res;
    }

    public DTO.TrackInPlaylist? Map(TrackInPlaylist? entity)
    {
        if (entity == null) return null;
        var res = new DTO.TrackInPlaylist()
        {
            Id = entity.Id,
            TrackId = entity.TrackId,
            Track = null,
            PlaylistId = entity.PlaylistId,
            Playlist = null,

        };
        return res;
    }
}