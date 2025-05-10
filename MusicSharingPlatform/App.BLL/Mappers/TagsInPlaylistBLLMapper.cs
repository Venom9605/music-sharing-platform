using App.DAL.DTO;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Mappers;

public class TagsInPlaylistBLLMapper : IBLLMapper<App.BLL.DTO.TagsInPlaylist, App.DAL.DTO.TagsInPlaylist>
{
    public TagsInPlaylist? Map(DTO.TagsInPlaylist? entity)
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

    public DTO.TagsInPlaylist? Map(TagsInPlaylist? entity)
    {
        if (entity == null) return null;
        var res = new DTO.TagsInPlaylist()
        {
            Id = entity.Id,
            
            PlaylistId = entity.PlaylistId,
            Playlist = entity.Playlist != null ? new DTO.Playlist
            {
                Id = entity.Playlist.Id,
                Name = entity.Playlist.Name
            } : null,
            
            TagId = entity.TagId,
            Tag = entity.Tag != null ? new DTO.Tag
            {
                Id = entity.Tag.Id,
                Name = entity.Tag.Name
            } : null,

        };
        return res;
    }
}