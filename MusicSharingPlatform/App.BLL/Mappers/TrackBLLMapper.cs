
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
            
            ArtistInTracks = entity.ArtistInTracks?.Select(a => new ArtistInTrack
            {
                Id = a.Id,
                TrackId = a.TrackId,
                UserId = a.UserId,
                ArtistRoleId = a.ArtistRoleId
            }).ToList(),
            
            TrackLinks = entity.TrackLinks?.Select(l => new TrackLink
            {
                Id = l.Id,
                TrackId = l.TrackId,
                LinkTypeId = l.LinkTypeId,
                Url = l.Url
            }).ToList(),
            
            TagsInTracks = entity.TagsInTracks?.Select(t => new TagsInTrack
            {
                Id = t.Id,
                TrackId = t.TrackId,
                TagId = t.TagId
            }).ToList(),
            
            MoodsInTracks = entity.MoodsInTracks?.Select(m => new MoodsInTrack
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
            ArtistInTracks = entity.ArtistInTracks?.Select(a => new App.BLL.DTO.ArtistInTrack
            {
                Id = a.Id,
                TrackId = a.TrackId,
                UserId = a.UserId,
                ArtistRoleId = a.ArtistRoleId,
                ArtistDisplayName = a.User?.DisplayName,
                ArtistRoleName = a.ArtistRole?.Name
            }).ToList(),
            Rating = entity.Rating?.Select(r => new App.BLL.DTO.Rating
                {
                    Id = r.Id,
                    TrackId = r.TrackId,
                    UserId = r.UserId,
                    Score = r.Score,
                    Comment = r.Comment,
                    ArtistDisplayName = r.User?.DisplayName
                }
                ).ToList(),
            
            TrackLinks = entity.TrackLinks?.Select(t => new App.BLL.DTO.TrackLink
            {
                Id = t.Id,
                TrackId = t.TrackId,
                LinkTypeId = t.LinkTypeId,
                Url = t.Url,
                LinkTypeName = t.LinkType?.Name,
            }).ToList(),
            
            TagsInTracks = entity.TagsInTracks?.Select(t => new App.BLL.DTO.TagsInTrack()
            {
                Id = t.Id,
                TrackId = t.TrackId,
                TagId = t.TagId,
                TagName = t.Tag?.Name,
            }).ToList(),
            
            MoodsInTracks = entity.MoodsInTracks?.Select(t => new App.BLL.DTO.MoodsInTrack()
            {
                Id = t.Id,
                TrackId = t.TrackId,
                MoodId = t.MoodId,
                MoodName = t.Mood?.Name,
            }).ToList(),

        };
        return res;
    }
}