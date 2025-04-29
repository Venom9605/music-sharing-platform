using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;

namespace App.BLL.Services;

public class ArtistInTrackService : BaseService<App.BLL.DTO.ArtistInTrack, App.DAL.DTO.ArtistInTrack, App.DAL.Interfaces.IArtistInTrackRepository>, IArtistInTrackService
{
    public ArtistInTrackService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.ArtistInTrack, ArtistInTrack, Guid> bllMapper) : base(serviceUOW, serviceUOW.ArtistInTrackRepository, bllMapper)
    {
    }

    public void CustomMethodTest()
    {
        ServiceRepository.CustomMethodTest();
    }
}