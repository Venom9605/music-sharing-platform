using Base.Interfaces;

namespace App.DTO.v1.Mappers;

public class TrackMapper : IMapper<App.DTO.v1.Track, App.BLL.DTO.Track>
{
    public Track? Map(BLL.DTO.Track? entity)
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
        };

        return res;
    }

    public BLL.DTO.Track? Map(Track? entity)
    {
        if (entity == null) return null;
        
        var res = new BLL.DTO.Track()
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
    
    public BLL.DTO.Track? Map(App.DTO.v1.TrackCreate? entity)
    {
        if (entity == null) return null;
        
        var res = new BLL.DTO.Track()
        {
            Id = Guid.NewGuid(),
            Title = entity.Title,
            FilePath = entity.FilePath,
            CoverPath = entity.CoverPath,
            Duration = 0,
            TimesPlayed = 0,
            TimesSaved = 0,
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