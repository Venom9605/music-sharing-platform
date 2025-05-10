using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class PlaylistUOWMapper : IUOWMapper<DTO.Playlist, Domain.Playlist>
{
    public Playlist? Map(Domain.Playlist? entity)
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
                    TimesSaved = tip.Track.TimesSaved,

                    ArtistInTracks = tip.Track.ArtistInTracks?.Select(a => new ArtistInTrack
                    {
                        Id = a.Id,
                        TrackId = a.TrackId,
                        UserId = a.UserId,
                        ArtistRoleId = a.ArtistRoleId
                    }).ToList(),

                    Rating = tip.Track.Rating?.Select(r => new Rating
                    {
                        Id = r.Id,
                        TrackId = r.TrackId,
                        UserId = r.UserId,
                        Score = r.Score,
                        Comment = r.Comment
                    }).ToList(),

                    TrackLinks = tip.Track.TrackLinks?.Select(l => new TrackLink
                    {
                        Id = l.Id,
                        TrackId = l.TrackId,
                        LinkTypeId = l.LinkTypeId,
                        Url = l.Url
                    }).ToList(),

                    TagsInTracks = tip.Track.TagsInTracks?.Select(t => new TagsInTrack
                    {
                        Id = t.Id,
                        TrackId = t.TrackId,
                        TagId = t.TagId
                    }).ToList(),

                    MoodsInTracks = tip.Track.MoodsInTracks?.Select(m => new MoodsInTrack
                    {
                        Id = m.Id,
                        TrackId = m.TrackId,
                        MoodId = m.MoodId
                    }).ToList()

                } : null
            }).ToList()
        };
        return res;
    }

    public Domain.Playlist? Map(Playlist? entity)
    {
        if (entity == null) return null;
        var res = new Domain.Playlist()
        {
            Id = entity.Id,
            
            UserId = entity.UserId,
            User = null,
            
            Name = entity.Name,
            Description = entity.Description,
            CoverUrl = entity.CoverUrl,
            IsPublic = entity.IsPublic,
            TagsInPlaylists = null,
            MoodsInPlaylists = null,
            TrackInPlaylists = null
        };
        return res;
    }
}