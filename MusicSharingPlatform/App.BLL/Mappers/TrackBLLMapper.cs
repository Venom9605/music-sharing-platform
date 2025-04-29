
using App.DAL.DTO;
using Base.BLL.Interfaces;

namespace App.BLL.Mappers;

public class TrackBLLMapper : IBLLMapper<App.BLL.DTO.Track, App.DAL.DTO.Track>
{
    public Track? Map(DTO.Track? entity)
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

    public DTO.Track? Map(Track? entity)
    {
        if (entity == null) return null;
        
        var res = new DTO.Track()
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