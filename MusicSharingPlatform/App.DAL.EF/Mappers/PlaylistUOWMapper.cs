using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class PlaylistUOWMapper : IUOWMapper<DTO.Playlist, Domain.Playlist>
{
    public Playlist? Map(Domain.Playlist? entity)
    {
        if (entity == null) return null;
        var res = new Playlist()
        {
            Id = entity.Id,
            UserId = entity.UserId,
            User = null,
            Name = entity.Name,
            Description = entity.Description,
            CoverUrl = entity.CoverUrl,
            IsPublic = entity.IsPublic,
            TagsInPlaylists = null,
            MoodsInPlaylists = null,
            TrackInPlaylists = null
        };
        return res;
    }

    public Domain.Playlist? Map(Playlist? entity)
    {
        if (entity == null) return null;
        var res = new Domain.Playlist()
        {
            Id = entity.Id,
            UserId = entity.UserId,
            User = null,
            Name = entity.Name,
            Description = entity.Description,
            CoverUrl = entity.CoverUrl,
            IsPublic = entity.IsPublic,
            TagsInPlaylists = null,
            MoodsInPlaylists = null,
            TrackInPlaylists = null
        };
        return res;
    }
}