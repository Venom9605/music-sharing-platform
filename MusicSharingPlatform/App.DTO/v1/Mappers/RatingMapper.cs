using Base.Interfaces;

namespace App.DTO.v1.Mappers;

public class RatingMapper : IMapper<App.DTO.v1.Rating, App.BLL.DTO.Rating>
{
    public Rating? Map(BLL.DTO.Rating? entity)
    {
        if (entity == null) return null;
        
        var res = new Rating()
        {
            Id = entity.Id,
            TrackId = entity.TrackId,
            UserId = entity.UserId,
            Score = entity.Score,
            Comment = entity.Comment,
            ArtistDisplayName = entity.ArtistDisplayName
        };
        
        return res;
    }

    public BLL.DTO.Rating? Map(Rating? entity)
    {
        if (entity == null) return null;
        
        var res = new BLL.DTO.Rating()
        {
            Id = entity.Id,
            TrackId = entity.TrackId,
            UserId = entity.UserId,
            Score = entity.Score,
            Comment = entity.Comment,
            ArtistDisplayName = entity.ArtistDisplayName
        };
        
        return res;
    }
    
    public BLL.DTO.Rating? Map(RatingCreate? entity, string userId)
    {
        if (entity == null) return null;
        
        var res = new BLL.DTO.Rating()
        {
            Id = Guid.NewGuid(),
            TrackId = entity.TrackId,
            UserId = userId,
            Score = entity.Score,
            Comment = entity.Comment,
        };
        
        return res;
    }
    
}