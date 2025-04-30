using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;

namespace App.BLL.Services;

public class MoodService : BaseService<App.BLL.DTO.Mood, App.DAL.DTO.Mood, App.DAL.Interfaces.IMoodRepository>, IMoodService
{
    public MoodService(
        IAppUOW serviceUOW, 
        IBLLMapper<DTO.Mood, Mood, Guid> bllMapper) : base(serviceUOW, serviceUOW.MoodRepository, bllMapper)
    {
    }
    
}