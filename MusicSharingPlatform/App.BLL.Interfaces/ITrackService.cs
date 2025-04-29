using App.DAL.Interfaces;
using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface ITrackService : IBaseService<App.BLL.DTO.Track>, ITrackRepositoryCustom
{
    
}