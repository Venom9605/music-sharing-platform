using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class UserSavedTracksUOWMapper : IUOWMapper<DTO.UserSavedTracks, Domain.UserSavedTracks>
{
    public UserSavedTracks? Map(Domain.UserSavedTracks? entity)
    {
        if (entity == null) return null;
        var res = new UserSavedTracks()
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
            } : null

        };
        return res;
    }

    public Domain.UserSavedTracks? Map(UserSavedTracks? entity)
    {
        if (entity == null) return null;
        var res = new Domain.UserSavedTracks()
        {
            Id = entity.Id,
            
            TrackId = entity.TrackId,
            Track = null,
            
            UserId = entity.UserId,
            User = null

        };
        return res;
    }
}