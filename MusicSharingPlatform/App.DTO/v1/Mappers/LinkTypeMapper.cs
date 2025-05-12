using Base.Interfaces;

namespace App.DTO.v1.Mappers;

public class LinkTypeMapper : IMapper<App.DTO.v1.LinkType, App.BLL.DTO.LinkType>
{
    public LinkType? Map(BLL.DTO.LinkType? entity)
    {
        if (entity == null) return null;
        return new LinkType
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
    

    public BLL.DTO.LinkType? Map(LinkType? entity)
    {
        if (entity == null) return null;
        return new BLL.DTO.LinkType
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}