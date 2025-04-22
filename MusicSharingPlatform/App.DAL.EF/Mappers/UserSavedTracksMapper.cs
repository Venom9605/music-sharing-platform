using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class UserSavedTracksMapper : IMapper<DTO.UserSavedTracks, Domain.UserSavedTracks>
{
    public UserSavedTracks? Map(Domain.UserSavedTracks? entity)
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