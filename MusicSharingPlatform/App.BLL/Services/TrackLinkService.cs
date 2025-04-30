using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;

namespace App.BLL.Services;

public class TrackLinkService : BaseService<App.BLL.DTO.TrackLink, App.DAL.DTO.TrackLink, App.DAL.Interfaces.ITrackLinkRepository>, ITrackLinkService
{
    public TrackLinkService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.TrackLink, TrackLink, Guid> bllMapper) : base(serviceUOW, serviceUOW.TrackLinkRepository, bllMapper)
    {
    }
    
}