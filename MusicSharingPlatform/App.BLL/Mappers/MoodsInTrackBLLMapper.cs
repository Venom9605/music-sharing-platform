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
            Mood = entity.Mood != null ? new Mood
            {
                Id = entity.Mood.Id,
                Name = entity.Mood.Name
            } : null,
            
            TrackId = entity.TrackId,
            Track = entity.Track != null ? new Track
            {
                Id = entity.Track.Id,
                Title = entity.Track.Title
            } : null,
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
            Mood = entity.Mood != null ? new DTO.Mood
            {
                Id = entity.Mood.Id,
                Name = entity.Mood.Name
            } : null,
            
            TrackId = entity.TrackId,
            Track = entity.Track != null ? new DTO.Track
            {
                Id = entity.Track.Id,
                Title = entity.Track.Title
            } : null,
        };
        return res;
    }
}