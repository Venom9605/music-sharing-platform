using Base.Interfaces;

namespace App.DTO.v1.Mappers;

public class TagMapper : IMapper<App.DTO.v1.Tag, App.BLL.DTO.Tag>
{
    public Tag? Map(BLL.DTO.Tag? entity)
    {
        if (entity == null) return null;
        return new Tag
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public BLL.DTO.Tag? Map(Tag? entity)
    {
        if (entity == null) return null;
        return new BLL.DTO.Tag
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}