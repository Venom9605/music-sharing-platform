
using App.DAL.DTO;
using Base.BLL.Interfaces;

namespace App.BLL.Mappers;

public class ArtistRoleBLLMapper : IBLLMapper<App.BLL.DTO.ArtistRole, App.DAL.DTO.ArtistRole>
{
    public ArtistRole? Map(DTO.ArtistRole? entity)
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

    public DTO.ArtistRole? Map(ArtistRole? entity)
    {
        if (entity == null) return null;
        var res = new DTO.ArtistRole()
        {
            Id = entity.Id,
            Name = entity.Name,
            ArtistInTracks = null

        };
        return res;
    }
}