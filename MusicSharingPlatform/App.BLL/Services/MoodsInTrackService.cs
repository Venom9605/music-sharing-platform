using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;

namespace App.BLL.Services;

public class MoodsInTrackService : BaseService<App.BLL.DTO.MoodsInTrack, App.DAL.DTO.MoodsInTrack, App.DAL.Interfaces.IMoodsInTrackRepository>, IMoodsInTrackService
{
    public MoodsInTrackService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.MoodsInTrack, MoodsInTrack, Guid> bllMapper) : base(serviceUOW, serviceUOW.MoodsInTrackRepository, bllMapper)
    {
    }
    
}