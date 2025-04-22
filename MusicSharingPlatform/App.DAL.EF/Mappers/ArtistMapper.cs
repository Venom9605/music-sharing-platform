using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class ArtistMapper : IMapper<DTO.Artist, Domain.Artist, string>
{
    public Artist? Map(Domain.Artist? entity)
    {
        if (entity == null) return null;
        var res = new Artist()
        {
            Id = entity.Id,
            DisplayName = entity.DisplayName,
            Bio = entity.Bio,
            ProfilePicture = entity.ProfilePicture,
            JoinDate = entity.JoinDate,
            ArtistInTracks = null,
            Ratings = null,
            SavedTracks = null,
            UserLinks = null,
            Playlists = null
        };
        return res;
    }

    public Domain.Artist? Map(Artist? entity)
    {
        if (entity == null) return null;
        var res = new Domain.Artist()
        {
            Id = entity.Id,
            DisplayName = entity.DisplayName,
            Bio = entity.Bio,
            ProfilePicture = entity.ProfilePicture,
            JoinDate = entity.JoinDate,
            ArtistInTracks = null,
            Ratings = null,
            SavedTracks = null,
            UserLinks = null,
            Playlists = null
        };
        return res;
    }
}