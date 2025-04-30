using App.DAL.DTO;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Mappers;

public class MoodsInTrackBLLMapper : IBLLMapper<App.BLL.DTO.MoodsInTrack, App.DAL.DTO.MoodsInTrack>
{
    public MoodsInTrack? Map(DTO.MoodsInTrack? entity)
    {
        if (entity == null) return null;
        var res = new MoodsInTrack()
        {
            Id = entity.Id,
            MoodId = entity.MoodId,
            Mood = null,
            TrackId = entity.TrackId,
            Track = null,
        };
        return res;
    }

    public DTO.MoodsInTrack? Map(MoodsInTrack? entity)
    {
        if (entity == null) return null;
        var res = new DTO.MoodsInTrack()
        {
            Id = entity.Id,
            MoodId = entity.MoodId,
            Mood = null,
            TrackId = entity.TrackId,
            Track = null,
        };
        return res;
    }
}