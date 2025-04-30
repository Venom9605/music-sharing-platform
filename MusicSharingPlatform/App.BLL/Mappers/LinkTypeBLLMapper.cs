using App.DAL.DTO;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Mappers;

public class LinkTypeBLLMapper : IBLLMapper<App.BLL.DTO.LinkType, App.DAL.DTO.LinkType>
{
    public LinkType? Map(DTO.LinkType? entity)
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

    public DTO.LinkType? Map(LinkType? entity)
    {
        if (entity == null) return null;
        var res = new DTO.LinkType()
        {
            Id = entity.Id,
            Name = entity.Name,
            UserLinks = null,
            TrackLinks = null
        };
        return res;
    }
}