using App.DAL.DTO;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Mappers;

public class TrackInPlaylistBLLMapper : IBLLMapper<App.BLL.DTO.TrackInPlaylist, App.DAL.DTO.TrackInPlaylist>
{
    public TrackInPlaylist? Map(DTO.TrackInPlaylist? entity)
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

    public DTO.TrackInPlaylist? Map(TrackInPlaylist? entity)
    {
        if (entity == null) return null;
        var res = new DTO.TrackInPlaylist()
        {
            Id = entity.Id,
            
            TrackId = entity.TrackId,
            Track = entity.Track != null ? new DTO.Track
            {
                Id = entity.Track.Id,
                Title = entity.Track.Title,
                FilePath = entity.Track.FilePath,
                CoverPath = entity.Track.CoverPath,
                Duration = entity.Track.Duration,
                TimesPlayed = entity.Track.TimesPlayed,
                TimesSaved = entity.Track.TimesSaved,

                ArtistInTracks = entity.Track.ArtistInTracks?.Select(a => new DTO.ArtistInTrack
                {
                    Id = a.Id,
                    TrackId = a.TrackId,
                    UserId = a.UserId,
                    ArtistRoleId = a.ArtistRoleId,
                    ArtistDisplayName = a.User?.DisplayName,
                    ArtistRoleName = a.ArtistRole?.Name
                }).ToList(),

                TagsInTracks = entity.Track.TagsInTracks?.Select(t => new DTO.TagsInTrack
                {
                    Id = t.Id,
                    TrackId = t.TrackId,
                    TagId = t.TagId,
                    TagName = t.Tag?.Name
                }).ToList(),

                MoodsInTracks = entity.Track.MoodsInTracks?.Select(m => new DTO.MoodsInTrack
                {
                    Id = m.Id,
                    TrackId = m.TrackId,
                    MoodId = m.MoodId,
                    MoodName = m.Mood?.Name
                }).ToList(),

                Rating = entity.Track.Rating?.Select(r => new DTO.Rating
                {
                    Id = r.Id,
                    TrackId = r.TrackId,
                    UserId = r.UserId,
                    Score = r.Score,
                    Comment = r.Comment,
                    ArtistDisplayName = r.User?.DisplayName
                }).ToList(),

                TrackLinks = entity.Track.TrackLinks?.Select(l => new DTO.TrackLink
                {
                    Id = l.Id,
                    TrackId = l.TrackId,
                    LinkTypeId = l.LinkTypeId,
                    Url = l.Url,
                    LinkTypeName = l.LinkType?.Name
                }).ToList()

            } : null,
            
            PlaylistId = entity.PlaylistId,
            Playlist = entity.Playlist != null ? new DTO.Playlist
            {
                Id = entity.Playlist.Id,
                Name = entity.Playlist.Name,
            } : null,


        };
        return res;
    }
}