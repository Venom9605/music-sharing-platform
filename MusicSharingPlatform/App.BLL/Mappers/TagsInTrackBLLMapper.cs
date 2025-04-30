using App.DAL.DTO;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Mappers;

public class TagsInTrackBLLMapper : IBLLMapper<App.BLL.DTO.TagsInTrack, App.DAL.DTO.TagsInTrack>
{
    public TagsInTrack? Map(DTO.TagsInTrack? entity)
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

    public DTO.TagsInTrack? Map(TagsInTrack? entity)
    {
        if (entity == null) return null;
        var res = new DTO.TagsInTrack()
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