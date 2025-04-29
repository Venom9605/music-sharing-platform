using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class MoodUOWMapper : IUOWMapper<DTO.Mood, Domain.Mood>
{
    public Mood? Map(Domain.Mood? entity)
    {
        if (entity == null) return null;
        var res = new Mood()
        {
            Id = entity.Id,
            Name = entity.Name,
            MoodsInTracks = null,
            MoodsInPlaylists = null

        };
        return res;
    }

    public Domain.Mood? Map(Mood? entity)
    {
        if (entity == null) return null;
        var res = new Domain.Mood()
        {
            Id = entity.Id,
            Name = entity.Name,
            MoodsInTracks = null,
            MoodsInPlaylists = null

        };
        return res;
    }
}