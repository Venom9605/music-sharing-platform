using App.DAL.Interfaces;
using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface IRatingService : IBaseService<App.BLL.DTO.Rating>, IRatingRepositoryCustom
{
    
}