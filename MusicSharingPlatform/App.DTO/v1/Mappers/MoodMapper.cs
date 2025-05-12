using Base.Interfaces;

namespace App.DTO.v1.Mappers;

public class MoodMapper : IMapper<App.DTO.v1.Mood, App.BLL.DTO.Mood>
{
    public Mood? Map(BLL.DTO.Mood? entity)
    {
        if (entity == null) return null;
        return new Mood
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public BLL.DTO.Mood? Map(Mood? entity)
    {
        if (entity == null) return null;
        return new BLL.DTO.Mood
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
}