using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class ArtistRoleUOWMapper : IUOWMapper<DTO.ArtistRole, Domain.ArtistRole>
{
    public ArtistRole? Map(Domain.ArtistRole? entity)
    {
        if (entity == null) return null;
        var res = new ArtistRole()
        {
            Id = entity.Id,
            Name = entity.Name,
            ArtistInTracks = null

        };
        return res;
    }

    public Domain.ArtistRole? Map(ArtistRole? entity)
    {
        if (entity == null) return null;
        var res = new Domain.ArtistRole()
        {
            Id = entity.Id,
            Name = entity.Name,
            ArtistInTracks = null

        };
        return res;
    }
}