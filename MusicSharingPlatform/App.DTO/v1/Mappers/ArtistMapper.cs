using Base.Interfaces;

namespace App.DTO.v1.Mappers;

public class ArtistMapper : IMapper<App.DTO.v1.Artist, App.BLL.DTO.Artist, string>
{
    public Artist? Map(BLL.DTO.Artist? entity)
    {
        if (entity == null) return null;

        return new Artist
        {
            Id = entity.Id,
            DisplayName = entity.DisplayName,
            Bio = entity.Bio,
            ProfilePicture = entity.ProfilePicture,
            JoinDate = entity.JoinDate
        };
    }

    public BLL.DTO.Artist? Map(Artist? entity)
    {
        throw new NotImplementedException();
    }
    
}