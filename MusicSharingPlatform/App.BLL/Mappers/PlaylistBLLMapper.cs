using App.DAL.DTO;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Mappers;

public class PlaylistBLLMapper : IBLLMapper<App.BLL.DTO.Playlist, App.DAL.DTO.Playlist>
{
    public Playlist? Map(DTO.Playlist? entity)
    {
        if (entity == null) return null;
        var res = new Playlist()
        {
            Id = entity.Id,
            
            UserId = entity.UserId,
            User = entity.User != null ? new Artist
            {
                Id = entity.User.Id,
                DisplayName = entity.User.DisplayName
            } : null,
            
            Name = entity.Name,
            Description = entity.Description,
            CoverUrl = entity.CoverUrl,
            IsPublic = entity.IsPublic,
            TagsInPlaylists = null,
            MoodsInPlaylists = null,
            TrackInPlaylists = entity.TrackInPlaylists?.Select(tip => new TrackInPlaylist
            {
                Id = tip.Id,
                PlaylistId = tip.PlaylistId,
                TrackId = tip.TrackId,
                Track = tip.Track != null ? new Track
                {
                    Id = tip.Track.Id,
                    Title = tip.Track.Title,
                    FilePath = tip.Track.FilePath,
                    CoverPath = tip.Track.CoverPath,
                    Duration = tip.Track.Duration,
                    TimesPlayed = tip.Track.TimesPlayed,
                    TimesSaved = tip.Track.TimesSaved
                    // You can add ArtistInTracks, Tags, Moods etc. if needed
                } : null
            }).ToList(),
        };
        return res;
    }

    public DTO.Playlist? Map(Playlist? entity)
    {
        if (entity == null) return null;
        var res = new DTO.Playlist()
        {
            Id = entity.Id,
            
            UserId = entity.UserId,
            User = entity.User != null ? new DTO.Artist
            {
                Id = entity.User.Id,
                DisplayName = entity.User.DisplayName
            } : null,
            
            Name = entity.Name,
            Description = entity.Description,
            CoverUrl = entity.CoverUrl,
            IsPublic = entity.IsPublic,
            TagsInPlaylists = null,
            MoodsInPlaylists = null,
            TrackInPlaylists = entity.TrackInPlaylists?.Select(tip => new DTO.TrackInPlaylist
            {
                Id = tip.Id,
                PlaylistId = tip.PlaylistId,
                TrackId = tip.TrackId,
                Track = tip.Track != null ? new DTO.Track
                {
                    Id = tip.Track.Id,
                    Title = tip.Track.Title,
                    FilePath = tip.Track.FilePath,
                    CoverPath = tip.Track.CoverPath,
                    Duration = tip.Track.Duration,
                    TimesPlayed = tip.Track.TimesPlayed,
                    TimesSaved = tip.Track.TimesSaved,

                    ArtistInTracks = tip.Track.ArtistInTracks?.Select(a => new DTO.ArtistInTrack
                    {
                        Id = a.Id,
                        TrackId = a.TrackId,
                        UserId = a.UserId,
                        ArtistRoleId = a.ArtistRoleId,
                        ArtistDisplayName = a.User?.DisplayName,
                        ArtistRoleName = a.ArtistRole?.Name
                    }).ToList(),

                    Rating = tip.Track.Rating?.Select(r => new DTO.Rating
                    {
                        Id = r.Id,
                        TrackId = r.TrackId,
                        UserId = r.UserId,
                        Score = r.Score,
                        Comment = r.Comment,
                        ArtistDisplayName = r.User?.DisplayName,
                    }).ToList(),

                    TrackLinks = tip.Track.TrackLinks?.Select(tl => new DTO.TrackLink
                    {
                        Id = tl.Id,
                        TrackId = tl.TrackId,
                        LinkTypeId = tl.LinkTypeId,
                        Url = tl.Url,
                        LinkTypeName = tl.LinkType?.Name,
                    }).ToList(),

                    TagsInTracks = tip.Track.TagsInTracks?.Select(tag => new DTO.TagsInTrack
                    {
                        Id = tag.Id,
                        TrackId = tag.TrackId,
                        TagId = tag.TagId,
                        TagName = tag.Tag?.Name
                    }).ToList(),

                    MoodsInTracks = tip.Track.MoodsInTracks?.Select(mood => new DTO.MoodsInTrack
                    {
                        Id = mood.Id,
                        TrackId = mood.TrackId,
                        MoodId = mood.MoodId,
                        MoodName = mood.Mood?.Name
                    }).ToList()

                } : null
            }).ToList(),
        };
        return res;
    }
}