using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class RatingUOWMapper : IUOWMapper<DTO.Rating, Domain.Rating>
{
    public Rating? Map(Domain.Rating? entity)
    {
        if (entity == null) return null;
        var res = new Rating()
        {
            Id = entity.Id,
            
            TrackId = entity.TrackId,
            Track = entity.Track != null ? new Track
            {
                Id = entity.Track.Id,
                Title = entity.Track.Title
            } : null,
            
            UserId = entity.UserId,
            User = entity.User != null ? new Artist
            {
                Id = entity.User.Id,
                DisplayName = entity.User.DisplayName
            } : null,
            
            Score = entity.Score,
            Comment = entity.Comment,

        };
        return res;
    }

    public Domain.Rating? Map(Rating? entity)
    {
        if (entity == null) return null;
        var res = new Domain.Rating()
        {
            Id = entity.Id,
            TrackId = entity.TrackId,
            Track = null,
            UserId = entity.UserId,
            User = null,
            Score = entity.Score,
            Comment = entity.Comment,

        };
        return res;
    }
}