using App.DAL.Interfaces;
using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface ITrackLinkService : IBaseService<App.BLL.DTO.TrackLink>, ITrackLinkRepositoryCustom
{
    
}