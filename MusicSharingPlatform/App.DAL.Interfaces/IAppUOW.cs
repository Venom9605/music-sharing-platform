using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface IAppUOW : IBaseUOW
{
    IArtistInTrackRepository ArtistInTrackRepository { get; }
    IArtistRepository ArtistRepository { get; }
    IArtistRoleRepository ArtistRoleRepository { get; }
    ILinkTypeRepository LinkTypeRepository { get; }
    IMoodRepository MoodRepository { get; }
    IMoodsInPlaylistRepository MoodsInPlaylistRepository { get; }
    IMoodsInTrackRepository MoodsInTrackRepository { get; }
    IPlaylistRepository PlaylistRepository { get; }
    IRatingRepository RatingRepository { get; }
    ITagRepository TagRepository { get; }
    ITagsInPlaylistRepository TagsInPlaylistRepository { get; }
    ITagsInTrackRepository TagsInTrackRepository { get; }
    ITrackInPlaylistRepository TrackInPlaylistRepository { get; }
    ITrackLinkRepository TrackLinkRepository { get; }
    ITrackRepository TrackRepository { get; }
    IUserLinkRepository UserLinkRepository { get; }
    IUserSavedTracksRepository UserSavedTracksRepository { get; }
}