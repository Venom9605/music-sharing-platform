using App.BLL.Interfaces;
using App.BLL.Mappers;
using App.BLL.Services;
using App.DAL.EF;
using App.DAL.Interfaces;
using Base.BLL;

namespace App.BLL;

public class AppBLL : BaseBLL<IAppUOW>, IAppBLL
{
    public AppBLL(IAppUOW uow) : base(uow)
    {
    }

    private ITrackService? _trackService;
    public ITrackService TrackService => 
        _trackService ??= new TrackService(
            BLLUOW,
            new TrackBLLMapper()
            );
    
    private IArtistInTrackService? _artistInTrackService;
    public IArtistInTrackService ArtistInTrackService => 
        _artistInTrackService ??= new ArtistInTrackService(
            BLLUOW,
            new ArtistInTrackBLLMapper()
            );

    
    private IArtistRoleService? _artistRoleService;
    public IArtistRoleService ArtistRoleService => 
        _artistRoleService ??= new ArtistRoleService(
            BLLUOW,
            new ArtistRoleBLLMapper()
            );

    
    private IArtistService? _artistService;
    public IArtistService ArtistService => 
        _artistService ??= new ArtistService(
            BLLUOW,
            new ArtistBLLMapper()
            );
    
    private ILinkTypeService? _linkTypeService;
    public ILinkTypeService LinkTypeService => 
        _linkTypeService ??= new LinkTypeService(
            BLLUOW,
            new LinkTypeBLLMapper()
            );
    
    private IMoodService? _moodService;
    public IMoodService MoodService => 
        _moodService ??= new MoodService(
            BLLUOW,
            new MoodBLLMapper()
            );
    
    private IMoodsInPlaylistService? _moodsInPlaylistService;
    public IMoodsInPlaylistService MoodsInPlaylistService => 
        _moodsInPlaylistService ??= new MoodsInPlaylistService(
            BLLUOW,
            new MoodsInPlaylistBLLMapper()
            );
    
    private IMoodsInTrackService? _moodsInTrackService;
    public IMoodsInTrackService MoodsInTrackService => 
        _moodsInTrackService ??= new MoodsInTrackService(
            BLLUOW,
            new MoodsInTrackBLLMapper()
            );
    
    private IPlaylistService? _playlistService;
    public IPlaylistService PlaylistService => 
        _playlistService ??= new PlaylistService(
            BLLUOW,
            new PlaylistBLLMapper()
            );
    
    private IRatingService? _ratingService;
    public IRatingService RatingService => 
        _ratingService ??= new RatingService(
            BLLUOW,
            new RatingBLLMapper()
            );
    
    private ITagService? _tagService;
    public ITagService TagService => 
        _tagService ??= new TagService(
            BLLUOW,
            new TagBLLMapper()
            );
    
    private ITagsInPlaylistService? _tagsInPlaylistService;
    public ITagsInPlaylistService TagsInPlaylistService => 
        _tagsInPlaylistService ??= new TagsInPlaylistService(
            BLLUOW,
            new TagsInPlaylistBLLMapper()
            );
    
    private ITagsInTrackService? _tagsInTrackService;
    public ITagsInTrackService TagsInTrackService => 
        _tagsInTrackService ??= new TagsInTrackService(
            BLLUOW,
            new TagsInTrackBLLMapper()
            );
    
    private ITrackInPlaylistService? _trackInPlaylistService;
    public ITrackInPlaylistService TrackInPlaylistService => 
        _trackInPlaylistService ??= new TrackInPlaylistService(
            BLLUOW,
            new TrackInPlaylistBLLMapper()
            );
    
    private ITrackLinkService? _trackLinkService;
    public ITrackLinkService TrackLinkService => 
        _trackLinkService ??= new TrackLinkService(
            BLLUOW,
            new TrackLinkBLLMapper()
            );
    
    private IUserLinkService? _userLinkService;
    public IUserLinkService UserLinkService => 
        _userLinkService ??= new UserLinkService(
            BLLUOW,
            new UserLinkBLLMapper()
            );
    
    private IUserSavedTracksService? _userSavedTracksService;
    public IUserSavedTracksService UserSavedTracksService => 
        _userSavedTracksService ??= new UserSavedTracksService(
            BLLUOW,
            new UserSavedTracksBLLMapper(),
            new TrackBLLMapper()
            );

}
