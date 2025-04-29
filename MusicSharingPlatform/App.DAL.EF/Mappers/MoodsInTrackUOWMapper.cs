using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class MoodsInTrackUOWMapper : IUOWMapper<DTO.MoodsInTrack, Domain.MoodsInTrack>
{
    public MoodsInTrack? Map(Domain.MoodsInTrack? entity)
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

    public Domain.MoodsInTrack? Map(MoodsInTrack? entity)
    {
        if (entity == null) return null;
        var res = new Domain.MoodsInTrack()
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