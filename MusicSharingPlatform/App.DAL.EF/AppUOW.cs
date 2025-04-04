using App.DAL.EF.Repositories;
using App.DAL.Interfaces;
using Base.Dal.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUOW<AppDbContext>, IAppUOW
{
    public AppUOW(AppDbContext uowDbContext) : base(uowDbContext)
    {
    }

    private IArtistInTrackRepository? _artistInTrackRepository;
    public IArtistInTrackRepository ArtistInTrackRepository =>
        _artistInTrackRepository ??= new ArtistInTrackRepository(UOWDbContext);
    
    private IArtistRepository? _artistRepository;
    public IArtistRepository ArtistRepository =>
        _artistRepository ??= new ArtistRepository(UOWDbContext);
    
    private IArtistRoleRepository? _artistRoleRepository;
    public IArtistRoleRepository ArtistRoleRepository =>
        _artistRoleRepository ??= new ArtistRoleRepository(UOWDbContext);
    
    private ILinkTypeRepository? _linkTypeRepository;
    public ILinkTypeRepository LinkTypeRepository =>
        _linkTypeRepository ??= new LinkTypeRepository(UOWDbContext);
    
    private IMoodRepository? _moodRepository;
    public IMoodRepository MoodRepository =>
        _moodRepository ??= new MoodRepository(UOWDbContext);
    
    private IMoodsInPlaylistRepository? _moodsInPlaylistRepository;
    public IMoodsInPlaylistRepository MoodsInPlaylistRepository =>
        _moodsInPlaylistRepository ??= new MoodsInPlaylistRepository(UOWDbContext);
    
    private IMoodsInTrackRepository? _moodsInTrackRepository;
    public IMoodsInTrackRepository MoodsInTrackRepository =>
        _moodsInTrackRepository ??= new MoodsInTrackRepository(UOWDbContext);
    
    private IPlaylistRepository? _playlistRepository;
    public IPlaylistRepository PlaylistRepository =>
        _playlistRepository ??= new PlaylistRepository(UOWDbContext);
    
    private IRatingRepository? _ratingRepository;
    public IRatingRepository RatingRepository =>
        _ratingRepository ??= new RatingRepository(UOWDbContext);
    
    private ITagRepository? _tagRepository;
    public ITagRepository TagRepository =>
        _tagRepository ??= new TagRepository(UOWDbContext);
    
    private ITagsInPlaylistRepository? _tagsInPlaylistRepository;
    public ITagsInPlaylistRepository TagsInPlaylistRepository =>
        _tagsInPlaylistRepository ??= new TagsInPlaylistRepository(UOWDbContext);
    
    private ITagsInTrackRepository? _tagsInTrackRepository;
    public ITagsInTrackRepository TagsInTrackRepository =>
        _tagsInTrackRepository ??= new TagsInTrackRepository(UOWDbContext);
    
    private ITrackInPlaylistRepository? _trackInPlaylistRepository; 
    public ITrackInPlaylistRepository TrackInPlaylistRepository =>
        _trackInPlaylistRepository ??= new TrackInPlaylistRepository(UOWDbContext);
    
    private ITrackLinkRepository? _trackLinkRepository;
    public ITrackLinkRepository TrackLinkRepository =>
        _trackLinkRepository ??= new TrackLinkRepository(UOWDbContext);
    
    private ITrackRepository? _trackRepository;
    public ITrackRepository TrackRepository =>
        _trackRepository ??= new TrackRepository(UOWDbContext);
    
    private IUserLinkRepository? _userLinkRepository;
    public IUserLinkRepository UserLinkRepository =>
        _userLinkRepository ??= new UserLinkRepository(UOWDbContext);
    
    private IUserSavedTracksRepository? _userSavedTracksRepository;
    public IUserSavedTracksRepository UserSavedTracksRepository =>
        _userSavedTracksRepository ??= new UserSavedTracksRepository(UOWDbContext);

}