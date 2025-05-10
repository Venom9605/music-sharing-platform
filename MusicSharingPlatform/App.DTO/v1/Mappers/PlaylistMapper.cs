using Base.Interfaces;

namespace App.DTO.v1.Mappers;

public class PlaylistMapper : IMapper<App.DTO.v1.Playlist, App.BLL.DTO.Playlist>
{
    public Playlist? Map(BLL.DTO.Playlist? entity)
    {
        if (entity == null) return null;

        return new Playlist
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            CoverUrl = entity.CoverUrl,
            IsPublic = entity.IsPublic,
            UserId = entity.UserId,
            TracksInPlaylist = entity.TrackInPlaylists?.Select(tip => new TrackInPlaylist
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
                        ArtistRoleId = a.ArtistRoleId,
                        ArtistDisplayName = a.ArtistDisplayName,
                        ArtistRoleName = a.ArtistRoleName
                    }).ToList(),
                    Rating = tip.Track.Rating?.Select(r => new Rating
                    {
                        Id = r.Id,
                        TrackId = r.TrackId,
                        UserId = r.UserId,
                        Score = r.Score,
                        Comment = r.Comment,
                        ArtistDisplayName = r.ArtistDisplayName
                    }).ToList(),
                    TrackLinks = tip.Track.TrackLinks?.Select(tl => new TrackLink
                    {
                        Id = tl.Id,
                        TrackId = tl.TrackId,
                        LinkTypeId = tl.LinkTypeId,
                        Url = tl.Url,
                        LinkTypeName = tl.LinkTypeName
                    }).ToList(),
                    TagsInTracks = tip.Track.TagsInTracks?.Select(tag => new TagsInTrack
                    {
                        Id = tag.Id,
                        TrackId = tag.TrackId,
                        TagId = tag.TagId,
                        TagName = tag.TagName
                    }).ToList(),
                    MoodsInTracks = tip.Track.MoodsInTracks?.Select(mood => new MoodsInTrack
                    {
                        Id = mood.Id,
                        TrackId = mood.TrackId,
                        MoodId = mood.MoodId,
                        MoodName = mood.MoodName
                    }).ToList()
                } : null
            }).ToList()
        };
    }


    public BLL.DTO.Playlist? Map(Playlist? entity)
    {
        throw new NotImplementedException();
    }
}