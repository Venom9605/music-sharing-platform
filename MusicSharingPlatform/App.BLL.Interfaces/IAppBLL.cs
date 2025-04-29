using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface IAppBLL : IBaseBLL
{
    ITrackService TrackService { get; }
    IArtistInTrackService ArtistInTrackService { get; }
    IArtistRoleService ArtistRoleService { get; }
    IArtistService ArtistService { get; }
}