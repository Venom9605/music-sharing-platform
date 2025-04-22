using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class TrackInPlaylistMapper : IMapper<DTO.TrackInPlaylist, Domain.TrackInPlaylist>
{
    public TrackInPlaylist? Map(Domain.TrackInPlaylist? entity)
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

    public Domain.TrackInPlaylist? Map(TrackInPlaylist? entity)
    {
        if (entity == null) return null;
        var res = new Domain.TrackInPlaylist()
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