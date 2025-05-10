using Base.Interfaces;

namespace App.DTO.v1.Mappers;

public class UserSavedTracksMapper : IMapper<App.DTO.v1.UserSavedTracks, App.BLL.DTO.UserSavedTracks>
{
    public UserSavedTracks? Map(BLL.DTO.UserSavedTracks? entity)
    {
        if (entity == null) return null;
        
        var res = new UserSavedTracks()
        {
            Id = entity.Id,
            TrackId = entity.TrackId,
            UserId = entity.UserId
        };
        
        return res;
    }

    public BLL.DTO.UserSavedTracks? Map(UserSavedTracks? entity)
    {
        if (entity == null) return null;
        
        var res = new App.BLL.DTO.UserSavedTracks()
        {
            Id = entity.Id,
            TrackId = entity.TrackId,
            UserId = entity.UserId
        };
        
        return res;
    }
    
    public BLL.DTO.UserSavedTracks? Map(UserSavedTracksCreate? entity, string userId)
    {
        if (entity == null) return null;
        
        var res = new App.BLL.DTO.UserSavedTracks()
        {
            Id = Guid.NewGuid(),
            TrackId = entity.TrackId,
            UserId = userId
        };
        
        return res;
    }
    
}