using App.DAL.DTO;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Mappers;

public class PlaylistBLLMapper : IBLLMapper<App.BLL.DTO.Playlist, App.DAL.DTO.Playlist>
{
    public Playlist? Map(DTO.Playlist? entity)
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

    public DTO.Playlist? Map(Playlist? entity)
    {
        if (entity == null) return null;
        var res = new DTO.Playlist()
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