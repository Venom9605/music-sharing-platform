using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class TagMapper : IMapper<DTO.Tag, Domain.Tag>
{
    public Tag? Map(Domain.Tag? entity)
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

    public Domain.Tag? Map(Tag? entity)
    {
        if (entity == null) return null;
        var res = new Domain.Tag()
        {
            Id = entity.Id,
            Name = entity.Name,
            TagsInTracks = null,
            TagsInPlaylists = null

        };
        return res;
    }
}