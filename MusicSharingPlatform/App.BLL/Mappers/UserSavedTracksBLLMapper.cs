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
            Track = null,
            UserId = entity.UserId,
            User = null

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
            Track = null,
            UserId = entity.UserId,
            User = null

        };
        return res;
    }
}