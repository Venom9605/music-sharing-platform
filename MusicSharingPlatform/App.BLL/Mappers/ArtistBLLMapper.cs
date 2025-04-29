using App.DAL.DTO;
using Base.BLL.Interfaces;

namespace App.BLL.Mappers;

public class ArtistBLLMapper : IBLLMapper<App.BLL.DTO.Artist, App.DAL.DTO.Artist, string>
{
    public Artist? Map(DTO.Artist? entity)
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

    public DTO.Artist? Map(Artist? entity)
    {
        if (entity == null) return null;
        
        var res = new DTO.Artist()
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