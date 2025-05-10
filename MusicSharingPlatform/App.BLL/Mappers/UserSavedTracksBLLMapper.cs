using App.DAL.DTO;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Mappers;

public class UserSavedTracksBLLMapper : IBLLMapper<App.BLL.DTO.UserSavedTracks, App.DAL.DTO.UserSavedTracks>
{
    public UserSavedTracks? Map(DTO.UserSavedTracks? entity)
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

    public DTO.UserSavedTracks? Map(UserSavedTracks? entity)
    {
        if (entity == null) return null;
        var res = new DTO.UserSavedTracks()
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
            } : null

        };
        return res;
    }
}