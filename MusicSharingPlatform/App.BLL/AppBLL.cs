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

}
