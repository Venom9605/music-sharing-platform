using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;

namespace App.BLL.Services;

public class RatingService : BaseService<App.BLL.DTO.Rating, App.DAL.DTO.Rating, App.DAL.Interfaces.IRatingRepository>, IRatingService
{
    public RatingService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.Rating, Rating, Guid> bllMapper) : base(serviceUOW, serviceUOW.RatingRepository, bllMapper)
    {
    }
    
}