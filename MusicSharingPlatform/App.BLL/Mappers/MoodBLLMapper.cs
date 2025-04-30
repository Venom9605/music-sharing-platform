using App.DAL.DTO;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Mappers;

public class MoodBLLMapper : IBLLMapper<App.BLL.DTO.Mood, App.DAL.DTO.Mood>
{
    public Mood? Map(DTO.Mood? entity)
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

    public DTO.Mood? Map(Mood? entity)
    {
        if (entity == null) return null;
        var res = new DTO.Mood()
        {
            Id = entity.Id,
            Name = entity.Name,
            MoodsInTracks = null,
            MoodsInPlaylists = null

        };
        return res;
    }
}