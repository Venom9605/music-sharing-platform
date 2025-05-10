using App.DAL.DTO;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Mappers;

public class RatingBLLMapper : IBLLMapper<App.BLL.DTO.Rating, App.DAL.DTO.Rating>
{
    public Rating? Map(DTO.Rating? entity)
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

    public DTO.Rating? Map(Rating? entity)
    {
        if (entity == null) return null;
        var res = new DTO.Rating()
        {
            Id = entity.Id,
            
            TrackId = entity.TrackId,
            Track = entity.Track != null ? new DTO.Track
            {
                Id = entity.Track.Id,
                Title = entity.Track.Title
            } : null,
            
            UserId = entity.UserId,
            User = entity.User != null ? new DTO.Artist
            {
                Id = entity.User.Id,
                DisplayName = entity.User.DisplayName
            } : null,
            
            Score = entity.Score,
            Comment = entity.Comment,

        };
        return res;
    }
}