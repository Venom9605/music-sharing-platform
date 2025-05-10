using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class TagsInPlaylistUOWMapper : IUOWMapper<DTO.TagsInPlaylist, Domain.TagsInPlaylist>
{
    public TagsInPlaylist? Map(Domain.TagsInPlaylist? entity)
    {
        if (entity == null) return null;
        var res = new TagsInPlaylist()
        {
            Id = entity.Id,
            
            PlaylistId = entity.PlaylistId,
            Playlist = entity.Playlist != null ? new Playlist
            {
                Id = entity.Playlist.Id,
                Name = entity.Playlist.Name
            } : null,
            
            TagId = entity.TagId,
            Tag = entity.Tag != null ? new Tag
            {
                Id = entity.Tag.Id,
                Name = entity.Tag.Name
            } : null,

        };
        return res;
    }

    public Domain.TagsInPlaylist? Map(TagsInPlaylist? entity)
    {
        if (entity == null) return null;
        var res = new Domain.TagsInPlaylist()
        {
            Id = entity.Id,
            
            PlaylistId = entity.PlaylistId,
            Playlist = null,
            
            TagId = entity.TagId,
            Tag = null,

        };
        return res;
    }
}