﻿using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class TagsInTrackUOWMapper : IUOWMapper<DTO.TagsInTrack, Domain.TagsInTrack>
{
    public TagsInTrack? Map(Domain.TagsInTrack? entity)
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