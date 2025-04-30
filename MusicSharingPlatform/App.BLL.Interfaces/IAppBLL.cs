using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface IAppBLL : IBaseBLL
{
    ITrackService TrackService { get; }
    IArtistInTrackService ArtistInTrackService { get; }
    IArtistRoleService ArtistRoleService { get; }
    IArtistService ArtistService { get; }
    ILinkTypeService LinkTypeService { get; }
    IMoodService MoodService { get; }
    IMoodsInPlaylistService MoodsInPlaylistService { get; }
    IMoodsInTrackService MoodsInTrackService { get; }
    IPlaylistService PlaylistService { get; }
    IRatingService RatingService { get; }
    ITagService TagService { get; }
    ITagsInPlaylistService TagsInPlaylistService { get; }
    ITagsInTrackService TagsInTrackService { get; }
    ITrackInPlaylistService TrackInPlaylistService { get; }
    ITrackLinkService TrackLinkService { get; }
    IUserLinkService UserLinkService { get; }
    IUserSavedTracksService UserSavedTracksService { get; }
        
    
}