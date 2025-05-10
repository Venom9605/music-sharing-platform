using App.DAL.DTO;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class TrackInPlaylistUOWMapper : IUOWMapper<DTO.TrackInPlaylist, Domain.TrackInPlaylist>
{
    public TrackInPlaylist? Map(Domain.TrackInPlaylist? entity)
    {
        if (entity == null) return null;
        var res = new TrackInPlaylist()
        {
            Id = entity.Id,
            
            TrackId = entity.TrackId,
            Track = entity.Track != null ? new Track
            {
                Id = entity.Track.Id,
                Title = entity.Track.Title,
                FilePath = entity.Track.FilePath,
                CoverPath = entity.Track.CoverPath,
                Duration = entity.Track.Duration,
                TimesPlayed = entity.Track.TimesPlayed,
                TimesSaved = entity.Track.TimesSaved,

                ArtistInTracks = entity.Track.ArtistInTracks?.Select(a => new ArtistInTrack
                {
                    Id = a.Id,
                    TrackId = a.TrackId,
                    UserId = a.UserId,
                    ArtistRoleId = a.ArtistRoleId,
                    User = a.User != null ? new Artist
                    {
                        Id = a.User.Id,
                        DisplayName = a.User.DisplayName
                    } : null,
                    ArtistRole = a.ArtistRole != null ? new ArtistRole
                    {
                        Id = a.ArtistRole.Id,
                        Name = a.ArtistRole.Name
                    } : null
                }).ToList(),

                Rating = entity.Track.Rating?.Select(r => new Rating
                {
                    Id = r.Id,
                    TrackId = r.TrackId,
                    UserId = r.UserId,
                    Score = r.Score,
                    Comment = r.Comment,
                    User = r.User != null ? new Artist
                    {
                        Id = r.User.Id,
                        DisplayName = r.User.DisplayName
                    } : null
                }).ToList(),

                TrackLinks = entity.Track.TrackLinks?.Select(tl => new TrackLink
                {
                    Id = tl.Id,
                    TrackId = tl.TrackId,
                    LinkTypeId = tl.LinkTypeId,
                    Url = tl.Url,
                    LinkType = tl.LinkType != null ? new LinkType
                    {
                        Id = tl.LinkType.Id,
                        Name = tl.LinkType.Name
                    } : null
                }).ToList(),

                TagsInTracks = entity.Track.TagsInTracks?.Select(t => new TagsInTrack
                {
                    Id = t.Id,
                    TrackId = t.TrackId,
                    TagId = t.TagId,
                    Tag = t.Tag != null ? new Tag
                    {
                        Id = t.Tag.Id,
                        Name = t.Tag.Name
                    } : null
                }).ToList(),

                MoodsInTracks = entity.Track.MoodsInTracks?.Select(m => new MoodsInTrack
                {
                    Id = m.Id,
                    TrackId = m.TrackId,
                    MoodId = m.MoodId,
                    Mood = m.Mood != null ? new Mood
                    {
                        Id = m.Mood.Id,
                        Name = m.Mood.Name
                    } : null
                }).ToList()

            } : null,
            
            PlaylistId = entity.PlaylistId,
            Playlist = entity.Playlist != null ? new Playlist
            {
                Id = entity.Playlist.Id,
                Name = entity.Playlist.Name,
            } : null,

        };
        return res;
    }

    public Domain.TrackInPlaylist? Map(TrackInPlaylist? entity)
    {
        if (entity == null) return null;
        var res = new Domain.TrackInPlaylist()
        {
            Id = entity.Id,
            TrackId = entity.TrackId,
            Track = null,
            PlaylistId = entity.PlaylistId,
            Playlist = null,

        };
        return res;
    }
}