using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class LinkTypeUOWMapper : IUOWMapper<DTO.LinkType, Domain.LinkType>
{
    public LinkType? Map(Domain.LinkType? entity)
    {
        if (entity == null) return null;
        var res = new LinkType()
        {
            Id = entity.Id,
            Name = entity.Name,
            UserLinks = null,
            TrackLinks = null
        };
        return res;
    }

    public Domain.LinkType? Map(LinkType? entity)
    {
        if (entity == null) return null;
        var res = new Domain.LinkType()
        {
            Id = entity.Id,
            Name = entity.Name,
            UserLinks = null,
            TrackLinks = null
        };
        return res;
    }
}