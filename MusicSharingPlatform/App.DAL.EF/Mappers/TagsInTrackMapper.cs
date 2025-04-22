using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class TagsInTrackMapper : IMapper<DTO.TagsInTrack, Domain.TagsInTrack>
{
    public TagsInTrack? Map(Domain.TagsInTrack? entity)
    {
        if (entity == null) return null;
        var res = new TagsInTrack()
        {
            Id = entity.Id,
            TrackId = entity.TrackId,
            Track = null,
            TagId = entity.TagId,
            Tag = null,

        };
        return res;
    }

    public Domain.TagsInTrack? Map(TagsInTrack? entity)
    {
        if (entity == null) return null;
        var res = new Domain.TagsInTrack()
        {
            Id = entity.Id,
            TrackId = entity.TrackId,
            Track = null,
            TagId = entity.TagId,
            Tag = null,

        };
        return res;
    }
}