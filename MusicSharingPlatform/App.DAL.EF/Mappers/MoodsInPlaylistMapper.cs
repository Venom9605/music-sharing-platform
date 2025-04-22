using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class MoodsInPlaylistMapper : IMapper<DTO.MoodsInPlaylist, Domain.MoodsInPlaylist>
{
    public MoodsInPlaylist? Map(Domain.MoodsInPlaylist? entity)
    {
        if (entity == null) return null;
        var res = new MoodsInPlaylist()
        {
            Id = entity.Id,
            MoodId = entity.MoodId,
            Mood = null,
            PlaylistId = entity.PlaylistId,
            Playlist = null,
        };
        return res;
    }

    public Domain.MoodsInPlaylist? Map(MoodsInPlaylist? entity)
    {
        if (entity == null) return null;
        var res = new Domain.MoodsInPlaylist()
        {
            Id = entity.Id,
            MoodId = entity.MoodId,
            Mood = null,
            PlaylistId = entity.PlaylistId,
            Playlist = null,
        };
        return res;
    }
}