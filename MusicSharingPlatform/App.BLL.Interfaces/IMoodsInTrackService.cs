using App.DAL.Interfaces;
using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface IMoodsInTrackService : IBaseService<App.BLL.DTO.MoodsInTrack>, IMoodsInTrackRepositoryCustom
{
    
}