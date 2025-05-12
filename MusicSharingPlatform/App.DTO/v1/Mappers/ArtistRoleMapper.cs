using Base.Interfaces;

namespace App.DTO.v1.Mappers;

public class ArtistRoleMapper : IMapper<App.DTO.v1.ArtistRole, App.BLL.DTO.ArtistRole>
{
    public ArtistRole? Map(BLL.DTO.ArtistRole? entity)
    {
        if (entity == null) return null;
        return new ArtistRole
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public BLL.DTO.ArtistRole? Map(ArtistRole? entity)
    {
        if (entity == null) return null;
        return new BLL.DTO.ArtistRole
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}