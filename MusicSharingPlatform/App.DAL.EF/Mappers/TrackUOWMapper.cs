using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class TrackUOWMapper : IUOWMapper<DTO.Track, Domain.Track>
{
    public Track? Map(Domain.Track? entity)
    {
        if (entity == null) return null;
        var res = new Track()
        {
            Id = entity.Id,
            Title = entity.Title,
            FilePath = entity.FilePath,
            CoverPath = entity.CoverPath,
            Duration = entity.Duration,
            TimesPlayed = entity.TimesPlayed,
            TimesSaved = entity.TimesSaved,
            ArtistInTracks = null,
            Rating = null,
            SavedByUsers = null,
            TrackLinks = null,
            TagsInTracks = null,
            MoodsInTracks = null,
            TrackInPlaylists = null,
        };
        return res;
    }

    public Domain.Track? Map(Track? entity)
    {
        if (entity == null) return null;
        
        var res = new Domain.Track()
        {
            Id = entity.Id,
            Title = entity.Title,
            FilePath = entity.FilePath,
            CoverPath = entity.CoverPath,
            Duration = entity.Duration,
            TimesPlayed = entity.TimesPlayed,
            TimesSaved = entity.TimesSaved,
            ArtistInTracks = null,
            Rating = null,
            SavedByUsers = null,
            TrackLinks = null,
            TagsInTracks = null,
            MoodsInTracks = null,
            TrackInPlaylists = null,
        };
        return res;
    }
}