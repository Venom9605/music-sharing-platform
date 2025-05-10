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
            Track = entity.Track != null ? new Track
            {
                Id = entity.Track.Id,
                Title = entity.Track.Title
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

    public DTO.TagsInTrack? Map(TagsInTrack? entity)
    {
        if (entity == null) return null;
        var res = new DTO.TagsInTrack()
        {
            Id = entity.Id,
            
            TrackId = entity.TrackId,
            Track = entity.Track != null ? new DTO.Track
            {
                Id = entity.Track.Id,
                Title = entity.Track.Title
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