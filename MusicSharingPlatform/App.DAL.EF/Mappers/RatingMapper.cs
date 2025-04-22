using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class RatingMapper : IMapper<DTO.Rating, Domain.Rating>
{
    public Rating? Map(Domain.Rating? entity)
    {
        if (entity == null) return null;
        var res = new Rating()
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