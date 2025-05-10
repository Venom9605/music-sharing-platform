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
            ArtistInTracks = entity.ArtistInTracks?
                .Select(a => new ArtistInTrack
                {
                    Id = a.Id,
                    TrackId = a.TrackId,
                    UserId = a.UserId,
                    ArtistRoleId = a.ArtistRoleId,
                    User = a.User != null
                        ? new Artist
                        {
                            Id = a.User.Id,
                            DisplayName = a.User.DisplayName
                        }
                        : null,
                    ArtistRole = a.ArtistRole != null
                        ? new ArtistRole
                        {
                            Id = a.ArtistRole.Id,
                            Name = a.ArtistRole.Name
                        }
                        : null
                }).ToList(),
            
            Rating = entity.Rating?
                .Select(r => new Rating
                    {
                        Id = r.Id,
                        TrackId = r.TrackId,
                        UserId = r.UserId,
                        User = r.User != null
                            ? new Artist
                            {
                                Id = r.User.Id,
                                DisplayName = r.User.DisplayName
                            }
                            : null,
                        Score = r.Score,
                        Comment = r.Comment
                    }
                    ).ToList(),
            
            TrackLinks = entity.TrackLinks?
                .Select(t => new TrackLink()
                {
                    Id = t.Id,
                    TrackId = t.TrackId,
                    LinkTypeId = t.LinkTypeId,
                    LinkType = t.LinkType != null 
                        ? new LinkType()
                        {
                            Id = t.LinkType.Id,
                            Name = t.LinkType.Name
                        }
                        : null,
                    Url = t.Url
                }).ToList(),
            
            TagsInTracks = entity.TagsInTracks?
                .Select(t => new TagsInTrack()
                {
                    Id = t.Id,
                    TrackId = t.TrackId,
                    TagId = t.TagId,
                    Tag = t.Tag != null 
                        ? new Tag()
                        {
                            Id = t.Tag.Id,
                            Name = t.Tag.Name
                        }
                        : null
                }).ToList(),
            
            MoodsInTracks = entity.MoodsInTracks?
                .Select(t => new MoodsInTrack()
                {
                    Id = t.Id,
                    TrackId = t.TrackId,
                    MoodId = t.MoodId,
                    Mood = t.Mood != null 
                        ? new Mood()
                        {
                            Id = t.Mood.Id,
                            Name = t.Mood.Name
                        }
                        : null
                }).ToList(),
            
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
            
            ArtistInTracks = entity.ArtistInTracks?.Select(a => new Domain.ArtistInTrack
            {
                Id = a.Id,
                TrackId = a.TrackId,
                UserId = a.UserId,
                ArtistRoleId = a.ArtistRoleId
            }).ToList(),
            
            TrackLinks = entity.TrackLinks?.Select(l => new Domain.TrackLink
            {
                Id = l.Id,
                TrackId = l.TrackId,
                LinkTypeId = l.LinkTypeId,
                Url = l.Url
            }).ToList(),
            
            TagsInTracks = entity.TagsInTracks?.Select(t => new Domain.TagsInTrack
            {
                Id = t.Id,
                TrackId = t.TrackId,
                TagId = t.TagId
            }).ToList(),
            
            MoodsInTracks = entity.MoodsInTracks?.Select(m => new Domain.MoodsInTrack
            {
                Id = m.Id,
                TrackId = m.TrackId,
                MoodId = m.MoodId
            }).ToList(),
            
            Rating = null,
            SavedByUsers = null,
            TrackInPlaylists = null,
        };
        return res;
    }
}