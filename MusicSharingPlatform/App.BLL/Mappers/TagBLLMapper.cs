using App.DAL.DTO;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Mappers;

public class TagBLLMapper : IBLLMapper<App.BLL.DTO.Tag, App.DAL.DTO.Tag>
{
    public Tag? Map(DTO.Tag? entity)
    {
        if (entity == null) return null;
        var res = new Tag()
        {
            Id = entity.Id,
            Name = entity.Name,
            TagsInTracks = null,
            TagsInPlaylists = null

        };
        return res;
    }

    public DTO.Tag? Map(Tag? entity)
    {
        if (entity == null) return null;
        var res = new DTO.Tag()
        {
            Id = entity.Id,
            Name = entity.Name,
            TagsInTracks = null,
            TagsInPlaylists = null

        };
        return res;
    }
}