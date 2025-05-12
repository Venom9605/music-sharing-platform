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
            
            ArtistInTracks = entity.ArtistInTracks?.Select(a => new ArtistInTrack
            {
                Id = a.Id,
                TrackId = a.TrackId,
                UserId = a.UserId,
                ArtistRoleId = a.ArtistRoleId,
                ArtistDisplayName = a.ArtistDisplayName,
                ArtistRoleName = a.ArtistRoleName
            }).ToList(),
            
            Rating = entity.Rating?.Select(r => new Rating
            {
                Id = r.Id,
                TrackId = r.TrackId,
                UserId = r.UserId,
                Score = r.Score,
                Comment = r.Comment,
                ArtistDisplayName = r.ArtistDisplayName
            }).ToList(),
            
            TrackLinks = entity.TrackLinks?.Select(t => new TrackLink
            {
                Id = t.Id,
                TrackId = t.TrackId,
                LinkTypeId = t.LinkTypeId,
                Url = t.Url,
                LinkTypeName = t.LinkTypeName
            }).ToList(),
            
            TagsInTracks = entity.TagsInTracks?.Select(t => new TagsInTrack()
            {
                Id = t.Id,
                TrackId = t.TrackId,
                TagId = t.TagId,
                TagName = t.TagName
            }).ToList(),
            
            MoodsInTracks = entity.MoodsInTracks?.Select(t => new MoodsInTrack()
            {
                Id = t.Id,
                TrackId = t.TrackId,
                MoodId = t.MoodId,
                MoodName = t.MoodName
            }).ToList(),
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
    
    public BLL.DTO.Track? Map(App.DTO.v1.TrackCreate? entity, string userId)
    {
        if (entity == null) return null;
        
        var trackId = Guid.NewGuid();
        
        var artistInTracks = new List<BLL.DTO.ArtistInTrack>
        {
            new()
            {
                Id = Guid.NewGuid(),
                TrackId = trackId,
                UserId = userId,
                ArtistRoleId = entity.ArtistRoleId
            }
        };
        
        if (entity.ArtistInTracks != null)
        {
            artistInTracks.AddRange(entity.ArtistInTracks
                .Where(a => a.UserId != userId)
                .Select(a => new BLL.DTO.ArtistInTrack
                {
                    Id = Guid.NewGuid(),
                    TrackId = trackId,
                    UserId = a.UserId,
                    ArtistRoleId = a.ArtistRoleId
                }));
        }
        
        var res = new BLL.DTO.Track()
        {
            Id = trackId,
            Title = entity.Title,
            FilePath = entity.FilePath,
            CoverPath = entity.CoverPath,
            Duration = 0,
            TimesPlayed = 0,
            TimesSaved = 0,
            
            ArtistInTracks = artistInTracks,

            TagsInTracks = entity.TagsInTracks?.Select(t => new BLL.DTO.TagsInTrack
            {
                Id = Guid.NewGuid(),
                TrackId = trackId,
                TagId = t.TagId
            }).ToList(),

            MoodsInTracks = entity.MoodsInTracks?.Select(m => new BLL.DTO.MoodsInTrack
            {
                Id = Guid.NewGuid(),
                TrackId = trackId,
                MoodId = m.MoodId
            }).ToList(),

            TrackLinks = entity.TrackLinks?.Select(l => new BLL.DTO.TrackLink
            {
                Id = Guid.NewGuid(),
                TrackId = trackId,
                LinkTypeId = l.LinkTypeId,
                Url = l.Url
            }).ToList()
        };

        return res;
    }
    
}