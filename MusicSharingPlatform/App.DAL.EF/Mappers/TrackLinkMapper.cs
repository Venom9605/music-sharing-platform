using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class TrackLinkMapper : IMapper<DTO.TrackLink, Domain.TrackLink>
{
    public TrackLink? Map(Domain.TrackLink? entity)
    {
        if (entity == null) return null;
        var res = new TrackLink()
        {
            Id = entity.Id,
            TrackId = entity.TrackId,
            Track = null,
            LinkTypeId = entity.LinkTypeId,
            LinkType = null,
            Url = entity.Url,
            

        };
        return res;
    }

    public Domain.TrackLink? Map(TrackLink? entity)
    {
        if (entity == null) return null;
        var res = new Domain.TrackLink()
        {
            Id = entity.Id,
            TrackId = entity.TrackId,
            Track = null,
            LinkTypeId = entity.LinkTypeId,
            LinkType = null,
            Url = entity.Url,
            

        };
        return res;
    }
}